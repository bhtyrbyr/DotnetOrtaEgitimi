using System;

namespace WebApi.Services
{
    public class ConsoleLogger : ILoggerServices    
    {
        public void write(string message)
        {
            Console.WriteLine("[ConsoleLogger] - " + message);
        }
    }
}