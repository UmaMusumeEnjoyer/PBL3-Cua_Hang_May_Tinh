using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using CuaHangMayTinh.DAL;
using CuaHangMayTinh.UI.Forms;
namespace CuaHangMayTinh.UI.UserControls_Ad
{
    public partial class EmployeeManager: UserControl
    {
        public EmployeeManager()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            string SQL = "SELECt * From Account join Employee on Account.Employee_Id = Employee.Employee_Id and Account.role != 'admin'";
            DataTable dt = new DataTable();
            DbConnect DAL = new DbConnect();
            dt = DAL.GetData(SQL);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = dt;

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void EmployeeManager_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //this.Hide();
            EmployeeRegistrationForm ad = new EmployeeRegistrationForm();
            ad.ShowDialog();
            //this.Show();
        }
    }
}
