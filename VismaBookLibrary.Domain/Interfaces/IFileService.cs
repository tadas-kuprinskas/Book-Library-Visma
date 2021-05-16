using System.Collections.Generic;
using VismaBookLibrary.Domain.Models;

namespace VismaBookLibrary.Domain.Interfaces
{
    public interface IFileService
    {
        IEnumerable<Book> GetAll();
        void Overwrite(IEnumerable<Book> books);
        void SaveNew(Book book);
    }
}