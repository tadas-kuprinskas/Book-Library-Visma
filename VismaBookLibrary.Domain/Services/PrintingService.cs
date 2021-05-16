using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaBookLibrary.Domain.Extensions;
using VismaBookLibrary.Domain.Interfaces;
using VismaBookLibrary.Domain.Models;

namespace VismaBookLibrary.Domain.Services
{
    public class PrintingService : IPrintService
    {
        private readonly IWriter _writer;

        public PrintingService(IWriter writer)
        {
            _writer = writer;
        }

        public void PrintFromList(IEnumerable<Book> list)
        {
            foreach (var item in list)
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(item))
                {
                    string name = descriptor.Name;
                    object value = descriptor.GetValue(item);
                    _writer.PrintLine($"{name} : {value}");
                }
                _writer.PrintLine("");
            }
        }

        public void PrintFromEnum<T>()
        {
            _writer.PrintLine("\nAvailable Commands:");

            var commands = Enum.GetValues(typeof(T)).Cast<T>();

            foreach (var command in commands)
            {
                _writer.PrintLine($"{command } : {command.ToDescriptionString()}");
            }
        }
    }
}
