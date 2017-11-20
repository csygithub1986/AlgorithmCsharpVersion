using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmCsharpVersion.DynamicPrograming
{
    public class LongestIncreasingSubsequence
    {
        private static int GetLongest(int[] sequence, out int[] subSequence)
        {
            int[] d = new int[sequence.Length];//记录求解过程中的值，d中最大的即最终答案
            d[0] = 1;

            int[] s = new int[sequence.Length];//记录求解过程中前一个比自己小的数，前序
            s[0] = -1;//-1表示没有前序的值

            subSequence = null;

            int maxValue = 0;

            for (int i = 1; i < sequence.Length; i++)
            {
                d[i] = 1;
                s[i] = -1;
                for (int j = 0; j < i; j++)
                {
                    if (sequence[j] < sequence[i])
                    {
                        if (d[i] < d[j] + 1)
                        {
                            d[i] = d[j] + 1;
                            s[i] = j;
                        }
                    }
                }
                if (i == sequence.Length - 1)
                {
                    //求d中最大的

                    int lastIndex = 0;//序列最后一个的索引
                    for (int ii = 0; ii < d.Length; ii++)
                    {
                        if (d[ii] > maxValue)
                        {
                            maxValue = d[ii];
                            lastIndex = ii;
                        }
                    }
                    //求路径
                    int[] indexs = new int[maxValue];
                    indexs[maxValue - 1] = lastIndex;
                    for (int k = 1; k < indexs.Length; k++)
                    {
                        indexs[indexs.Length - 1 - k] = s[s[indexs[indexs.Length - k]]];
                    }

                    subSequence = new int[maxValue];
                    for (int k = 0; k < subSequence.Length; k++)
                    {
                        subSequence[k] = sequence[indexs[k]];
                    }
                }
            }
            return maxValue;
        }

        public static void Test()
        {
            int[] testSequence = { 1, 3, 7, 3, 4, 6, 2, 6, 8, 5, 9, 5, 7, 6, 12, 9, 11, 15 };
        }
    }
}
