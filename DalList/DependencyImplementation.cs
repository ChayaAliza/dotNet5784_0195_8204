namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

//Implementation of the methods of the dependency entity.
internal class DependencyImplementation : IDependency
{
    //A method that generates a new dependency.
    public int Create(Dependency de1)
    {
        int id = DataSource.Config.NextDependencyId;
        Dependency copy = de1 with { Id = id };
        DataSource.Dependencys.Add(copy);
        return id;
    }
    //A method that requests/receives a single object.
    public Dependency? Read(int id)
    {
        return DataSource.Dependencys.FirstOrDefault(x => x.Id == id);
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        return DataSource.Dependencys.FirstOrDefault(x => filter(x));
    }

    //A method that requests/receives all objects of a certain type.
    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter = null)
    {
        if (filter != null)
        {
            return from item in DataSource.Dependencys
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Dependencys
               select item;
    }
    //A method that updates an existing object.
    public void Update(Dependency de1)
    {
        Dependency? removeDependency = Read(de1.Id)!;
        if (Read(de1.Id) is null) //Checking if there is an object with the same ID number, in the list.
            throw new DalDoesNotExistException($"Task with ID={de1.Id} doesn't exist"); //A suitable exception throw.

        DataSource.Dependencys.Remove(removeDependency); //Removes the reference to an existing object from a list.
        DataSource.Dependencys.Add(de1); //Added to the list the reference to the updated object received as a parameter.
    }

    //A method that deletes an existing object.
    public void Delete(int id)
    {
        if (Read(id) is null) //If it is allowed to delete the entity - check if it exists in the list.
            throw new DalDoesNotExistException($"Task with ID={id} doesn't exist"); //A suitable exception throw.
        DataSource.Dependencys.Remove(Read(id)!); //Deleting the object from the list.

    }
    
}
