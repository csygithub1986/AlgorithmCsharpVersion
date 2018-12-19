using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmCsharpVersion.String
{
    class KMP : ITest
    {
        void GetNextVal(string str, int[] next)
        {
            int i = 0;
            int j = -1;
            next[0] = -1;
            while (i < str.Length - 1)
            {
                if (j == -1 || str[i] == str[j])
                {
                    i++;
                    j++;
                    next[i] = j;
                }
                else
                {
                    j = next[j];
                }
            }
        }

        public int IndexOf(string zstr, string mstr)
        {
            int i, j;
            int[] next = new int[mstr.Length];
            GetNextVal(mstr, next);
            i = 0;
            j = 0;
            while (i < zstr.Length && j < mstr.Length)
            {
                if (j == -1 || zstr[i] == mstr[j])
                {
                    ++i;
                    ++j;
                }
                else
                {
                    j = next[j];
                }
            }
            if (j == mstr.Length)
                return i - mstr.Length;
            return -1;
        }



        string a;
        string b;

        public KMP()
        {
            //Random ran = new Random();
            //a = TestTool.GetRandomStrings(100000, ran.Next(10000));
            //b = TestTool.GetRandomStrings(500, ran.Next(10000));
        }

        public void AlgorithmTest()
        {
            int index = IndexOf(a, b);
            Console.WriteLine(index);
        }

        public void BruteForceTest()
        {
        }
    }
}
