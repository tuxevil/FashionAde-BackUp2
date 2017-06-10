using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.Accounts;

namespace FashionAde.ApplicationServices
{
    public interface IMessageSenderService
    {
        void Send(IMessage message, string to);
        void Send(IMessage message, string to, string replyTo);
        void SendWithTemplate(string filePrefix, IBasicUser user, object data, string to);
        void SendWithTemplate(string filePrefix, IBasicUser user, object data, string to, string replyTo);
    }





}
