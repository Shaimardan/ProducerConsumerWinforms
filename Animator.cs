using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Produc_Consum_Circle_4
{
    class Animator
    {
        private CommonData data//экземляр класса общих данных
        {
            get;
            set;
        }

        private Graphics mainG;

        private bool stop = false;

        private BufferedGraphics bg;
        public Graphics MainGraphics
        {
            get => mainG;
            set
            {
                mainG = value;
                //  Circle.ConteinerSize = mainG.VisibleClipBounds.Size.ToSize();
                bg = BufferedGraphicsManager.Current.Allocate(
                mainG, Rectangle.Round(mainG.VisibleClipBounds));
            }
        }
        private Thread t;

        public Animator(Graphics g)
        {
            MainGraphics = g;

        }

        public void Start()
        {
            stop = false;
            AddProd();
            if (t == null || !t.IsAlive)
            {
                //   ThreadStart ts = new ThreadStart(Animate);
                t = new Thread(new ThreadStart(Animate));
                t.Start();

            }

        }

        private List<Producer> prods;
        private Consumer cons;

        private void AddProd()
        {

            //var
            data = new CommonData();//класскоторый по очереди создает элемент
            Circle.ConteinerSize = mainG.VisibleClipBounds.Size.ToSize();
            prods = new List<Producer>(3)//список продюсеров
            {
                new Producer(data, 0),
                new Producer(data, 1),
                new Producer(data, 2)
            };
            cons = new Consumer(data); //потребитель\

             cons.Start();//запускаем потребителя


             prods[0].Start();
             prods[1].Start();
             prods[2].Start();
        }

        ///подпроцесс работает на ур метода 



        public static List<Circle> circ = new List<Circle>();









        private void Animate()
        {
            while (!stop)
            {
                bg.Graphics.Clear(Color.White);
                try
                {
                    circ = circ.FindAll(it => it.IsAlive);
                }
                catch { }
                //  foreach (var circle in circ)
                var cnt = circ.Count;
                // Que(bg.Graphics, data);
                   for (int i = 0; i < cnt; i++)
                   {
                       circ[i].Paint(bg.Graphics);
                   }
               // cons.Draw_Extended_Circle(bg.Graphics);
                for (int i = 0; i < 3; i++)
                {
                   // circ[i].Paint(bg.Graphics);
                    prods[i].DrawCirc(bg.Graphics);
                    prods[i].DrawQue(bg.Graphics);
                }

                try
                {
                    bg.Render(MainGraphics);
                }
                catch (Exception e) { }
                Thread.Sleep(33);//статический меетод который приостанавливает выполнение на милисекунды

            }
        }

     /*   private void Que(Graphics g, CommonData data)
        {
            // var d=data[0].
            //   Color bc = Color.FromArgb(200, Color.Black);//альфа канал прозрачности .цвет задали полупрозрачный

            var d = data.values[0];
            int i = 0;
            int cnt = data.values[0].Count;
            foreach (var number in data.values[0])
            {
                Brush b = new SolidBrush(number.CircleColor);
                Pen p = new Pen(number.CircleColor);
                g.FillEllipse(b, 150, Circle.ConteinerSize.Height - 150 + 31 * i, 30, 30);
                g.DrawEllipse(p, 150, Circle.ConteinerSize.Height - 150 + 31 * i, 30, 30);
                i++;
                //    Console.WriteLine(number);
            }
            i = 0;
            foreach (var number in data.values[1])
            {
                Brush b = new SolidBrush(number.CircleColor);
                Pen p = new Pen(number.CircleColor);
                g.FillEllipse(b, Circle.ConteinerSize.Width / 2, Circle.ConteinerSize.Height - 150 + 31 * i, 30, 30);
                g.DrawEllipse(p, Circle.ConteinerSize.Width / 2, Circle.ConteinerSize.Height - 150 + 31 * i, 30, 30);
                i++;
                //    Console.WriteLine(number);
            }
            i = 0;
            foreach (var number in data.values[2])
            {
                Brush b = new SolidBrush(number.CircleColor);
                Pen p = new Pen(number.CircleColor);
                g.FillEllipse(b, Circle.ConteinerSize.Width - 150, Circle.ConteinerSize.Height - 150 + 31 * i, 30, 30);
                g.DrawEllipse(p, Circle.ConteinerSize.Width - 150, Circle.ConteinerSize.Height - 150 + 31 * i, 30, 30);
                i++;
                //    Console.WriteLine(number);
            }

            //  g.FillEllipse(b, Circle.ConteinerSize.Width-50, Circle.ConteinerSize.Height - 50, 30, 30);
            //  g.DrawEllipse(p, Circle.ConteinerSize.Width - 50, Circle.ConteinerSize.Height - 50, 30, 30);

        }

    */

        public void Stop()
        {
            Circle.StopAll = true;
            prods[0].Stop();
            prods[1].Stop();
            prods[2].Stop();
           cons.Stop();
            stop = true;


        }
    }
}

