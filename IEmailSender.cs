using System;
using System.Collections.Generic;
using System.Text;

namespace IoC_Containers
{
    public interface IEmailSender
    {
        void SendMessage(string message);
    }
}