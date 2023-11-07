namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer eng)
    {
        if(Read(eng.Id) is not null)
            throw new Exception($"Engineer with ID={eng.Id} already exists");
        DataSource.Engineers.Add(eng);
        return eng.Id;
    }

    public Engineer? Read(int id)
    {
        for (int i = 0; i < DataSource.countEngineers; i++)
        {
            if (DataSource.Engineers[i].Id == id)
                return DataSource.Engineers[i];
        }
        return null;
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
        DataSource.Engineers.Remove(Read(id));

    }

    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
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
}
