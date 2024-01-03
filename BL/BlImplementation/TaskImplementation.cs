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
            Milestone = CalculateMilestone(id),
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
   
    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        Func<BO.Task, bool>? filterTemp = filter != null ? filter! : item => true;
        List<BO.Task>? boTasks = null;

        foreach (DO.Task? doTask in s_dal.Task.ReadAll())
        {
            boTasks!.Add(new BO.Task()
            {
                Id = doTask!.Id,
                Description = doTask.Description,
                Alias = doTask.Allas,
                IsActive = doTask.active,
                CreateAt = doTask.CreatedAt,
                Status = CalculateStatus(doTask),
                Milestone = CalculateMilestone(doTask.Id),
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

            });
        }
        return boTasks!.Where(filterTemp).ToList();
    }

    public void Update(BO.Task boTask)
    {
        try
        {
            if (boTask.Milestone != null)
            {
                var dependenciesToCreate = boTask.Dependencies!
               .Select(task => new DO.Dependency
               {
                   DependentTask = boTask.Id,
                   DependsOnTask = task.Id
               })
               .ToList();
                dependenciesToCreate.ForEach(dependency => s_dal.Dependency.Create(dependency));
            }
            DO.Task doTask = s_dal.Task.Read(boTask.Id)!;
            s_dal.Task.Update(doTask);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engineer with ID={boTask.Id} not exists", ex);
        }
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

    private MilestoneInTask CalculateMilestone(int id)
    {
        MilestoneInTask milestone = null!;
        List<DO.Dependency?>? dependencyList = new List<DO.Dependency?>(s_dal.Dependency.ReadAll(dependency => dependency.DependentTask == id));
        foreach (DO.Dependency? dependency in dependencyList)
        {
            DO.Task task = s_dal.Task.Read(dependency!.DependsOnTask)!;
            if (task.MilesStone == true)
            {
                milestone = new BO.MilestoneInTask
                {
                    Id = task.Id, 
                    Alias = task.Allas
                };
                     
            }
        }
        return milestone;
    }

    private EngineerInTask CalculateEngineer(int id)
    {
        DO.Task? doTask = s_dal.Task.Read(id);
        BO.EngineerInTask engineerInTask = null!;
        if (doTask != null)
        {
            DO.Engineer? doEngineer = s_dal?.Engineer?.Read((int)doTask?.EngineerId!);///??????
            engineerInTask = new BO.EngineerInTask
            {
                Id = (int)doTask?.EngineerId!,///?????
                Name = doEngineer?.Name
            };
        }
        
        return engineerInTask;
    }

    private List<TaskInList> calculateTaskInList(int id)
    {
        DO.Task? doTask = s_dal.Task.Read(id)!;

        List<DO.Dependency?>? dependencyList = new List<DO.Dependency?>(s_dal.Dependency.ReadAll(dependency => dependency.DependentTask == id));

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


