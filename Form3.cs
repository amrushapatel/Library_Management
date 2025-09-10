using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace librarymanagement
{
    public partial class Form3 : Form
    {
        SqlConnection conn;

        public Form3()
        {
            InitializeComponent();
            conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDBFilename=C:\Users\Raj Parmar\source\repos\Shabdaa\librarymanagement\Database1.mdf; 
            Integrated Security=True");
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            String query = "SELECT * FROM [NewBook]";
            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) 
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                textBox1.Text = selectedRow.Cells["bName"].Value.ToString();
                textBox2.Text = selectedRow.Cells["bAuthor"].Value.ToString();  
                textBox3.Text = selectedRow.Cells["bPubl"].Value.ToString();  
                dateTimePicker1.Value = DateTime.Parse(selectedRow.Cells["bPDate"].Value.ToString()); 
                textBox4.Text = selectedRow.Cells["bPrice"].Value.ToString();
                textBox5.Text = selectedRow.Cells["bQuan"].Value.ToString();  
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            String bid = textBox1.Text;  
            if (!string.IsNullOrEmpty(bid))
            {
                String query = "DELETE FROM NewBook WHERE bName = @bName"; 
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@bName", bid);

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
            }
            else
            {
                MessageBox.Show("Please enter a valid record to delete.");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                String bid = selectedRow.Cells["bid"].Value.ToString();  
                String name = textBox1.Text;
                String author = textBox2.Text;
                String publisher = textBox3.Text;
                String publishedDate = dateTimePicker1.Value.ToString("yyyy-MM-dd"); 
                String price = textBox4.Text;
                String quantity = textBox5.Text;
                String query = "UPDATE NewBook SET bName = @name, bAuthor = @author, bPubl = @publisher, bPDate = @publishedDate, bPrice = @price, bQuan = @quantity WHERE bid = @bid";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@author", author);
                cmd.Parameters.AddWithValue("@publisher", publisher);
                cmd.Parameters.AddWithValue("@publishedDate", publishedDate);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@bid", bid);  
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();  
                conn.Close();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Data updated successfully.");
                    String refreshQuery = "SELECT * FROM [NewBook]";
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
                    MessageBox.Show("No data was updated. Please check your inputs.","Error",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a record to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
