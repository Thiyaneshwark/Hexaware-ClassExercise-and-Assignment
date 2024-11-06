using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamWriteReaderDemo
{
    internal class StreamWriteReaderDemo
    {
        public void WriteandRead()
        {
            String filepath = @"D:\HEXAWARE\C#\DOT NET FSD\Assignments\Day4-C# Class Exercise\Text.txt";
            using(StreamWriter writer =new StreamWriter(filepath))
            {
                writer.WriteLine("Sample content from console App!");
            }
            using (StreamReader reader = new StreamReader(filepath))
            {
                string contentReadFromFile=reader.ReadToEnd();
                Console.WriteLine(contentReadFromFile);
            }
        }
    }
}
