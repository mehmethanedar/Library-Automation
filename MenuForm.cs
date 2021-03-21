using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VeritabaniProje
{
    public partial class MenuForm : Form
    {

        public GirisForm frm1;
        public MenuForm()
        {
            InitializeComponent();
        }

        private void cikisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnPersonelKayit_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            PersonelEklemeForm frm3 = new PersonelEklemeForm();
            frm3.ShowDialog();
        }

        private void btnKitapkayit_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            KitapEklemeForm frm4 = new KitapEklemeForm();
            frm4.ShowDialog();
        }

        private void btnOkuyucukayit_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            UyeEklemeForm frm5 = new UyeEklemeForm();
            frm5.ShowDialog();
        }

        private void btnBilgisayarKayit_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            BilgisayarEklemeForm frm6 = new BilgisayarEklemeForm();
            frm6.ShowDialog();
        }

        private void btnOduncVerme_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            OduncVerForm oduncVerForm = new OduncVerForm();
            oduncVerForm.ShowDialog();
        }

        private void btnKitapliste_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            KitapListeleForm kitapListeleForm = new KitapListeleForm();
            kitapListeleForm.ShowDialog();
        }

        private void btnPersonelListele_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            PersonelListeleForm personelListeleForm = new PersonelListeleForm();
            personelListeleForm.ShowDialog();
        }

        private void btnOkuyuculiste_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            UyeListeleForm uyeListeleForm = new UyeListeleForm();
            uyeListeleForm.ShowDialog();
        }

        private void btnBilgisayarListele_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            BilgisayarListeleForm bilgisayarListeleForm = new BilgisayarListeleForm();
            bilgisayarListeleForm.ShowDialog();
        }

        private void btnOduncVerilenler_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            OduncVerilenlerForm oduncVerilenlerForm = new OduncVerilenlerForm();
            oduncVerilenlerForm.ShowDialog();
        }
    }
}
