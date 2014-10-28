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
            Console.Write("Input encode word:");
            string word = Console.ReadLine();
            Console.Write("是否已知機率 ( 1代表示已知，須自行輸入; 0代表未知，由電腦運算 ):");
            string know = Console.ReadLine();
            switch ( know )
            { 
                case "1":
                    Arithmetic.inputPro inputcal = new Arithmetic.inputPro();
                    Arithmetic.encoding EnCoding1 = new Arithmetic.encoding( inputcal.calLetter(word));
                    string output1 = EnCoding1.encoder(word);
                    Console.WriteLine( "Output : {0}", "0." + output1 );
                    Arithmetic.decoding DeCoding1 = new Arithmetic.decoding(output1+"000000");
                    string input1 = DeCoding1.decode( EnCoding1.getProbability, word.Count());
                    Console.WriteLine( "Origin Input: {0}", word );
                    Console.WriteLine("Decoder Input : {0}", input1);
                    break;
                case "0":
                    Arithmetic.encoding EnCoding = new Arithmetic.encoding(word);
                    string output = EnCoding.encoder(word);
                    Console.WriteLine( "Output : {0}", "0.1" + output );
                    Arithmetic.decoding DeCoding = new Arithmetic.decoding(output);
                    string input = DeCoding.decode( EnCoding.getProbability, word.Count());
                    Console.WriteLine( "Origin Input: {0}", word );
                    Console.WriteLine("Decoder Input : {0}", input);
                    break;
                default :
                    Console.WriteLine("只有0跟1喔~");
                    break;
            }
            Console.Read();
        }
    }
}
