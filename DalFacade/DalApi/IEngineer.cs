
namespace DalApi;
using DO;

public interface IEngineer
{
    int Create(Engineer eng);
    
   Engineer? Read(int id);
    List<Engineer> ReadAll();
    void Update(Engineer eng);
    void Delete(int id);
}
