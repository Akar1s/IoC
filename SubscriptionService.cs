using System;
using System.Collections.Generic;
using System.Text;

namespace IoC_Containers
{
    public class SubscriptionService(IEmailSender a)
    {
        public void Subscribe()
        {
            a.SendMessage("Спасибо за подписку на OnlyFance");
        }
    }
}