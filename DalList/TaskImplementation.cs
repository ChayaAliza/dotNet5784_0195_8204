namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class TaskImplementation : ITask
{
    public int Create(Task Task1)
    {
        int id = DataSource.Config.NextTaskId;
        Task copy = Task1 with { Id = id };
        DataSource.Tasks.Add(copy);
        return id;
    }


    public Task? Read(int id)
    {
        for (int i = 0; i < DataSource.Tasks.Count; i++)
        {
            if (DataSource.Tasks[i].Id == id) { return DataSource.Tasks[i]; }
        }
      
         return null;
    }

    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }

    public void Update(Task item)
    {
        if(Read(item.Id)is null)
        {
            throw new Exception("an item with this id isnt exist");
        }
        DataSource.Tasks.Remove(item);
            
        DataSource.Tasks.Add(item);
    }
    public void Delete(int id)
    {
        if (DataSource.Dependencys.Find(x => x.DependentTask == id) is not null)  {
            throw new Exception($"Task with Id={id} cannot be deleted");
        }

        if (Read(id) is null)  {
            throw new Exception($"Task with Id={id} isnt exist");
        }
        DataSource.Tasks.Remove(Read(id)!); ;
           
    }
}
