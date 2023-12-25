using DalApi;
using DO;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Dal;

internal class EngineerImplementation : IEngineer
{
    const string filePath = "engineers";
    public int Create(Engineer de1)
    {
        int id = de1.Id;
        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(filePath);
        if (engineers.Any(e => e.Id == id))
            throw new DalAlreadyExistsException($"Engineer with ID={id} already exists");

        engineers.Add(de1);
        XMLTools.SaveListToXMLSerializer<Engineer>(engineers, filePath);
        return id;

    }

    public void Delete(int id)
    {
        throw new DalDeletionImpossible($"Engineer is indelible entity");
    }

    public Engineer? Read(int id)
    {
        return XMLTools.LoadListFromXMLSerializer<Engineer>("engineers").FirstOrDefault(x => x.Id == id);
    } 

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        return XMLTools.LoadListFromXMLSerializer<Engineer>(filePath).FirstOrDefault<Engineer>(filter);
    } 


    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        return filter == null ? XMLTools.LoadListFromXMLSerializer<Engineer>(filePath).Select(item => item) : XMLTools.LoadListFromXMLSerializer<Engineer>(filePath).Where(filter!);
    }

    public void Update(Engineer de1)
    {
        var existingEngineer = Read(e => e.Id == de1.Id);
        if (existingEngineer is null)
            throw new DalDoesNotExistException($"Engineer with ID={de1.Id} does not exist");

        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(filePath);
        engineers.Remove(existingEngineer);
        engineers.Add(de1);
        XMLTools.SaveListToXMLSerializer<Engineer>(engineers, filePath);
    }

    public void Reset()
    {
        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(filePath);
        engineers.Clear();
        XMLTools.SaveListToXMLSerializer<Engineer>(engineers, filePath);
    }
}
