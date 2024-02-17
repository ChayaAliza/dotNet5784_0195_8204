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

        // Property that represents a list of engineers. It has a getter and setter for accessing and updating the EngineerList data.
        public IEnumerable<BO.Engineer> EngineerList
        {
            get { return (IEnumerable<BO.Engineer>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }

        public static readonly DependencyProperty EngineerListProperty =
            DependencyProperty.Register("EngineerList", typeof(IEnumerable<BO.Engineer>), typeof(EngineerInListWindow), new PropertyMetadata(null));


        // Property to set the default experience level to All
        public BO.EngineerExperience level { get; set; } = BO.EngineerExperience.All;

        // Constructor function that initializes the EngineerInListWindow
        public EngineerInListWindow()
        {
            InitializeComponent();

            // Initializes the EngineerList property by calling the ReadAll method from the business logic layer (s_bl).
            EngineerList = s_bl?.Engineer.ReadAll()!;
        }

        // Event handler for the click event of the "Add Engineer" button.
        private void btnAddEngineer_Click(object sender, RoutedEventArgs e)
        {
            // Opens a new EngineerWindow for adding a new engineer.
            new EngineerWindow().ShowDialog();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        // Event handler for the selection changed event of the engineer selector combo box.
        private void cbEngineerSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Updates the EngineerList based on the selected engineer experience level.
            EngineerList = (level == BO.EngineerExperience.All) ?
            s_bl?.Engineer.ReadAll()! : s_bl?.Engineer.ReadAll(item => item.Level == level)!;
        }

        // Event handler for the double-click event on the ListView items.
        private void update_add(object sender, MouseButtonEventArgs e)
        {
            // Opens the EngineerWindow for editing the selected engineer.
            BO.Engineer? engineerInList = (sender as ListView)?.SelectedItem as BO.Engineer;
            new EngineerWindow(engineerInList!.Id).ShowDialog();
        }

        // Event handler for the window activity event.
        private void Window_activity(object sender, EventArgs e)
        {
            // Updates the EngineerList when the window is active.
            EngineerList = s_bl.Engineer.ReadAll()!;
        }

    }
}
