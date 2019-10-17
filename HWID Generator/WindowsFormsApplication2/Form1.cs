using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HWID_Generator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = FingerPrint.Value();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
                Clipboard.SetText(textBox1.Text);
        }
    }
}
