using BUS_ScorpionAccess;
using DTO_ScorpionAccess;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
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

namespace ScorpiconAccess
{
    /// <summary>
    /// Interaction logic for ExportFileAttendance.xaml
    /// </summary>
    public partial class ExportFileAttendance : System.Windows.Window
    {
        BUS_General bUS_General;
        BackgroundWorker worker;
        DateTime selectedMonth;
        string location;

        System.Data.DataTable attendanceTable;
        int deptId;
        public DTO_Department selectedDepartment;
        public ExportFileAttendance(int deptId)
        {
            InitializeComponent();
            this.deptId = deptId;
        }

        private void btChooseLocation_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel 2013|*.xlsx|Excel 2003|*.xls";
            saveFileDialog1.Title = "Ta";
            saveFileDialog1.ShowDialog();

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string folderName = dialog.SelectedPath;
                tbFileLocation.Text = folderName;
            }
        }

        private void Page3_Loaded(object sender, RoutedEventArgs e)
        {
            tbFileLocation.Text = string.Format("D:\\Attendance_{0}{1}.xlsx", selectedMonth.Month, selectedMonth.Year);
        }

        private void Page1_Loaded(object sender, RoutedEventArgs e)
        {
            bUS_General = new BUS_General();

            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            selectedDepartment = Repository.lstAllDepartment.FirstOrDefault(d => d.Id == deptId.ToString());
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            
            attendanceTable = bUS_General.GetAttendance(selectedMonth,deptId);

            string fileTitle = string.Format("BẢNG CHẤM CÔNG THÁNG {0}/{1}", selectedMonth.Month, selectedMonth.Year);
            string fileSubtitle = string.Format("Bộ phận: {0}", selectedDepartment.Name);

            ExcelUtlity obj = new ExcelUtlity();
            obj.WriteDataTableToExcel(attendanceTable, selectedDepartment.Name, location, fileTitle, fileSubtitle);
        }

        private void LastPage_Loaded(object sender, RoutedEventArgs e)
        {
                        
        }

        private void Wizard_Finish(object sender, Xceed.Wpf.Toolkit.Core.CancelRoutedEventArgs e)
        {
            worker.RunWorkerAsync();
        }

        private void stackMainTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btClose_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dpMonth_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedMonth = (DateTime)dpMonth.SelectedDate;
        }

        private void tbFileLocation_TextChanged(object sender, TextChangedEventArgs e)
        {
            location = tbFileLocation.Text;
        }
    }
}
