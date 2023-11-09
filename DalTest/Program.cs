using Dal;
using DalApi;
using DO;
using System.Numerics;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace DalTest;

class Program
{
    private static IDependency? s_dalDependency = new DependencyImplementation(); //stage 1
    private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
    private static ITask? s_dalTask = new TaskImplementation(); //stage 1
    public static void InfoOfEngineer(char x)
    {
        while (x != 0)//exit!
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
                    Engineer e = new Engineer(Id, Name, Email, Level, Cost);

                    try
                    {
                        int result = s_dalEngineer!.Create(e);
                        Console.WriteLine("the engineer was added");
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
                        Console.WriteLine(s_dalEngineer!.Read(id));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 'c'://read all
                    Console.WriteLine("all the engineers:");
                    List<Engineer> listReadAllEngineers = s_dalEngineer!.ReadAll();
                    foreach (var item in listReadAllEngineers)
                        Console.WriteLine(item);
                    break;
                case 'd'://update
                    Console.WriteLine("enter id of engineer to update");
                    int idUpdate = int.Parse(Console.ReadLine()!);//search of the id to update
                    try
                    {
                        Console.WriteLine(s_dalEngineer!.Read(idUpdate));
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
                        s_dalEngineer!.Delete(idDelete);
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
        //___________________________________________________________________________________________________
        //___________________________________________________________________________________________________
        //___________________________________________________________________________________________________
        //___________________________________________________________________________________________________
        //___________________________________________________________________________________________________
        //___________________________________________________________________________________________________
        //___________________________________________________________________________________________________
    }


    public static void InfoOfTask(char x)
    {
        switch (x)
        {

            case 'a'://add
                //Console.WriteLine("enter task's id");//razzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz??????????????????????????????????????????????|
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
                    s_dalTask!.Create(t);
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
                    Console.WriteLine(s_dalTask!.Read(id));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;
            case 'c'://read all
                Console.WriteLine("all the tasks with their customers:");
                List<DO.Task> listReadAllTasks = s_dalTask!.ReadAll()!;
                foreach (var item in listReadAllTasks)
                    Console.WriteLine(item);
                break;
            case 'd'://update!!
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
                    DO.Task Ut = new DO.Task(123, Udescription, UcomplexityLevel, Ualias, Umilestone, UcreatedAt, Ustart, UscheduledDate, UForecastDate, Udeadline, Ucomplete, Udeliverables, Uremarks, Uideng);
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
                    s_dalTask!.Delete(idDelete);
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


    static void Main(string[] args)
    {
        Initialization.Do(s_dalDependency, s_dalEngineer, s_dalTask);

        Console.WriteLine("for Engineer press1");
        Console.WriteLine("for Dependency press 2");
        Console.WriteLine("for Task press 3");
        Console.WriteLine("for exit press 0");
        int select = int.Parse(Console.ReadLine()!);
        char x;
        while (select != 0)
        {
            switch (select)
            {
                case 1:
                    Console.WriteLine("for exit press 0");//!!!!!!
                    Console.WriteLine("for add a Engineer press a");
                    Console.WriteLine("for read a Engineer press b");
                    Console.WriteLine("for read all Engineers press c");
                    Console.WriteLine("for update a Engineer press d");
                    Console.WriteLine("for delete a Engineer press e");
                    x = char.Parse(Console.ReadLine()!);
                    InfoOfEngineer(x);//doing this function 
                    break;
                case 2:
                    Console.WriteLine("for exit press 0");//!!!!!!
                    Console.WriteLine("for add a Task press a");
                    Console.WriteLine("for read a Task press b");
                    Console.WriteLine("for read all Tasks press c");
                    Console.WriteLine("for update a Task press d");
                    Console.WriteLine("for delete a Task press e");
                    x = char.Parse((Console.ReadLine()!));
                    InfoOfTask(x); //doing this function 
                    break;
                case 3:
                    Console.WriteLine("for add an item in order press a");
                    Console.WriteLine("for read item in order press b");
                    Console.WriteLine("for read all items in orders press c");
                    Console.WriteLine("for update an item in order press d");
                    Console.WriteLine("for delete an item in order press e");
                    Console.WriteLine("for read an item in order by id of order and product press f");
                    Console.WriteLine("for read an items in order press g");
                    x = char.Parse((Console.ReadLine()!));
                    InfoOfEngineer(x);//doing this function 
                    break;
                default:
                    break;
            }
            Console.WriteLine("enter a number");
            select = int.Parse(Console.ReadLine()!);
        }

    }
}



