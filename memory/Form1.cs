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
        private int x;
        private int y;
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
    }
}
