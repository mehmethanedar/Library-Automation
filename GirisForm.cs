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
    public partial class GirisForm : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        public MenuForm frm2;
        public GirisForm()
        {
            InitializeComponent();
            frm2 = new MenuForm();
            frm2.frm1 = this;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {        
            string user = textBox1.Text;
            string pass = textBox2.Text;
            con = new SqlConnection("server =(localdb)\\MSSQLLocalDB; Initial Catalog = Kutuphane_db; Integrated Security = True");
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM Kullanici where kullaniciAdi='" + textBox1.Text + "' AND sifre='" + textBox2.Text + "'" +
                " AND (kullaniciTur= 'personel' OR kullaniciTur ='admin')";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                this.Visible = false;
                frm2.ShowDialog();
            }
            else
            {
                MessageBox.Show("Kullanıcı adını ve şifrenizi kontrol ediniz.");
            }
            con.Close();

            
            //baglanti = new SqlConnection("server =(localdb)\\MSSQLLocalDB; Initial Catalog = Kutuphane_db; Integrated Security = True");
            //string sorgu = "Insert into Personel (kullaniciID,kullaniciAdi,sifre) values (@ad,@sifre,@sifre2)";
            //komut = new SqlCommand(sorgu, baglanti);
            //komut.Parameters.AddWithValue("@ad", 1);
            //komut.Parameters.AddWithValue("@sifre", textBox2.Text);
            //komut.Parameters.AddWithValue("@sifre2", textBox2.Text);
            //baglanti.Open();
            //komut.ExecuteNonQuery();
            //baglanti.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
    }
}
