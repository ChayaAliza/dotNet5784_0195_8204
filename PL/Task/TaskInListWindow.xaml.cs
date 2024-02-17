using PL.Engineer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
    /// Interaction logic for TaskInListWindow.xaml
    /// </summary>
    public partial class TaskInListWindow : Window
    {
        // Static instance of the business logic layer
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        // Property to store the current status filter
        public BO.Status status { get; set; } = BO.Status.All;

        /// List of tasks property
        public IEnumerable<BO.Task> TaskList
        {
            get { return (IEnumerable<BO.Task>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        // Dependency property for TaskList
        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.Task>), typeof(TaskInListWindow), new PropertyMetadata(null));

        // Constructor
        public TaskInListWindow()
        {
            InitializeComponent();
            // Initialize TaskList with all tasks from the business logic layer
            TaskList = s_bl?.Task.ReadAll()!;
        }

        // Event handler for ListView selection change
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Currently no specific logic for selection change
        }

        /// Update or add a new task
        private void update_add(object sender, MouseButtonEventArgs e)
        {
            // Retrieve selected task from ListView and open TaskWindow dialog for editing
            BO.Task? taskInList = (sender as ListView)?.SelectedItem as BO.Task;
            new TaskWindow(taskInList!.Id).ShowDialog();
        }

        /// Event handler for task selector combo box selection change
        private void cbTaskSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Update TaskList based on the selected status filter
            TaskList = (status == BO.Status.All) ?
            s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(item => item.Status == status)!;
        }

        /// Event handler for "Add Task" button click
        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            // Open TaskWindow dialog for adding a new task
            new TaskWindow().ShowDialog();
        }

        /// Event handler for window activity
        private void Window_activity(object sender, EventArgs e)
        {
            // Update TaskList to contain all tasks from the business logic layer
            TaskList = s_bl.Task.ReadAll()!;
        }

    }
}
