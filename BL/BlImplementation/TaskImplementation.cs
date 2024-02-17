using BlApi;
using BO;

namespace BlImplementation;

// Implementation of the ITask interface
internal class TaskImplementation : ITask
{
    // Access to the data access layer
    private DalApi.IDal s_dal = DalApi.Factory.Get;

    // Creates a new task in the system
    public int Create(BO.Task boTask)
    {
        // Convert BO.Task to DO.Task and attempt to create it in the data layer
        DO.Task doTask = new DO.Task(boTask.Id, boTask.Description, (DO.EngineerExperience)boTask.Level!, boTask.Alias, false, boTask.CreateAt, boTask.Start, boTask.ScheduledDate, boTask.ForecastDate, boTask.Deadline, boTask.Complete, boTask.Deliverables, boTask.Remarks, boTask.Engineer!.Id, false);
        try
        {
            // Create the task in the data layer
            int newId = s_dal.Task.Create(doTask);

            // Create dependencies if they exist
            if (boTask.Dependencies != null)
            {
                var dependenciesToCreate = boTask.Dependencies!
                    .Select(task => new DO.Dependency
                    {
                        DependentTask = boTask.Id,
                        DependsOnTask = newId
                    })
                    .ToList();
                dependenciesToCreate.ForEach(dependency => s_dal.Dependency.Create(dependency));
            }

            return newId;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            // If task with the same ID already exists, throw a business logic exception
            throw new BO.BlAlreadyExistsException($"Task with ID={boTask.Id} already exists", ex);
        }
    }

    // Deletes a task from the system
    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    // Reads a task from the system by ID
    public BO.Task? Read(int id)
    {
        // Read task from the data layer
        DO.Task? doTask = s_dal.Task.Read(id)!;

        // Convert DO.Task to BO.Task and return
        return new BO.Task()
        {
            Id = doTask.Id,
            Description = doTask.Description,
            Alias = doTask.Allas,
            IsActive = doTask.active,
            CreateAt = doTask.CreatedAt,
            Status = CalculateStatus(doTask),
            Milestone = null,
            Start = doTask.Start,
            ScheduledDate = doTask.ScheduledDate,
            ForecastDate = doTask.ForecastDate,
            Deadline = doTask.DeadLine,
            Complete = doTask.Complete,
            Deliverables = doTask.Deliverables,
            Remarks = doTask.Remarks,
            Engineer = CalculateEngineer(id),
            Dependencies = calculateTaskInList(id),
            Level = (BO.EngineerExperience)doTask.CopmlexityLevel!
        };
    }

    // Reads all tasks from the system
    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        // Query all tasks from the data layer
        IEnumerable<BO.Task> tasks =
            from DO.Task doTask in s_dal.Task.ReadAll()
            select new BO.Task()
            {
                Id = doTask!.Id,
                Description = doTask.Description,
                Alias = doTask.Allas,
                IsActive = doTask.active,
                CreateAt = doTask.CreatedAt,
                Status = CalculateStatus(doTask),
                Milestone = null,
                Start = doTask.Start,
                ScheduledDate = doTask.ScheduledDate,
                ForecastDate = doTask.ForecastDate,
                Deadline = doTask.DeadLine,
                Complete = doTask.Complete,
                Deliverables = doTask.Deliverables,
                Remarks = doTask.Remarks,
                Engineer = CalculateEngineer(doTask.Id),
                Dependencies = calculateTaskInList(doTask.Id),
                Level = (BO.EngineerExperience)doTask.CopmlexityLevel!
            };

        // Apply filter if provided
        if (filter == null)
            return tasks;
        return tasks.Where(filter!);
    }
   
    /// Updates an existing task in the system.
    public void Update(BO.Task boTask)
    {
        try
        {
            // Create dependencies for the task
            var dependenciesToCreate = boTask.Dependencies!
                .Select(task => new DO.Dependency
                {
                    DependentTask = boTask.Id,
                    DependsOnTask = task.Id
                })
                .ToList();
            dependenciesToCreate.ForEach(dependency => s_dal.Dependency.Create(dependency));

            // Convert BO.Task to DO.Task and update it in the data layer
            DO.Task doTask = new DO.Task(boTask.Id, boTask.Description, (DO.EngineerExperience)boTask.Level!, boTask.Alias, false, boTask.CreateAt, boTask.Start, boTask.ScheduledDate, boTask.ForecastDate, boTask.Deadline, boTask.Complete, boTask.Deliverables, boTask.Remarks, boTask.Engineer!.Id, boTask.IsActive);
            s_dal.Task.Update(doTask);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            // If task doesn't exist, throw a business logic exception
            throw new BO.BlAlreadyExistsException($"Engineer with ID={boTask.Id} not exists", ex);
        }
    }


    // Calculates the status of a task based on its properties
    public static Status CalculateStatus(DO.Task task)
    {
        if (task.Start == null && task.DeadLine == null)
            return Status.Unscheduled;

        if (task.Start != null && task.DeadLine != null && task.Complete == null)
            return Status.Scheduled;

        if (task.Start != null && task.DeadLine != null && task.Complete <= task.ForecastDate)
            return Status.OnTrack;

        if (task.Start != null && task.DeadLine != null && task.Complete > task.ForecastDate)
            return Status.InJeopardy;

        return Status.Unscheduled;
    }

    // Calculates the engineer associated with a task
    private EngineerInTask CalculateEngineer(int id)
    {
        DO.Task? doTask = s_dal.Task.Read(id);
        BO.EngineerInTask engineerInTask = null!;
        if (doTask != null)
        {
            DO.Engineer? doEngineer = s_dal?.Engineer?.Read((int)doTask?.EngineerId!);
            engineerInTask = new BO.EngineerInTask
            {
                Id = (int)doTask?.EngineerId!,
                Name = doEngineer?.Name
            };
        }

        return engineerInTask;
    }

    // Calculates the list of dependencies for a task
    public List<TaskInList> calculateTaskInList(int id)
    {
        List<DO.Dependency?>? dependencyList = new List<DO.Dependency?>(s_dal.Dependency.ReadAll(dependency => dependency.DependentTask == id));
        DO.Task? doTask = s_dal.Task.Read(id)!;
        var dependentTasks = (from dependence in dependencyList
                              where dependence.DependentTask == id
                              let taskDependOn = s_dal.Task.Read(dependence.DependsOnTask)
                              select new BO.TaskInList()
                              {
                                  Id = dependence.DependsOnTask,
                                  Description = taskDependOn?.Description,
                                  Alias = taskDependOn?.Allas,
                                  Status = CalculateStatus(doTask)
                              });
        return dependentTasks.ToList();
    }
}
