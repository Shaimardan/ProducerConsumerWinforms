using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Produc_Consum_Circle_4
{
    class CommonData
    {
        public static readonly int ProducerCount = 3;

        public Queue<Circle>[] values =
        {//3 очереди  3 различных продюсера что то в них ложат
            new Queue<Circle>(),
            new Queue<Circle>(),
            new Queue<Circle>()
        };

        //в addvalue добавить прорисовку
        // 

        public void AddValue(int index, Circle value)
        {
            index = Math.Abs(index) % ProducerCount;
            try
            {
                Monitor.Enter(values);
                while (values[index].Count >= 3)
                    Monitor.Wait(values);
                // Console.WriteLine("Produser #{0} adds {1}", index, value);
                values[index].Enqueue(value);
                Monitor.PulseAll(values);
            }
            finally
            {
                Monitor.Exit(values);
            }
        }

        public void Paint(Graphics g)
        {
            // CommonData val = values;

        }

        public Circle[] ConsumeValues()
        {
            var result = new Circle[ProducerCount];
            int i = 0;
            Monitor.Enter(values);
            try
            {
                foreach (var queue in values)
                {
                    while (queue.Count == 0)
                    {
                        Monitor.Wait(values);//приостонавливает если данные не готовы
                        //тут может сломаться при выключении
                    }

                    result[i++] = queue.Dequeue();
                }

                Monitor.PulseAll(values); //пробуждает
            }
            finally
            {
                Monitor.Exit(values);
            }

            return result;
        }
    }
}
