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
    public partial class frmAraçOtoparkÇıkışı : Form
    {
        public frmAraçOtoparkÇıkışı()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-20OHPP8;Initial Catalog=araç_otopark;Integrated Security=True");
        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void frmAraçOtoparkÇıkışı_Load(object sender, EventArgs e)
        {
            DoluYerler();
            Plakalar();
            timer1.Enabled = true;
        }

        private void Plakalar()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from araç_otopark_kaydı", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboPlaka.Items.Add(read["plaka"].ToString());
            }
            baglanti.Close();
        }

        private void DoluYerler()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from araçdurumu where durumu='DOLU'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboParkYeri.Items.Add(read["parkyeri"].ToString());
            }
            baglanti.Close();
        }

        private void comboPlaka_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from araç_otopark_kaydı where plaka='"+comboPlaka.SelectedItem+"' ", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                textParkYeri.Text = read["parkyeri"].ToString();
            }
            baglanti.Close();
        }

        private void comboParkYeri_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from araç_otopark_kaydı where parkyeri='" + comboParkYeri.SelectedItem + "' ", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                textParkyeri2.Text = read["parkyeri"].ToString();
                textTC.Text = read["tc"].ToString();
                textAd.Text = read["ad"].ToString();
                textSoyad.Text = read["soyad"].ToString();
                textMarka.Text = read["marka"].ToString();
                textSeri.Text = read["seri"].ToString();
                textPlaka.Text = read["plaka"].ToString();
                lblGelişTarihi.Text = read["tarih"].ToString(); 
            
            }
            baglanti.Close();
            DateTime geliş, çıkış;
            geliş = DateTime.Parse (lblGelişTarihi.Text);
            çıkış = DateTime.Parse(lblÇıkışTarihi.Text);
            TimeSpan fark;
            fark = çıkış - geliş;
            lblSüre.Text=fark.TotalHours.ToString("0.00");
           
        
            lblToplamTutar.Text=(double.Parse(lblSüre.Text)*(0.75)).ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblÇıkışTarihi.Text=DateTime.Now.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete  from araç_otopark_kaydı where plaka='"+textPlaka.Text+"'", baglanti);
            komut.ExecuteNonQuery();
            SqlCommand komut2 = new SqlCommand("update araçdurumu set durumu='BOŞ' where parkyeri='" + textParkyeri2.Text + "'", baglanti);
            komut2.ExecuteNonQuery();
            SqlCommand komut3 = new SqlCommand ("insert into satis(parkyeri,plaka,geliş_tarihi,çıkış_tarihi,süre,tutar) values(@parkyeri,@plaka,@geliş_tarihi,@çıkış_tarihi,@süre,@tutar)", baglanti);
            komut3.Parameters.AddWithValue("@parkyeri", textParkyeri2.Text);
            komut3.Parameters.AddWithValue("@plaka", textPlaka.Text);
            komut3.Parameters.AddWithValue("@geliş_tarihi", lblGelişTarihi.Text);
            komut3.Parameters.AddWithValue("@çıkış_tarihi", lblÇıkışTarihi.Text);
            komut3.Parameters.AddWithValue("@süre",  double.Parse(lblSüre.Text));
            komut3.Parameters.AddWithValue("@tutar",double.Parse (lblToplamTutar.Text));
            komut3.ExecuteNonQuery();
            

            baglanti.Close();

            MessageBox.Show("Araç Çıkışı Yapıldı");
            foreach(Control item in groupBox2.Controls)
            {
                if(item is TextBox)
                {
                    item.Text = "";
                    textParkYeri.Text = "";
                    comboParkYeri.Text = "";
                    comboPlaka.Text = "";
                }
            }
            comboPlaka.Items.Clear();
            comboParkYeri.Items.Clear();
            DoluYerler();
            Plakalar();
        }
    }
}
