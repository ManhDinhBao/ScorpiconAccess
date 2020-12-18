using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ScorpiconAccess.View
{
    /// <summary>
    /// Interaction logic for ChangeCardView.xaml
    /// </summary>
    public partial class ChangeCardView : Window
    {
        public ChangeCardView()
        {
            InitializeComponent();
        }

        private void btMinnimize_Click(object sender, RoutedEventArgs e)
        {

        }

        private void stackMainTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void btMaximize_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
