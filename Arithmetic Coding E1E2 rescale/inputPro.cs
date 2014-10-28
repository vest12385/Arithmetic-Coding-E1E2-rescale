using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arithmetic
{
    class inputPro
    {
        public inputPro()
        {
        }
        ~inputPro() { }
        public Dictionary<char, code> calLetter(string word)
        {
            Arithmetic.encoding Letter = new Arithmetic.encoding(word);
            Dictionary<char, code> probability = Letter.getProbability;
            List<char> wordChar = Letter.getwordChar;
            decimal limit = 0m;
            for (int i = 0; i < wordChar.Count(); i++)
            {
                Console.WriteLine("請問 {0} 的機率是多少?", wordChar[i] );
                probability[wordChar[i]].prob = Convert.ToDecimal( Console.ReadLine());
                probability[wordChar[i]].low = Math.Round(limit, 7);
                probability[wordChar[i]].high = Math.Round(limit + probability[wordChar[i]].prob, 7);
                limit = limit + probability[wordChar[i]].prob;
            }
            return probability;
        }
    }
}
