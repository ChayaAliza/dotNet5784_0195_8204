namespace Dal;
using DalApi;
sealed internal class DalList : IDal
{
    public IDependency Dependency => new DependencyImplementation();
    public ITask Task => new TaskImplementation();
    public IEngineer Engineer => new EngineerImplementation();
    public static IDal Instance { get; } = new DalList();
    private DalList() { }

};