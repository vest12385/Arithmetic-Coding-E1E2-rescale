using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arithmetic
{
    class encoding
    {
        private Dictionary<char, code> probability = new Dictionary<char, code>();
        private List<char> wordChar = new List<char>();
        private string output = null;
        public encoding(  Dictionary<char, code> probability )
        {
            this.probability = probability;
        }
        public encoding(string word)
        {
            calculateProbability(word);
        }
        public Dictionary<char, code> getProbability
        {
            get { return probability; }
        }
        public List<char> getwordChar
        {
            get { return wordChar; }
        }
        ~encoding() { }
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
        private void handleProbability()
        {
            int AllWord = wordChar.Count;
            wordChar = wordChar.Distinct().ToList();
            decimal limit = 0m;
            foreach (char ecahWord in wordChar)
            {
                if (probability.ContainsKey(ecahWord))
                {
                    probability[ecahWord].prob /= AllWord;
                    probability[ecahWord].low = Math.Round(limit, 7 );
                    probability[ecahWord].high = Math.Round(limit + probability[ecahWord].prob, 7);
                    limit = limit + probability[ecahWord].prob;
                }
            }
        }
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
                highLow = output0(highLow);
                range = highLow[0] - highLow[1];
            }
            output += 1;
            return output;
        }
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
    class code
    {
        public decimal low = 0;
        public decimal high = 1;
        public decimal prob = 1;
    }
}
