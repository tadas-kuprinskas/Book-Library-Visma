using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaBookLibrary.Domain.Interfaces
{
    public interface IFilterCommand
    {
        void Execute(string option, string phrase);
    }
}
