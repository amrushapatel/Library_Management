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

namespace librarymanagement
{
    public partial class Form5 : Form
    {
        SqlConnection conn;
        public Form5()
        {
            InitializeComponent();
            conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDBFilename=C:\Users\Raj Parmar\source\repos\Shabdaa\librarymanagement\Database1.mdf; 
            Integrated Security=True");
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                textBox1.Text = selectedRow.Cells["sname"].Value.ToString();
                textBox2.Text = selectedRow.Cells["rollno"].Value.ToString();
                textBox3.Text = selectedRow.Cells["dep"].Value.ToString();
                textBox4.Text = selectedRow.Cells["sem"].Value.ToString();
                textBox5.Text = selectedRow.Cells["con"].Value.ToString();
                textBox6.Text = selectedRow.Cells["email"].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String query = "SELECT * FROM [Newstud]";
            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                String id = selectedRow.Cells["studid"].Value.ToString();
                String name = textBox1.Text;
                String rn = textBox2.Text;
                String dp = textBox3.Text;
                String sm = textBox4.Text;
                String ct = textBox5.Text;
                String em = textBox6.Text;
                String query = "UPDATE Newstud SET sname = @name, rollno = @rn, dep = @dp, sem = @sm, con = @ct, email = @em WHERE studid = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@rn", rn);
                cmd.Parameters.AddWithValue("@dp", dp);
                cmd.Parameters.AddWithValue("@sm", sm);
                cmd.Parameters.AddWithValue("@ct", ct);
                cmd.Parameters.AddWithValue("@em", em);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Data updated successfully.");
                    String refreshQuery = "SELECT * FROM [Newstud]";
                    SqlDataAdapter sda = new SqlDataAdapter(refreshQuery, conn);
                    DataTable dt = new DataTable();
                    conn.Open();
                    sda.Fill(dt);
                    conn.Close();
                    dataGridView1.DataSource = dt;
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                }
                else
                {
                    MessageBox.Show("No data was updated. Please check your inputs.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a record to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String studid = textBox1.Text;
            if (!string.IsNullOrEmpty(studid))
            {
                String query = "DELETE FROM Newstud WHERE sname = @sname";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@sname", studid);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                button1_Click(sender, e);
                MessageBox.Show("Record deleted successfully.");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
            }
            else
            {
               MessageBox.Show("Please enter a valid record to delete.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will DELETE your unsaved data.", "Are you sure ?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }
        }
    }
}
