using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Proyecto_DB
{
    public partial class Info : Form
    {
        public Info()
        {
            InitializeComponent();
        }

        private void Info_Load(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void btnGithub_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/Rodas171315/PTI_5-DylanR-Control-de-Horas");
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            Process.Start("mailto:dylang007@gmail.com");
        }

        private void btnFB_Click(object sender, EventArgs e)
        {
            Process.Start("https://facebook.com/dylan.rodas.512");
        }

        private void btnTwitter_Click(object sender, EventArgs e)
        {
            Process.Start("https://twitter.com/ManiacalKnight9");
        }

        private void btnInstagram_Click(object sender, EventArgs e)
        {
            Process.Start("https://instagram.com/maniacalknight9/");
        }

        private void btnSpartan_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/Rodas171315");
        }
    }
}
