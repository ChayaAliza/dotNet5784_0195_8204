
namespace DalApi;
using DO;

 public interface IDependency
{
    int Create(Dependency de1);
    Dependency? Read(int id);
    List<Dependency> ReadAll();
    void Update(Dependency de1);
    void Delete(int id);
}

