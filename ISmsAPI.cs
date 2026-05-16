using System;
using System.Collections.Generic;
using System.Text;

namespace IoC_Containers
{
    public interface ISmsAPI
    {
        void SendMessage(string message);
    }
}