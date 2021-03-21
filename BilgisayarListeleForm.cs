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
    public partial class BilgisayarListeleForm : Form
    {
        string id;
        SqlConnection baglanti = new SqlConnection("server =(localdb)\\MSSQLLocalDB; Initial Catalog = Kutuphane_db; Integrated Security = True");
        public BilgisayarListeleForm()
        {
            InitializeComponent();
        }
        public void listele()
        {
            baglanti.Open();
            string kayit2 = "SELECT * FROM Bilgisayar";
            SqlCommand komut2 = new SqlCommand(kayit2, baglanti);
            SqlDataAdapter da2 = new SqlDataAdapter(komut2);
            DataTable dt = new DataTable();
            da2.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[1].HeaderCell.Value = "Marka";
            dataGridView1.Columns[2].HeaderCell.Value = "Model";
            dataGridView1.Columns[3].HeaderCell.Value = "Stok Sayısı";
            dataGridView1.Columns[0].Visible = false;
            baglanti.Close();
        }
        private void BilgisayarListeleForm_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            id = dataGridView1.CurrentRow.Cells["BilgisayarID"].Value.ToString();
            txtMarka.Text = dataGridView1.CurrentRow.Cells["marka"].Value.ToString();
            txtModel.Text = dataGridView1.CurrentRow.Cells["model"].Value.ToString();
            txtStokSayisi.Text = dataGridView1.CurrentRow.Cells["stokSayisi"].Value.ToString();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            MenuForm frm2 = new MenuForm();
            frm2.ShowDialog();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string kayit1 = "Exec bilgisayarUpdate '"+id+"','"+ txtMarka.Text + "','"+ txtModel.Text + "','"+ txtStokSayisi.Text + "'";

            SqlCommand komut1 = new SqlCommand(kayit1, baglanti);
            komut1.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Bilgisayar Güncelleme İşlemi Gerçekleşti.");
            listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string silme = "DELETE from Bilgisayar where BilgisayarID = '" + id + "'";
            SqlCommand komut = new SqlCommand(silme, baglanti);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            txtMarka.Text = "";
            txtModel.Text = "";
            txtStokSayisi.Text = "";
            listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string kayit2 = "select * from dbo.DüsükStokPc()";
            SqlCommand komut2 = new SqlCommand(kayit2, baglanti);
            SqlDataAdapter da2 = new SqlDataAdapter(komut2);
            DataTable dt = new DataTable();
            da2.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[1].HeaderCell.Value = "Marka";
            dataGridView1.Columns[2].HeaderCell.Value = "Model";
            dataGridView1.Columns[3].HeaderCell.Value = "Stok Sayısı";
            dataGridView1.Columns[0].Visible = false;
            baglanti.Close();
        }
    }
}
