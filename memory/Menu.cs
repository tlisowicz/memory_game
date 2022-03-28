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
        private int columns;
        private int rows;

        public Menu(Form1 form1)
        {
            InitializeComponent();
            mainWindow = form1;
            
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            this.SetDesktopLocation(Screen.PrimaryScreen.Bounds.Width / 3, Screen.PrimaryScreen.Bounds.Height / 3);

            //initiating labels' text
            label6.Text = (30 * trackBar1.Value).ToString() + " s";
            label7.Text = (5 * trackBar2.Value).ToString() + " s";
            label8.Text = (trackBar3.Value).ToString() + " s";

            //initiating time variables
            mainWindow.Game_time = int.Parse(label6.Text.Split(' ')[0]);
            mainWindow.Cards_heads_time = int.Parse(label7.Text.Split(' ')[0]);
            mainWindow.Cards_uncover_time = int.Parse(label8.Text.Split(' ')[0]);
            rows = 6;
            columns = 8;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pass_Values(columns, rows);
            mainWindow.start_game();
            this.Close();
        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label6.Text = (30* trackBar1.Value).ToString() + " s";
            mainWindow.Game_time = int.Parse(label6.Text.Split(' ')[0]);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label7.Text = (5 * trackBar2.Value).ToString() + " s";
            mainWindow.Cards_heads_time = int.Parse(label7.Text.Split(' ')[0]);
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            label8.Text = (trackBar3.Value).ToString() + " s";
            mainWindow.Cards_uncover_time = int.Parse(label8.Text.Split(' ')[0]);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out rows) == false || rows > 10)
            {
                rows = 6;
                MessageBox.Show("Podano nieprawidłową wartość");
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox2.Text, out columns) == false || columns > 12)
            {
                columns = 8;
                MessageBox.Show("Podano nieprawidłową wartość");
            }
        }

        private void pass_Values(int col, int row)
        {
            mainWindow.Rows = row;
            mainWindow.Columns = col;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            mainWindow.Nick = textBox3.Text;
        }
    }
}
