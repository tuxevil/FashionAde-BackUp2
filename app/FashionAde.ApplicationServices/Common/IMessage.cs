using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FashionAde.ApplicationServices
{
    public interface IMessage
    {
        string Subject { get; set; }
        string Body { get; set; }
    }
}
