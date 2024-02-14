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
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.Status status { get; set; } = BO.Status.All;
        public IEnumerable<BO.Task> TaskList
        {
            get { return (IEnumerable<BO.Task>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.Task>), typeof(TaskInListWindow), new PropertyMetadata(null));

        public TaskInListWindow()
        {
            InitializeComponent();
            TaskList = s_bl?.Task.ReadAll()!;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void update_add(object sender, MouseButtonEventArgs e)
        {
            BO.Task? taskInList = (sender as ListView)?.SelectedItem as BO.Task;
            new TaskWindow(taskInList!.Id).ShowDialog();

        }

        private void cbTaskSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskList = (status == BO.Status.All) ?
            s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(item => item.Status == status)!;
        }

        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            new TaskWindow().ShowDialog();
        }
        private void Window_activity(object sender, EventArgs e)
        {
            TaskList = s_bl.Task.ReadAll()!;
        }

    }
}
