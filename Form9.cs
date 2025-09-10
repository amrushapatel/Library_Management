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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string uname, pass;
            uname = textBox1.Text;
            pass = textBox2.Text;
            
            if (string.IsNullOrWhiteSpace(uname))
            {
                MessageBox.Show("Please input Username properly.");
                return;
            }
            if (string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Enter proper password");
                return;
            }
            if (pass.Length > 14 || pass.Length < 8)
            {
                MessageBox.Show("Password cannot be more than 14 character or less than 8 character");
                return;
            }
            if (!pass.Any(char.IsUpper))
            {
                MessageBox.Show("Must contain atleast one Uppercase in Password");
                return;
            }
            if (!pass.Any(char.IsLower))
            {
                MessageBox.Show("Atleast one character should be Lowercase");
                return;
            }
            if (uname.Contains("@"))
            {
                MessageBox.Show("Username and Password is valid.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form1 frm = new Form1();
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Username must contain '@'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }
        }
    }
}
