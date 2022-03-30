using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace memory
{
    public partial class Menu : Form
    {
        Form1 mainWindow;
        private int trackbar1_previous_val;
        private int trackbar4_previous_val;
        public Menu(Form1 form1)
        {
            InitializeComponent();
            mainWindow = form1;
            
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            this.SetDesktopLocation(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2);

            //initiating labels' text
            label6.Text = (30 * trackBar1.Value).ToString() + " s";
            label7.Text = (5 * trackBar2.Value).ToString() + " s";
            label8.Text = (0.25 * trackBar3.Value).ToString() + " s";

            //initiating variables
            mainWindow.Game_time = int.Parse(label6.Text.Split(' ')[0]);
            mainWindow.Cards_heads_time = float.Parse(label8.Text.Split(' ')[0]);
            mainWindow.Cards_uncover_time = int.Parse(label7.Text.Split(' ')[0]);
            mainWindow.Cards_number = int.Parse(label10.Text);
            trackbar1_previous_val = trackBar1.Value;
            trackbar4_previous_val = trackBar4.Value;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainWindow.start_game();
            this.Close();
        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label6.Text = (30* trackBar1.Value).ToString() + " s";
            mainWindow.Game_time = int.Parse(label6.Text.Split(' ')[0]);

            if (trackBar1.Value - trackbar1_previous_val >= 0)
            {
                label11.Text = mainWindow.Score_multiplier - (float)1 / 7 + "X";
            }
            else
            {
                label11.Text = mainWindow.Score_multiplier + (float)1 / 5 + "X";
            }
            mainWindow.Score_multiplier = float.Parse(label11.Text.Split('X')[0]);
            trackbar1_previous_val = trackBar1.Value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label7.Text = (5 * trackBar2.Value).ToString() + " s";
            mainWindow.Cards_uncover_time = int.Parse(label7.Text.Split(' ')[0]);
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            label8.Text = (0.25 * trackBar3.Value).ToString() + " s";
            mainWindow.Cards_heads_time = float.Parse(label8.Text.Split(' ')[0]);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            mainWindow.Nickname = textBox3.Text;
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            if (trackBar4.Value - trackbar4_previous_val >= 0)
            {
                label11.Text = mainWindow.Score_multiplier + (float)1/5 + "X";
            }
            else
            {
                label11.Text = mainWindow.Score_multiplier - (float)1/ 5 + "X";
            }
            label10.Text = (12*trackBar4.Value).ToString();
            mainWindow.Cards_number = int.Parse(label10.Text);

            mainWindow.Score_multiplier = float.Parse(label11.Text.Split('X')[0]);
            trackbar4_previous_val = trackBar4.Value;
        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
