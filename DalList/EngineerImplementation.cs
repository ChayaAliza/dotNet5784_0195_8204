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

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Engineer? Read(int id)
    {
        for(int i = 0; i < DataSource.countEngineers; i++){
            if (DataSource.Engineers[i].Id == id)
                return DataSource.Engineers[i];
        }
        return null;
    }

    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
    }

    public void Update(Engineer eng)
    {
        throw new NotImplementedException();
    }
}
