namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

//Implementation of the methods of the task entity.
internal class TaskImplementation : ITask
{
    //A method that generates a new task.
    public int Create(Task item)
    {
        int id = DataSource.Config.NextTaskId;
        Task copy = item with { Id = id };
        DataSource.Tasks.Add(copy);
        return id;
    }
    //A method that updates an existing object.
    public void Update(Task item)
    {
        Task? removeTask = Read(item.Id)!;
        if (Read(item.Id) is null) //Checking if there is an object with the same ID number, in the list.
            throw new DalDoesNotExistException($"Task with ID={item.Id} doesn't exist"); //A suitable exception throw.

        DataSource.Tasks.Remove(removeTask); //Removes the reference to an existing object from a list.
        DataSource.Tasks.Add(item); //Added to the list the reference to the updated object received as a parameter.
    }
    //A method that deletes an existing object.
    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
    //A method that requests/receives a single object.
    public Task? Read(int id)
    {
        return DataSource.Tasks.FirstOrDefault(x => x.Id == id);
    }

    public Task? Read(Func<Task, bool> filter)
    {
        return DataSource.Tasks.FirstOrDefault(x => filter(x));
    }

    //A method that requests/receives all objects of a certain type.
    public IEnumerable<Task> ReadAll(Func<Task, bool>? filter = null)
    {
        if (filter != null)
        {
            return from item in DataSource.Tasks
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Tasks
               select item;
    }

}
