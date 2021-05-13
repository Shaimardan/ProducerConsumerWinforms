using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Produc_Consum_Circle_4
{
    
    class Circle
    {
        private int size;//приватное поле размер
        private PointF pos;
        private int Speed;
        private Thread t;



        public bool IsAlive
        {
            get => (t != null && t.IsAlive);

        }
        public bool Stop
        {
            get;
            set;
        }

        public static bool StopAll
        {
            get;
            set;
        }
        public float XPos
        {

            get => pos.X;
            set
            {
                if (value < 0 || value > containerSize.Width)
                {
                    if (value < 0) value = 0;
                    if (value > containerSize.Width - size - 1)
                        value = containerSize.Width - size - 1;
                    // Stop = true;

                }
                pos.X = value;

            }

        }

        public float YPos
        {
            get => pos.Y;
            set
            {

                if (value < 0 || value > containerSize.Height - size)
                {
                    if (value < 0) value = 0;
                    if (value > containerSize.Height - size - 1)
                        value = containerSize.Height - size - 1;
                    Stop = true;

                }
                pos.Y = value;

            }
        }




        public int Size//свойство.свойство - элемент класса с помощью которых мы можем задавать или получать значения
        {
            get => size;
            set
            {
                if (value < 5) size = 5;
                else if (value > 100) size = 100;
                else size = value;
            }
            //get;
            //set;

        }

        private float dx;
        private float dy;
        private int dr;

        public Color CircleColor // автореализируемое свойство
        {
            get;
            set;
        }


        private static Random r = new Random();//статический единй для всех экземляров класса 
        private static Size containerSize = new Size(1, 1);
        public static Size ConteinerSize
        {
            get => containerSize;
            set
            {
                containerSize = value;
            }


        }

        //  public int argb;
        public bool in_centre = false;


        public int[] argb = new int[3] { 0, 0, 0 };
        public Circle(int index, int[] mas)//
        {

            size = 40;
           // Speed = speed;
            // lifetime = r.Next(500, 800);
            //   argb = r.Next(80, 250);
            //Color bc = Color.FromArgb(, Color.Black);
            var red = r.Next(255);
            var grn = r.Next(255);
            var blu = r.Next(255);
              Speed = r.Next(2,30);
            //CircleColor = Color.FromArgb(col);

            if (index == 0)
            {
                Speed = r.Next(13, 20);

                //Thread.Sleep(400 + Inde * r.Next(2000));
                pos = new PointF(0, ConteinerSize.Height / 2);
                dx = 2;
                dy = 0;
                CircleColor = Color.FromArgb(red, 0, 0);
                argb[0] = red;

            }
            if (index == 1)
            {
                pos = new PointF(containerSize.Width / 2, 0);
                dx = 0;
                dy = 3;
                //  CircleColor = Color.Blue;
                CircleColor = Color.FromArgb(0, red, 0);
                argb[1] = red;
            }
            if (index == 2)
            {
                Speed = r.Next(2, 13);

                pos = new PointF(ConteinerSize.Width, ConteinerSize.Height / 2);
                dx = -2;
                dy = 0;
                //CircleColor = Color.Green;
                CircleColor = Color.FromArgb(0, 0, red);
                argb[2] = red;
            }
            if (index == 3)
            {
                pos = new PointF(ConteinerSize.Width / 2 - 1, ConteinerSize.Height / 2 - 1);
                dx = -2;
                dy = -2;
                dr = 4;
                //CircleColor = Color.Green;
                CircleColor = Color.FromArgb(mas[0], mas[1], mas[2]);
                Speed = 2;
            }

        }

        private int ar = 250;
        public int Ar//свойство.свойство - элемент класса с помощью которых мы можем задавать или получать значения
        {
            get => ar;
            set
            {
                if (value <= 0) Stop = true;

            }
            //get;
            //set;
        }
        public void Paint(Graphics g)
        {
            Color bc = Color.FromArgb(200, CircleColor);//альфа канал прозрачности .цвет задали полупрозрачный
            Brush b = new SolidBrush(CircleColor);
            Pen p = new Pen(CircleColor);
            g.FillEllipse(b, XPos, YPos, size, size);
            g.DrawEllipse(p, XPos, YPos, size, size);
        }

        private void Move()
        {
            if (dr != 0)
            {
                Color r = CircleColor;
                CircleColor = Color.FromArgb(ar, r);
                ar = ar - 2;
                if (ar <= 0) Stop = true;
                size += dr;
            }
        
            if (XPos != ConteinerSize.Width / 2 || YPos != ConteinerSize.Height / 2)
            {
                XPos += dx;
                YPos += dy;


            }
            else
            {

                in_centre = true;
                // Stop = true;
            }
        }

        public void Start()//формирует и запускает подпроцесс
        {
            Stop = false;
            StopAll = false;
            t = new Thread(
                new ThreadStart(Run)
                );
            t.Start();//запуск потока на выполнения
        }

        private void Run()//сам подпроцесс будет выполнять код из ран 
        {

            while (!Stop && !StopAll /*&& it++ < lifetime*/)
            {
                Move();
                Thread.Sleep(Speed);
            }

        }
    }
}
