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
    public partial class frmMarka : Form
    {
        public frmMarka()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-20OHPP8;Initial Catalog=araç_otopark;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into markabilgileri(marka) values('" + textBox1.Text + "')", baglanti);
            komut.ExecuteNonQuery(); 
            baglanti.Close();
            MessageBox.Show("marka eklendi");
            textBox1.Clear();   
        }

        private void frmMarka_Load(object sender, EventArgs e)
        {

        }
    }
}
