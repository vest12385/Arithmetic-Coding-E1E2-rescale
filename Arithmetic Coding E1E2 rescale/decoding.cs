using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arithmetic
{
    class decoding
    {
        private string output; 
        private decimal toDec = 0m;
        public decoding(string output)
        {
            this.output = output;
        }
        ~decoding() { }
        public string decode( Dictionary<char, code> probability, int length)
        {
            string answer = string.Empty;
            int round = 0;
            List<decimal> highLow = new List<decimal>() { 1, 0 };
            decimal range = highLow[0] - highLow[1];
            toDec = binToDec(output);
            while (round != length)
            {
                foreach (KeyValuePair<char, code> item in probability)
                {
                    if (item.Value.low <= toDec && toDec < item.Value.high)
                    {
                        answer += item.Key;
                        highLow[0] = highLow[1] + range * item.Value.high;
                        highLow[1] = highLow[1] + range * item.Value.low;
                        highLow = output0(highLow);
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
