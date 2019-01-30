using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace dataGen
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            string path = "..\\data_out";
            System.IO.Directory.CreateDirectory(path);
            string fileName = System.IO.Path.GetRandomFileName();
            path = System.IO.Path.Combine(path, fileName);

            int neuron_one = 0;
            float neuron_two = 0;

            
            if (!System.IO.File.Exists(path))
            {
                using (System.IO.FileStream fs = System.IO.File.Create(path))
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fs))
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        neuron_one = rnd.Next(0, 2);
                        neuron_two = rnd.Next(0, 100);
                        sw.WriteLine(neuron_one.ToString() + ',' + neuron_two.ToString());
                    }
                }
            }
        }
    }
}
