namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

//Implementation of the methods of the engineer entity.
internal class EngineerImplementation : IEngineer
{
    //A method that generates a new engineer.
    public int Create(Engineer item)
    {
        if(Read(item.Id) is not null)
            throw new DalAlreadyExistsException($"Engineer with ID={item.Id} already exists");
        DataSource.Engineers.Add(item);
        return item.Id;
    }
    //A method that deletes an existing object.
    public void Delete(int id)
    {
        throw new NotImplementedException();

    }
    //A method that updates an existing object.
    public void Update(Engineer item)
    {
        Engineer? removeTask = Read(item.Id)!;
        if (Read(item.Id) is null) //Checking if there is an object with the same ID number, in the list.
            throw new DalDoesNotExistException($"Task with ID={item.Id} doesn't exist"); //A suitable exception throw.

        DataSource.Engineers.Remove(removeTask); //Removes the reference to an existing object from a list.
        DataSource.Engineers.Add(item); //Added to the list the reference to the updated object received as a parameter.
    }
 
    //A method that requests/receives a single object.
    public Engineer? Read(int id)
    {
        return DataSource.Engineers.FirstOrDefault(x => x.Id == id);
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        return DataSource.Engineers.FirstOrDefault(x => filter(x));
    }

    //A method that requests/receives all objects of a certain type.
    public IEnumerable<Engineer> ReadAll(Func<Engineer, bool>? filter = null)
    {
        if (filter != null)
        {
            return from item in DataSource.Engineers
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Engineers
               select item;
    }

}
