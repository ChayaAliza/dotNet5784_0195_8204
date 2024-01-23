using BlApi;
using BO;
using DO;

namespace BlTest
{
    internal class Program
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        enum MainMenu { EXIT, MILESTONE, ENGINEER, TASK }
        enum SubMenu { EXIT, CREATE, READ, READALL, UPDATE, DELETE }
        public static void EngineerMenu()
        {
            int chooseSubMenu;
            do
            {
                Console.WriteLine("enum SubMenu { EXIT ,CREATE , READ, READALL ,UPDATE,DELETE }");
                int.TryParse(Console.ReadLine() ?? throw new Exception("Enter a number please"), out chooseSubMenu);

                switch (chooseSubMenu)
                {
                    case 1:
                        Console.WriteLine("Enter id, name,isactive, email, level, and cost");
                        int idEngineer, idTask;
                        string nameEngineer, emailEngineer, inputLevel;
                        DO.EngineerExperience levelEngineer;
                        bool isActive;
                        double costEngineer;
                        int.TryParse(Console.ReadLine() ?? throw new Exception("enter a number please"), out idEngineer);
                        nameEngineer = (Console.ReadLine()!);
                        isActive = Console.ReadLine() == "false" ? false : true;
                        emailEngineer = Console.ReadLine()!;
                        inputLevel = Console.ReadLine()!;
                        levelEngineer = (DO.EngineerExperience)Enum.Parse(typeof(DO.EngineerExperience), inputLevel);
                        double.TryParse(Console.ReadLine() ?? throw new Exception("enter a doublenumber please"), out costEngineer);
                        int.TryParse(Console.ReadLine() ?? throw new Exception("enter a number please"), out idTask);
                        BO.Engineer newEng = new BO.Engineer()
                        {
                            Id = idEngineer,
                            Name = nameEngineer,
                            IsActive = isActive,
                            Email = emailEngineer,
                            Level = (BO.EngineerExperience)levelEngineer,
                            Cost = costEngineer,
                            Task = new BO.TaskInEngineer()
                            {
                                Id = idTask,
                                Alias = s_bl.Task.Read(idTask)!.Alias
                            }
                        };
                        try
                        {
                            s_bl.Engineer.Create(newEng);
                        }
                        catch (Exception ex)
                        {
                            throw new BlFailedToCreate("Failed to build engineer ", ex);
                        }
                        break;
                    case 2:
                        int id;
                        Console.WriteLine("Enter id for reading");
                        int.TryParse(Console.ReadLine() ?? throw new Exception("enter a number please"), out id);
                        try
                        {
                            if (s_bl.Engineer!.Read(id) is null)
                                Console.WriteLine("no engineer found");
                            else
                            {
                                Console.WriteLine(s_bl.Engineer!.Read(id)!.ToString());
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new BlFailedToRead("Failed to read engineer ", ex);
                        }
                        break;
                    case 3:
                        try
                        {
                            s_bl.Engineer!.ReadAll()
                            .ToList()
                            .ForEach(engineer => Console.WriteLine(engineer.ToString()));
                        }
                        catch (Exception ex)
                        {
                            throw new BlFailedToReadAll("Failed to readall engineer ", ex);
                        }
                        break;
                    case 4:
                        int idEngineerUpdate, idTaskUpdate;
                        string? nameEngineerUpdate;
                        string emailEngineerUpdate, inputUpdateLevel;
                        BO.EngineerExperience levelEngineerUpdate;
                        double costEngineerUpdate;
                        bool isActiveUpdate;
                        Console.WriteLine("Enter id for reading");
                        idEngineerUpdate = int.Parse(Console.ReadLine()!);
                        try
                        {
                            BO.Engineer updatedEngineer = s_bl.Engineer.Read(idEngineerUpdate)!;
                            Console.WriteLine(updatedEngineer.ToString());
                            Console.WriteLine("Enter name, isactive,level,cost,role and id of task to update");//if null to put the same details
                            nameEngineerUpdate = Console.ReadLine() ?? updatedEngineer.Name;
                            isActiveUpdate = Console.ReadLine() == "false" ? false : true;
                            emailEngineerUpdate = Console.ReadLine() ?? updatedEngineer.Email!;
                            inputUpdateLevel = Console.ReadLine()!;
                            levelEngineerUpdate = string.IsNullOrWhiteSpace(inputUpdateLevel) ? updatedEngineer.Level : (BO.EngineerExperience)Enum.Parse(typeof(BO.EngineerExperience), inputUpdateLevel);
                            double.TryParse(Console.ReadLine() ?? throw new Exception("enter a number please"), out costEngineerUpdate);
                            int.TryParse(Console.ReadLine() ?? throw new Exception("enter a number please"), out idTaskUpdate);
                            BO.Engineer newEngUpdate = new BO.Engineer()
                            {
                                Id = idEngineerUpdate,
                                Name = nameEngineerUpdate,
                                IsActive = isActiveUpdate,
                                Email = emailEngineerUpdate,
                                Level = (BO.EngineerExperience)levelEngineerUpdate,
                                Cost = costEngineerUpdate,
                                Task = new BO.TaskInEngineer()
                                {
                                    Id = idTaskUpdate,
                                    Alias = s_bl.Task.Read(idTaskUpdate)!.Alias
                                }
                            };
                            try
                            {
                                s_bl.Engineer.Update(newEngUpdate);
                            }
                            catch (Exception ex)
                            {
                                throw new BlFailedToCreate($"failed to update engineer id={idEngineerUpdate}", ex);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new BlFailedToRead($"failed to read id: {idEngineerUpdate} of engineer", ex);
                        }
                        break;
                    case 5:
                        int idDelete;
                        Console.WriteLine("Enter id for deleting");
                        int.TryParse(Console.ReadLine() ?? throw new Exception("enter a number please"), out idDelete);
                        try
                        {
                            s_bl.Engineer!.Delete(idDelete);
                        }
                        catch (Exception ex)
                        {
                            throw new BlFailedToDelete("failed to delete engineer", ex);
                        }

                        break;
                    default: return;
                }
            } while (chooseSubMenu > 0 && chooseSubMenu < 6);
        }

        private static void TaskMenu()
        {
            int chooseSubMenu;
            do
            {
                Console.WriteLine("enum SubMenu { EXIT ,CREATE , READ, READALL ,UPDATE,DELETE }");
                int.TryParse(Console.ReadLine() ?? throw new Exception("enter a number please"), out chooseSubMenu);
                switch (chooseSubMenu)
                {
                    case 1:
                        Console.WriteLine("Enter  description, alias,deriverables, remarks,milestone, dates and task's level");
                        int taskId, taskEngineerId, milestoneInTaskId, engineerInTaskId, taskInListId;
                        string taskDescription, taskAlias, taskDeliverables, taskRemarks, inputEE;
                        bool isActive;
                        TimeSpan requiredEffortTime;
                        DateTime taskCreateAt;
                        DateTime taskStart, taskForecastDate, taskDeadline, taskComplete;
                        BO.EngineerExperience taskLevel;
                        List<BO.TaskInList> taskInList = new List<TaskInList>();
                        int.TryParse(Console.ReadLine() ?? throw new Exception("enter a number please"), out taskId);
                        taskDescription = Console.ReadLine()!;
                        taskAlias = Console.ReadLine()!;
                        isActive = Console.ReadLine() == "false" ? false : true;
                        DateTime.TryParse(Console.ReadLine() ?? throw new Exception("enter a date please"), out taskCreateAt);
                        DateTime.TryParse(Console.ReadLine() ?? throw new Exception("enter a date please"), out taskStart);
                        DateTime.TryParse(Console.ReadLine() ?? throw new Exception("enter a date please"), out taskForecastDate);
                        DateTime.TryParse(Console.ReadLine() ?? throw new Exception("enter a date please"), out taskDeadline);
                        DateTime.TryParse(Console.ReadLine() ?? throw new Exception("enter a date please"), out taskComplete);
                        taskDeliverables = Console.ReadLine()!;
                        requiredEffortTime = TimeSpan.Zero;
                        taskRemarks = Console.ReadLine()!;
                        int.TryParse(Console.ReadLine() ?? throw new Exception("enter a number please"), out engineerInTaskId);
                        inputEE = Console.ReadLine()!;
                        taskLevel = (BO.EngineerExperience)Enum.Parse(typeof(BO.EngineerExperience), inputEE);
                        int.TryParse(Console.ReadLine() ?? throw new Exception("enter a number please"), out taskEngineerId);
                        int.TryParse(Console.ReadLine() ?? throw new Exception("enter a number please"), out milestoneInTaskId);
                        int.TryParse(Console.ReadLine() ?? throw new Exception("enter a number please"), out taskInListId);
                        while (taskInListId != -1)
                        {
                            taskInList!.Add(new BO.TaskInList()
                            {
                                Id = taskInListId,
                                Description = s_bl.Task.Read(taskInListId)!.Description,
                                Alias = s_bl.Task.Read(taskInListId)!.Alias,
                                Status = Tools.CalculateStatus(null, null, null, null)
                            });
                            taskInListId = int.Parse(Console.ReadLine()!);
                        }
                        BO.Task newTask = new BO.Task()
                        {
                            Id = taskId,
                            Description = taskDescription,
                            Alias = taskAlias,
                            IsActive = isActive,
                            CreateAt = DateTime.Now,
                            Start = null,
                            ForecastDate = null,
                            Deadline = null,
                            Complete = null,
                            Deliverables = taskDeliverables,
                            Remarks = taskRemarks,
                            Engineer = new EngineerInTask()
                            {
                                Id = engineerInTaskId,
                                Name = s_bl.Engineer.Read(engineerInTaskId)!.Name!
                            },
                            Level = taskLevel,
                            Status = Tools.CalculateStatus(null, null, null, null),
                            Milestone = new MilestoneInTask()
                            {
                                Id = milestoneInTaskId,
                                Alias = s_bl.Milestone.Read(milestoneInTaskId)!.Alias
                            },
                            Dependencies = taskInList!
                        };
                        try { s_bl.Task.Create(newTask); }
                        catch (Exception ex) { throw new BlFailedToCreate("failed to create task", ex); }
                        break;
                    case 2:
                        int id;
                        Console.WriteLine("Enter id for reading");
                        int.TryParse(Console.ReadLine() ?? throw new Exception("enter a number please"), out id);
                        try
                        {
                            if (s_bl.Task!.Read(id) is null)
                                Console.WriteLine("no task found");
                            else
                                Console.WriteLine(s_bl.Task!.Read(id)!.ToString());
                        }
                        catch (Exception ex)
                        {
                            throw new BlFailedToRead($"failed to read id: {id} in task", ex);
                        }
                        break;
                    case 3:
                        try
                        {
                            s_bl.Task!.ReadAll()
                                      .ToList()
                                      .ForEach(task => Console.WriteLine(task.ToString()));
                        }
                        catch (Exception ex)
                        {
                            throw new BlFailedToRead($"failed to readall task", ex);
                        }
                        break;
                    case 4:

                        int idTaskUpdate, engineerInTaskIdUpdate, taskEngineerIdUpdate;
                        string? taskDescriptionUpdate, taskAliasUpdate, taskDeliverablesUpdate, taskRemarksUpdate, inputEEUpdate;
                        bool isActiveUpdate;
                        DateTime taskCreateAtUpdate, taskStartUpdate, taskForecastDateUpdate, taskDeadlineUpdate, taskCompleteUpdate;
                        BO.EngineerExperience? taskLevelUpdate;
                        List<BO.TaskInList> taskInListUpdate = new List<BO.TaskInList>();
                        Console.WriteLine("Enter id for reading");
                        int.TryParse(Console.ReadLine() ?? throw new Exception("enter a number please"), out idTaskUpdate);
                        BO.Task updatedTask = s_bl.Task.Read(idTaskUpdate)!;
                        Console.WriteLine(updatedTask.ToString());
                        Console.WriteLine("Enter details to update");
                        taskDescriptionUpdate = Console.ReadLine() ?? updatedTask.Description;
                        taskAliasUpdate = Console.ReadLine() ?? updatedTask.Alias;
                        isActiveUpdate = updatedTask.IsActive;
                        DateTime.TryParse(Console.ReadLine() ?? throw new Exception("enter a number please"), out taskCreateAtUpdate);
                        DateTime.TryParse(Console.ReadLine() ?? throw new Exception("enter a number please"), out taskStartUpdate);
                        DateTime.TryParse(Console.ReadLine() ?? throw new Exception("enter a number please"), out taskForecastDateUpdate);
                        DateTime.TryParse(Console.ReadLine() ?? throw new Exception("enter a number please"), out taskDeadlineUpdate);
                        DateTime.TryParse(Console.ReadLine() ?? throw new Exception("enter a number please"), out taskCompleteUpdate);
                        taskDeliverablesUpdate = Console.ReadLine() ?? updatedTask.Deliverables;
                        taskRemarksUpdate = Console.ReadLine() ?? updatedTask.Remarks;
                        inputEEUpdate = Console.ReadLine() ?? updatedTask.Level.ToString();
                        taskLevelUpdate = string.IsNullOrWhiteSpace(inputEEUpdate) ? updatedTask.Level : (BO.EngineerExperience)Enum.Parse(typeof(BO.EngineerExperience), inputEEUpdate);
                        int.TryParse(Console.ReadLine() ?? updatedTask.Engineer!.Id.ToString(), out taskEngineerIdUpdate);
                        int.TryParse(Console.ReadLine() ?? updatedTask.Milestone!.Id.ToString(), out milestoneInTaskId);
                        int.TryParse(Console.ReadLine() ?? null, out taskInListId);
                        int.TryParse(Console.ReadLine() ?? updatedTask.Engineer!.Id.ToString(), out engineerInTaskIdUpdate);
                        while (taskInListId != -1)
                        {
                            taskInListUpdate!.Add(new BO.TaskInList()
                            {
                                Id = taskInListId,
                                Description = s_bl.Task.Read(taskInListId)!.Description,
                                Alias = s_bl.Task.Read(taskInListId)!.Alias,
                                Status = Tools.CalculateStatus(updatedTask.Start, updatedTask.ForecastDate, updatedTask.Deadline, updatedTask.Complete)
                            });
                            int.TryParse(Console.ReadLine() ?? throw new Exception("enter a number please"), out taskInListId);

                        }

                        BO.Task newTaskUpdate = new BO.Task()
                        {
                            Id = idTaskUpdate,
                            Description = taskDescriptionUpdate,
                            Alias = taskAliasUpdate,
                            IsActive = isActiveUpdate,
                            CreateAt = updatedTask.CreateAt,
                            Start = updatedTask.Start,
                            ForecastDate = updatedTask.ForecastDate,
                            Deadline = updatedTask.Deadline,
                            Complete = updatedTask.Complete,
                            Deliverables = taskDeliverablesUpdate,
                            Remarks = taskRemarksUpdate,
                            Engineer = new EngineerInTask()
                            {

                                Id = engineerInTaskIdUpdate,
                                Name = s_bl.Engineer.Read(engineerInTaskIdUpdate)!.Name!
                            },
                            Level = taskLevelUpdate,
                            Status = Tools.CalculateStatus(updatedTask.Start, updatedTask.ForecastDate, updatedTask.Deadline, updatedTask.Complete),
                            Milestone = new MilestoneInTask()
                            {
                                Id = milestoneInTaskId,
                                Alias = s_bl.Milestone.Read(milestoneInTaskId)!.Alias
                            },
                            Dependencies = taskInListUpdate!
                        };
                        try { s_bl.Task.Update(newTaskUpdate); }
                        catch (Exception ex) { throw new BlFailedToUpdate($"failed to update id:{idTaskUpdate} in task", ex); }
                        ; break;
                    case 5:
                        int idDelete;
                        Console.WriteLine("Enter id for deleting");
                        try
                        {
                            int.TryParse(Console.ReadLine() ?? throw new Exception("enter a number please"), out idDelete);
                            s_bl.Task!.Delete(idDelete);
                        }
                        catch (Exception ex)
                        {
                            throw new BlFailedToDelete($"failed to delete in task", ex);
                        }
                        break;
                    default: return;
                }
            } while (chooseSubMenu > 0 && chooseSubMenu < 6);
        }

        static void Main(string[] args)
        {
            Console.Write("Would you like to create Initial data? (Y/N)");
            string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
            if (ans == "Y")
                DalTest.Initialization.Do();
            int chooseMainMenu;
            Console.WriteLine("enum MainMenu { EXIT, MILESTONE, ENGINEER, TASK }");
            int.TryParse(Console.ReadLine() ?? throw new Exception("Enter a number please"), out chooseMainMenu);
            do
            {
                switch (chooseMainMenu)
                {
                    case 1:  //MilestoneMenu();
                        break;
                    case 2:
                        EngineerMenu();
                        break;
                    case 3:
                        TaskMenu();
                        break;
                    default:
                        break;
                }
                Console.WriteLine("enum MainMenu { EXIT, MILESTONE, ENGINEER, TASK }");
                int.TryParse(Console.ReadLine() ?? throw new Exception("Enter a number please"), out chooseMainMenu);
            } while (chooseMainMenu > 0 && chooseMainMenu < 5);
           

        }
    }
}