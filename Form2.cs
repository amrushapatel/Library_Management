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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace librarymanagement
{
    public partial class Form2 : Form
    {
        SqlConnection conn;
        public Form2()
        {
            InitializeComponent();
            conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDBFilename=C:\Users\Raj Parmar\source\repos\Shabdaa\librarymanagement\Database1.mdf; 
            Integrated Security=True");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "")
            {
                String name = textBox1.Text;
                String bauthor = textBox2.Text;
                String publ = textBox3.Text;
                String pDate = dateTimePicker1.Text;
                String price = textBox4.Text;
                String quan = textBox5.Text;

                String query = "INSERT INTO [Newbook] (bName, bAuthor, bPubl, bPDate, bPrice, bQuan) VALUES ('" + name + "', '" + bauthor + "','" + publ + "', '" + pDate + "', '" + price + "', '" + quan + "')";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@name", textBox1.Text);
                cmd.Parameters.AddWithValue("@bauthor", textBox2.Text);
                cmd.Parameters.AddWithValue("@publ", textBox3.Text);
                cmd.Parameters.AddWithValue("@pDate", dateTimePicker1.Text);
                cmd.Parameters.AddWithValue("@price", textBox4.Text);
                cmd.Parameters.AddWithValue("@quan", textBox4.Text);


                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Data Saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
            }
            else
            {
                MessageBox.Show("Empty Filed Not Allowed","Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will DELETE your unsaved data.", "Are you sure ?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK) 
            {
                this.Close();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
