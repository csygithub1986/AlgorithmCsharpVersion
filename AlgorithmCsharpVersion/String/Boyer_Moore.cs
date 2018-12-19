using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmCsharpVersion.String
{
    class Boyer_Moore
    {
        public int Arithmetic_BM(string operateStr, string findStr)
        {
            //i：匹配开始的索引，j：operateStr字符串的索引迭代，k：findStr字符串索引迭代
            int i = 0, j = findStr.Length - 1, k = j;
            int n, m = 0; //n:坏字符规则计算出的移动位数，m:好后缀计算出的移动位数

            while (k >= 0 && j < operateStr.Length)
            {
                if (k == 0) //全部匹配，return
                {
                    return i;
                    break;
                }
                if (operateStr[j] == findStr[k]) //匹配，next
                {
                    j--;
                    k--;
                }
                else
                {
                    //当k<要匹配的字符串长度时，说明已经有匹配的字符了，即有“好后缀”
                    if (k < findStr.Length - 1)
                    {
                        //采用"好后缀规则"，先找出“全好后缀”有没有在前面存在
                        var goodSuffix = findStr.Substring(k + 1); //分割出全好后缀
                        var tempStr = findStr.Substring(0, k + 1); //去掉好缀后的字符串
                        //最全好后缀在剩下的字符串中出现
                        if (tempStr.Contains(goodSuffix))
                        {
                            var lastGoodSuffix = char.Parse(goodSuffix.Substring(goodSuffix.Length - 1)); //好后缀的最后一个字符
                            //找出 该字符的出现位置
                            IList<int> indexs = new List<int>();
                            for (int x = 0; x < tempStr.Length; x++)
                            {
                                if (lastGoodSuffix == tempStr[x])
                                {
                                    indexs.Add(x);
                                }
                            }
                            //找出 好后缀在搜索词中的上一次出现位置
                            var result = -1;
                            for (int x = indexs.Count - 1; x >= 0; x--)
                            {
                                if (indexs[x] >= goodSuffix.Length &&
                                    tempStr.Substring(indexs[x] - goodSuffix.Length + 1, goodSuffix.Length) == goodSuffix)
                                {
                                    result = indexs[x];
                                    break;
                                }
                            }
                            //好后缀规则结果
                            m = findStr.Length - 1 - result;
                        }
                        //最长好后缀没有没出现，但是好后缀最后一个字符，出现在头部
                        //后移位数 = 好后缀的位置 - (0)搜索词中的上一次出现位置
                        else if (findStr.Substring(0, 1) == findStr.Substring(findStr.Length - 1))
                        {
                            m = findStr.Length - 1;
                        }
                        else //好后缀只出现一次  (后移位数 = 好后缀的位置 - (-1)搜索词中的上一次出现位置)
                        {
                            m = findStr.Length;
                        }
                    }
                    //坏字符规则：后移位数 = 坏字符的位置 - 搜索词中的上一次出现位置
                    n = (j - i) - findStr.LastIndexOf(operateStr[j]);
                    //比较坏字符规则和好后缀规则移动的位数，得出最终移动位数
                    if (n > m)
                    {
                        i += n;
                        j = i + findStr.Length - 1;
                    }
                    else
                    {
                        i += m;
                        j = i + findStr.Length - 1;
                    }
                    k = findStr.Length - 1;
                    m = 0; //清零
                }
            }
            return -1;
        }
    }
}
