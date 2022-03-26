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
        private int width;
        private int height;
        public int game_time;
        public int cards_heads_time;
        public int cards_uncover_time;
        
        public Menu(Form1 form1)
        {
            InitializeComponent();
            mainWindow = form1;
            
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            this.SetDesktopLocation(Screen.PrimaryScreen.Bounds.Width / 3, Screen.PrimaryScreen.Bounds.Height / 3);
            label6.Text = (30 * trackBar1.Value).ToString() + " s";
            label7.Text = (5 * trackBar2.Value).ToString() + " s";
            label8.Text = (trackBar3.Value).ToString() + " s";
            game_time = int.Parse(label6.Text.Split(' ')[0]);
            cards_heads_time = int.Parse(label7.Text.Split(' ')[0]);
            cards_uncover_time = int.Parse(label8.Text.Split(' ')[0]);
            width = 6;
            height = 8;
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            for (int i = 0; i< width*height; ++i)
            {
                PictureBox picture = new PictureBox();
                //picture.Size = new System.Drawing.Size(50, 20);
                picture.Image = Image.FromFile("C:/Users/tomek/Desktop/zyzz.bmp");
                picture.SizeMode = PictureBoxSizeMode.StretchImage;
                mainWindow.flowLayoutPanel1.Controls.Add(picture);
            }
            mainWindow.label2.Text = (5 * trackBar2.Value).ToString() + " s";
            mainWindow.WindowState = FormWindowState.Normal;
            mainWindow.timer1.Start();
            this.Close();


        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label6.Text = (30* trackBar1.Value).ToString() + " s";
            game_time = int.Parse(label6.Text.Split(' ')[0]);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label7.Text = (5 * trackBar2.Value).ToString() + " s";
            cards_heads_time = int.Parse(label7.Text.Split(' ')[0]);
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            label8.Text = (trackBar3.Value).ToString() + " s";
            cards_uncover_time = int.Parse(label8.Text.Split(' ')[0]);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out width) == false || width > 10)
            {
                width = 6;
                MessageBox.Show("Podano nieprawidłową wartość");
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox2.Text, out height) == false || height > 12)
            {
                height = 8;
                MessageBox.Show("Podano nieprawidłową wartość");
            }
        }
    }
}
