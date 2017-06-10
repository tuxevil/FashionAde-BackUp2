using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FashionAde.ApplicationServices
{
    public interface IContactMessage : IMessage
    {
        string To { get; set; }
        string From { get; set; }
    }
}
