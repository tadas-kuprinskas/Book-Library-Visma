using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaBookLibrary.Domain.Enums
{
    public enum FilteringEnums
    {
        [Description("Filter by ISBN")]
        Isbn,
        [Description("Filter by Language")]
        Language,
        [Description("Filter by the Name of the book")]
        Name,
        [Description("Filter by Author")]
        Author,
        [Description("Filter by Category")]
        Category       
    }
}
