using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmallWorld
{
    public partial class GUI : Form
    {
        public GUI()
        {
            InitializeComponent();
        }

        private void Run_Click(object sender, EventArgs e)
        {
            timer1.Start();
            Normal.Run(int.Parse(TestCase.Text));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time.Text = timer1.Interval.ToString();
        }
    }
}
