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

namespace VeritabaniProje
{
    public partial class UyeEklemeForm : Form
    {
        string ilID = "", ilceID = "", okulID = "";
        SqlConnection baglanti = new SqlConnection("server =(localdb)\\MSSQLLocalDB; Initial Catalog = Kutuphane_db; Integrated Security = True");
        public UyeEklemeForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            MenuForm frm2 = new MenuForm();
            frm2.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string kayit1 = "insert into IletisimBilgileri(telefon,adres,eposta,ilID,ilceID,okulID) values (@telefon,@adres,@eposta,@ilID,@ilceID,@okulID)";

            SqlCommand komut1 = new SqlCommand(kayit1, baglanti);
            komut1.Parameters.AddWithValue("@telefon", txtCepTelefonu.Text);
            komut1.Parameters.AddWithValue("@adres", txtAdres.Text);
            komut1.Parameters.AddWithValue("@eposta", txtEposta.Text);
            komut1.Parameters.AddWithValue("@ilID", Convert.ToInt32(ilID));
            komut1.Parameters.AddWithValue("@ilceID", Convert.ToInt32(ilceID));
            komut1.Parameters.AddWithValue("@okulID", Convert.ToInt32(okulID));

            komut1.ExecuteNonQuery();

            string altKayit = "select iletisimID from IletisimBilgileri where telefon = '" + txtCepTelefonu.Text + "'";
            SqlCommand komut3 = new SqlCommand(altKayit, baglanti);

            SqlDataReader dr;
            dr = komut3.ExecuteReader();
            string iletisimID = "";

            if (dr.Read())
            {
                iletisimID = dr["iletisimID"].ToString();
            }
            dr.Close();
            string kayit2 = "insert into Kullanici(kullaniciAdi,sifre,tcNo,adSoyad,cinsiyet,dogumTarihi,iletisimID,kullaniciTur) " +
                "values (@kullaniciAdi,@sifre,@tcNo,@adSoyad,@cinsiyet,@dogumTarihi,@iletisimID,@kullaniciTur)";

            SqlCommand komut2 = new SqlCommand(kayit2, baglanti);
            komut2.Parameters.AddWithValue("@kullaniciAdi", txtKullaniciAdi.Text);
            komut2.Parameters.AddWithValue("@sifre", txtSifre.Text);
            komut2.Parameters.AddWithValue("@tcNo", txtTc.Text);
            komut2.Parameters.AddWithValue("@adSoyad", txtAdiSoyadi.Text);
            komut2.Parameters.AddWithValue("@cinsiyet", comboBox3.Text);
            komut2.Parameters.AddWithValue("@dogumTarihi", dateTimePicker1.Value);
            komut2.Parameters.AddWithValue("@iletisimID", iletisimID);
            komut2.Parameters.AddWithValue("@kullaniciTur", "üye");

            komut2.ExecuteNonQuery();

            baglanti.Close();
            

            string Kayit = "select kullaniciID from Kullanici where iletisimID= '" + iletisimID + "'";
            SqlCommand komut = new SqlCommand(Kayit, baglanti);
            baglanti.Open();
            dr = komut.ExecuteReader();
            string kullaniciID = "";

            if (dr.Read())
            {
                kullaniciID = dr["kullaniciID"].ToString();
            }
            dr.Close();
            string kayit3 = "insert into Uye(kullaniciID,kayitTarihi,okuduguKitapSayisi) " +
                "values (@kullaniciID,@kayitTarihi,@okuduguKitapSayisi)";

            SqlCommand komut4 = new SqlCommand(kayit3, baglanti);
            komut4.Parameters.AddWithValue("@kullaniciID", kullaniciID);
            komut4.Parameters.AddWithValue("@kayitTarihi", DateTime.Now);
            komut4.Parameters.AddWithValue("@okuduguKitapSayisi", 0);

            komut4.ExecuteNonQuery();

            baglanti.Close();


            MessageBox.Show("Üye Kayıt İşlemi Gerçekleşti.");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();

            string kayit = "SELECT ilID FROM Il where ilAdi = '" + comboBox2.Text + "'";
            SqlCommand komut = new SqlCommand(kayit, baglanti);

            SqlDataReader dr;
            baglanti.Open();
            dr = komut.ExecuteReader();

            while (dr.Read())
            {
                ilID = dr["ilID"].ToString();
            }
            baglanti.Close();


            kayit = "SELECT * FROM Ilce where ilID = '" + ilID + "'";
            komut = new SqlCommand(kayit, baglanti);
            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["ilceAdi"]);
            }
            baglanti.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxOkul.Items.Clear();
            string kayit = "SELECT ilceID FROM Ilce where ilceAdi = '" + comboBox1.Text + "'";
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            SqlDataReader dr;
            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                ilceID = dr["ilceID"].ToString();
            }
            baglanti.Close();

            kayit = "SELECT * FROM Okul where ilceID = '" + ilceID + "'";
            komut = new SqlCommand(kayit, baglanti);
            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBoxOkul.Items.Add(dr["okulAdi"]);
            }
            baglanti.Close();
        }

        private void comboBoxOkul_SelectedIndexChanged(object sender, EventArgs e)
        {
            string kayit = "SELECT okulID FROM Okul where okulAdi = '" + comboBoxOkul.Text + "'";
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            SqlDataReader dr;
            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                okulID = dr["okulID"].ToString();
            }
            baglanti.Close();
        }

        private void UyeEklemeForm_Load(object sender, EventArgs e)
        {
            string kayit = "SELECT *FROM Il";
            SqlCommand komut = new SqlCommand(kayit, baglanti);

            SqlDataReader dr;
            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr["ilAdi"]);
            }
            baglanti.Close();
        }
    }
}
