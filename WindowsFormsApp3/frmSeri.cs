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

namespace WindowsFormsApp3
{
    public partial class frmSeri : Form
    {
        public frmSeri()
        {
            InitializeComponent();
        }
        SqlConnection bag = new SqlConnection(@"Data Source=.\SQLExpress;initial catalog=arac_otopark;integrated security=true");

        private void frmSeri_Load(object sender, EventArgs e)
        {
            marka();
        }

        private void marka()
        {
            bag.Open();
            SqlCommand komut = new SqlCommand("select marka from markabilgileri", bag);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["marka"].ToString());
            }
            bag.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bag.Open();
            SqlCommand komut = new SqlCommand("insert into seribilgileri(marka,seri) values('" + comboBox1.Text + "','" + textBox1.Text + "')", bag);
            komut.ExecuteNonQuery();
            bag.Close();
            MessageBox.Show("Markaya Bağlı Araç Serisi Kaydı Yapıldı", "Kayıt");
            textBox1.Clear();
            comboBox1.Text = "";
            comboBox1.Items.Clear();
            marka();
        }
    }
}
