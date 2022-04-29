using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Макет_курсовой
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pole p = new pole(pictureBox2, pictureBox2.Width, pictureBox2.Height);
        }
        public Victim[] victims = new Victim[5];
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                victims[i] = new Victim();
            }
            pole.loadGrid();
            //выключение кнопки после отрисовки поля
            button1.Enabled = false;

        }
        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
            //pole.drawLudi();
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            //вывод пользовательского значения жертв
            ric.kolvoj = Convert.ToInt32(numericUpDown1.Value);
            // временная штука для вывода количества
            label2.Text = Convert.ToString(ric.kolvoj);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //очистка поля (типо обновление)
            pole.gr.Clear(Color.White);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                victims[i].MoveVictim();
            }
            pictureBox2.Image = pole.bmp;
        }
    }
    class ric
    {
        //статические значения для поля (заменить поле потом)
        //переменная для количества жертв
        public static int kolvoj;
        public static Pen pole = new Pen(Color.LawnGreen);
        public static Random random = new Random();
        public static Pen jertvi = new Pen(Color.Black);
        public static Pen whitePen = new Pen(Color.White);

    }
    class pole
    {
        public static int width = Screen.PrimaryScreen.Bounds.Width;
        public static int height = Screen.PrimaryScreen.Bounds.Height;
        public static PictureBox pic = new PictureBox();
        public static Bitmap bmp = new Bitmap(width, height);
        public static Graphics gr = Graphics.FromImage(bmp);

        //public static int[,] MatricaPolia = new int[width,height];

        public pole(PictureBox pic, int width, int height)
        {
            pole.pic = pic;
            pole.width = width;
            pole.height = height;

        }
        public static void loadGrid()
        {
            for (int i = 0; i < 1500/5; i++)
            {
                gr.DrawLine(ric.pole, i * 5, 0, i * 5, 880);
            }
            //ширина
            for (int i = 0; i < 880/5; i++)
            {
                gr.DrawLine(ric.pole, 0, i * 5, 1500, i * 5);
            }
            pic.Image = bmp;
        }
    }
    public class Victim
    {
        
        public int randStartPosX = ric.random.Next(1300);
        public int randStartPosY = ric.random.Next(1000);

        public int x = 0;
        public int y = 0;

        public static int r = pole.width;
        public static int c = pole.height;
        public int[,] mainMatrix = new int[2000,2000];

        public void MoveVictim()
        {
            int moveRotate = ric.random.Next(0,5);
            if(moveRotate == 0)
            {
                x = x + 5;
            }
            if (moveRotate == 1)
            {
                x = x - 5;
            }
            if (moveRotate == 2)
            {
                y = y + 5;
            }
            if (moveRotate == 3)
            {
                y = y - 5;
            }

            if(mainMatrix[randStartPosX+x, randStartPosY+y] == 0)
            {
                mainMatrix[randStartPosX, randStartPosY] = 0;
                pole.gr.DrawRectangle(ric.jertvi, randStartPosX+x, randStartPosY+y, 5, 5);
            }
            
            if(mainMatrix[randStartPosX+x+5, randStartPosY+y] == 1)
            {
                mainMatrix[randStartPosX, randStartPosY] = 0;
                pole.gr.DrawRectangle(ric.whitePen, randStartPosX+x+5, randStartPosY+y, 5, 5);
            }
            if (mainMatrix[randStartPosX+x-5, randStartPosY+y] == 1)
            {
                mainMatrix[randStartPosX, randStartPosY] = 0;
                pole.gr.DrawRectangle(ric.whitePen, randStartPosX+x-5, randStartPosY+y, 5, 5);
            }
            if (mainMatrix[randStartPosX+x, randStartPosY+y-5] == 1)
            {
                mainMatrix[randStartPosX, randStartPosY] = 0;
                pole.gr.DrawRectangle(ric.whitePen, randStartPosX+x, randStartPosY+y-5, 5, 5);
            }
            if (mainMatrix[randStartPosX+x, randStartPosY+y+5] == 1)
            {
                mainMatrix[randStartPosX, randStartPosY] = 0;
                pole.gr.DrawRectangle(ric.whitePen, randStartPosX+x, randStartPosY+y+5, 5, 5);
            }
            //обновлять каждый раз или хранить прошлое движение в пред клетке
        }
    }
}