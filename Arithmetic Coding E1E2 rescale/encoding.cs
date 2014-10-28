using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arithmetic
{
    /// <summary>
    /// 編碼類別
    /// </summary>
    class encoding
    {
        private Dictionary<char, code> probability = new Dictionary<char, code>();  //字典
        private List<char> wordChar = new List<char>();     //alphabet
        private string output = null;   //解碼後結果
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="probability">字典</param>
        public encoding(  Dictionary<char, code> probability )
        {
            this.probability = probability;
        }
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="probability">字典</param>
        public encoding(string word)
        {
            calculateProbability(word);
        }
        /// <summary>
        /// 取得字典
        /// </summary>
        public Dictionary<char, code> getProbability
        {
            get { return probability; }
        }
        ~encoding() { } //解構子
        /// <summary>
        /// 計算每一個character出現次數
        /// 若第一次出現則放入字典
        /// 若有出現過將該字對應到的類別(code)裡的prob(機率)加一
        /// </summary>
        /// <param name="word">alphabet</param>
        public void calculateProbability(string word)
        {
            wordChar = word.ToList();
            wordChar.Sort();
            foreach (char ecahWord in wordChar)
            {
                code dd = new code();
                if (probability.ContainsKey(ecahWord))
                {
                    probability[ecahWord].prob += 1;
                }
                else
                {
                    probability.Add(ecahWord, dd);
                }
            }
            handleProbability();
        }
        /// <summary>
        /// 計算機率跟範圍
        /// </summary>
        /// <param name="wordChar"> alphabet </param>
        /// <param name="AllWord"> 總共有多少個字元 </param>
        /// <param name="limit"> 計算目前到哪個下邊界 </param>
        /// <param name="probability[ecahWord].prob"> 以ecahWord為key 所對應到的 機率 </param>
        /// <param name="probability[ecahWord].low"> 以ecahWord為key 所對應到的 下邊界 </param>
        /// <param name="probability[ecahWord].high"> 以ecahWord為key 所對應到的 上邊界 </param>
        private void handleProbability()
        {
            int AllWord = wordChar.Count;
            wordChar = wordChar.Distinct().ToList();
            decimal limit = 0m;
            int i = 0;
            foreach (KeyValuePair<char, code> item in probability)
            {
                Console.WriteLine("請問 {0} 的機率是多少?", item.Key);
                item.Value.prob = Convert.ToDecimal(Console.ReadLine());
                item.Value.low = Math.Round(limit, 7);
                item.Value.high = Math.Round(limit + item.Value.prob, 7);
                limit = limit + item.Value.prob;
                i++;
            }
        }
        /// <summary>
        /// 主要編碼部分，完全依課本演算法做
        /// </summary>
        /// <param name="word">alphabet</param>
        /// <returns>編碼結果</returns>
        public string encoder(string word)
        {
            decimal low = 0;
            decimal high = 1;
            decimal range = high - low;
            List<decimal> highLow = new List<decimal>() { high, low };
            for (int i = 0; i < word.Count(); i++)
            {
                Console.WriteLine("Now is {0} ( {0}'s Rang is {1,7:G5} ~ {2,7:G5} ), Entirety range is {3,7:G5}", word[i], probability[word[i]].low, probability[word[i]].high, Math.Round(range, 7));
                highLow[0] = highLow[1] + (range * probability[word[i]].high);
                Console.WriteLine("Hight = low + ( {0,7:G5} * {1,7:G5} ) = {2,7:G5} ", Math.Round(range, 7), probability[word[i]].high, Math.Round(highLow[0], 7));
                highLow[1] = highLow[1] + (range * probability[word[i]].low);
                Console.WriteLine("Low = low + ( {0,7:G5} * {1,7:G5} ) = {2,7:G5} ", Math.Round(range, 7), probability[word[i]].low, Math.Round(highLow[1], 7));
                Console.WriteLine("Outout = {0} ~ {1}", highLow[1], highLow[0]);
                highLow = output0(highLow);     //檢查範圍是否跨越0.5，若有繼續執行，若無繼續遞迴
                range = highLow[0] - highLow[1];
            }
            output += 1;    //最後範圍跨越0.5取0.5較好計算
            return output;
        }
        /// <summary>
        /// 若範圍落在0.5以內，則做E1 rescale，並回傳一個0
        /// </summary>
        /// <param name="highLow">紀錄high 跟 low的List</param>
        /// <returns>high 跟 low 的計算結果</returns>
        private List<decimal> output0 (List<decimal> highLow)
        {
            if (highLow[0] >= 0.5m && highLow[1] < 0.5m)
                return highLow;
            while (highLow[0] < 0.5m)
            {
                highLow[0] = 2 * highLow[0];
                highLow[1] = 2 * highLow[1];
                output += 0;
                Console.WriteLine("輸出0 : {0}", "0." + output);
            }
            output1(highLow);
            return highLow;
        }
        /// <summary>
        /// 若範圍落在0.5以外，則做E2 rescale，並回傳一個1
        /// </summary>
        /// <param name="highLow">紀錄high 跟 low的List</param>
        /// <returns>high 跟 low 的計算結果</returns>
        private List<decimal> output1 (List<decimal> highLow)
        {
            if (highLow[0] >= 0.5m && highLow[1] < 0.5m)
                return highLow;
            while (highLow[0] >= 0.5m && highLow[1] >= 0.5m)
            {
                highLow[0] = 2 * ( highLow[0] - 0.5m );
                highLow[1] = 2 * ( highLow[1] - 0.5m );
                output += 1;
                Console.WriteLine("輸出1 : {0}", "0." + output);
            }
            output0(highLow);
            return highLow;
        }
    }
    /// <summary>
    /// code 類別 prob 為機率, low 為 下邊界, high 為 上邊界
    /// </summary>
    class code
    {
        public decimal low = 0;
        public decimal high = 1;
        public decimal prob = 1;
    }
}
