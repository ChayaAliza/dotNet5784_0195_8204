namespace BlApi;

public interface ITask
{
    public int Create(BO.Task boTask);
    public BO.Task? Read(int id);
    public IEnumerable<BO.Task> ReadAll();
    public void Update(BO.Task boTask);
    public void Delete(int id);
}
