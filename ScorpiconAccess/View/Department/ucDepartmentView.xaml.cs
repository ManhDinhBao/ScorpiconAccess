using DTO_ScorpionAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ucDepartmentView.xaml
    /// </summary>
    public partial class ucDepartmentView : UserControl
    {
        #region Constructor
        public DTO_Department Department { get; set; }
        public string DeptName { get; set; }
        public ucDepartmentView()
        {
            InitializeComponent();
            DataContext = this;
        }
        #endregion

        #region Custom Event
        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when department element updated")]
        public event EventHandler UpdatedDepartment;
        #endregion

        #region Control click event
        private void btChildInfo_Click(object sender, RoutedEventArgs e)
        {
            wdDepartmentChildView view = new wdDepartmentChildView();
            view.MyDepartment = new List<DTO_Department>();
            view.MyDepartment.Add(Department);
            if (view.ShowDialog() == true)
            {
                UpdatedDepartment(sender, e);
            }
        }
        private void btInfo_Click(object sender, RoutedEventArgs e)
        {
            wdDepartmentDetail view = new wdDepartmentDetail(EType.WindowMode.EDIT_MODE,Department);
            if(view.ShowDialog()==true)
            {
                UpdatedDepartment(sender, e);
            }
        }        
        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DeptName = Department.Name;
            BindingExpression binding = tbDeptName.GetBindingExpression(TextBlock.TextProperty);
            binding.UpdateTarget();
        }      
    }
}
