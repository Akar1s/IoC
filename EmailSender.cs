using System;
using System.Collections.Generic;
using System.Text;

namespace IoC_Containers
{
    public class EmailSender : IEmailSender
    {
        public void SendMessage(string message)
        {
            Console.WriteLine($"Отправленное сообщение на почту: {message}");
        }
    }
}