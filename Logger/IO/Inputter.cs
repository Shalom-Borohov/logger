using System;

namespace Logger.IO
{
    public class Inputter
    {
        public int ReadParsedLine() => Int32.Parse(ReadLine());
        public string ReadLine() => Console.ReadLine();
    }
}
