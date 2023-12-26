
using DalApi;
using System.ComponentModel;
using System.Diagnostics;

namespace Dal;

//stage 3
sealed internal class DalXml : IDal
{
    public IDependency Dependency => new DependencyImplementation();

    public ITask Task => new TaskImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public static IDal Instance { get; } = new DalXml();
    public DalXml() { }
}
