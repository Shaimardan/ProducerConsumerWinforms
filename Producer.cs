using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Produc_Consum_Circle_4
{
    class Producer
    {
        private CommonData Data
        {
            get;
            set;
        }

        private int index;//в какую очередь будет записывать
        public int Index
        {
            get => index;
            set => index = Math.Abs(value) % CommonData.ProducerCount;
        }

        private  List<Circle> circ = new List<Circle>();

        private Thread t = null;
        private bool stop = false;//признак   очтановки

        private static Random r = new Random((int)DateTime.Now.Ticks);

        public Producer(CommonData data, int index)
        {
            Data = data;
            Index = index;
        }

        public void Start()
        {//запускает в своем потоке 
            if (t?.IsAlive == true) return;
            stop = false;
            t = new Thread(new ThreadStart(produce));
            t.Start();
        }

      //
        //private List col
        //   private static List<Color> col = new List<Color>();
        private List<Color> col = new List<Color>() { Color.Red, Color.Blue, Color.Green };
        private int[] mas;
        private void produce()
        {
            try
            {

              

                while (!stop)
                {
                    //проверка пока не дошел до центра 
                    // var red = r.Next(255);

                    var c = new Circle(Index, mas);
                   
                    Animator.circ.Add(c);
                    circ.Add(c);
                    c.Start();//запустить процесс движения кружка


                   
                    while (c.XPos != Circle.ConteinerSize.Width / 2 || c.YPos != Circle.ConteinerSize.Height / 2)//из за этого не выключается
                    {
                        //     Monitor.Wait(c);
                   //     Monitor.Wait(c);
                        Thread.Sleep(2);
                    }
                 //   Monitor.Pulse(Index);

                    Data.AddValue(Index, c);//добавляет в одну из очередей

                }
            }
            catch { }
        }

        public void DrawCirc(Graphics g)
        {
            var cnt = circ.Count;
            for (int i = 0; i < cnt; i++)
            {
                circ[i].Paint(g);
            }

        }

        public void DrawQue(Graphics g)
        {
          
            var d =Data.values[Index];
            
            int l = 0;
            int cnt = Data.values[Index].Count;
              foreach (var number in Data.values[Index])
          //  for(int i=0;i<cnt;i++)
            {
                Brush b = new SolidBrush(number.CircleColor);
                Pen p = new Pen(number.CircleColor);
                g.FillEllipse(b, Circle.ConteinerSize.Width / 4*Index, Circle.ConteinerSize.Height - 150 + 31 * l, 30, 30);
                g.DrawEllipse(p, Circle.ConteinerSize.Width / 4 * Index, Circle.ConteinerSize.Height - 150 + 31 * l, 30, 30);
                l++;
               //    Console.WriteLine(number);
            }

        }

            public void Stop()
        {
            stop = true;
            t.Interrupt();
        }
    }
}
