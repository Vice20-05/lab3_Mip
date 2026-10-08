using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab3_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //back but
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
           
        }

        //home
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            webBrowser1.GoHome();

        }


        //forward
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }


        //right
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
        }

        private void toolStripTextBox1_Click(object sender,EventArgs e)
        {

        }


        private void toolStripButton4_Click(object sender, EventArgs e)
        {
           
        }
    }
}
