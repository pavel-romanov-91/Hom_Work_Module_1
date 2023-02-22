using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;
using System.Configuration;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace Hom_Work_Module_1
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;
        private SqlDataAdapter dataAdapter = null;
        private DataSet dataSet = null;
        private DataTable table = null;
        private SqlConnection connection = new SqlConnection();
        private SqlConnection connectionAsync = new SqlConnection();


        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection= new SqlConnection(ConfigurationManager.ConnectionStrings["MSSQL"].ConnectionString);
            //connectionAsync.ConnectionString = ConfigurationManager.ConnectionStrings["MSSQL"].ConnectionString;
            //connectionAsync.ConnectionString += ";Asynchronous Processing=true";
            //await connectionAsync.OpenAsync();
            //sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\Hom_Work_Module_1\Hom_Work_Module_1\Database1.mdf;Integrated Security=True");
            //sqlConnection.Open();
            //dataAdapter = new SqlDataAdapter("SELECT * FROM[Products]", sqlConnection);
            //dataSet = new DataSet();
            //dataAdapter.Fill(dataSet);
            //table = dataSet.Tables["Products"];

            //string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\Hom_Work_Module_1\Hom_Work_Module_1\Database1.mdf;Integrated Security=True";
            //sqlConnection = new SqlConnection(connectionString);
            //await sqlConnection.OpenAsync();
            //SqlDataReader? sqlReader = null;
            // SqlCommand command = new SqlCommand("SELECT * FROM[Products]", sqlConnection);
            //try
            //{
            //    sqlReader = await command.ExecuteReaderAsync();
            //    while (await sqlReader.ReadAsync())
            //    {
            //        listBox1.Items.Add(Convert.ToString(sqlReader["Id"]) + " " + Convert.ToString(sqlReader["Name"]) + " " + Convert.ToString(sqlReader["Price"]));
            //    }
            //}
            //catch (Exception ex) 
            //{
            //    MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);

            //}
            //finally
            //{ 
            //    if (sqlReader != null) sqlReader.Close(); 
            //}
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State!= ConnectionState.Closed) sqlConnection.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (label7.Visible) label7.Visible = false;

            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) &&
                !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO [Products] (Name, Price) VALUES(@Name, @Price)", sqlConnection);
                command.Parameters.AddWithValue("Name", textBox1.Text);
                command.Parameters.AddWithValue("Price", textBox2.Text);
                await command.ExecuteNonQueryAsync();
            }
            else
            {
                label7.Visible = true;
                label7.Text = "Поля 'Имя' и 'Цена' должны быть заполнен!";
            }
        }

        private async void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            SqlDataReader? sqlReder = null;
            SqlCommand comand = new SqlCommand("SELECT * FROM [Products]", sqlConnection);
            try
            {
                sqlReder = await comand.ExecuteReaderAsync();
                while (await sqlReder.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReder["Id"]) + " " + Convert.ToString(sqlReder["Name"]) + " " + Convert.ToString(sqlReder["Price"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                if (sqlReder != null) sqlReder.Close();
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (label8.Visible) label8.Visible = false;

            if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) &&
                !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) &&
                !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text))
            {
                SqlCommand command = new SqlCommand("UPDATE [Products] SET [Name]=@Name, [Price]=@Price WHERE [Id]=@Id", sqlConnection);
                command.Parameters.AddWithValue("Id", textBox5.Text);
                command.Parameters.AddWithValue("Name", textBox3.Text);
                command.Parameters.AddWithValue("Price", textBox4.Text);
                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text))
            {
                label8.Visible = true;
                label8.Text = "Id должны быть заполнен!";
            }
            else
            {
                label8.Visible = true;
                label8.Text = "Поля 'Id', 'Имя' и 'Цена' должны быть заполнен!";
            }

        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (label6.Visible) label6.Visible = false;

            if (!string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
            {
                SqlCommand command = new SqlCommand("DELETE FROM [Products] WHERE [Id]=@Id", sqlConnection);
                command.Parameters.AddWithValue("Id", textBox6.Text);
                await command.ExecuteNonQueryAsync();
            }
            else
            {
                label9.Visible = true;
                label9.Text = "Id должны быть заполнен!";
            }
               
        }

        private async void подключитсяКБДToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            await sqlConnection.OpenAsync();
            
            dataAdapter = new SqlDataAdapter("SELECT * FROM[Products]", sqlConnection);
            dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            table = dataSet.Tables["Products"];
            SqlDataReader? sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM[Products]", sqlConnection);
            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["Id"]) + " " + Convert.ToString(sqlReader["Name"]) + " " + Convert.ToString(sqlReader["Price"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                if (sqlReader != null) sqlReader.Close();
            }




        }

    }
}