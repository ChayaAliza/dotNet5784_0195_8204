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
        throw new NotImplementedException();
    }

    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencys);
    }

    public void Update(Dependency de1)
    {
        throw new NotImplementedException();
    }
}
