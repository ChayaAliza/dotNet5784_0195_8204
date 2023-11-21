namespace Dal;
using DalApi;
sealed public class DalList : IDal
{
    public IDependency Dependency => new DependencyImplementation();
    public ITask Task => new TaskImplementation();
    public IEngineer Engineer => new EngineerImplementation();
    
};