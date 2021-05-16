using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaBookLibrary.Domain.Interfaces
{
    public interface IWriter
    {
        void PrintLine(string input);
        void PrintLine();
        string ReadLine(string message);
    }
}
