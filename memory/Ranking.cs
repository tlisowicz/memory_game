using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace memory
{
    public partial class Ranking : Form
    {
        Form1 mainWindow;
        private bool ranking_window_closing = true;
        public Ranking(Form1 mainWindow)
        {
            this.mainWindow = mainWindow;
            InitializeComponent();
        }

        private void fill_labes()
        {
            List<string> lines = new List<string>(File.ReadAllLines("..\\..\\wyniki.txt"));
            label5.Text = lines[0] + " pkt";
            label6.Text = lines[1] + " pkt";
            label7.Text = lines[2] + " pkt";
            label8.Text = lines[3] + " pkt";
        }
        private void Ranking_Load(object sender, EventArgs e)
        {
            this.SetDesktopLocation(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2);
            fill_labes();
        }

        private void Ranking_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ranking_window_closing == true)
            {
                if (MessageBox.Show("Chcesz zagrać jeszcze raz?", "Zamykanie", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    mainWindow.Is_restart = true;
                    ranking_window_closing = false;
                    Application.Restart();
                    
                }
                else
                {
                    e.Cancel = false;
                    mainWindow.Is_restart = true;
                    mainWindow.Close();
                }
            }              
        }
    }
}
