using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HandsOnSimulation
{
    public partial class Form1 : Form
    {
        double multiplier, seed, increment, module, actuallengthperiod;
        int iterations;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<double> l1=new List<double>();
            multiplier = double.Parse(textBox1.Text);
            seed = double.Parse(textBox2.Text);
            increment = double.Parse(textBox3.Text);
            module = double.Parse(textBox4.Text);
            //actuallengthperiod = double.Parse(textBox5.Text);
            iterations = int.Parse(textBox6.Text);
            double xi = seed;
            int c = 1;
            bool ok = false;
            for(int i=0;i<iterations;i++)
            {
                
                
                double rand = (multiplier * xi + increment) % module;
                l1.Add(xi);
                xi = rand;
                if (l1[0] == rand &&i!=0)
                {
                    ok = true;
                    textBox5.Text = c.ToString();
                }
                if(ok!=true)
                {
                    c++;
                }
                
                
            }
            foreach(int i in l1)
            {
                comboBox1.Items.Add(i);
            }

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
