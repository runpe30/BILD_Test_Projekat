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

namespace DevicesApp
{
    public partial class Form1 : Form
    {

        String connection = @"Server=DESKTOP-1HFCEM8\SQLExpress2; Database=DevicesDB; Integrated Security=true";
        SqlConnection Conn;

        public Form1()
        {
            InitializeComponent();
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {

            try
            {
                Conn = new SqlConnection();
                Conn.ConnectionString = connection;
                Conn.Open();
                //MessageBox.Show("Database is ready.");
            }

            catch (Exception err)
            {
                MessageBox.Show("Error!");
            }

            Ucitaj();
            
            

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            //String upit = "SELECT ([Name], [Parent]) FROM DeviceTypes";
            //SqlCommand komanda = new SqlCommand();
            //try
            //{
            //    komanda.CommandText = upit;
            //    komanda.Connection = Conn;
            //    SqlDataAdapter adapter = new SqlDataAdapter();
            //    adapter.SelectCommand = komanda;
                
                
            //}
            //catch (Exception err)
            //{
            //    MessageBox.Show("neeee");
            //}
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int idParent = Int32.Parse(textBox1.Text);
            String ime = textBox2.Text;
            String upit = "INSERT INTO DeviceTypes VALUES(@imeParam, @idParentParam)";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = Conn;
                komanda.Parameters.Add("idParentParam", SqlDbType.Int);
                komanda.Parameters.Add("imeParam", SqlDbType.NVarChar);
                komanda.Parameters["idParentParam"].Value = idParent;
                komanda.Parameters["imeParam"].Value = ime;
                komanda.ExecuteNonQuery();
                MessageBox.Show("uspjesno upisano");
            }
            catch (Exception err)
            {
                MessageBox.Show("Error " + err);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            int idP = Int32.Parse(textBox3.Text);
            DataTable table = new DataTable();
            String upit = "SELECT id, Name, parentID FROM DeviceTypes where ID=@Param";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = Conn;
                komanda.Parameters.Add("Param", SqlDbType.Int);
                komanda.Parameters["Param"].Value = idP;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = komanda;
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }
            catch (Exception err)
            {
                MessageBox.Show("Error!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String upit = "DELETE FROM DeviceTypes WHERE ID=@idParam";
            if (dataGridView1.SelectedRows.Count < 1)
            {
                MessageBox.Show("izaberite red");
                return;
            }
            int id = Int32.Parse(dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString());
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = Conn;
                komanda.Parameters.Add("idParam", SqlDbType.Int);
                komanda.Parameters["idParam"].Value = id;
                komanda.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                MessageBox.Show("Error!");
            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

            
            //String upit = "SELECT * FROM DeviceTypes";
            //SqlCommand komanda = new SqlCommand();
            //try
            //{
            //    komanda.CommandText = upit;
            //    komanda.Connection = Conn;
            //    SqlDataReader reader = komanda.ExecuteReader();
            //    while (reader.Read())
            //    {
            //        comboBox1.Items.Add(reader["Name"].ToString());
            //    }
            //    reader.Close();


            //}
            //catch (Exception err)
            //{
            //    MessageBox.Show("neeee");
            //}
        }

        public void Ucitaj()
        {
            String upit = "SELECT * FROM DeviceTypes";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = Conn;
                SqlDataReader reader = komanda.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["Name"].ToString());
                }
                reader.Close();


            }
            catch (Exception err)
            {
                MessageBox.Show("neeee");
            }
        }

        public void SelectID ()
        {
            String str;
            String upit = "SELECT * FROM DeviceTypes WHERE Name=str";
            SqlCommand komanda = new SqlCommand();
            try
            {
                str = comboBox1.SelectedItem.ToString();
                komanda.CommandText = upit;
                komanda.Connection = Conn;

                SqlDataReader reader = komanda.ExecuteReader();
                while (reader.Read())
                {
                    textBox1.Text = (reader["ID"].ToString());
                }
                reader.Close();

            }
            catch (Exception err)
            {
                MessageBox.Show("Error!");
            }
        }

    }
}
