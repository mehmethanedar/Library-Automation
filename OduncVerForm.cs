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
    public partial class OduncVerForm : Form
    {
        string secili = "",kullaniciID="", kitapID = "", bilgisayarID = "";
        SqlConnection baglanti = new SqlConnection("server =(localdb)\\MSSQLLocalDB; Initial Catalog = Kutuphane_db; Integrated Security = True");
        public OduncVerForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            MenuForm frm2 = new MenuForm();
            frm2.ShowDialog();
        }

        private void OduncVerForm_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            string kayit = "SELECT * FROM Kullanici";
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[1].HeaderCell.Value = "Kullanıcı Adı";
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            baglanti.Close();
        }
        public void kitapListele()
        {
            secili = "kitap";
            baglanti.Open();
            string kayit = "SELECT * FROM Kitap";
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            dataGridView2.Columns[2].HeaderCell.Value = "Kullanıcı Adı";
            dataGridView2.Columns[5].HeaderCell.Value = "Stok Sayısı";
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[1].Visible = false;
            dataGridView2.Columns[3].Visible = false;
            dataGridView2.Columns[4].Visible = false;
            dataGridView2.Columns[6].Visible = false;
            dataGridView2.Columns[7].Visible = false;
            dataGridView2.Columns[8].Visible = false;
            baglanti.Close();
        }
        private void radioButtonKitap_CheckedChanged(object sender, EventArgs e)
        {
            kitapListele();            
        }
        public void bilgisayarListele()
        {
            secili = "bilgisayar";
            baglanti.Open();
            string kayit = "SELECT * FROM Bilgisayar";
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            dataGridView2.Columns[1].HeaderCell.Value = "Marka";
            dataGridView2.Columns[2].HeaderCell.Value = "Model";
            dataGridView2.Columns[3].HeaderCell.Value = "Stok Sayısı";
            dataGridView2.Columns[0].Visible = false;
            baglanti.Close();
        }
        private void radioButtonBilgisayar_CheckedChanged(object sender, EventArgs e)
        {
            bilgisayarListele();            
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            string kayit = "SELECT * FROM Kullanici where kullaniciAdi like '%"+textBox1.Text +"%'";
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string kayit = "";
            if (secili=="bilgisayar")
            {
                kayit = "SELECT * FROM Bilgisayar where marka like '%" + textBox2.Text + "%'";
            }
            else if (secili == "kitap")
            {
                kayit = "SELECT * FROM Kitap where kitapAdi like '%" + textBox2.Text + "%'";
            }
            baglanti.Open();
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            baglanti.Close();
        }
        
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            kullaniciID = dataGridView1.CurrentRow.Cells["kullaniciID"].Value.ToString();
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (secili=="kitap")
            {
                kitapID = dataGridView2.CurrentRow.Cells["kitapID"].Value.ToString();
            }
            else if (secili=="bilgisayar")
            {
                bilgisayarID = dataGridView2.CurrentRow.Cells["bilgisayarID"].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (secili == "kitap")
            {
                string kayit = "insert into KitapKayit(kullaniciID,kitapID,alisTarihi,verisTarihi) " +
                "values (@kullaniciID,@kitapID,@alisTarihi,@verisTarihi)";

                SqlCommand komut = new SqlCommand(kayit, baglanti);
                baglanti.Open();
                komut.Parameters.AddWithValue("@kullaniciID", kullaniciID);
                komut.Parameters.AddWithValue("@kitapID", kitapID);
                komut.Parameters.AddWithValue("@alisTarihi", dateTimePicker1.Value);
                komut.Parameters.AddWithValue("@verisTarihi", DateTime.Now);
                komut.ExecuteNonQuery();
                baglanti.Close();

                kayit = "SELECT stokSayisi FROM Kitap where kitapID = '" + kitapID + "'";
                SqlCommand komut2 = new SqlCommand(kayit, baglanti);

                SqlDataReader dr;
                baglanti.Open();
                dr = komut2.ExecuteReader();
                int stokSayisi=0;
                if (dr.Read())
                {
                    stokSayisi = Convert.ToInt32(dr["stokSayisi"].ToString());
                    stokSayisi--;
                }
                baglanti.Close();

                kayit = "update Kitap set stokSayisi = '" + stokSayisi.ToString() + "' where kitapID = '" + kitapID + "' "; ;
                SqlCommand komut3 = new SqlCommand(kayit, baglanti);
                baglanti.Open();
                komut3.ExecuteNonQuery();
                baglanti.Close();
                kitapListele();

                MessageBox.Show("Kitap Kayıt İşlemi Gerçekleşti.");
            }
            else if (secili == "bilgisayar")
            {
                string kayit = "insert into BilgisayarKayit(bilgisayarID,kullaniciID,alisTarihi,verisTarihi) " +
                "values (@bilgisayarID,@kullaniciID,@alisTarihi,@verisTarihi)";

                SqlCommand komut = new SqlCommand(kayit, baglanti);
                baglanti.Open();
                komut.Parameters.AddWithValue("@bilgisayarID", bilgisayarID);
                komut.Parameters.AddWithValue("@kullaniciID", kullaniciID);
                komut.Parameters.AddWithValue("@alisTarihi", dateTimePicker1.Value);
                komut.Parameters.AddWithValue("@verisTarihi", DateTime.Now);
                komut.ExecuteNonQuery();
                baglanti.Close();


                kayit = "SELECT stokSayisi FROM Bilgisayar where bilgisayarID = '" + bilgisayarID + "'";
                SqlCommand komut2 = new SqlCommand(kayit, baglanti);

                SqlDataReader dr;
                baglanti.Open();
                dr = komut2.ExecuteReader();
                int stokSayisi = 0;
                if (dr.Read())
                {
                    stokSayisi = Convert.ToInt32(dr["stokSayisi"].ToString());
                    stokSayisi--;
                }
                baglanti.Close();

                kayit = "update Bilgisayar set stokSayisi = '" + stokSayisi.ToString() + "' where bilgisayarID = '" + bilgisayarID + "' "; ;
                SqlCommand komut3 = new SqlCommand(kayit, baglanti);
                baglanti.Open();
                komut3.ExecuteNonQuery();
                baglanti.Close();
                bilgisayarListele();

                MessageBox.Show("Bilgisayar Kayıt İşlemi Gerçekleşti.");
            }

        }
    }
}
