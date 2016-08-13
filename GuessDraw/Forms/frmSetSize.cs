using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuessDraw.Forms
{
    public partial class frmSetSize : Form
    {
        public int SetSize { get; private set; }
        public frmSetSize(Color c, int _size)
        {
            InitializeComponent();


            SetSize = _size;
            brushPreview1.SetColor(c);
            brushPreview1.SetSize(SetSize);
            trackBar1.Value = SetSize;
        }

        private void frmSetSize_Load(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            SetSize = trackBar1.Value;
            brushPreview1.SetSize(SetSize);
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
