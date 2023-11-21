namespace DalTest;
using Dal;
using DalApi;
using DO;
using System.Reflection.Emit;
using System.Security.Cryptography;


public static class Initialization
{
    //private static IEngineer? s_dalEngineer;
    //private static ITask? s_dalTask;
    //private static IDependency? s_dalDependency;
    private static IDal? s_dal;
    private static readonly Random s_rand = new();

    private static void createEngineer()
    {

        (string Name, string Email)[] engineerDetails =
         { ("Netanal Bretler","Nb214490195@gmail.com") ,
          ("Refal Klein","Rklein1983@gmail.com") ,
          ("Yair Cohen" , "0527661610Yc@gmail.com"),
          ("Shimon Levi" , "Sh.Lv1970@gmail.cim")
        };
        foreach (var engineerD in engineerDetails)
        {
            int _id;
            do
            {
                _id = s_rand.Next(200000000, 400000000);

            } while (s_dal!.Engineer.Read(_id) != null);
            EngineerExperience level = (EngineerExperience)s_rand.Next(0, 3);
            double Cost = 0;
            switch (level)
            {
                case EngineerExperience.export:
                    Cost = 700;
                    break;
                case EngineerExperience.Junior:
                    Cost = 350;
                    break;
                case EngineerExperience.Rookie:
                    Cost = 200;
                    break;
            }
            Engineer newEng = new(_id, engineerD.Name, engineerD.Email, level, Cost);
            s_dal!.Engineer.Create(newEng);
        }
    }
    private static void createTask()
    {
        (string Alias, string Description)[] TaskDetails =
         {
            ("A soup making", "make a soup as the recipe"),
            ("libary", "take the books and go change them"),
            ("change money", "remember to take the money from the waller"),
            ("Fueneral", "u have to go to make Achdut with Am Israel")
        };
        foreach (var taskd in TaskDetails)
        {

            EngineerExperience CopmlexityLevel = (EngineerExperience)s_rand.Next(0, 3);
            DateTime Start = new DateTime(2004, 8, 7);
            DateTime? ForecastDate = null;
            DateTime? ScheduledDate = null;
            DateTime? DeadLine = null;
            DateTime? Complete = null;
            DateTime? CreatedAt = null;
            bool MilesStone = false;

            string? Deliverables = null;
            string? Remarks = null;
            int? EngineerId = null;
            Task newTask = new(0, taskd.Description, CopmlexityLevel, taskd.Alias, MilesStone, CreatedAt, Start, ScheduledDate , ForecastDate, DeadLine, Complete , Deliverables , Remarks , EngineerId);
            s_dal!.Task.Create(newTask);
        }
    }
    private static void createDependency()
    {
        (int DependentTask, int DependsOnTask)[] DependencysDetails =
        {
            (1,1),
            (2, 2),
            (3, 3),
            (4, 4)
        };
        foreach (var Dependencyd in DependencysDetails)
        {
            Dependency De = new(0, Dependencyd.DependentTask, Dependencyd.DependsOnTask);
            s_dal!.Dependency.Create(De);
        }
    }

    public static void Do(IDal dal)
    {

        //s_dalDependency = dalDependency ?? throw new NullReferenceException("DAL can not be null!");
        //s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        //s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        s_dal = dal ?? throw new NullReferenceException("DAL can not be null!");
        createEngineer();
        createTask();
        createDependency();
    }

}
