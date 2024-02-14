using BO;
using PL.Engineer;
using System;
using System.Collections.Generic;
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
        public TaskWindow(int id = 0)
        {
            if (id != 0)
            {
                try
                {
                    Task = s_bl!.Task.Read(id)!;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{ex}");
                }
            }
            else
            {
             
                Task = new BO.Task()
                {

                    Id = 0,
                    Description = "",
                    Alias = "",
                    Status= null,
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
                    Level = null
                };
            }
            InitializeComponent();
        }
      


        private void addDependency_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)!.Content.ToString() == "Add")
            {
                try
                {
                    s_bl.Task.Create(Task);
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
                    s_bl.Task.Update(Task);
                    MessageBox.Show("updation successful", "Confirmation", MessageBoxButton.OK);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex}", "Confirmation", MessageBoxButton.OK);

                }
            }

        }
        public BO.Task Task
        {
            get { return (BO.Task)GetValue(TaskProperty); }
            set { SetValue(TaskProperty, value); }
        }

        public static readonly DependencyProperty TaskProperty =
            DependencyProperty.Register("Task", typeof(BO.Task), typeof(TaskWindow), new PropertyMetadata(null));

        public List<BO.TaskInList>? DependencyList
        {
            get { return (List<BO.TaskInList>)GetValue(DependenceProperty); }
            set { SetValue(DependenceProperty, value); }
        }

        public static readonly DependencyProperty DependenceProperty =
                  DependencyProperty.Register("DependencyList", typeof(List<string>), typeof(TaskWindow), new PropertyMetadata(null));


    }
}
