namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer eng)
    {
        if(Read(eng.Id) is not null)
            throw new Exception($"Engineer with ID={eng.Id} already exists");
        DataSource.Engineers.Add(eng);
        return eng.Id;
    }
 
    public void Delete(int id)
    {
        if (DataSource.Tasks.Find(x => x.EngineerId == id) is not null)
        {
            throw new Exception($"Engineer with Id={id} cannot be deleted");
        }

        if (Read(id) is null) 
        {
            throw new Exception($"Engineer with Id={id} isnt exist");
        }
        DataSource.Engineers.Remove(Read(id)!);

    }


    public void Update(Engineer eng)
    {

        if (Read(eng.Id) is null)
        {
            throw new Exception("an item with this id isnt exist");
        }
        DataSource.Engineers.Remove(eng);

        DataSource.Engineers.Add(eng);
    }
    
     public Engineer? Read(int id)
    {
        for (int i = 0; i < DataSource.countEngineers; i++)
        {
            if (DataSource.Engineers[i].Id == id)
                return DataSource.Engineers[i];
        }
        return null;
        //throw new NotImplementedException();
    }
    
    List<Engineer> ICrud<Engineer>.ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
    }
    
}
