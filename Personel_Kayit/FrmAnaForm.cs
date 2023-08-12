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
    public partial class FrmAnaForm : Form
    {
        public FrmAnaForm()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        //sql sınıfınfan nesne yaratabilmek için tanımla:
        SqlConnection baglanti = new SqlConnection("Data Source=OZCAN;Initial Catalog=PersonelVeriTabani;Integrated Security=True");

        //Temizleme butonu için bir metot oluşturalım:
        void temizle()
        {
            txtAd.Text = "";
            txtSoyad.Text = "";
            cmbSehir.Text = "";
            mskMaas.Text = "";
            txtMeslek.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            txtId.Text = "";
            txtAd.Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'personelVeriTabaniDataSet.Tbl_Personel' table. You can move, or remove it, as needed.
            this.tbl_PersonelTableAdapter.Fill(this.personelVeriTabaniDataSet.Tbl_Personel);

            FrmGiris giris = new FrmGiris();
            giris.Hide();


        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            // Veri tabanından veri alındığını belirten kod
            this.tbl_PersonelTableAdapter.Fill(this.personelVeriTabaniDataSet.Tbl_Personel);
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();//Sql bağlantısını açıyoruz.

            SqlCommand komut = new SqlCommand("insert into Tbl_Personel (perad,persoyad,persehir,permaas,permeslek,perdurum) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);//Sql'e komut vermek için SqlCommand sınıfından komut isminde bir nesne oluşturduk ve bu nesne ile veri tabanına ekleme yaptık. insert into ile nereye yapacağımızı belirttik(hangi  veri tabanı), daha sornra sütun belirterek köprü görevi görmesi için bunlara parametre atadık(@p1 gibi).
            komut.Parameters.AddWithValue("@p1", txtAd.Text); //Üst satırda belirtilen komut kullanılarak, parametreler ile veri tabanının sütunlarıyla textBoxlar arasında bir köprü oluşturduk.
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", cmbSehir.Text);
            komut.Parameters.AddWithValue("@p4", mskMaas.Text);
            komut.Parameters.AddWithValue("@p5", txtMeslek.Text);
            komut.Parameters.AddWithValue("@p6", label8.Text);
            komut.ExecuteNonQuery();//Tabloda değişiklik yapılırken kullanılır. (ekle,sil,Güncelle)

            baglanti.Close();//Sql bağlantısını Kapatıyoruz.

            MessageBox.Show("Personel Kaydedildi."); //Kaydedildiğine dair ekran çıktısı mesajı.


        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                label8.Text = "True";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                label8.Text = "False";
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex; // çift tıklanan hücreyi secilen adlı değişkene atadık.

            //Şimdi secilen değişkenine atanan bilgileri çağıralım. (yazdıralım):
            txtId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cmbSehir.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            mskMaas.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            label8.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            txtMeslek.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
        }

        private void label8_TextChanged(object sender, EventArgs e)
        {
            if (label8.Text == "True")
            {
                radioButton1.Checked = true;
            }
            if (label8.Text == "False")
            {
                radioButton2.Checked = true;
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutsil = new SqlCommand("Delete From Tbl_Personel where Perid = @k1",baglanti);
            komutsil.Parameters.AddWithValue("@k1",txtId.Text);
            komutsil.ExecuteNonQuery(); 
            baglanti.Close();
            MessageBox.Show("Kayıt silindi.");
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutguncelle = new SqlCommand("Update Tbl_Personel Set PerAd=@a1, PerSoyad=@a2, PerSehir=@a3, PerMaas=@a4, PerDurum=@a5, PerMeslek=@a6 where Perid=@a7",baglanti);
            komutguncelle.Parameters.AddWithValue("@a1", txtAd.Text);
            komutguncelle.Parameters.AddWithValue("@a2", txtSoyad.Text);
            komutguncelle.Parameters.AddWithValue("@a3", cmbSehir.Text);
            komutguncelle.Parameters.AddWithValue("@a4", mskMaas.Text);
            komutguncelle.Parameters.AddWithValue("@a5", label8.Text);
            komutguncelle.Parameters.AddWithValue("@a6", txtMeslek.Text);
            komutguncelle.Parameters.AddWithValue("@a7", txtId.Text);
            komutguncelle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Bilgiler güncellendi.");
        }

        private void btnIstatistik_Click(object sender, EventArgs e)
        {
            Frmistatistik frem_i = new Frmistatistik();
            frem_i.Show();
        }

        private void btnGrafikler_Click(object sender, EventArgs e)
        {
            FrmGrafikler frmGrafik = new FrmGrafikler();
            frmGrafik.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void cmbSehir_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
