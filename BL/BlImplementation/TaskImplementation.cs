using BlApi;
using BO;



namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private DalApi.IDal s_dal = Factory.Get;

    public int Create(BO.Task boTask)
    {
        DO.Task doTask = new DO.Task(boTask.Id , boTask.Description , (DO.EngineerExperience)boTask.Level! , boTask.Alias , false
            , boTask.CreateAt , boTask.Start , boTask.ScheduledDate , boTask.ForecastDate , boTask.Deadline , boTask.Complete , boTask.Deliverables
            , boTask.Remarks , boTask.Engineer!.Id , boTask.IsActive);
        try
        {
            var dependenciesToCreate = boTask.Dependencies!
                .Select(task => new DO.Dependency
                {
                    DependentTask = boTask.Id,
                    DependsOnTask = task.Id
                })
                .ToList();
            dependenciesToCreate.ForEach(dependency => s_dal.Dependency.Create(dependency));

            int newId = s_dal.Task.Create(doTask);

            return newId;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={boTask.Id} already exists", ex);
        }
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Task? Read(int id)
    {

        DO.Task? doTask = s_dal.Task.Read(id)!;
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
   
    public IEnumerable<BO.Task> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Task boTask)
    {
        throw new NotImplementedException();
    }
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
    private EngineerInTask CalculateEngineer(int id)
    {
        DO.Task? t = s_dal.Task.Read(id);
        if (t != null)
        {
            BO.EngineerInTask engineer = new BO.EngineerInTask();
            engineer.Id = t.EngineerId;
            DO.Engineer? e = s_dal.Engineer.Read(t.EngineerId);
            if (e != null)
            {
                engineer.Name = e.Name;
                return engineer;
            }
            else
            {
                // Handle case where Engineer data is null
                // For example: throw exception, log, return default values, etc.
            }
        }
        else
        {
            // Handle case where Task data is null
            // For example: throw exception, log, return default values, etc.
        }

        // Return default value or handle exceptional cases
        return new BO.EngineerInTask(); // You might want to handle the default return based on your application logic
    }

    private List<TaskInList> calculateTaskInList(int id)
    {
        DO.Task? doTask = s_dal.Task.Read(id)!;

        List<DO.Dependency?>? dependencyList = new List<DO.Dependency?>(s_dal.Dependency.ReadAll(dependency => dependency.DependentTask == id));//לוקחת את המשימות שהמשימה תלויה בהם

        List<BO.TaskInList?>? tasksInList = new List<BO.TaskInList?>(dependencyList.Select(dependency =>
        {
            if (dependency?.DependsOnTask != null)
            {
                var task = s_dal.Task.Read(dependency.DependsOnTask);
                if (task != null)
                {
                    return new BO.TaskInList
                    {
                        Id = (int)dependency.DependsOnTask,
                        Description = task.Description,
                        Alias = task.Allas,
                        Status = CalculateStatus(doTask)
                    };
                }
            }
            return null;
        })).Where(dependency => dependency != null).ToList();
        return tasksInList!;
    }

}


