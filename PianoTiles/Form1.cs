using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PianoTiles
{
    public partial class Form1 : Form
    {
        public int[,] map = new int[8, 4];
        public int cellWidth = 50;
        public int cellHeight = 80;

        public Form1()
        {
            InitializeComponent();

            this.Text = "Piano";
            this.Width = cellWidth * 4 + 15;
            this.Height = cellHeight * 8 + 40;
            this.Paint += new PaintEventHandler(Repaint);
            this.KeyUp += new KeyEventHandler(OnKeyboardPressed);
            Init();
        }

        private void OnKeyboardPressed(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "D1":
                    CheckForPressedButton(0);
                    break;
                case "D2":
                    CheckForPressedButton(1);
                    break;
                case "D3":
                    CheckForPressedButton(2);
                    break;
                case "D4":
                    CheckForPressedButton(3);
                    break;
            }
        }

        public void CheckForPressedButton(int i)
        {
            if (map[7, i] != 0)
            {
                MoveMap();
                PlaySound(i);
            }
            else
            {
                MessageBox.Show("You lost!");
                Init();
            }
        }

        public void PlaySound(int sound)
        {
            System.IO.Stream str = null;
            switch (sound)
            {
                case 0:
                    str = Properties.Resources.g6;
                    break;
                case 1:
                    str = Properties.Resources.f6;
                    break;
                case 2:
                    str = Properties.Resources.d6;
                    break;
                case 3:
                    str = Properties.Resources.e6;
                    break;
            }
            System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
            snd.Play();
        }

        public void MoveMap()
        {
            for(int i = 7; i > 0; i--)
            {
                for(int j = 0; j < 4; j++)
                {
                    map[i, j] = map[i - 1, j];
                }
            }
            AddNewLine();
            Invalidate();
        }

        public void AddNewLine()
        {
            Random r = new Random();
            int j = r.Next(0, 4);
            for (int k = 0; k < 4; k++)
                map[0, k] = 0;
            map[0, j] = 1;
        }

        public void Init()
        {
            ClearMap();
            GenerateMap();
            Invalidate();
        }

        public void ClearMap()
        {
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    map[i, j] = 0;
                }
            }
        }

        public void GenerateMap()
        {
            Random r = new Random();
            for(int i = 0; i < 8; i++)
            {
                int j = r.Next(0, 4);
                map[i, j] = 1;
            }
        }

        public void DrawMap(Graphics g)
        {
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    if(map[i,j] == 0)
                    {
                        g.FillRectangle(new SolidBrush(Color.White), cellWidth * j, cellHeight * i, cellWidth, cellHeight);
                    }
                    if(map[i,j] == 1)
                    {
                        g.FillRectangle(new SolidBrush(Color.Black), cellWidth * j, cellHeight * i, cellWidth, cellHeight);
                    }
                }
            }
            for(int i = 0; i < 8; i++)
            {
                g.DrawLine(new Pen(new SolidBrush(Color.Black)), 0, i * cellHeight, 4 * cellWidth, i * cellHeight);
            }
            for(int i = 0; i < 4; i++)
            {
                g.DrawLine(new Pen(new SolidBrush(Color.Black)), i * cellWidth, 0, i * cellWidth, 8 * cellHeight);
            }
        }

        private void Repaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawMap(g);
        }
    }
}
