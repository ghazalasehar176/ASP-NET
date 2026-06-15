using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace class14
{
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
        }
        public void label2_Click() { 
        
        }

        public void ClearForm()
        {

            txtn.Clear();
            txte.Clear();
            txta.Clear();

        }
        //private void btn_save_click_Click(object sender, EventArgs e)
        //{
           
        //}

        public void LoadUser() {
            DbConnection con = new DbConnection();
            SqlConnection conn = new SqlConnection(con.GetConnection());

            string query = "SELECT * FROM Users";
            SqlDataAdapter dt = new SqlDataAdapter(query, conn);

            DataTable table = new DataTable();

            dt.Fill(table);

            dataGridView3.DataSource = table;
        }

        //private void UserForm_Load(object sender, EventArgs e)
        //{
        //    LoadUser();
        //}

        private void UserForm_Load_1(object sender, EventArgs e)
        {
            LoadUser();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            DbConnection con = new DbConnection();
            SqlConnection conn = new SqlConnection(con.GetConnection());

            string query = "INSERT INTO Users(name , email , age ) Values(@name , @email , @age)";
            SqlCommand queryRun = new SqlCommand(query, conn);

            conn.Open();

            queryRun.Parameters.AddWithValue("@name", txtn.Text);
            queryRun.Parameters.AddWithValue("@email", txte.Text);
            queryRun.Parameters.AddWithValue("@age", txta.Text);


            queryRun.ExecuteNonQuery();

            ClearForm();

            MessageBox.Show("User Saved Successfully ");
            LoadUser();
        }
    }
}
