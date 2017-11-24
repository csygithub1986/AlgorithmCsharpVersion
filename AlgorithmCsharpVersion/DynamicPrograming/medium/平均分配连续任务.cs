using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmCsharpVersion.DynamicPrograming
{
    /// <summary>
    /// 有n个任务，第i个任务量为s(i)，分配个m个人（m小于等于n），要求分配给一个人的任务必须是序号连续的，不能随机组合。求最大任务量和最小任务量之差最小的分配方式。
    /// 如果蛮力搜索，相当于n-1个空插入m-1个隔板，最差的时间复杂度为，排列组合O(n!/m!/(n-m)!)，略小于O(2^n)。（很奇怪，n的最大组合和2^n大小相当，前者略小）
    /// 耗费了我六七个小时才搞定，验证结果：
    /// 任务：14  4  2  13  9  8  5  6  7  18  7  12  17  13  18  3  4  2  15  9  10  16  3  7  15  5  14  4  16  1
    /// 任务数：30，人数：10
    /// 算法测试：
    /// 最大差值:14 详细：(14  4  2  13  ) (9  8  5  6  ) (7  18  7  ) (12  17  ) (13  18  ) (3  4  2  15  9  ) (10  16  ) (3  7  15  ) (5  14  ) (4  16  1  )
    /// 耗时：2 ms
    /// 蛮力验证：
    /// 最大差值:14 详细：(14  4  2  13  ) (9  8  5  6  ) (7  18  7  ) (12  17  ) (13  18  ) (3  4  2  15  9  ) (10  16  ) (3  7  15  ) (5  14  ) (4  16  1  )
    /// 耗时：81521 ms
    /// 
    /// n=50，蛮力需要运行一天
    /// n=60，三年
    /// n=70，三千多年
    /// n=80，三百万年
    /// n=90，30亿年
    /// n=100，3亿亿年。而算法一直都只要几毫秒
    /// </summary>
    public class 平均分配连续任务 : ITest
    {
        //算法，时间复杂度为O(n^2*m)
        private int Distribute(int[] s, int n, int m, out int[][] result, out int finalMax, out int finalMin)
        {
            if (n < 1 || m < 1 || n < m)
            {
                throw new Exception("值不符合要求");
            }

            int[] p = new int[n];//p[i]表示前i个任务的总任务量
            p[0] = s[0];
            for (int i = 1; i < n; i++)
            {
                p[i] = p[i - 1] + s[i];
            }

            result = new int[m][];  //最终结果
            int[][] maxValue = new int[n][]; //max[i][j]/min[i][j]分别表示有i+1个任务，有j+1个人时的任务最大值/最小值
            int[][] minValue = new int[n][];
            int[][] lastTaskCount = new int[n][]; //lastTask[i][j]表示有i+1个任务，有j+1个人时最后一个人分到的任务个数
            //初始化
            for (int i = 0; i < n; i++)
            {
                maxValue[i] = new int[m];
                minValue[i] = new int[m];
                lastTaskCount[i] = new int[m];
            }
            //人数=1时
            for (int i = 0; i < n; i++)
            {
                maxValue[i][0] = p[i];
                minValue[i][0] = p[i];
                lastTaskCount[i][0] = i + 1;
            }
            //求解
            for (int i = 1; i < n; i++)
            {
                for (int j = 1; j <= i && j < m; j++)
                {
                    //多加一个人时，先把最后一个分给这个人，再结合除这个以外的分法计算一个差值。然后循环计算将最后k个分给这个人，得出最终的最优解
                    //这里还可以优化，就是最后一个已经最大后，就不用再循环了
                    int tempSub = int.MaxValue;//差值
                    for (int k = 1; k <= i - j + 1; k++)//k表示将最后k个分给新人
                    {
                        int max = Math.Max(maxValue[i - k][j - 1], p[i] - p[i - k]);
                        int min = Math.Min(minValue[i - k][j - 1], p[i] - p[i - k]);
                        int sub = max - min;
                        if (tempSub > sub)
                        {
                            tempSub = sub;
                            maxValue[i][j] = max;
                            minValue[i][j] = min;
                            lastTaskCount[i][j] = k;
                        }
                    }

                }
            }
            //根据解推算结果
            int currentN = n - 1; //总任务数
            for (int i = m - 1; i >= 0; i--)
            {
                int count = lastTaskCount[currentN][i];
                result[i] = new int[count];
                for (int j = 0; j < count; j++)
                {
                    result[i][j] = s[currentN - count + j + 1];
                }
                currentN -= count;
            }
            finalMax = maxValue[n - 1][m - 1];
            finalMin = minValue[n - 1][m - 1];
            return maxValue[n - 1][m - 1] - minValue[n - 1][m - 1];
        }

        //蛮力，相当于n-1个空插入m-1个隔板。但设计排列组合太麻烦，直接用二进制设计，2^(n-1)中有m个1就行。蛮力也不简单
        private int BruteForce(int[] s, int n, int m, out int[][] result, out int finalMax, out int finalMin)
        {

            long total = (long)Math.Pow(2, n - 1);//只支持n-m<32
            long finalWay = 0;//最终的分配方式

            int finalSub = int.MaxValue;
            finalMax = 0;
            finalMin = 0;
            for (long i = 0; i < total; i++)
            {
                //求当前分配方式时，最大、最小、差值
                int[] position;//对应的值就是分割的点，0代表第一节有一个
                if (GetPositionOfOne(m - 1, i, out position))
                {
                    //记录和中的最大最小值
                    int tempMax = int.MinValue;
                    int tempMin = int.MaxValue;
                    //计算1~m个和值
                    for (int j = 0; j < m; j++)
                    {
                        int sum = 0;
                        if (j == 0)
                        {
                            for (int jj = 0; jj <= position[0]; jj++)
                            {
                                sum += s[jj];
                            }
                        }
                        else if (j == m - 1)
                        {
                            for (int jj = position[j - 1] + 1; jj < n; jj++)
                            {
                                sum += s[jj];
                            }
                        }
                        else
                        {
                            for (int jj = position[j - 1] + 1; jj <= position[j]; jj++)
                            {
                                sum += s[jj];
                            }
                        }
                        if (tempMax < sum)
                        {
                            tempMax = sum;
                        }
                        if (tempMin > sum)
                        {
                            tempMin = sum;
                        }
                    }
                    int sub = tempMax - tempMin;
                    if (sub < finalSub)
                    {
                        finalSub = sub;
                        finalWay = i;
                        finalMax = tempMax;
                        finalMin = tempMin;
                    }
                }
            }
            //反推细节
            result = new int[m][];

            int[] pos;//对应的值就是分割的点，0代表第一节有一个
            GetPositionOfOne(m - 1, finalWay, out pos);

            for (int j = 0; j < m; j++)
            {
                if (j == 0)
                {
                    result[j] = new int[pos[0] + 1];
                    for (int jj = 0; jj <= pos[0]; jj++)
                    {
                        result[j][jj] = s[jj];
                    }
                }
                else if (j == m - 1)
                {
                    result[j] = new int[n - 1 - pos[m - 2]];
                    for (int jj = pos[j - 1] + 1; jj < n; jj++)
                    {
                        result[j][jj - pos[j - 1] - 1] = s[jj];
                    }
                }
                else
                {
                    result[j] = new int[pos[j] - pos[j - 1]];
                    for (int jj = pos[j - 1] + 1; jj <= pos[j]; jj++)
                    {
                        result[j][jj - pos[j - 1] - 1] = s[jj];
                    }
                }
            }

            return finalSub;
        }

        //value的二进制中是否有k个1，如果是返回1的位置，如果不是返回null
        private bool GetPositionOfOne(int k, long value, out int[] pos)
        {
            pos = new int[k];
            int posIndex = 0;
            int valueIndex = 0;
            while (value != 0)
            {
                if ((value & 0x01) == 0x01)
                {
                    if (posIndex >= k)
                    {
                        return false;
                    }
                    pos[posIndex] = valueIndex;
                    posIndex++;
                }
                value >>= 1;
                valueIndex++;
            }
            if (posIndex != k)
            {
                return false;
            }
            return true;
        }


        #region 测试

        int n = 100;//任务数
        int m = 2;//人数
        int[] s;
        public 平均分配连续任务()
        {
            s = new int[n];
            //随机任务
            Random ran = new Random();
            for (int i = 0; i < n; i++)
            {
                s[i] = ran.Next(1, 20);
            }
            Console.WriteLine("有n个任务，第i个任务量为s(i)，分配个m个人（m小于等于n），要求分配给一个人的任务必须是序号连续的，不能随机组合。求最大任务量和最小任务量之差最小的分配方式");
            Console.Write(string.Format("任务："));
            for (int i = 0; i < n; i++)
            {
                Console.Write(s[i] + "  ");
            }
            Console.WriteLine();
            Console.WriteLine("任务数" + n + " ，人数：" + m);
        }

        public void AlgorithmTest()
        {
            int[][] result;
            int max;
            int min;
            int sub = Distribute(s, n, m, out result, out max, out min);
            Print(sub, result, max, min);
        }

        public void BruteForceTest()
        {
            //int[][] result;
            int max;
            int min;
            //int sub = BruteForce(s, n, m, out result, out max, out min);
            //Print(sub, result,max,min);
        }

        private void Print(int sub, int[][] result, int max, int min)
        {
            Console.Write("最大差值:{0}  最大值{1}  最小值{2} 详细：", sub, max, min);
            for (int i = 0; i < m; i++)
            {
                Console.Write("(");
                for (int j = 0; j < result[i].Length; j++)
                {
                    Console.Write(result[i][j] + "  ");
                }
                Console.Write(") ");
            }
            Console.WriteLine();
        }
        #endregion

    }
}
