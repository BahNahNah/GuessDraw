using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuessDraw.Forms
{
    public partial class frmSelection : Form
    {
        public frmSelection()
        {
            InitializeComponent();
        }

        private void Login()
        {
            string ip;
            int port = 55346;
            if (tbIP.Text.Contains(':'))
            {
                string[] temp = tbIP.Text.Split(':');
                ip = temp[0];
                if (!int.TryParse(temp[1], out port))
                {
                    MessageBox.Show("Invalid port.");
                    return;
                }
            }
            else
            {
                ip = tbIP.Text;
            }

            IPAddress ipTest;
            if (!IPAddress.TryParse(ip, out ipTest))
            {
                MessageBox.Show("Invalid IP.");
                return;
            }

            this.Hide();
            using (var frm = new frmMain(tbName.Text, ip, port))
            {
                if(frm.ShowDialog() != DialogResult.Abort)
                    Environment.Exit(0);
            }
           
        }

        private void tbConnect_Click(object sender, EventArgs e)
        {
            Login();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            using (frmServer s = new frmServer(55346))
            {
                this.Hide();
                s.ShowDialog();
            }
            Environment.Exit(0);
        }

        private void tbIP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Login();
        }

        private void frmSelection_Load(object sender, EventArgs e)
        {

        }
    }
}
