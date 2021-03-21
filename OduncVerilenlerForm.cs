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
    public partial class OduncVerilenlerForm : Form
    {
        string kullaniciadi = "", kitapadi="", BilgisayarMarka="", kitapKayitID = "", bilgisayarKayitID="";
        DataTable kitapTable = new DataTable();
        DataTable table = new DataTable();
        SqlConnection baglanti = new SqlConnection("server =(localdb)\\MSSQLLocalDB; Initial Catalog = Kutuphane_db; Integrated Security = True");
        public OduncVerilenlerForm()
        {
            InitializeComponent();
        }
        public void kitapListele()
        {
            kitapTable = new DataTable();
            List<string> kitapKayitID = new List<string>();
            List<string> kayitlar = new List<string>();
            List<string> kullaniciAdi = new List<string>();
            List<string> kitapAdi = new List<string>();
            List<string> alisTarihi = new List<string>();
            List<string> verisTarihi = new List<string>();

            baglanti.Open();
            string kayit = "SELECT * FROM KitapKayit";
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            SqlDataReader dr;
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                kitapKayitID.Add(dr["kitapKayitID"].ToString());
                kayitlar.Add(dr["kitapID"].ToString());
                kayitlar.Add(dr["kullaniciID"].ToString());
                alisTarihi.Add(dr["alisTarihi"].ToString());
                verisTarihi.Add(dr["verisTarihi"].ToString());
            }
            baglanti.Close();

            for (int i = 0; i < kayitlar.Count; i++)
            {
                kayit = "SELECT * FROM Kitap where kitapID ='" + kayitlar[i] + "'";
                komut = new SqlCommand(kayit, baglanti);
                baglanti.Open();
                dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    kitapAdi.Add(dr["kitapAdi"].ToString());
                }
                baglanti.Close();

                i++;
                kayit = "SELECT * FROM Kullanici where kullaniciID ='" + kayitlar[i] + "'";
                komut = new SqlCommand(kayit, baglanti);
                baglanti.Open();
                dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    kullaniciAdi.Add(dr["kullaniciAdi"].ToString());
                }
                baglanti.Close();
            }
            kitapTable.Columns.Add("kitapKayitID", typeof(string));
            kitapTable.Columns.Add("Kullanici", typeof(string));
            kitapTable.Columns.Add("Kitap_Adi", typeof(string));
            kitapTable.Columns.Add("Alınacak Tarihi", typeof(string));
            kitapTable.Columns.Add("Veriş Tarihi", typeof(string));
            
            for (int i = 0; i < kullaniciAdi.Count; i++)
            {
                kitapTable.Rows.Add(kitapKayitID[i], kullaniciAdi[i], kitapAdi[i], alisTarihi[i], verisTarihi[i]);
            }
            dataGridView1.DataSource = kitapTable;
            dataGridView1.Columns[0].Visible = false;
            baglanti.Close();
            
        }

        public void BilgisayarListele()
        {
            table = new DataTable();
            List<string> bilgisayarKayitID = new List<string>();
            List<string> kayitlar = new List<string>();
            List<string> kullaniciAdi = new List<string>();
            List<string> bilgisayarAdi = new List<string>();
            List<string> alisTarihi = new List<string>();
            List<string> verisTarihi = new List<string>();

            baglanti.Open();
            string kayit = "SELECT * FROM BilgisayarKayit";
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            SqlDataReader dr;
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                bilgisayarKayitID.Add(dr["bilgisayarKayitID"].ToString());
                kayitlar.Add(dr["bilgisayarID"].ToString());
                kayitlar.Add(dr["kullaniciID"].ToString());
                alisTarihi.Add(dr["alisTarihi"].ToString());
                verisTarihi.Add(dr["verisTarihi"].ToString());
            }
            baglanti.Close();

            for (int i = 0; i < kayitlar.Count; i++)
            {
                kayit = "SELECT * FROM Bilgisayar where BilgisayarID ='" + kayitlar[i] + "'";
                komut = new SqlCommand(kayit, baglanti);
                baglanti.Open();
                dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    bilgisayarAdi.Add(dr["marka"].ToString());
                }
                baglanti.Close();

                i++;
                kayit = "SELECT * FROM Kullanici where kullaniciID ='" + kayitlar[i] + "'";
                komut = new SqlCommand(kayit, baglanti);
                baglanti.Open();
                dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    kullaniciAdi.Add(dr["kullaniciAdi"].ToString());
                }
                baglanti.Close();
            }

            table.Columns.Add("bilgisayarKayitID", typeof(string));
            table.Columns.Add("Kullanici", typeof(string));
            table.Columns.Add("Marka", typeof(string));
            table.Columns.Add("Alınacak Tarihi", typeof(string));
            table.Columns.Add("Veriş Tarihi", typeof(string));
            for (int i = 0; i < kullaniciAdi.Count; i++)
            {
                table.Rows.Add(bilgisayarKayitID[i],kullaniciAdi[i], bilgisayarAdi[i], alisTarihi[i], verisTarihi[i]);
            }

            dataGridView2.DataSource = table;
            dataGridView2.Columns[0].Visible = false;
            baglanti.Close();
        }

        private void OduncVerilenlerForm_Load(object sender, EventArgs e)
        {
            kitapListele();
            BilgisayarListele();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            MenuForm frm2 = new MenuForm();
            frm2.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataView dv = kitapTable.DefaultView;
            dv.RowFilter = "Kullanici LIKE '" + textBox1.Text + "%'";
            dataGridView1.DataSource = dv;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            DataView dv = table.DefaultView;
            dv.RowFilter = "Kullanici LIKE '" + textBox2.Text + "%'";
            dataGridView2.DataSource = dv;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kullaniciID = "";
            int okuduguKitapSayisi = 0, stokSayisi = 0;
            baglanti.Open();
            string kayit = "SELECT * FROM Kullanici where kullaniciAdi = '" + kullaniciadi + "'";
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            SqlDataReader dr;
            dr = komut.ExecuteReader();
            if (dr.Read())
            {
                kullaniciID = (dr["kullaniciID"].ToString());
            }
            baglanti.Close();

            baglanti.Open();
            kayit = "SELECT * FROM Uye where kullaniciID = '" + kullaniciID + "'";
            komut = new SqlCommand(kayit, baglanti);
            dr = komut.ExecuteReader();
            if (dr.Read())
            {
                okuduguKitapSayisi = Convert.ToInt32(dr["okuduguKitapSayisi"].ToString());
            }
            baglanti.Close();


            baglanti.Open();
            kayit = "update Uye set okuduguKitapSayisi=@okuduguKitapSayisi where kullaniciID = '" + kullaniciID + "'";
            komut = new SqlCommand(kayit, baglanti);
            komut.Parameters.AddWithValue("@okuduguKitapSayisi", okuduguKitapSayisi + 1);
            komut.ExecuteNonQuery();
            baglanti.Close();


            baglanti.Open();
            kayit = "SELECT * FROM Kitap where kitapAdi = '" + kitapadi + "'";
            komut = new SqlCommand(kayit, baglanti);
            dr = komut.ExecuteReader();
            if (dr.Read())
            {
                stokSayisi = Convert.ToInt32(dr["stokSayisi"].ToString());
            }
            baglanti.Close();


            baglanti.Open();
            kayit = "update Kitap set stokSayisi=@stokSayisi where kitapAdi = '" + kitapadi + "'";
            komut = new SqlCommand(kayit, baglanti);
            komut.Parameters.AddWithValue("@stokSayisi", stokSayisi + 1);
            komut.ExecuteNonQuery();
            baglanti.Close();


            string silme = "DELETE from KitapKayit WHERE kitapKayitID = '" + kitapKayitID + "'";
            komut = new SqlCommand(silme, baglanti);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            kitapListele();
            textBox1.Text = "";






            
            


            baglanti.Open();
            kayit = "SELECT * FROM Bilgisayar where marka = '" + BilgisayarMarka + "'";
            komut = new SqlCommand(kayit, baglanti);
            dr = komut.ExecuteReader();
            if (dr.Read())
            {
                stokSayisi = Convert.ToInt32(dr["stokSayisi"].ToString());
            }
            baglanti.Close();


            baglanti.Open();
            kayit = "update Bilgisayar set stokSayisi=@stokSayisi where marka = '" + BilgisayarMarka + "'";
            komut = new SqlCommand(kayit, baglanti);
            komut.Parameters.AddWithValue("@stokSayisi", stokSayisi + 1);
            komut.ExecuteNonQuery();
            baglanti.Close();


            silme = "DELETE from BilgisayarKayit WHERE bilgisayarKayitID = '" + bilgisayarKayitID + "'";
            komut = new SqlCommand(silme, baglanti);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            BilgisayarListele();
            textBox2.Text = "";
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            kullaniciadi = dataGridView1.CurrentRow.Cells["Kullanici"].Value.ToString();
            kitapKayitID = dataGridView1.CurrentRow.Cells["kitapKayitID"].Value.ToString();
            kitapadi = dataGridView1.CurrentRow.Cells["Kitap_Adi"].Value.ToString();
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            kullaniciadi = dataGridView2.CurrentRow.Cells["Kullanici"].Value.ToString();
            bilgisayarKayitID = dataGridView2.CurrentRow.Cells["bilgisayarKayitID"].Value.ToString();
            BilgisayarMarka = dataGridView2.CurrentRow.Cells["Marka"].Value.ToString();
        }
    }
}
