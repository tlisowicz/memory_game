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
        public Menu(Form1 form1)
        {
            InitializeComponent();
            mainWindow = form1;
            
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            this.SetDesktopLocation(Screen.PrimaryScreen.Bounds.Width / 3, Screen.PrimaryScreen.Bounds.Height / 3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int x;
            if (!int.TryParse(textBox5.Text, out x)) MessageBox.Show("wprowadzono błędną wartość", "Błąd");
            PictureBox picture = new PictureBox();
            picture.Image = Image.FromFile("C:/Users/tomek/Desktop/zyzz.bmp");
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            mainWindow.Controls.Add(picture);

            
            
        }

    }
}
