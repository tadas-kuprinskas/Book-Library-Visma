using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaBookLibrary.Domain.Interfaces;

namespace VismaBookLibrary.Application.Writers
{
    public class ConsoleWriter : IWriter
    {
        public void PrintLine(string input)
        {
            Console.WriteLine(input);
        }

        public void PrintLine()
        {
            Console.WriteLine();
        }

        public string ReadLine(string message)
        {
            PrintLine(message);
            return Console.ReadLine();
        }
    }
}
