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
    public partial class Form1 : Form
    {
        Menu menu;
        public Form1()
        {
            InitializeComponent();
            menu = new Menu(this);
            menu.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.SetDesktopLocation(Screen.PrimaryScreen.Bounds.Width / 3, Screen.PrimaryScreen.Bounds.Height / 3);
            this.WindowState = FormWindowState.Minimized;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (menu.cards_heads_time != 0)
            {
                label1.Text = "Widoczność odwróconcyh kart:";
                menu.cards_heads_time--;
                label2.Text = menu.cards_heads_time.ToString() + " s";
            }
            else
            { 
                label1.Text = "Pozostały czas:";
                menu.game_time--;
                if (menu.game_time ==0)
                {
                    timer1.Stop();
                }
                label2.Text = menu.game_time.ToString() + " s";

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Czy napewno chcesz zakończyć?", "Zamykanie", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void label1_TextChanged(object sender, EventArgs e)
        {
            label1.TextAlign = ContentAlignment.TopRight;
        }
    }
}
