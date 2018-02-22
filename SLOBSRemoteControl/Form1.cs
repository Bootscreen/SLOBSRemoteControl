using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SLOBSRemoteControl.SLOBSAPI_Scene;

namespace SLOBSRemoteControl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.named_pipe = textBox1.Text;
            Properties.Settings.Default.hide_on_start = checkBox1.Checked;
            Properties.Settings.Default.Save();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = Properties.Settings.Default.named_pipe;
            checkBox1.Checked = Properties.Settings.Default.hide_on_start;
        }
    }
}
