using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace librarymanagement
{
    public partial class Form6 : Form
    {
        SqlConnection conn;
        int studID;  

        public Form6()
        {
            InitializeComponent();
            conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDBFilename=C:\Users\Raj Parmar\source\repos\Shabdaa\librarymanagement\Database1.mdf; 
            Integrated Security=True");
        }
        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string rollNo = textBox1.Text;  
            string query = "SELECT * FROM Newstud WHERE rollno = @rollNo";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@rollNo", rollNo);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                studID = Convert.ToInt32(reader["StudID"]);
                textBox2.Text = reader["sname"].ToString();
                textBox3.Text = reader["dep"].ToString();
                textBox4.Text = reader["sem"].ToString();
                textBox5.Text = reader["con"].ToString();
                textBox6.Text = reader["email"].ToString();
            }
            else
            {
                MessageBox.Show("No student found with the provided Roll Number.");
            }

            conn.Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            string rollNo = textBox1.Text;
            string studentName = textBox2.Text;
            string department = textBox3.Text;
            string semester = textBox4.Text;
            string contact = textBox5.Text;
            string email = textBox6.Text;
            string bookName = comboBox1.SelectedItem.ToString();
            DateTime issueDate = dateTimePicker1.Value;
            string checkQuery = "SELECT COUNT(*) FROM IssuedBooks WHERE StudID = @studID";

            SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
            checkCmd.Parameters.AddWithValue("@studID", studID);

            conn.Open();
            int bookCount = (int)checkCmd.ExecuteScalar();
            conn.Close();

            if (bookCount >= 3)
            {
                MessageBox.Show("Maximum 3 books can be issued to 1 student.");
            }
            else
            {
                string issueQuery = "INSERT INTO IssuedBooks (StudID, RollNo, Sname, Dep, Sem, Con, Email, BookName, BookIssueDate) " +
                                    "VALUES (@studID, @rollNo, @studentName, @department, @semester, @contact, @email, @bookName, @issueDate)";

                SqlCommand issueCmd = new SqlCommand(issueQuery, conn);
                issueCmd.Parameters.AddWithValue("@studID", studID);       
                issueCmd.Parameters.AddWithValue("@rollNo", rollNo);        
                issueCmd.Parameters.AddWithValue("@studentName", studentName);  
                issueCmd.Parameters.AddWithValue("@department", department);  
                issueCmd.Parameters.AddWithValue("@semester", semester);        
                issueCmd.Parameters.AddWithValue("@contact", contact);         
                issueCmd.Parameters.AddWithValue("@email", email);            
                issueCmd.Parameters.AddWithValue("@bookName", bookName);       
                issueCmd.Parameters.AddWithValue("@issueDate", issueDate);      

                conn.Open();                
                issueCmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Book issued successfully and student details copied.");
            }
        }


    }
}
