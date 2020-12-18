using DTO_ScorpionAccess;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScorpiconAccess.View
{
    /// <summary>
    /// Interaction logic for ucPeriodView.xaml
    /// </summary>
    public partial class ucPeriodView : UserControl
    {
        public string PeriodName { get; set; }
        public DTO_Period MyPeriod { get; set; }
        public string  Detail { get; set; }
        public ucPeriodView()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            

        }

        private void btInfo_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
