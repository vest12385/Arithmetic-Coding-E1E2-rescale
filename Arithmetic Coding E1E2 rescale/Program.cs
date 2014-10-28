using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arithmetic_Coding_E1E2_rescale
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Input encode word:");       //讀入Alphabet
            string word = Console.ReadLine();
            Arithmetic.encoding EnCoding1 = new Arithmetic.encoding( word );
            string output1 = EnCoding1.encoder(word);
            Console.WriteLine("Output : {0}", "0." + output1);    //將編碼結果輸出
            Arithmetic.decoding DeCoding1 = new Arithmetic.decoding(output1+"000000");
            string input1 = DeCoding1.decode(EnCoding1.getProbability, word.Count());    //將解碼碼結果輸出
            Console.WriteLine( "Origin Input: {0}", word );
            Console.WriteLine("Decoder Input : {0}", input1);
            Console.Read();
        }
    }
}
