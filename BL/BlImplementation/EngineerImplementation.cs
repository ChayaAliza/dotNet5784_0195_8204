namespace BlImplementation;
using BlApi;
using BO;
using System.Collections.Generic;

// Implementation of the IEngineer interface
internal class EngineerImplementation : IEngineer
{

    // Access to the data access layer
    private DalApi.IDal s_dal = DalApi.Factory.Get;

    // Creates a new engineer in the system
    public int Create(BO.Engineer boEngineer)
    {
        // Convert BO.Engineer to DO.Engineer and attempt to create it in the data layer
        DO.Engineer doEngineer = new DO.Engineer(boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience)boEngineer.Level, boEngineer.Cost, boEngineer.IsActive);
        try
        {
            int idEng = s_dal.Engineer.Create(doEngineer);
            return idEng;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            // If engineer with the same ID already exists, throw a business logic exception
            throw new BO.BlAlreadyExistsException($"Engineer with ID={boEngineer.Id} already exists", ex);
        }
    }

    // Deletes an engineer from the system
    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    // Reads an engineer from the system by ID
    public Engineer? Read(int id)
    {
        // Read engineer from the data layer
        DO.Engineer doEngineer = s_dal.Engineer.Read(id)!;
        // Read associated task for the engineer
        var doTasks = s_dal.Task.ReadAll(tas => tas.EngineerId == doEngineer.Id).FirstOrDefault();
        TaskInEngineer? taskInEngineer = null;
        // If there's a task associated with the engineer, create BO.TaskInEngineer object
        if (doTasks != null)
        {
            taskInEngineer = new BO.TaskInEngineer
            {
                Id = doTasks.Id,
                Alias = doTasks.Allas
            };
        }
        // If engineer is not found, throw a business logic exception
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");
        // Convert DO.Engineer to BO.Engineer and return
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

    // Reads all engineers from the system
    public IEnumerable<Engineer> ReadAll(Func<Engineer, bool>? filter = null)
    {
        // Query all engineers from the data layer
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

        // Apply filter if provided
        if (filter == null)
            return engineers;
        return engineers.Where(filter!);
    }

    // Updates an existing engineer in the system
    public void Update(BO.Engineer boEngineer)
    {
        // Read existing engineer from the data layer
        DO.Engineer? doEngineer = s_dal.Engineer.Read(boEngineer.Id)!;
        // If engineer doesn't exist, throw a business logic exception
        if (Read(doEngineer.Id) is null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={doEngineer.Id} doesn't exist");
        // Convert BO.Engineer to DO.Engineer and update it in the data layer
        DO.Engineer doEn = new DO.Engineer(boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience)boEngineer.Level, boEngineer.Cost);
        s_dal.Engineer.Update(doEn);
    }
}
