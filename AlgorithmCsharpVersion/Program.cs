using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmCsharpVersion
{
    class Program
    {
        static void Main(string[] args)
        {
            //write function here
            ITest test = new DynamicPrograming.DecodeWays();

            Console.WriteLine();
            DateTime t1 = DateTime.Now;
            Console.WriteLine("算法测试：");
            test.AlgorithmTest();
            DateTime t2 = DateTime.Now;
            Console.WriteLine("耗时：" + (t2 - t1).TotalMilliseconds.ToString("F0") + " ms");

            Console.WriteLine();
            Console.WriteLine("蛮力验证：");
            test.BruteForceTest();
            DateTime t3 = DateTime.Now;
            Console.WriteLine("耗时：" + (t3 - t2).TotalMilliseconds.ToString("F0") + " ms");


            Console.WriteLine("回车退出...");
            Console.ReadLine();
        }
    }
}
