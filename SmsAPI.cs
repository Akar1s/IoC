using System;
using System.Collections.Generic;
using System.Text;

namespace IoC_Containers
{
    public class SmsAPI : ISmsAPI
    {
        public void SendMessage(string message)
        {
            Console.WriteLine($"Отправленное сообщение по смс: {message}");
        }
    }
}