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
    public partial class KitapEklemeForm : Form
    {
        string kategoriID = "";
        SqlConnection baglanti = new SqlConnection("server =(localdb)\\MSSQLLocalDB; Initial Catalog = Kutuphane_db; Integrated Security = True");
        public KitapEklemeForm()
        {
            InitializeComponent();
        }
        
        private void btnCikis_Click_1(object sender, EventArgs e)
        {
            this.Visible = false;
            MenuForm frm2 = new MenuForm();
            frm2.ShowDialog();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string kayit = "insert into Kitap(isbn,kitapAdi,yayinEviAdi,yazarAdi,stokSayisi,basimTarihi,ciltNo,kategoriID) " +
                "values (@isbn,@kitapAdi,@yayinEviAdi,@yazarAdi,@stokSayisi,@basimTarihi,@ciltNo,@kategoriID)";

            SqlCommand komut = new SqlCommand(kayit, baglanti);
            komut.Parameters.AddWithValue("@isbn", txtIsbn.Text);
            komut.Parameters.AddWithValue("@kitapAdi", txtKitapAdi.Text);
            komut.Parameters.AddWithValue("@yayinEviAdi", txtYayinEvi.Text);
            komut.Parameters.AddWithValue("@yazarAdi", txtYazarAdi.Text);
            komut.Parameters.AddWithValue("@stokSayisi", txtStokSayisi.Text);
            komut.Parameters.AddWithValue("@basimTarihi", dateTimePicker1.Value);
            komut.Parameters.AddWithValue("@ciltNo", txtCiltNo.Text);
            komut.Parameters.AddWithValue("@kategoriID", Convert.ToInt32(kategoriID));

            komut.ExecuteNonQuery();

            baglanti.Close();
            MessageBox.Show("Kitap Kayıt İşlemi Gerçekleşti.");
        }

        private void comboBoxKitapTuru_SelectedIndexChanged(object sender, EventArgs e)
        {
            string kayit = "SELECT kategoriID FROM Kategori where kategoriAdi = '" + comboBoxKitapTuru.Text + "'";
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            SqlDataReader dr;
            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                kategoriID = dr["kategoriID"].ToString();
            }
            baglanti.Close();
        }

        private void KitapEklemeForm_Load(object sender, EventArgs e)
        {
            string kayit = "SELECT *FROM Kategori";
            SqlCommand komut = new SqlCommand(kayit, baglanti);

            SqlDataReader dr;
            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBoxKitapTuru.Items.Add(dr["kategoriAdi"]);
            }
            baglanti.Close();
        }
    }
}
