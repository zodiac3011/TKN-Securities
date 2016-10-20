using System;
using System.Windows.Forms;

namespace TKNSecurities
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            MessageBox.Show("This is BETA VERSION");
            this.timer1.Enabled = true;
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Close();
        }
    }
}