using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arithmetic
{
    class decoding
    {
        private string output; //編碼結果
        private decimal toDec = 0m; //十進制小數
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="output"></param>
        public decoding(string output)
        {
            this.output = output;
        }
        ~decoding() { } //解構子
        /// <summary>
        /// 作解碼動作
        /// </summary>
        /// <param name="probability"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public string decode( Dictionary<char, code> probability, int length)
        {
            string answer = string.Empty;   //最後的答案
            int round = 0;
            List<decimal> highLow = new List<decimal>() { 1, 0 };
            decimal range = highLow[0] - highLow[1];
            toDec = binToDec(output);
            while (round != length) //結束條件
            {
                foreach (KeyValuePair<char, code> item in probability)
                {
                    if (item.Value.low <= toDec && toDec < item.Value.high)
                    {
                        answer += item.Key;
                        highLow[0] = highLow[1] + range * item.Value.high;
                        highLow[1] = highLow[1] + range * item.Value.low;
                        highLow = output0(highLow);     //檢查範圍是否跨越0.5，若有繼續執行，若無繼續遞迴
                        toDec = binToDec(output);
                        toDec -= highLow[1];
                        range = highLow[0] - highLow[1];
                        toDec /= (range);
                        break;
                    }
                }
                round++;
            }
            return answer;
        }
        /// <summary>
        /// 作二進制小數轉十進制小數
        /// </summary>
        /// <param name="outout"> 一開始為編碼結果，之後會慢慢扣掉 </param>
        /// <returns>十進制小數</returns>
        private decimal binToDec(string outout)
         {
            decimal toDec = 0m;
            for (int i = 1; i <= 6; i++)
            {
                if( outout[i - 1].Equals( '1' ) )
                    toDec += Convert.ToDecimal( Math.Pow(2, -i));
            }
            return toDec;
        }
        /// <summary>
        /// 若範圍落在0.5以內，則做E1 rescale，並刪除一個0
        /// </summary>
        /// <param name="highLow">紀錄high 跟 low的List</param>
        /// <returns>high 跟 low 的計算結果</returns>
        private List<decimal> output0(List<decimal> highLow)
        {
            if (highLow[0] >= 0.5m && highLow[1] < 0.5m)
                return highLow;
            while (highLow[0] < 0.5m)
            {
                output = output.Substring(1, output.Count() - 1);
                Console.WriteLine("去除0 : {0}", output.Substring(0, 5));
                highLow[0] = 2 * highLow[0];
                highLow[1] = 2 * highLow[1];
            }
            output1(highLow);
            return highLow;
        }
        /// <summary>
        /// 若範圍落在0.5以外，則做E2 rescale，並刪除一個1
        /// </summary>
        /// <param name="highLow">紀錄high 跟 low的List</param>
        /// <returns>high 跟 low 的計算結果</returns>
        private List<decimal> output1(List<decimal> highLow)
        {
            if (highLow[0] >= 0.5m && highLow[1] < 0.5m)
                return highLow;
            while (highLow[0] >= 0.5m && highLow[1] >= 0.5m)
            {
                output = output.Substring(1, output.Count() - 1);
                Console.WriteLine("去除1 : {0}", output.Substring(0, 5));
                highLow[0] = 2 * (highLow[0] - 0.5m);
                highLow[1] = 2 * (highLow[1] - 0.5m);
            }
            output0(highLow);
            return highLow;
        }
    }
}
