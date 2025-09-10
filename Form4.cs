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
    public partial class Form4 : Form
    {
        SqlConnection conn;
        public Form4()
        {
            InitializeComponent();
            conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDBFilename=C:\Users\Raj Parmar\source\repos\Shabdaa\librarymanagement\Database1.mdf; 
            Integrated Security=True");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "")
            {
                String name = textBox1.Text;
                String rn = textBox2.Text;
                String d = textBox3.Text;
                String sm = textBox4.Text;
                String ct = textBox5.Text;
                String em = textBox6.Text;

                String query = "INSERT INTO [Newstud] (sname, rollno, dep, sem, con, email) VALUES ('" + name + "', '" + rn + "','" + d + "', '" + sm + "', '" + ct + "', '" + em + "')";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@name", textBox1.Text);
                cmd.Parameters.AddWithValue("@rn", textBox2.Text);
                cmd.Parameters.AddWithValue("@d", textBox3.Text);
                cmd.Parameters.AddWithValue("@sm", textBox4.Text);
                cmd.Parameters.AddWithValue("@ct", textBox5.Text);
                cmd.Parameters.AddWithValue("@em", textBox6.Text);


                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Data Saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
            }
            else
            {
                MessageBox.Show("Empty Filed Not Allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will DELETE your unsaved data.", "Are you sure ?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }
        }
    }
}
