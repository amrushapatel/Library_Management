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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace librarymanagement
{
    public partial class Form7 : Form
    {
        SqlConnection conn;
        public Form7()
        {
            InitializeComponent();
            conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDBFilename=C:\Users\Raj Parmar\source\repos\Shabdaa\librarymanagement\Database1.mdf; 
            Integrated Security=True");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM IssuedBooks WHERE RollNo = '" + textBox1.Text + "' AND BookReturnDate IS NULL";
            SqlCommand cmd = new SqlCommand(query, conn);
            /*cmd.Parameters.AddWithValue("@uname", textBox1.Text);*/
            DataTable dt = new DataTable();
            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            if (dt.Rows.Count != 0)
            {
                dataGridView1.DataSource = dt;

            }
            else
            {
                MessageBox.Show("Invalid ID or Not Booked Issued ","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String rollno = textBox1.Text;
            String bookname = comboBox1.Text;
            DateTime returndate = dateTimePicker2.Value;
            conn.Open();

            String selectQuery = "SELECT StudID, SName, Dep, Sem, Con, Email, BookIssueDate FROM IssuedBooks WHERE RollNo = @rollno AND BookName = @bookname";
            SqlCommand selectCmd = new SqlCommand(selectQuery, conn);
            selectCmd.Parameters.AddWithValue("@rollno", rollno);
            selectCmd.Parameters.AddWithValue("@bookname", bookname);

            SqlDataReader reader = selectCmd.ExecuteReader();

            if (reader.Read())
            {
                String studId = reader["StudID"].ToString();
                String studentName = reader["SName"].ToString();
                String department = reader["Dep"].ToString();
                String sem = reader["Sem"].ToString();
                String contact = reader["Con"].ToString();
                String email = reader["Email"].ToString();
                DateTime issuedate = Convert.ToDateTime(reader["BookIssueDate"]);

                reader.Close();

                String insertQuery = "INSERT INTO ReturnBooks (RollNo, BookName, StudID, SName, Dep, Sem, Con, Email, BookIssueDate, BookReturnDate) " +
                                     "VALUES (@rollno, @bookname, @studid, @studentname, @department, @sem, @contact, @email, @issuedate, @returndate)";
                SqlCommand insertCmd = new SqlCommand(insertQuery, conn);

                insertCmd.Parameters.AddWithValue("@rollno", rollno);
                insertCmd.Parameters.AddWithValue("@bookname", bookname);
                insertCmd.Parameters.AddWithValue("@studid", studId);
                insertCmd.Parameters.AddWithValue("@studentname", studentName);
                insertCmd.Parameters.AddWithValue("@department", department);
                insertCmd.Parameters.AddWithValue("@sem", sem);
                insertCmd.Parameters.AddWithValue("@contact", contact);
                insertCmd.Parameters.AddWithValue("@email", email);
                insertCmd.Parameters.AddWithValue("@issuedate", issuedate);
                insertCmd.Parameters.AddWithValue("@returndate", returndate);

                insertCmd.ExecuteNonQuery();

                String deleteQuery = "DELETE FROM IssuedBooks WHERE RollNo = @rollno AND BookName = @bookname";
                SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn);

                deleteCmd.Parameters.AddWithValue("@rollno", rollno);
                deleteCmd.Parameters.AddWithValue("@bookname", bookname);
                deleteCmd.ExecuteNonQuery();

                MessageBox.Show("Data moved to ReturnBooks and deleted from IssuedBooks.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No record found in IssuedBooks table for the provided RollNo and BookName.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            conn.Close();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            String selectQuery = "SELECT * FROM IssuedBooks";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery, conn);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will DELETE your unsaved data.", "Are you sure ?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }
        }
    }
}
