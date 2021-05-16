using System.Collections.Generic;
using VismaBookLibrary.Domain.Models;

namespace VismaBookLibrary.Domain.Interfaces
{
    public interface IPrintService
    {
        void PrintFromList(IEnumerable<Book> list);
        void PrintFromEnum<T>();
    }
}