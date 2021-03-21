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
    public partial class KitapListeleForm : Form
    {
        string id = "";
        SqlConnection baglanti = new SqlConnection("server =(localdb)\\MSSQLLocalDB; Initial Catalog = Kutuphane_db; Integrated Security = True");
        public KitapListeleForm()
        {
            InitializeComponent();
        }
        public void listele()
        {
            baglanti.Open();
            string kayit = "SELECT * FROM Kitap";
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[1].HeaderCell.Value = "ISBN";
            dataGridView1.Columns[2].HeaderCell.Value = "Kitap Adı";
            dataGridView1.Columns[3].HeaderCell.Value = "YayınEvi Adı";
            dataGridView1.Columns[4].HeaderCell.Value = "Yazar Adı";
            dataGridView1.Columns[5].HeaderCell.Value = "Stok Sayısı";
            dataGridView1.Columns[6].HeaderCell.Value = "Basım Tarihi";
            dataGridView1.Columns[7].HeaderCell.Value = "Cilt No";
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            baglanti.Close();
        }

        private void KitapListeleForm_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            id = dataGridView1.CurrentRow.Cells["kitapID"].Value.ToString();
            txtIsbn.Text = dataGridView1.CurrentRow.Cells["isbn"].Value.ToString();
            txtKitapAdi.Text = dataGridView1.CurrentRow.Cells["kitapAdi"].Value.ToString();
            txtYayinEvi.Text = dataGridView1.CurrentRow.Cells["yayinEviAdi"].Value.ToString();
            txtYazarAdi.Text = dataGridView1.CurrentRow.Cells["yazarAdi"].Value.ToString();
            txtStokSayisi.Text = dataGridView1.CurrentRow.Cells["stokSayisi"].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells["basimTarihi"].Value.ToString();
            txtCiltNo.Text = dataGridView1.CurrentRow.Cells["ciltNo"].Value.ToString();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string kayit = "update Kitap set isbn=@isbn,kitapAdi=@kitapAdi,yayinEviAdi=@yayinEviAdi,yazarAdi=@yazarAdi,stokSayisi=@stokSayisi,basimTarihi=@basimTarihi,ciltNo=@ciltNo where kitapID = '"+id+"'";

            SqlCommand komut = new SqlCommand(kayit, baglanti);
            komut.Parameters.AddWithValue("@isbn", txtIsbn.Text);
            komut.Parameters.AddWithValue("@kitapAdi", txtKitapAdi.Text);
            komut.Parameters.AddWithValue("@yayinEviAdi", txtYayinEvi.Text);
            komut.Parameters.AddWithValue("@yazarAdi", txtYazarAdi.Text);
            komut.Parameters.AddWithValue("@stokSayisi", txtStokSayisi.Text);
            komut.Parameters.AddWithValue("@basimTarihi", dateTimePicker1.Value);
            komut.Parameters.AddWithValue("@ciltNo", txtCiltNo.Text);

            komut.ExecuteNonQuery();
            baglanti.Close();
            listele();
            MessageBox.Show("Kitap Düzenleme İşlemi Gerçekleşti.");
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            MenuForm frm2 = new MenuForm();
            frm2.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string silme = "DELETE from Kitap where kitapID = '"+id+"'";
            SqlCommand komut = new SqlCommand(silme, baglanti);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            listele();
            txtIsbn.Text = "";
            txtKitapAdi.Text = "";
            txtYayinEvi.Text = "";
            txtYazarAdi.Text = "";
            txtStokSayisi.Text = "";
            dateTimePicker1.Text = "";
            txtCiltNo.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string kayit = "select * from dbo.DüsükStok()";
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[1].HeaderCell.Value = "ISBN";
            dataGridView1.Columns[2].HeaderCell.Value = "Kitap Adı";
            dataGridView1.Columns[3].HeaderCell.Value = "YayınEvi Adı";
            dataGridView1.Columns[4].HeaderCell.Value = "Yazar Adı";
            dataGridView1.Columns[5].HeaderCell.Value = "Stok Sayısı";
            dataGridView1.Columns[6].HeaderCell.Value = "Basım Tarihi";
            dataGridView1.Columns[7].HeaderCell.Value = "Cilt No";
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            baglanti.Close();
        }
    }
}
