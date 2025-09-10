using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace librarymanagement
{
    public partial class Form8 : Form
    {
        SqlConnection conn;
        public Form8()
        {
            InitializeComponent();
            conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDBFilename=C:\Users\Raj Parmar\source\repos\Shabdaa\librarymanagement\Database1.mdf; 
            Integrated Security=True");

            conn.Open();
            string query = "SELECT * FROM IssuedBooks";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;

            string query1  = "SELECT * FROM ReturnBooks";
            SqlDataAdapter sda = new SqlDataAdapter(query1, conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView2.DataSource = dt;
            
        }
        private void dataGridView1_CellContentClick(object sender, EventArgs e)
        {

        }
    }
}
