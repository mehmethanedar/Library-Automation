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
    public partial class BilgisayarEklemeForm : Form
    {
        public BilgisayarEklemeForm()
        {
            InitializeComponent();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            MenuForm frm2 = new MenuForm();
            frm2.ShowDialog();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection("server =(localdb)\\MSSQLLocalDB; Initial Catalog = Kutuphane_db; Integrated Security = True");

            //string kayit3 = "insert into Bilgisayar(marka,model,stokSayisi) " +
            //    "values (@marka,@model,@stokSayisi)";

            //SqlCommand komut = new SqlCommand(kayit3, baglanti);
            //baglanti.Open();
            //komut.Parameters.AddWithValue("@marka", txtMarka.Text);
            //komut.Parameters.AddWithValue("@model", txtModel.Text);
            //komut.Parameters.AddWithValue("@stokSayisi", txtStokSayisi.Text);
            //komut.ExecuteNonQuery();

            //baglanti.Close();



            string kayit3 = "Exec bilgisayarEkle '"+ txtMarka.Text + "','" + txtModel.Text + "','" + txtStokSayisi.Text + "'";

            SqlCommand komut = new SqlCommand(kayit3, baglanti);
            baglanti.Open();
            komut.ExecuteNonQuery();

            baglanti.Close();



            MessageBox.Show("Bilgisayar Kayıt İşlemi Gerçekleşti.");
        }
    }
}
