using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RaoVat.Common;
namespace RaoVat.Hubs
{
    public class Chat : Hub
    {
        public void Connect(string name,string IDUser)
        {
            Clients.Caller.connect(name,IDUser);
        }
        public void Message(string name,string message)
        {
            Clients.All.message(name, message);
        }
    }
}