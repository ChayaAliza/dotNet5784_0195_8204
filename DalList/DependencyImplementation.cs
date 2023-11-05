namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class DependencyImplementation : IDependency
{
    public int Create(Dependency de1)
    {
        int id = DataSource.Config.NextDependencyId;
        Dependency copy = de1 with { Id = id };
        DataSource.Dependencys.Add(copy);
        return id;
    }

    public Dependency? Read(int id)
    {
        
        for (int i = 0; i < DataSource.Dependencys.Count; i++)
        {
            if (DataSource.Dependencys[i].Id == id) { return DataSource.Dependencys[i]; }   
            
        }
        return null;
    }

    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencys);
    }

    public void Update(Dependency de1)
    {
        if (Read(de1.Id) is null)
        {
            throw new Exception("an item with this id isnt exist");
        }
        DataSource.Dependencys.Remove(de1);

        DataSource.Dependencys.Add(de1);


       
    }
}
