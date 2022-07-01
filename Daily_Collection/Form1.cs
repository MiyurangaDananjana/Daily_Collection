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

namespace Daily_Collection
{
    public partial class Form1 : Form
    {
        //SqlConnection con = new SqlConnection(@"Data Data Source=192.168.1.2;Initial Catalog=Banker;User ID=sa;Password=abc");//database connection
        SqlConnection con = new SqlConnection(@"Data Source=192.168.1.2;Network Library=DBMSSOCN;Initial Catalog=Banker;User ID=sa;Password=abc");
        public Form1()
        {
            InitializeComponent();
            


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lodtable();

        }
        private void lodtable()
        {
            con.Open();
            SqlCommand command = new SqlCommand("SELECT SUM(RI_VALUE) FROM QDAILY_RECEIPT WHERE  MCODE ='1556' AND RE_DATE BETWEEN '" + dateTimePicker1.Text+"' AND '"+dateTimePicker2.Text+"'", con);
            SqlDataReader dr = command.ExecuteReader();
            SqlDataAdapter da = new SqlDataAdapter(command);
            if (dr.Read())
            {
                textBox1.Text = dr[0].ToString();
            }
            con.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime now = DateTime.Now;

                con.Open();


                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT RE_DATE,RD_VALUE,CM_DESC,RI_VALUE,RI_TYPE,IE_CODE FROM QDAILY_RECEIPT WHERE RE_DATE BETWEEN '" + dateTimePicker1.Text + "' AND '" + dateTimePicker2.Text + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                con.Close();
                lodtable();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
