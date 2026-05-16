using System;
using System.Collections.Generic;
using System.Text;

namespace IoC_Containers
{
    public class NotificationService(IEmailSender a, ISmsAPI b)
    {
        public void FromEmail(string message)
        {
            a.SendMessage(message);
        }

        public void FromSms(string message)
        {
            b.SendMessage(message);
        }
    }
}