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
using SmallWorld;
namespace WFA
{
    public partial class Form1 : Form
    {
        Stopwatch stopwatch;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "Time: ";
            stopwatch = new Stopwatch();
            stopwatch.Start();
            Normal.Run(int.Parse(comboBox1.Text));
            stopwatch.Stop();
            label1.Text += (stopwatch.ElapsedMilliseconds/1000).ToString();
            label1.Text += " s";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedItem = "1";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = (stopwatch.ElapsedMilliseconds/1000).ToString();
        }
    }
}
