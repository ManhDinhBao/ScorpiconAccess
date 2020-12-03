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
    /// Interaction logic for DoorDetailView.xaml
    /// </summary>
    public partial class DoorDetailView : UserControl
    {
        public DoorDetailView()
        {
            InitializeComponent();
        }

        private void itemAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void itemDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void itemProperties_Click(object sender, RoutedEventArgs e)
        {
            DoorSocketDetailView view = new DoorSocketDetailView();
            view.ShowDialog();
        }
    }
}
