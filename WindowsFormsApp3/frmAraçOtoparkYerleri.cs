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
    public partial class frmAraçOtoparkYerleri : Form
    {
        public frmAraçOtoparkYerleri()
        {
            InitializeComponent();
        }

        SqlConnection bag = new SqlConnection(@"Data Source=.\SQLExpress;initial catalog=arac_otopark;integrated security=true");

        private void frmAraçOtoparkYerleri_Load(object sender, EventArgs e)
        {
            BoşParkYerleri();
            DoluparkYerleri();
            bag.Open();
            SqlCommand komut = new SqlCommand("select * from arac_otopark_kaydı", bag);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                foreach (Control item in Controls)
                {
                    if (item is Button)
                    {
                        if (item.Text == dr["parkyeri"].ToString())
                        {
                            item.Text = dr["plaka"].ToString();
                        }
                    }
                }
            }
            bag.Close();
        }

        private void DoluparkYerleri()
        {
            bag.Open();
            SqlCommand komut = new SqlCommand("select * from aracdurumu", bag);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                foreach (Control item in Controls)
                {
                    if (item is Button)
                    {
                        if (item.Text == dr["parkyeri"].ToString() && dr["durum"].ToString() == "DOLU")
                        {
                            item.BackColor = Color.Red;
                        }
                    }
                }
            }
            bag.Close();
        }

        private void BoşParkYerleri()
        {
            int sayac = 1;
            foreach (Control item in Controls)
            {
                if (item is Button)
                {
                    item.Text = "P-" + sayac;
                    item.Name = "P-" + sayac;
                    sayac++;
                }
            }
        }
    }
}
