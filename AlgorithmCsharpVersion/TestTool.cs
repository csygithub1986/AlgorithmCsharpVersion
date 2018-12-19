using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmCsharpVersion
{
    class TestTool
    {
        //生成随机字符
        public static string GetRandomStrings(int length, int seed)
        {
            string buffer = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder sb = new StringBuilder();
            Random r = new Random(seed);
            int range = buffer.Length;
            for (int i = 0; i < length; i++)
            {
                sb.Append(buffer[r.Next(range)]);
            }
            return sb.ToString();
        }
    }
}
