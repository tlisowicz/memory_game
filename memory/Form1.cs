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
    public partial class Form1 : Form
    {
        Menu menu;
        private List<Image> Cards;
        private String nick;
        private int rows;
        private int columns;
        private int game_time;
        private int cards_heads_time;
        private int cards_uncover_time;
        private bool can_click = true;
        private Button firstClicked, secondClicked;

        public int Game_time { get => game_time; set => game_time = value; }
        public int Cards_heads_time { get => cards_heads_time; set => cards_heads_time = value; }
        public int Cards_uncover_time { get => cards_uncover_time; set => cards_uncover_time = value; }
        public string Nick { get => nick; set => nick = value; }
        public int Rows { get => rows; set => rows = value; }
        public int Columns { get => columns; set => columns = value; }

        public Form1()
        {
            InitializeComponent();
            menu = new Menu(this);
            menu.Show();
        }

        public void start_game()
        {
            Cards = new List<Image>();
            Random random = new Random();
            string folder = "..\\..\\icons";
            int how_many_icons = Rows * Columns / 2;
            foreach (string file in Directory.EnumerateFiles(folder, "*.png"))
            {
                how_many_icons--;
                if (how_many_icons >= 0)
                {
                    Image img = Image.FromFile(file);
                    img.Tag = how_many_icons * 2;
                    Cards.Add(img);
                    Cards.Add(img);
                }
                else break;
            }
            adjust_panel(tableLayoutPanel1);

            for (int i = 0; i < Rows * Columns; ++i)
            {
                Button picture = new Button();
                picture.Size = MaximumSize;
                picture.Dock = DockStyle.Fill;
                int index = random.Next(0, Cards.Count - 1);
                picture.BackgroundImage = Cards[index];
                Cards.RemoveAt(index);
                picture.BackgroundImageLayout = ImageLayout.Stretch;
                picture.Click += new EventHandler(picture_Click);

                tableLayoutPanel1.Controls.Add(picture);
            }
            label2.Text = (5 * menu.trackBar2.Value).ToString() + " s";
            WindowState = FormWindowState.Normal;
            timer1.Start();

        }
        private void picture_Click(object sender, EventArgs e)
        {
            if (firstClicked != null && secondClicked != null)
                return;
            
            Button clickedCard = sender as Button;

            if (clickedCard == null)
                return;

            if (clickedCard.Image == null)
                return;

            if (firstClicked == null && can_click)
            {
                firstClicked = clickedCard;
                firstClicked.Image = null;
                return;
            }

            if (can_click)
            {
                secondClicked = clickedCard;
                secondClicked.Image = null;
                if (firstClicked.BackgroundImage == secondClicked.BackgroundImage)
                {
                    firstClicked = null;
                    secondClicked = null;
                }
                else
                {
                    timer3.Start();
                }
            }
        }
        private void adjust_panel(TableLayoutPanel panel)
        {
            panel.Hide();
            panel.RowStyles.Clear();
            panel.ColumnStyles.Clear();
            panel.ColumnCount = Columns;
            panel.RowCount = Rows;
            float RowHeight = panel.Height / (float)Rows;
            float ColWidth = panel.Width / (float)Columns;
            for (int i = 0; i < Rows; i++)
            {
                panel.RowStyles.Add(new RowStyle(SizeType.Absolute, RowHeight));
            }

            for (int j = 0; j < Columns; j++)
            {
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ColWidth));
            }
            panel.Show();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Czy napewno chcesz zakończyć?", "Zamykanie", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.SetDesktopLocation(Screen.PrimaryScreen.Bounds.Width / 3, Screen.PrimaryScreen.Bounds.Height / 3);
            this.WindowState = FormWindowState.Minimized;
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Cards_heads_time != 0)
            {
                label1.Text = "Widoczność odwróconcyh kart:";
                Cards_heads_time--;
                label2.Text = Cards_heads_time.ToString() + " s";
            }
            else
            {
                label1.TextAlign = ContentAlignment.TopRight;
                label1.Text = "Pozostały czas:";
                foreach (Button control in tableLayoutPanel1.Controls)
                {
                    var bmp = new Bitmap(78, 78);
                    control.Image = Image.FromFile("..\\..\\white.png");
                }
                timer1.Stop();
                timer2.Start();
            }
        }

        private void label1_TextChanged(object sender, EventArgs e)
        {
            label1.TextAlign = ContentAlignment.TopRight;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (timer2.Enabled)
            {
                if (timer3.Enabled)
                {
                    timer3.Stop();
                }
                timer2.Stop();
                button1.Text = "Wznów";
                can_click = false;
            }
            else if (!timer1.Enabled)
            {
                if (!timer3.Enabled)
                {
                    timer3.Start();
                }
                timer2.Start();
                button1.Text = "Zatrzymaj";
                can_click = true;
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            trackBar1.Value = menu.trackBar3.Value;
            label4.Text = trackBar1.Value + " s";
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Cards_uncover_time = trackBar1.Value;

            label4.Text = trackBar1.Value + " s";
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            cards_uncover_time--;
            if (cards_uncover_time == 0)
            {
                timer3.Stop();
                firstClicked.Image = Image.FromFile("..\\..\\white.png");
                secondClicked.Image = Image.FromFile("..\\..\\white.png");
                firstClicked = null;
                secondClicked = null;
                can_click = true;
                cards_uncover_time = trackBar1.Value;
            }
            else
            {
                can_click = false;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Game_time--;
            if (Game_time == 0)
            {
                timer2.Stop();
                if (MessageBox.Show("Koniec czasu! Chcesz zagrać jeszcze raz?", "Niepowodzenie", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    menu = new Menu(this);
                    menu.Show();
                    foreach (Button button in tableLayoutPanel1.Controls)
                    {
                        button.Image = null;
                    }

                    this.WindowState = FormWindowState.Minimized;
                } else
                {
                    Environment.Exit(0);
                }
            }
            label2.Text = Game_time.ToString() + " s";
        }
    }
}
