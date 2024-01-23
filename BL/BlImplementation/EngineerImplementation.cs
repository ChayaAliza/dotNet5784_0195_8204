namespace BlImplementation;
using BlApi;
using BO;
using System.Collections.Generic;


internal class EngineerImplementation : IEngineer
{

    private DalApi.IDal s_dal = DalApi.Factory.Get;
    public int Create(BO.Engineer boEngineer)
    {
        DO.Engineer doEngineer = new DO.Engineer(boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience)boEngineer.Level, boEngineer.Cost, boEngineer.IsActive);
        try
        {
            int idEng = s_dal.Engineer.Create(doEngineer);
            return idEng;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engineer with ID={boEngineer.Id} already exists" , ex);
        }

    }
    
    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Engineer? Read(int id)
    {
        DO.Engineer doEngineer = s_dal.Engineer.Read(id)!;
        var doTasks = s_dal.Task.ReadAll(tas => tas.EngineerId == doEngineer.Id).FirstOrDefault();
        TaskInEngineer? taskInEngineer = null;
        if (doTasks != null)
        {
            taskInEngineer = new BO.TaskInEngineer
            {
                Id = doTasks.Id,
                Alias = doTasks.Allas
            };
        }
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");
        return new BO.Engineer()
        {
            Id = id,
            Name = doEngineer.Name,
            IsActive = doEngineer.active,
            Email = doEngineer.Email,
            Level = (BO.EngineerExperience)doEngineer.Level!,
            Cost = doEngineer.Cost,
            Task = taskInEngineer
        };
    }
    public IEnumerable<Engineer> ReadAll(Func<Engineer, bool>? filter = null)
    {

        IEnumerable<BO.Engineer> engineers =
        from DO.Engineer doEngineer in s_dal.Engineer.ReadAll()
        let task = s_dal.Task.ReadAll(task => task?.EngineerId == doEngineer.Id).FirstOrDefault()
        select new BO.Engineer()
        {
            Id = doEngineer.Id,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            Level = (BO.EngineerExperience)doEngineer.Level!,
            Cost = doEngineer.Cost,
            Task = task != null ? new BO.TaskInEngineer
            {
                Id = task.Id,
                Alias = task.Allas

            } : null
        };

        if (filter == null)
            return engineers;
        return engineers.Where(filter!);
    }

    public void Update(BO.Engineer boEngineer)
    {
        
        DO.Engineer? doEngineer = s_dal.Engineer.Read(boEngineer.Id)!;
        if (Read(doEngineer.Id) is null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={doEngineer.Id} doesn't exist");
        DO.Engineer doEn = new DO.Engineer(boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience)boEngineer.Level, boEngineer.Cost);
        s_dal.Engineer.Update(doEn);
    }


}
    
