using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmCsharpVersion.DynamicPrograming
{
    /// <summary>
    /// LongestCommon Subsequence 最长公共子序列
    /// </summary>
    class LCS : ITest
    {
        /**
         * 最关键是这样两句话，记lcs(n,m)是s1前n个，s2前m个序列的最大公共子序列，那么
         * 如果s1[n]==s2[m]，s1[n]一定在lcs(n,m)中
         * 如果s1[n]!=s2[n]，那么lcs(n,m)=max(lcs(n-1,m),lcs(n,m-1))
         * 下面是自己理解后写的，可能空间复杂度不够简洁
         */
        private int Algorithm(string s1, string s2, out string sub)
        {
            int len1 = s1.Length;
            int len2 = s2.Length;
            int[,] lenLCS = new int[len1 + 1, len2 + 1];
            int[,] lcs1 = new int[len1 + 1, len2 + 1];
            int[,] lcs2 = new int[len1 + 1, len2 + 1];
            char[,] lcs = new char[len1 + 1, len2 + 1];
            for (int i = 0; i < len1; i++)
            {
                for (int j = 0; j < len2; j++)
                {
                    if (s1[i] == s2[j])
                    {
                        lenLCS[i + 1, j + 1] = lenLCS[i, j] + 1;  // Math.Max(lenLCS[i + 1, j], lenLCS[i, j + 1]);
                        lcs1[i + 1, j + 1] = i;
                        lcs2[i + 1, j + 1] = j;
                        lcs[i + 1, j + 1] = s1[i];
                    }
                    else
                    {
                        lenLCS[i + 1, j + 1] = lenLCS[i + 1, j];
                        lcs1[i + 1, j + 1] = i + 1;
                        lcs2[i + 1, j + 1] = j;
                        if (lenLCS[i, j + 1] > lenLCS[i + 1, j])
                        {
                            lenLCS[i + 1, j + 1] = lenLCS[i, j + 1];
                            lcs1[i + 1, j + 1] = i;
                            lcs2[i + 1, j + 1] = j + 1;
                        }
                    }
                }
            }

            //反推
            int l1 = len1;
            int l2 = len2;
            sub = "";
            while (l1 > 0 && l2 > 0)
            {
                if (lcs[l1, l2] != 0)
                {
                    sub = lcs[l1, l2] + sub;
                }
                int temp = l1;
                l1 = lcs1[temp, l2];
                l2 = lcs2[temp, l2];
            }

            return lenLCS[len1, len2];
        }

        //坐标改变（还是len+1方便一点）
        private int Algorithm2(string s1, string s2, out string sub)
        {
            int len1 = s1.Length;
            int len2 = s2.Length;
            int[,] lenLCS = new int[len1, len2];
            int[,] lcs1 = new int[len1, len2];
            int[,] lcs2 = new int[len1, len2];
            char[,] lcs = new char[len1, len2];
            for (int i = 0; i < len1; i++)
            {
                for (int j = 0; j < len2; j++)
                {
                    if (s1[i] == s2[j])
                    {
                        if (i == 0 || j == 0)
                            lenLCS[i, j] = 1;
                        else
                            lenLCS[i, j] = lenLCS[i - 1, j - 1] + 1;  // Math.Max(lenLCS[i + 1, j], lenLCS[i, j + 1]);
                        lcs1[i, j] = i - 1;
                        lcs2[i, j] = j - 1;
                        lcs[i, j] = s1[i];
                    }
                    else
                    {
                        int leni = i > 0 ? lenLCS[i - 1, j] : 0;
                        int lenj = j > 0 ? lenLCS[i, j - 1] : 0;

                        if (lenj >= leni)
                        {
                            lenLCS[i, j] = lenj;
                            lcs1[i, j] = i;
                            lcs2[i, j] = j - 1;
                        }
                        else
                        {
                            lenLCS[i, j] = leni;
                            lcs1[i, j] = i - 1;
                            lcs2[i, j] = j;
                        }
                    }
                }
            }

            //反推
            int l1 = len1 - 1;
            int l2 = len2 - 1;
            sub = "";
            while (l1 >= 0 && l2 >= 0)
            {
                if (lcs[l1, l2] != 0)
                {
                    sub = lcs[l1, l2] + sub;
                }
                int temp = l1;
                l1 = lcs1[temp, l2];
                l2 = lcs2[temp, l2];
            }

            return lenLCS[len1 - 1, len2 - 1];
        }

        public void AlgorithmTest()
        {
            Random ran = new Random();
            string a = GetRandomStrings(100, ran.Next(10000));
            string b = GetRandomStrings(100, ran.Next(10000));
            Console.WriteLine(a);
            Console.WriteLine(b);
            string c;
            int len = Algorithm2(a, b, out c);
            Console.WriteLine("答案：" + len);
            Console.WriteLine(c);
        }

        private string GetRandomStrings(int length, int seed)
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

        public void BruteForceTest()
        {
        }
    }
}
