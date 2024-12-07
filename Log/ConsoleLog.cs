using System;

namespace Log
{
    public class ConsoleLog : ILog
    {
        public void print(string message)
        {
            Console.WriteLine(message);
        }
    }
}