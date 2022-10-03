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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace WindowsFormsApp3
{
    public partial class frmSatışListeleme : Form
    {
        public frmSatışListeleme()
        {
            InitializeComponent();
        }
        SqlConnection bag = new SqlConnection(@"Data Source=.\SQLExpress;initial catalog=arac_otopark;integrated security=true");
        DataSet daSet = new DataSet();
        private void frmSatışListeleme_Load(object sender, EventArgs e)
        {
            SatislariListele();
            Hesapla();
        }

        private void Hesapla()
        {
            bag.Open();
            SqlCommand komut = new SqlCommand(" select sum ( tutar ) from satis ", bag);
            label1.Text = " Toplam Tutar = " + komut.ExecuteScalar() + " TL ";
            bag.Close();
        }

        private void SatislariListele()
        {
            bag.Open();
            SqlDataAdapter adtr = new SqlDataAdapter(" select * from satis ", bag);
            adtr.Fill(daSet, " satis ");
            dataGridView1.DataSource = daSet.Tables[" satis "];
            bag.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 frm = new Form1();
            frm.Show();
        }
    }
}
