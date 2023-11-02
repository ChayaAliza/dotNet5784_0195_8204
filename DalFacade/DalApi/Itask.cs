namespace DalApi;
using DO;

public interface Itask
{
    int Create(Task Task);
    Task? Read(int id);
    List<Task?> ReadAll();
    void Update(Task item);
    void Delete(int id);

    

}
