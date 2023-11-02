namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : Itask
{
    public int Create(Task Task1)
    {
        int id = DataSource.Config.NextTaskId;
        Task copy = Task1 with { Id = id };
        DataSource.Tasks.Add(copy);
        return id;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task? Read(int id)
    {
        throw new NotImplementedException();
    }

    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }

    public void Update(Task item)
    {
        throw new NotImplementedException();
    }
}
