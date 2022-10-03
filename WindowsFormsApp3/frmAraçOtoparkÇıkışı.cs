using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class frmAraçOtoparkÇıkışı : Form
    {
        public frmAraçOtoparkÇıkışı()
        {
            InitializeComponent();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {
            //Hatalı Açıldı
        }

        SqlConnection bag = new SqlConnection(@"Data Source=.\SQLExpress;initial catalog=arac_otopark;integrated security=true");

        private void frmAraçOtoparkÇıkışı_Load(object sender, EventArgs e)
        {
            DoluYerler();
            Plakalar();
            timer1.Enabled = true;
        }

        private void Plakalar()
        {
            bag.Open();
            SqlCommand komut = new SqlCommand("select * from arac_otopark_kaydı", bag);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboPlaka.Items.Add(read["plaka"].ToString());
            }
            bag.Close();
        }

        private void DoluYerler()
        {
            bag.Open();
            SqlCommand komut = new SqlCommand("select * from aracdurumu where durum = 'DOLU'", bag);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboParkYeri.Items.Add(read["parkyeri"].ToString());
            }
            bag.Close();
        }

        private void comboPlaka_SelectedIndexChanged(object sender, EventArgs e)
        {
            bag.Open();
            SqlCommand komut = new SqlCommand("select * from arac_otopark_kaydı where plaka = '" + comboPlaka.SelectedItem + "'", bag);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtParkYeri.Text = read["parkyeri"].ToString();
            }
            bag.Close();
        }

        private void comboParkYeri_SelectedIndexChanged(object sender, EventArgs e)
        {
            bag.Open();
            SqlCommand komut = new SqlCommand("select * from arac_otopark_kaydı where parkyeri = '" + comboParkYeri.SelectedItem + "'", bag);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtPlaka.Text = read["plaka"].ToString();
                txtTc.Text = read["tc"].ToString();
                txtAd.Text = read["ad"].ToString();
                txtSoyad.Text = read["soyad"].ToString();
                txtMarka.Text = read["marka"].ToString();
                txtSeri.Text = read["seri"].ToString();
                txtRenk.Text = read["renk"].ToString();
                label15.Text = read["tarih"].ToString();
            }
            bag.Close();
            DateTime geliş, çıkış;
            geliş = DateTime.Parse(label15.Text);
            çıkış = DateTime.Parse(label16.Text);
            TimeSpan fark;
            fark = çıkış - geliş;
            label17.Text = fark.TotalHours.ToString("0.00");
            label18.Text = (double.Parse(label17.Text) * (1.25)).ToString("0.00");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label16.Text = DateTime.Now.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bag.Open();
            SqlCommand komut = new SqlCommand("delete from arac_otopark_kaydı where  plaka = '" + txtPlaka.Text + "'", bag);
            komut.ExecuteNonQuery();
            SqlCommand komut2 = new SqlCommand("update aracdurumu set durum = 'BOŞ' where  parkyeri = '" + txtParkYeri.Text + "'", bag);
            komut2.ExecuteNonQuery();
            SqlCommand komut3 = new SqlCommand("insert into satis(parkyeri,plaka,geliş_tarihi,çıkış_tarihi,süre,tutar) values(@parkyeri,@plaka,@geliş_tarihi,@çıkış_tarihi,@süre,@tutar)", bag);
            komut3.Parameters.AddWithValue("@parkyeri", txtParkYeri.Text);
            komut3.Parameters.AddWithValue("@plaka", txtPlaka.Text);
            komut3.Parameters.AddWithValue("@geliş_tarihi", label15.Text);
            komut3.Parameters.AddWithValue("@çıkış_tarihi", label16.Text);
            komut3.Parameters.AddWithValue("@süre", double.Parse(label17.Text));
            komut3.Parameters.AddWithValue("@tutar", double.Parse(label18.Text));
            komut3.ExecuteNonQuery();
            bag.Close();
            MessageBox.Show("Araç Çıkışı Yapıldı!");
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                    txtParkYeri.Text = "";
                    comboParkYeri.Text = "";
                    comboPlaka.Text = "";
                }
            }
            comboPlaka.Items.Clear();
            comboParkYeri.Items.Clear();
            DoluYerler();
            Plakalar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 frm = new Form1();
            frm.Show();
        }
    }
}
