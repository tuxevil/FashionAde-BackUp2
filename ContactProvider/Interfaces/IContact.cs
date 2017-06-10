using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactProvider
{
    public interface IContact
    {
        int Index { get; set; }
        string FirstName { get; set;}
        string LastName { get; set; }
        string Email { get; set; }
    }
}
