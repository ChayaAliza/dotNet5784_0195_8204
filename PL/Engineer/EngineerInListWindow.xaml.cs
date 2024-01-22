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
    /// Interaction logic for EngineerInListWindow.xaml
    /// </summary>
    public partial class EngineerInListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public EngineerInListWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          
        }
        public IEnumerable<BO.Engineer> EngineersList
        {
            get { return (IEnumerable<BO.Engineer>)GetValue(CourseListProperty); }
            set { SetValue(CourseListProperty, value); }
        }

        public static readonly DependencyProperty CourseListProperty =
            DependencyProperty.Register("CourseList", typeof(IEnumerable<BO.Engineer>), typeof(EngineerWindow), new PropertyMetadata(null));

    }
}
