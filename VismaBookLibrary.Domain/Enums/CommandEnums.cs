using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaBookLibrary.Domain.Enums
{
    public enum CommandEnums
    {       
        [Description("Add a new book")]
        Add,
        [Description("Delete a book")]
        Delete,
        [Description("Borrow a book")]
        Take,
        [Description("Return a book")]
        Return,
        [Description("List all books")]
        All,
        [Description("Filter books")]
        Filter
    }
}
