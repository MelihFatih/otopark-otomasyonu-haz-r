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

namespace OTOPARKOTOMASYONU2
{
    public partial class frmSatışcs : Form
    {
        public frmSatışcs()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-20OHPP8;Initial Catalog=araç_otopark;Integrated Security=True");
        DataSet daset=new DataSet();
        private void frmSatışcs_Load(object sender, EventArgs e)
        {
            SatislariListele();
            Hesapla();
        }

        private void Hesapla()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select sum(tutar) from satis", baglanti);
            label1.Text = "toplam tutar=" + komut.ExecuteScalar() + "TL";
            baglanti.Close();
        }

        private void SatislariListele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from satis", baglanti);
            adtr.Fill(daset, "satis");
            dataGridView1.DataSource = daset.Tables["satis"];
            baglanti.Close();
        }
    }
}
