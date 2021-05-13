using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Produc_Consum_Circle_4
{
    class Consumer
    {
        //потребитель
        //забирает если в каждой очереди есть 
        //берет из всех очередей
        private CommonData Data//экземляр класса общих данных
        {
            get;
            set;
        }

        private Thread t = null;
        public static bool stop = false;


        public Consumer(CommonData data)//конструктор
        {
            Data = data;
        }

        public void Start()
        {
            if (t?.IsAlive == true) return;
            stop = false;
            t = new Thread(new ThreadStart(consume));
            t.Start();
        }

        // public Circle c;
        private void consume()
        {
            try//ThreadInterruptedException
            {

                while (!stop)
                {
                    //запускать только когда собрались все в центре 
                    //  Console.WriteLine("Consumer: Ожидание данных");
                    var values = Data.ConsumeValues();//еслли данные не готовы подвиснем пока не соберется в трех очередях
                                                      //получаем массив из кругов нужно их обработать и отрисовать 
                                                      /* while (values[0].in_centre != true || values[1].in_centre != true || values[2].in_centre != true)
                                                       {                                  // Console.WriteLine(values.Sum());
                                                           Thread.Sleep(700);
                                                       }*/
                                                      //if(values[0].in_centre != true || values[1].in_centre != true || values[2].in_centre != true)
                                                      //  vatecircle(values);
                                                      // Animator.extendend = true;
                                                      //int[] nums5 = {100,200,140 };
                    var c = new Circle(3, Argb(values));
                    foreach (var k in values)
                    {
                        k.Stop = true;
                    }

                    Animator.circ[0]=c;
                    ///circ.Add(c);
                    c.Start();
                  
                }
            }
            catch { }
        }

        private List<Circle> circ = new List<Circle>();

        public void Draw_Extended_Circle(Graphics g)
        {
            var cnt = circ.Count;
            for (int i = 0; i < cnt; i++)
            {
                circ[i].Paint(g);
            }
          /*  for (int i = 0; i < cnt; i++)
            {
                circ[i].Paint(g);
            }**/
        }

        private int[] Argb(Circle[] values)
        {
            int[] nums2 = new int[3] { values[0].argb[0], values[1].argb[1], values[2].argb[2] };
            return nums2;
        }

        public void Stop()
        {
            stop = true;
            try
            {
                t.Interrupt();
            }
            catch { }
        }
    }
}
