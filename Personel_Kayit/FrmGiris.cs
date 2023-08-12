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

namespace Personel_Kayit
{
    public partial class FrmGiris : Form
    {
        public FrmGiris()
        {
            InitializeComponent();
        }

        //sql sınıfınfan nesne yaratabilmek için tanımla:
        SqlConnection baglanti = new SqlConnection("Data Source=OZCAN;Initial Catalog=PersonelVeriTabani;Integrated Security=True"); 

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From Tbl_Yonetici where KullaniciAd=@p1 and sifre=@p2",baglanti);
            komut.Parameters.AddWithValue("@p1", txtKullaniciAdi.Text);
            komut.Parameters.AddWithValue("@p2", txtParola.Text);
            SqlDataReader dr1 = komut.ExecuteReader();
            if(dr1.Read())
            {
                FrmAnaForm anaformagit = new FrmAnaForm();
                anaformagit.Show();
            }
            
            else{ MessageBox.Show("Hatalı Kullanıcı Adı ya da parola girdiniz. Lütfen tekrar deneyiniz."); }
            baglanti.Close();

        }
    }
}
