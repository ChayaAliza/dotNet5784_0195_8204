using Dal;
using DalApi;
using DO;


using System.Data.SqlTypes;
using System.Numerics;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace DalTest;

class Program
{
    //private static IDependency? s_dalDependency = new DependencyImplementation(); //stage 1
    //private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
    //private static ITask? s_dalTask = new TaskImplementation(); //stage 1
    //static readonly IDal s_dal = new DalList();
    static readonly IDal s_dal = new DalXml(); //stage 3


    public static void InfoOfEngineer(char x)
    {
        while (x != 0)//exit
        {
            switch (x)
            {

                case 'a'://add
                    Console.WriteLine("enter Engineer's id to add");
                    int Id = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("enter Engineer's name");
                    string? Name = (Console.ReadLine()!);
                    Console.WriteLine("enter Engineer's email");
                    string? Email = (Console.ReadLine()!) ;
                    Console.WriteLine("enter Engineer's level(0-for expert,1-for Junior,2-for Rookie)");

                    EngineerExperience? Level = (EngineerExperience)int.Parse(Console.ReadLine()!);
                    Console.WriteLine("enter Engineer's Cost");
                    double? Cost = double.Parse(Console.ReadLine()!);
                    DO.Engineer e = new DO.Engineer(Id, Name, Email, Level, Cost);

                    try
                    {
                        s_dal.Engineer!.Create(e);
                        Console.WriteLine("the engineer was added");
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;

                case 'b'://read by id
                    Console.WriteLine("enter engineer's id to read");
                    int id = int.Parse(Console.ReadLine()!);
                    try
                    {
                        Console.WriteLine(s_dal.Engineer!.Read(id));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 'c'://read all
                    Console.WriteLine("all the engineers:");
                    IEnumerable<DO.Engineer> listReadAllEngineers = s_dal.Engineer!.ReadAll()!;
                    foreach (var item in listReadAllEngineers)
                        Console.WriteLine(item);
                    break;
                case 'd'://update
                    Console.WriteLine("enter id of engineer to update");
                    int idUpdate = int.Parse(Console.ReadLine()!);//search of the id to update
                    try
                    {
                        Console.WriteLine(s_dal.Engineer!.Read(idUpdate));
                        int _id = idUpdate;
                        Console.WriteLine("enter Engineer's name");
                        string? _name = (Console.ReadLine()!);
                        Console.WriteLine("enter Engineer's email");
                        string? _email = (Console.ReadLine()!);
                        Console.WriteLine("enter Engineer's level(0-for expert,1-for Junior,2-for Rookie)");
                        EngineerExperience? _level = (EngineerExperience)int.Parse(Console.ReadLine()!);
                        Console.WriteLine("enter Engineer's Cost");
                        double? _cost = double.Parse(Console.ReadLine()!);
                        Engineer eUpdate = new Engineer(_id, _name, _email, _level, _cost);
                        s_dal.Engineer.Update(eUpdate);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 'e'://delete a product
                    Console.WriteLine("enter id of engineer to delete");
                    int idDelete = int.Parse(Console.ReadLine()!);
                    try
                    {
                        s_dal.Engineer!.Delete(idDelete);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                default:
                    break;
            }
            break;
        }
        
        
    }

    public static void InfoOfTask(char x)
    {
        switch (x)
        {

            case 'a'://add
                //Console.WriteLine("enter task's id");
                //int _id = int.Parse(Console.ReadLine());
                Console.WriteLine("enter task's description");
                string _description = (Console.ReadLine()!);
                Console.WriteLine("enter task's alias");
                string _alias = (Console.ReadLine()!);
                Console.WriteLine("enter task's engineerld");
                int _engineerld = int.Parse(Console.ReadLine()!);
                Console.WriteLine("enter task's milestone");
                bool _milestone = bool.Parse(Console.ReadLine()!);
                Console.WriteLine("enter task's date of created");
                DateTime _createdAt = Convert.ToDateTime(Console.ReadLine()!);
                Console.WriteLine("enter task's date of start");
                DateTime? _start = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("enter task's date of scheduled");
                DateTime? _scheduledDate = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("enter task's date of scheduled ForecastDate");
                DateTime? _ForecastDate = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("enter task's date of deadline");
                DateTime? _deadline = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("enter task's date of complete");
                DateTime? _complete = Convert.ToDateTime(Console.ReadLine()!);
                Console.WriteLine("enter task's deliverables");
                string? _deliverables = (Console.ReadLine()!);
                Console.WriteLine("enter task's remarks");
                string? _remarks = (Console.ReadLine());
                Console.WriteLine("enter Engeerid id");
                int _ideng= int.Parse(Console.ReadLine()!);
                Console.WriteLine("enter task's level(0-for expert,1-for Junior,2-for Rookie)");
                EngineerExperience? _complexityLevel = (EngineerExperience)int.Parse(Console.ReadLine()!);
                DO.Task t = new DO.Task(123, _description, _complexityLevel, _alias, _milestone, _createdAt, _start, _scheduledDate, _ForecastDate, _deadline, _complete, _deliverables, _remarks, _ideng);
                try
                {
                    s_dal.Task!.Create(t);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;
            case 'b'://read by id
                Console.WriteLine("enter task's id to read");
                int id = int.Parse(Console.ReadLine()!);
                try
                {
                    Console.WriteLine(s_dal.Task!.Read(id));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;
            case 'c'://read all
                Console.WriteLine("all the tasks with their customers:");
                IEnumerable<DO.Task> listReadAllTasks = s_dal.Task!.ReadAll()!;
                foreach (var item in listReadAllTasks)
                    Console.WriteLine(item);
                break;
            case 'd'://update
                Console.WriteLine("enter id of task to update");
                int idUpdate = int.Parse(Console.ReadLine()!);//search of the id to update
                try
                {
                    Console.WriteLine("enter task's description");
                    string Udescription = (Console.ReadLine()!);
                    Console.WriteLine("enter task's alias");
                    string Ualias = (Console.ReadLine()!);
                    Console.WriteLine("enter task's engineerld");
                    int Uengineerld = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("enter task's milestone");
                    bool Umilestone = bool.Parse(Console.ReadLine()!);
                    Console.WriteLine("enter task's date of created");
                    DateTime UcreatedAt = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("enter task's date of start");
                    DateTime? Ustart = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("enter task's date of scheduled");
                    DateTime? UscheduledDate = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("enter task's date of scheduled ForecastDate");
                    DateTime? UForecastDate = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("enter task's date of deadline");
                    DateTime? Udeadline = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("enter task's date of complete");
                    DateTime? Ucomplete = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("enter task's deliverables");
                    string? Udeliverables = (Console.ReadLine()!);
                    Console.WriteLine("enter task's remarks");
                    string? Uremarks = (Console.ReadLine()!);
                    Console.WriteLine("enter Engeerid id");
                    int Uideng = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("enter task's level(0-for expert,1-for Junior,2-for Rookie)");
                    EngineerExperience? UcomplexityLevel = (EngineerExperience)int.Parse(Console.ReadLine()!);
                    DO.Task ts = new DO.Task(idUpdate, Udescription, UcomplexityLevel, Ualias, Umilestone, UcreatedAt, Ustart, UscheduledDate, UForecastDate, Udeadline, Ucomplete, Udeliverables, Uremarks, Uideng);
                    s_dal.Task.Update(ts);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;
            case 'e'://delete an order
                Console.WriteLine("enter id of task to delete");
                int idDelete = int.Parse(Console.ReadLine()!);
                try
                {
                    s_dal.Task!.Delete(idDelete);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;
            default:
                break;
        }
    }
    public static void InfoOfDependency(char x)
    {
        while (x != 0)//exit
        {
            switch (x)
            {
                case 'a'://add
                    Console.WriteLine("enter Dependency's id to add");
                    int Id = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("enter Dependency's DependentTask");
                    int? NDependentTask = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("enter Dependency's DependsOnTask");
                    int? DependsOnTask = int.Parse(Console.ReadLine()!);
                    DO.Dependency d = new DO.Dependency(Id, NDependentTask, DependsOnTask);
                    try
                    {
                        s_dal.Dependency!.Create(d);
                        Console.WriteLine("the dependency was added");
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;

                case 'b'://read by id
                    Console.WriteLine("enter dependency's id to read");
                    int id = int.Parse(Console.ReadLine()!);
                    try
                    {
                        Console.WriteLine(s_dal.Dependency!.Read(id));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 'c'://read all
                    Console.WriteLine("all the dependencies:");
                    IEnumerable<DO.Dependency> listReadAllDependencies = s_dal.Dependency!.ReadAll()!;
                    foreach (var item in listReadAllDependencies)
                        Console.WriteLine(item);
                    break;
                case 'd'://update
                    Console.WriteLine("enter id of dependency to update");
                    int idUpdate = int.Parse(Console.ReadLine()!);//search of the id to update
                    try
                    {
                        Console.WriteLine(s_dal.Dependency!.Read(idUpdate));
                        int _id = idUpdate;
                        Console.WriteLine("enter Dependency's DependentTask");
                        int? NDependentT = int.Parse(Console.ReadLine()!);
                        Console.WriteLine("enter Dependency's DependsOnTask");
                        int? DependsOnT = int.Parse(Console.ReadLine()!);
                        DO.Dependency de = new DO.Dependency(_id, NDependentT, DependsOnT);
                        s_dal.Dependency.Update(de);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 'e'://delete a product
                    Console.WriteLine("enter id of Dependency to delete");
                    int idDelete = int.Parse(Console.ReadLine()!);
                    try
                    {
                        s_dal.Dependency!.Delete(idDelete);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                default:
                    break;
            }
            break;
        }


    }

    static void Main(string[] args)
    {
        Initialization.Do(s_dal);

        Console.WriteLine("for Engineer press1");
        Console.WriteLine("for Task press 2");
        Console.WriteLine("for Dependency press 3");
        Console.WriteLine("for exit press 0");
        int select = char.Parse(Console.ReadLine()!);
        char x;
        while (select != 0)
        {
            switch (select)
            {
                case 1:
                    Console.WriteLine("for exit press 0");
                    Console.WriteLine("for add a Engineer press a");
                    Console.WriteLine("for read a Engineer press b");
                    Console.WriteLine("for read all Engineers press c");
                    Console.WriteLine("for update a Engineer press d");
                    Console.WriteLine("for delete a Engineer press e");
                    x = char.Parse(Console.ReadLine()!);
                    InfoOfEngineer(x);//doing this function 
                    break;
                case 2:
                    Console.WriteLine("for exit press 0");
                    Console.WriteLine("for add a Task press a");
                    Console.WriteLine("for read a Task press b");
                    Console.WriteLine("for read all Tasks press c");
                    Console.WriteLine("for update a Task press d");
                    Console.WriteLine("for delete a Task press e");
                    x = char.Parse((Console.ReadLine()!));
                    InfoOfTask(x); //doing this function 
                    break;
                case 3:
                    Console.WriteLine("for exit press 0");
                    Console.WriteLine("for add a Dependency press a");
                    Console.WriteLine("for read a Dependency press b");
                    Console.WriteLine("for read all Dependency press c");
                    Console.WriteLine("for update a Dependency press d");
                    Console.WriteLine("for delete a Dependency press e");
                    x = char.Parse((Console.ReadLine()!));
                    InfoOfDependency(x);//doing this function 
                    break;
                default:
                    break;
            }
            Console.WriteLine("enter a number");
            select = int.Parse(Console.ReadLine()!);
        }

    }
}



