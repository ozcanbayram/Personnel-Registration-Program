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
    public partial class Frmistatistik : Form
    {
        public Frmistatistik()
        {
            InitializeComponent();
        }

        //sql sınıfınfan nesne yaratabilmek için tanımla:
        SqlConnection baglanti = new SqlConnection("Data Source=OZCAN;Initial Catalog=PersonelVeriTabani;Integrated Security=True");

        private void Frmistatistik_Load(object sender, EventArgs e)
        {
            //Toplam Personel Sayısı:
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("select count(*) from Tbl_Personel", baglanti); //count(*) ile Tablodakilerin hepsini seçtik.
            SqlDataReader dr1 = komut1.ExecuteReader(); //NoneQuery güncelleme ekleme ve silmede kullanılıyordu Reader de Select işlemi için seçmede, okumada kullanılıyor.
            while (dr1.Read())//döngü kadar dr1 i oku
            {
                lblToplamPersonel.Text = dr1[0].ToString(); //Sqlde "select count(*) from Tbl_Personel" komutu yazıldıktan sonra 0. indexte toplam sayı yazar bu yüzden SqlDataReader (dr1) yani data okuyucuya okuması için 0. indexi belirttik.
            }
            baglanti.Close();

            //Evli Personel sayısı:
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select count(*) from Tbl_Personel where PerDurum=1", baglanti);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                lblEvliPersonelSayisi.Text = dr2[0].ToString();
            }
            baglanti.Close();

            //Bekar Personel Sayısı:
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("select count(*) From Tbl_Personel where PerDurum=0", baglanti);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                lblBekarPersonelSayisi.Text = dr3[0].ToString();
            }
            baglanti.Close();

            //Şehir Sayısı:
            baglanti.Open();
            SqlCommand komut4 = new SqlCommand("Select count(distinct (PerSehir)) From Tbl_Personel", baglanti); // --> distinct komutu tekrarsız saymaya yarar aynı olanları sadece 1 kere sayar. Bu komutu count parantezi içine alınca sayısını döndürür.
            SqlDataReader dr4= komut4.ExecuteReader();
            while (dr4.Read())
            {
                lblSehirSayisi.Text = dr4[0].ToString();
            }
            baglanti.Close();

            //Toplam Maaş
            baglanti.Open();
            SqlCommand komut5 = new SqlCommand("Select sum(PerMaas) from Tbl_Personel", baglanti);
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                lblToplamMaas.Text= dr5[0].ToString();
            }
            baglanti.Close();

            //Ortalama Maaş
            baglanti.Open();
            SqlCommand komut6 = new SqlCommand("Select avg(PerMaas) from Tbl_Personel", baglanti);
            SqlDataReader dr6 = komut6.ExecuteReader();
            while(dr6.Read())
            {
                lblOrtalamaMaas.Text= dr6[0].ToString();
            }
            baglanti.Close();
        }
    }
}
