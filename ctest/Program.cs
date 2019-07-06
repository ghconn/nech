using System;
using System.Text;
using System.Linq;

namespace ctest
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            LetMeKK("鍘熷瀷/涓撳淇℃伅绠＄悊");
            Console.Read();
        }

        static void LetMeKK(string mixstr)
        {
            Encoding[] encodings = new Encoding[] { Encoding.Default, Encoding.UTF32, Encoding.UTF7, Encoding.UTF8, Encoding.Unicode, Encoding.GetEncoding("gb2312") };
            foreach (var encoding in encodings)
            {
                foreach (var encoding2 in encodings)
                {
                    Console.WriteLine(encoding.EncodingName + " get bytes, " + encoding2.EncodingName + " GetString\n" + encoding2.GetString(encoding.GetBytes(mixstr)));
                }
                Console.WriteLine();
            }
        }
    }
}
