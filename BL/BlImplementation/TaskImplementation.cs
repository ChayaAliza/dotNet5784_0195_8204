using BlApi;
using BO;
using DalApi;
using System.Linq;


namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private DalApi.IDal s_dal = DalApi.Factory.Get;

    public int Create(BO.Task boTask)
    {
        
      
        DO.Task doTask = new DO.Task(boTask.Id, boTask.Description, (DO.EngineerExperience)boTask.Level!, boTask.Alias, false, boTask.CreateAt, boTask.Start, boTask.ScheduledDate, boTask.ForecastDate, boTask.Deadline, boTask.Complete, boTask.Deliverables, boTask.Remarks,boTask.Engineer!.Id, false);
        try
        {
            
            int newId = s_dal.Task.Create(doTask);
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
    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null)
    {

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

        if (filter == null)
            return tasks;
        return tasks.Where(filter!);
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
            DO.Task doTask = new DO.Task(boTask.Id, boTask.Description, (DO.EngineerExperience)boTask.Level!, boTask.Alias , false, boTask.CreateAt, boTask.Start, boTask.ScheduledDate, boTask.ForecastDate, boTask.Deadline, boTask.Complete, boTask.Deliverables, boTask.Remarks, boTask.Engineer!.Id , boTask.IsActive);
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
            return Status.OnTrack;

        if (task.Start != null && task.DeadLine != null && task.Complete == null)
            return Status.Scheduled;

        if (task.Start != null && task.DeadLine != null && task.Complete <= task.ForecastDate)
            return Status.OnTrack;

        if (task.Start != null && task.DeadLine != null && task.Complete > task.ForecastDate)
            return Status.InJeopardy;

        return Status.Unscheduled;
    }

    //private MilestoneInTask CalculateMilestone(int id)
    //{
    //    MilestoneInTask milestone = null!;
    //    List<DO.Dependency?>? dependencyList = new List<DO.Dependency?>(s_dal.Dependency.ReadAll(dependency => dependency.DependentTask == id));
    //    foreach (DO.Dependency? dependency in dependencyList)
    //    {
    //        DO.Task task = s_dal.Task.Read(dependency!.DependsOnTask)!;
    //        if (task.MilesStone == true)
    //        {
    //            milestone = new BO.MilestoneInTask
    //            {
    //                Id = task.Id, 
    //                Alias = task.Allas
    //            };
                     
    //        }
    //    }
    //    return milestone;
    //}

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



    //public List<TaskInList> CalculateTaskInList(int id)
    //{
    //    var dependenciesList = _dal.Dependency.ReadAll(); //Creating a list of all dependencies whose id of our current task equals the id of the dependent task.
    //                                                      //A loop that goes through each of the dependencies.
    //    var dependentTasks = (from dependence in dependenciesList
    //                          where dependence.DependentTask == id
    //                          let taskDependOn = _dal.Task.Read(dependence.DependsOnTask)
    //                          select new BO.TaskInList()
    //                          {
    //                              Id = dependence.DependsOnTask,
    //                              Description = taskDependOn?.Description,
    //                              Alias = taskDependOn?.Alias,
    //                              Status = CalculateStatus(taskDependOn)
    //                          });
    //    return dependentTasks.ToList();
    //}


}














