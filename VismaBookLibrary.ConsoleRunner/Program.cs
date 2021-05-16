using System;
using VismaBookLibrary.Application;
using Microsoft.Extensions.DependencyInjection;

namespace VismaBookLibrary.ConsoleRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = DepedencyInjection.ConfigureServices();            

            var bookLibraryCLI = serviceProvider.GetService<BookLibraryCLI>();          

            while (true)
            {               
                bookLibraryCLI.ReadAndExecuteCommand();
            }
        }
    }
}
