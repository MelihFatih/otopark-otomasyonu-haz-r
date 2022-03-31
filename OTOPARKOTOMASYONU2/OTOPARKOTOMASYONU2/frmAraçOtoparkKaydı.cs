using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTOPARKOTOMASYONU2
{
    public partial class frmAraçOtoparkKaydı : Form
    {
        public frmAraçOtoparkKaydı()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-20OHPP8;Initial Catalog=araç_otopark;Integrated Security=True");
        private void frmAraçOtoparkKaydı_Load(object sender, EventArgs e)
        {
            BoşAraçlar();
            Marka();
           
        }

        private void Marka()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select marka from markabilgileri ", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                combomarka.Items.Add(read["marka"].ToString());
            }
            baglanti.Close();
        }

        private void BoşAraçlar()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from araçdurumu WHERE durumu='BOŞ'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboparkyeri.Items.Add(read["parkyeri"].ToString());
            }
            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into araç_otopark_kaydı(tc,ad,soyad,telefon,email,plaka,marka,seri,renk,parkyeri,tarih) values(@tc,@ad,@soyad,@telefon,@email,@plaka,@marka,@seri,@renk,@parkyeri,@tarih)", baglanti);
            komut.Parameters.AddWithValue("@tc",txttc.Text);
            komut.Parameters.AddWithValue("@ad", txtad.Text);
            komut.Parameters.AddWithValue("@soyad", txtsoyad.Text);
            komut.Parameters.AddWithValue("@telefon", txttelefon.Text);
            komut.Parameters.AddWithValue("@email", txtemail.Text);
            komut.Parameters.AddWithValue("@plaka", txtplaka.Text);
            komut.Parameters.AddWithValue("@marka", combomarka.Text);
            komut.Parameters.AddWithValue("@seri", comboseri.Text);
            komut.Parameters.AddWithValue("@renk", txtrenk.Text);
            komut.Parameters.AddWithValue("@parkyeri", comboparkyeri.Text);
            komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());



            komut.ExecuteNonQuery();
            

            SqlCommand komut2 = new SqlCommand("update araçdurumu set durumu='DOLU' where parkyeri='"+comboparkyeri.SelectedItem+"'",baglanti);
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("kayıt oluşturuldu", "kayıt");
            comboparkyeri.Items.Clear();
            BoşAraçlar();
            combomarka.Items.Clear ();
            Marka();
            comboseri.Items.Clear ();   
            foreach(Control item in grupkişi.Controls)
            {
                if(item is TextBox)
                {
                    item.Text = "";
                }

            }
            foreach (Control item in gruparaç.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }

            }
            foreach (Control item in gruparaç.Controls)
            {
                if (item is ComboBox)
                {
                    item.Text = "";
                }

            }

        }

        private void gruparaç_Enter(object sender, EventArgs e)
        {

        }

        private void btnmarka_Click(object sender, EventArgs e)
        {
            frmMarka marka=new frmMarka();
            marka.ShowDialog(); 
        }

        private void btnseri_Click(object sender, EventArgs e)
        {
            frmSeri seri =new frmSeri();
            seri.ShowDialog();  
        }

        private void combomarka_SelectedIndexChanged(object sender, EventArgs e)
        {    
            comboseri.Items.Clear();    
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select marka,seri from seribilgileri where marka='"+combomarka.SelectedItem+"'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboseri.Items.Add(read["seri"].ToString());
            }
            baglanti.Close();

        }
    }
}
