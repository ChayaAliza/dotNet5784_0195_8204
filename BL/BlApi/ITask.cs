namespace BlApi;

public interface ITask
{
    public int Create(BO.Task item);
    public BO.Task? Read(int id);
    public IEnumerable<BO.Task> ReadAll();
    public void Update(BO.Task item);
    public void Delete(int id);
}
