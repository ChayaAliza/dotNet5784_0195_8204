using DalTest;
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

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerInListWindow.xaml
    /// </summary>
    public partial class EngineerInListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.EngineerExperience level { get; set; } = BO.EngineerExperience.All;
        public EngineerInListWindow()
        {
            InitializeComponent();
            EngineerList = s_bl?.Engineer.ReadAll()!;
        }
        private void btnAddEngineer_Click(object sender, RoutedEventArgs e)
        {
            new EngineerWindow().ShowDialog();
        }
        public IEnumerable<BO.Engineer> EngineerList
        {
            get { return (IEnumerable<BO.Engineer>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }

        public static readonly DependencyProperty EngineerListProperty =
            DependencyProperty.Register("EngineerList", typeof(IEnumerable<BO.Engineer>), typeof(EngineerInListWindow), new PropertyMetadata(null));


        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbEngineerSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EngineerList = (level == BO.EngineerExperience.All) ?
            s_bl?.Engineer.ReadAll()! : s_bl?.Engineer.ReadAll(item => item.Level == level)!;
        }

        private void update_add(object sender, MouseButtonEventArgs e)
        {
            BO.Engineer? engineerInList = (sender as ListView)?.SelectedItem as BO.Engineer;
            new EngineerWindow(engineerInList!.Id).ShowDialog();
        }
        private void Window_activity(object sender, EventArgs e)
        {
            EngineerList = s_bl.Engineer.ReadAll()!;
        }

    }
}
