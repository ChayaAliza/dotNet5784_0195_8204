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

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerWindow.xaml
    /// This class represents the window for managing engineers.
    /// </summary>
    public partial class EngineerWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        /// Gets or sets the Engineer property.
        /// Represents the engineer whose details are being managed.
        public BO.Engineer Engineer
        {
            get { return (BO.Engineer)GetValue(EngineerProperty); }
            set { SetValue(EngineerProperty, value); }
        }

        /// Identifies the Engineer dependency property.
        public static readonly DependencyProperty EngineerProperty =
            DependencyProperty.Register("Engineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));

        /// <summary>
        /// Constructor for EngineerWindow class.
        /// Initializes a new instance of the EngineerWindow class.
        /// </summary>
        /// <param name="Id">The ID of the engineer. Defaults to 0.</param>
        public EngineerWindow(int Id = 0)
        {
            InitializeComponent();
            if (Id != 0)
            {
                try
                {
                    Engineer = s_bl!.Engineer.Read(Id)!;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{ex}");
                }
            }
            else
            {
                Engineer = new BO.Engineer();
            }
        }

        /// Handles the click event of the Save button.
        /// Saves the engineer details if adding a new engineer or updates existing engineer details.
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            if ((sender as Button)!.Content.ToString() == "Add")
            {
                try
                {
                    s_bl.Engineer.Create(Engineer);
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
                    s_bl.Engineer.Update(Engineer);
                    MessageBox.Show("updation successful", "Confirmation", MessageBoxButton.OK);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex}", "Confirmation", MessageBoxButton.OK);

                }
            }
        }

       
    }
}
