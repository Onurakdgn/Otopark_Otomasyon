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
    public partial class frmAraçOtoparkKaydı : Form
    {
        public frmAraçOtoparkKaydı()
        {
            InitializeComponent();
        }
        SqlConnection bag = new SqlConnection(@"Data Source=.\SQLExpress;initial catalog=arac_otopark;integrated security=true");
        private void frmAraçOtoparkKaydı_Load(object sender, EventArgs e)
        {
            BoşAraçlar();
            Marka();
        }

        private void Marka()
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

        private void BoşAraçlar()
        {
            bag.Open();
            SqlCommand komut = new SqlCommand("select * from aracdurumu where durum = 'BOŞ'", bag);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox3.Items.Add(dr["parkyeri"].ToString());
            }
            bag.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 frm = new Form1();
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bag.Open();
            SqlCommand komut = new SqlCommand("insert into arac_otopark_kaydı(tc,ad,soyad,telefon,email,plaka,marka,seri,renk,parkyeri,tarih) values(@tc,@ad,@soyad,@telefon,@email,@plaka,@marka,@seri,@renk,@parkyeri,@tarih)", bag);
            komut.Parameters.AddWithValue("@tc", textBox1.Text);
            komut.Parameters.AddWithValue("@ad", textBox2.Text);
            komut.Parameters.AddWithValue("@soyad", textBox3.Text);
            komut.Parameters.AddWithValue("@telefon", textBox4.Text);
            komut.Parameters.AddWithValue("@email", textBox5.Text);
            komut.Parameters.AddWithValue("@plaka", textBox6.Text);
            komut.Parameters.AddWithValue("@marka", comboBox1.Text);
            komut.Parameters.AddWithValue("@seri", comboBox2.Text);
            komut.Parameters.AddWithValue("@renk", textBox7.Text);
            komut.Parameters.AddWithValue("@parkyeri", comboBox3.Text);
            komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
            komut.ExecuteNonQuery();

            SqlCommand komut2 = new SqlCommand("update aracdurumu set durum='DOLU' where parkyeri= '" + comboBox3.SelectedItem + "'", bag);
            komut2.ExecuteNonQuery();
            
            bag.Close();
            MessageBox.Show("Araç Kaydı Oluşturuldu", "Kayıt");
            comboBox3.Items.Clear();
            BoşAraçlar();
            comboBox1.Items.Clear();
            Marka();
            comboBox2.Items.Clear();
            foreach (Control item in groupKişi.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            foreach (Control item in groupAraç.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                    
                }
            }
            foreach (Control item in groupAraç.Controls)
            {
                if (item is ComboBox)
                {
                    item.Text = "";

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmMarka frmm = new frmMarka();
            frmm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmSeri frms = new frmSeri();
            frms.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            bag.Open();
            SqlCommand komut = new SqlCommand("select marka,seri from seribilgileri where marka='"+comboBox1.SelectedItem+"'", bag);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr["seri"].ToString());
            }
            bag.Close();

        }
    }
}
