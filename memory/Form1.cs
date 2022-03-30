using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace memory
{
    public partial class Form1 : Form
    {
        Menu menu;
        Ranking ranking;
        private List<Image> Cards;
        private String nickname;
        private int cards_number;
        private int game_time;
        private float cards_heads_time;
        private int cards_uncover_time;
        private int pairs_left;
        private Button firstClicked, secondClicked;
        private bool can_click = true;
        private float score_multiplier = 1.0F;
        private bool is_restart = false;
        private bool timer3_was_enabled = false;

        public int Game_time { get => game_time; set => game_time = value; }
        public float Cards_heads_time { get => cards_heads_time; set => cards_heads_time = value; }
        public int Cards_uncover_time { get => cards_uncover_time; set => cards_uncover_time = value; }
        public string Nickname { get => nickname; set => nickname = value; }
        public int Cards_number { get => cards_number; set => cards_number = value; }
        public float Score_multiplier { get => score_multiplier; set => score_multiplier = value; }
        public int Pairs_left { get => pairs_left; set => pairs_left = value; }
        public bool Is_restart { get => is_restart; set => is_restart = value; }

        public Form1()
        {
            InitializeComponent();
            menu = new Menu(this);
            ranking = new Ranking(this);
            menu.Show();
        }

        public void start_game()
        {
            load_cards_images();
            adjust_panel(tableLayoutPanel1);
            generate_cards();

            Nickname = menu.textBox3.Text;
            label2.Text = (5 * menu.trackBar2.Value).ToString() + " s";
            WindowState = FormWindowState.Normal;
            timer1.Start();

        }

        private void load_cards_images()
        {
            Cards = new List<Image>();
            string folder = "..\\..\\icons";
            int how_many_icons = cards_number / 2;
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
        }

        private void generate_cards()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            pairs_left = Cards.Count / 2;
            for (int i = 0; i < Cards_number; ++i)
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
                    --pairs_left;
                    Game_time += 3;
                    if (pairs_left == 0)
                    {
                        timer2.Stop();
                        save_score((int)(Score_multiplier * (float)Game_time),"..\\..\\wyniki.txt");
                        ranking.label9.Text = "ZWYCIĘSTWO!";
                        ranking.Show();
                    }
                    firstClicked = null;
                    secondClicked = null;
                }
                else
                {
                    Game_time -= 2;
                    timer3.Start();
                }
            }
        }
        private void adjust_panel(TableLayoutPanel panel)
        {
            panel.Hide();
            panel.RowStyles.Clear();
            panel.ColumnStyles.Clear();
            panel.ColumnCount = get_columns_rows_num()[0];
            panel.RowCount = get_columns_rows_num()[1];
            float RowHeight = panel.Height / (float)panel.RowCount;
            float ColWidth = panel.Width / (float)panel.ColumnCount;
            for (int i = 0; i < get_columns_rows_num()[1]; i++)
            {
                panel.RowStyles.Add(new RowStyle(SizeType.Absolute, RowHeight));
            }

            for (int j = 0; j < get_columns_rows_num()[0]; j++)
            {
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ColWidth));
            }
            panel.Show();
        }

        private int[] get_columns_rows_num()
        {
            switch (menu.trackBar4.Value)
            {
                case 4:
                    return new int[2] { 8, 6 };

                case 5:
                    return  new int[2] { 10, 6 };

                case 6:
                    return new int[2] { 9, 8 };

                case 7:
                    return new int[2] { 12, 7 };

                case 8:
                    return new int[2] { 12, 8 };

                case 9:
                    return new int[2] { 12, 9 };

                case 10:
                    return new int[2] { 12, 10 };

                default:
                    return new int[2] { 8, 6 };

            }
        }

        private void save_score(int scores, string path)
        {
            if (!File.Exists(path))
            {
                StreamWriter sw = File.CreateText(path);
                sw.WriteLine("Wojtek 20");
                sw.WriteLine("Tomek 10");
                sw.WriteLine("Tomek 5");
                sw.WriteLine("Tomek 5");
                sw.Close();
            }
            List<string> lines = new List<string>(File.ReadAllLines(path));

            foreach (string s in lines)
            {
                int value = int.Parse(s.Split(' ')[1]);
                if (value <= scores)
                {
                    lines[lines.IndexOf(s)] = Nickname + " " + scores;
                    break;
                }
            }
            lines[lines.Count-1] = Nickname + " " + scores;
            File.WriteAllLines(path, lines);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Is_restart == false)
            {
                DialogResult response = MessageBox.Show("Czy napewno chcesz zakończyć?", "Zamykanie", MessageBoxButtons.OKCancel);
                if (response == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            this.WindowState = FormWindowState.Minimized;
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Cards_uncover_time != 0)
            {
                label1.Text = "Widoczność odwróconcyh kart:";
                Cards_uncover_time--;
                label2.Text = Cards_uncover_time.ToString() + " s";
            }
            else
            {
                label1.TextAlign = ContentAlignment.TopRight;
                label1.Text = "Pozostały czas:";
                foreach (Button control in tableLayoutPanel1.Controls)
                {
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
            //freezing
            if (timer2.Enabled)
            {
                if (timer3.Enabled)
                {
                    timer3.Stop();
                    timer3_was_enabled = true;
                }
                timer2.Stop();
                button1.Text = "Wznów";
                can_click = false;
            }

            //unfreezing
            else if (!timer1.Enabled && !timer2.Enabled)
            {
                if (!timer3.Enabled && timer3_was_enabled)
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
            label4.Text = 0.25 * trackBar1.Value + " s";
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Cards_heads_time = 0.25f * trackBar1.Value;

            label4.Text = 0.25f *trackBar1.Value + " s";
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            Cards_heads_time -= 0.25f;
            if (cards_heads_time == 0)
            {
                timer3.Stop();
                firstClicked.Image = Image.FromFile("..\\..\\white.png");
                secondClicked.Image = Image.FromFile("..\\..\\white.png");
                firstClicked = null;
                secondClicked = null;
                can_click = true;
                cards_heads_time = 0.25f * trackBar1.Value;
            }
            else
            {
                can_click = false;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Game_time--;
            if (Game_time <= 0)
            {
                timer2.Stop();
                ranking.label9.Text = "NIEPOWODZENIE!\nKoniec czasu";
                ranking.Show();

            }
            label2.Text = Game_time.ToString() + " s";
        }
    }
}
