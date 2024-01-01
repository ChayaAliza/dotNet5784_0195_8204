namespace BlImplementation;
using BlApi;
using BO;
using System.Collections.Generic;


internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal s_dal = Factory.Get;
   
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
        DO.Engineer? doEngineer = s_dal.Engineer.Read(id);
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
            Task = null
        };
        
}

    public IEnumerable<Engineer> ReadAll()
    {
        return (from DO.Engineer doEngineer in s_dal.Engineer.ReadAll()
                select new BO.Engineer()
                {
                    Id =doEngineer.Id,
                    Name = doEngineer.Name,
                    IsActive = doEngineer.active,
                    Email = doEngineer.Email,
                    Level = (BO.EngineerExperience)doEngineer.Level!,
                    Cost = doEngineer.Cost,
                    Task = null
                });
    }

    public void Update(BO.Engineer boEngineer)
    {
        DO.Engineer? doEngineer = s_dal.Engineer.Read(boEngineer.Id)!;
        if (Read(doEngineer.Id) is null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={doEngineer.Id} doesn't exist");
        s_dal.Engineer.Delete(doEngineer.Id);
        s_dal.Engineer.Create(doEngineer);
    }
}
