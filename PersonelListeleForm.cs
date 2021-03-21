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
    public partial class PersonelListeleForm : Form
    {
        string id = "", ilid = "", ilceid = "";
        SqlConnection baglanti = new SqlConnection("server =(localdb)\\MSSQLLocalDB; Initial Catalog = Kutuphane_db; Integrated Security = True");
        public PersonelListeleForm()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            id = dataGridView1.CurrentRow.Cells["iletisimID"].Value.ToString();
            txtKullaniciAdi.Text = dataGridView1.CurrentRow.Cells["kullaniciAdi"].Value.ToString();
            txtSifre.Text = dataGridView1.CurrentRow.Cells["sifre"].Value.ToString();
            txtTc.Text = dataGridView1.CurrentRow.Cells["tcNo"].Value.ToString();
            txtAdiSoyadi.Text = dataGridView1.CurrentRow.Cells["adSoyad"].Value.ToString();
            comboBox3.Text = dataGridView1.CurrentRow.Cells["cinsiyet"].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells["dogumTarihi"].Value.ToString();

            string kayit2 = "SELECT * FROM IletisimBilgileri where iletisimID = '" + id + "'";
            SqlCommand komut2 = new SqlCommand(kayit2, baglanti);
            SqlDataReader dr;
            baglanti.Open();
            dr = komut2.ExecuteReader();
            if (dr.Read())
            {
                ilceid = dr["ilceID"].ToString();
                txtCepTelefonu.Text = dr["telefon"].ToString();
                txtAdres.Text = dr["adres"].ToString();
                txtEposta.Text = dr["eposta"].ToString();
            }
            baglanti.Close();

            kayit2 = "SELECT * FROM Ilce where ilceID = '" + ilceid + "'";
            komut2 = new SqlCommand(kayit2, baglanti);
            baglanti.Open();
            dr = komut2.ExecuteReader();
            if (dr.Read())
            {
                ilid = dr["ilID"].ToString();
                comboBox1.Text = dr["ilceAdi"].ToString();
            }
            baglanti.Close();

            kayit2 = "SELECT * FROM IL where ilID = '" + ilid + "'";
            komut2 = new SqlCommand(kayit2, baglanti);
            baglanti.Open();
            dr = komut2.ExecuteReader();
            if (dr.Read())
            {
                comboBox2.Text = dr["ilAdi"].ToString();
            }
            
            baglanti.Close();
        }
        public void listele()
        {
            baglanti.Open();
            string kayit2 = "SELECT * FROM Kullanici where kullaniciTur = 'personel'";
            SqlCommand komut2 = new SqlCommand(kayit2, baglanti);
            SqlDataAdapter da2 = new SqlDataAdapter(komut2);
            DataTable dt = new DataTable();
            da2.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[1].HeaderCell.Value = "Kullanıcı Adı";
            dataGridView1.Columns[2].HeaderCell.Value = "Sifre";
            dataGridView1.Columns[3].HeaderCell.Value = "Tc";
            dataGridView1.Columns[4].HeaderCell.Value = "Ad Soyad";
            dataGridView1.Columns[5].HeaderCell.Value = "Cinsiyet";
            dataGridView1.Columns[6].HeaderCell.Value = "Doğum Tarihi";
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            baglanti.Close();

        }

        private void PersonelListeleForm_Load(object sender, EventArgs e)
        {
            listele();
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

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            MenuForm frm2 = new MenuForm();
            frm2.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Close();
            string kayit = "SELECT ilceID FROM Ilce where ilceAdi = '" + comboBox1.Text + "'";
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            SqlDataReader dr;
            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                ilceid = dr["ilceID"].ToString();
            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string silme = "DELETE from Kullanici where iletisimID = '" + id + "'";
            SqlCommand komut = new SqlCommand(silme, baglanti);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            listele();
            txtEposta.Text = "";
            txtAdiSoyadi.Text = "";
            txtAdres.Text = "";
            txtCepTelefonu.Text = "";
            txtKullaniciAdi.Text = "";
            dateTimePicker1.Text = "";
            txtSifre.Text = "";
            txtTc.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            listele();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            baglanti.Close();
            string kayit = "SELECT ilID FROM Il where ilAdi = '" + comboBox2.Text + "'";
            SqlCommand komut = new SqlCommand(kayit, baglanti);

            SqlDataReader dr;
            baglanti.Open();
            dr = komut.ExecuteReader();

            while (dr.Read())
            {
                ilid = dr["ilID"].ToString();
            }
            baglanti.Close();

            kayit = "SELECT * FROM Ilce where ilID = '" + ilid + "'";
            komut = new SqlCommand(kayit, baglanti);
            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["ilceAdi"]);
            }
            baglanti.Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string kayit1 = "update IletisimBilgileri set telefon=@telefon,adres=@adres,eposta=@eposta,ilID=@ilID,ilceID=@ilceID where iletisimID = '" + id + "'";

            SqlCommand komut1 = new SqlCommand(kayit1, baglanti);
            komut1.Parameters.AddWithValue("@telefon", txtCepTelefonu.Text);
            komut1.Parameters.AddWithValue("@adres", txtAdres.Text);
            komut1.Parameters.AddWithValue("@eposta", txtEposta.Text);
            komut1.Parameters.AddWithValue("@ilID", Convert.ToInt32(ilid));
            komut1.Parameters.AddWithValue("@ilceID", Convert.ToInt32(ilceid));

            komut1.ExecuteNonQuery();
            
            string kayit2 = "update Kullanici set kullaniciAdi=@kullaniciAdi,sifre=@sifre,tcNo=@tcNo,adSoyad=@adSoyad,cinsiyet=@cinsiyet,dogumTarihi=@dogumTarihi where iletisimID = '" + id + "'";

            SqlCommand komut2 = new SqlCommand(kayit2, baglanti);
            komut2.Parameters.AddWithValue("@kullaniciAdi", txtKullaniciAdi.Text);
            komut2.Parameters.AddWithValue("@sifre", txtSifre.Text);
            komut2.Parameters.AddWithValue("@tcNo", txtTc.Text);
            komut2.Parameters.AddWithValue("@adSoyad", txtAdiSoyadi.Text);
            komut2.Parameters.AddWithValue("@cinsiyet", comboBox3.Text);
            komut2.Parameters.AddWithValue("@dogumTarihi", dateTimePicker1.Value);

            komut2.ExecuteNonQuery();

            baglanti.Close();
            listele();
            MessageBox.Show("Personel Güncelleme İşlemi Gerçekleşti.");
        }
    }
}
