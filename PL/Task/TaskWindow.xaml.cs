using BO;
using PL.Engineer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.Status Status { get; set; } = BO.Status.All;


        public BO.Task? Task
        {
            get { return (BO.Task)GetValue(TaskProperty); }
            set { SetValue(TaskProperty, value); }
        }

        public static readonly DependencyProperty TaskProperty =
            DependencyProperty.Register("Task", typeof(BO.Task), typeof(TaskWindow), new PropertyMetadata(null));


        // Dependency property to bind the task list to the UI.
        public IEnumerable<BO.TaskInList> TaskList
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.TaskInList>),
            typeof(TaskWindow), new PropertyMetadata(null));

        // Dependency property to bind the task dependencies to the UI.
        public IEnumerable<BO.TaskInList> TaskDependencies
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(TaskDependenciesProperty); }
            set { SetValue(TaskDependenciesProperty, value); }
        }

        public static readonly DependencyProperty TaskDependenciesProperty =
            DependencyProperty.Register("TaskDependencies", typeof(IEnumerable<BO.TaskInList>), typeof(TaskWindow), new PropertyMetadata(null));

        public int CheckedDependTask { get; set; } = 0; // Property to hold the ID of the selected dependency task.

        // Constructor for the TaskWindow class.
        public TaskWindow(int id = 0)
        {
            try
            {
                // Initializing the window with the data of the specified task or with empty data if no ID is provided.
                if (id != 0)
                {
                    Task = s_bl!.Task.Read(id)!;
                }
                else
                {
                    Task = new BO.Task()
                    {
                        Id = 0,
                        Description = "",
                        Alias = "",
                        Status = null,
                        CreateAt = DateTime.Now,
                        Start = null,
                        ScheduledDate = null,
                        Deadline = DateTime.Now,
                        Complete = null,
                        Deliverables = "",
                        Remarks = "",
                        Engineer = new EngineerInTask()
                        {
                            Id = 0,
                            Name = ""
                        },
                        Level = null,
                        Dependencies = null,
                        IsActive = false
                    };
                }

                // Initializing the task dependencies collection based on the current task's dependencies.
                TaskDependencies = Task.Dependencies != null ? new ObservableCollection<BO.TaskInList>(Task.Dependencies) : new ObservableCollection<BO.TaskInList>();

                // Retrieving and initializing the task list for dependency selection.
                var task = s_bl?.Task.ReadAll().Select(x => new BO.TaskInList()
                {
                    Id = x.Id,
                    Alias = x.Alias,
                    Description = x.Description,
                    Status = (Status)x.Status!
                }).ToList();

                TaskList = task != null ? new ObservableCollection<BO.TaskInList>(task) : new ObservableCollection<BO.TaskInList>();
            }
            catch (Exception ex)
            {
                // Throwing any exceptions that occur during initialization.
                throw new Exception($"{ex.Message}");
            }

            // Initializing the window components.
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)!.Content.ToString() == "Add")
            {
                try
                {
                    s_bl.Task.Create(Task!);
                    MessageBox.Show("addition successful", "Confirmation", MessageBoxButton.OK);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex}", "Confirmation", MessageBoxButton.OK);
                }
            }
            else
            {
                try
                {
                    s_bl.Task.Update(Task!);
                    MessageBox.Show("updation successful", "Confirmation", MessageBoxButton.OK);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex}", "Confirmation", MessageBoxButton.OK);

                }
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cbCheckedDependTask_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Checking if a task is selected for dependency.
                if (CheckedDependTask != 0)
                {
                    // Prompting the user for confirmation.
                    MessageBoxResult ans = MessageBox.Show("Are you sure you want to add the dependency?", "Confirmation", MessageBoxButton.YesNo);

                    // Proceeding if the user confirms.
                    if (ans == MessageBoxResult.Yes)
                    {
                        // Retrieving the selected dependency task.
                        BO.Task dependency = s_bl.Task.Read(CheckedDependTask)!;
                        // Initializing the task's dependencies list if not already initialized.
                        if (Task!.Dependencies == null)
                            Task.Dependencies = new List<TaskInList>();

                        // Adding the selected task to the current task's dependencies.
                        Task.Dependencies.Add(new BO.TaskInList()
                        {
                            Id = dependency.Id,
                            Alias = dependency.Alias,
                            Description = dependency.Description,
                            Status = (Status)dependency.Status!
                        });

                        // Refreshing the UI with the updated dependencies list.
                        TaskDependencies = new ObservableCollection<BO.TaskInList>(Task.Dependencies);
                    }
                    CheckedDependTask = 0;
                }
            }
            catch (Exception ex)
            {
                // Displaying any exceptions that occur.
                MessageBox.Show($"{ex}", "Confirmation", MessageBoxButton.OK);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

}
