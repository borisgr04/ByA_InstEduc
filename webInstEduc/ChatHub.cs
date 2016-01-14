using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace AspIdentity
{
    public class ChatHub : Hub
    {
        //private static Hashtable htUsers_ConIds = new Hashtable(20);
        private static Dictionary<string, string> htUsers_ConIds = new Dictionary<string, string>();
        public void registerConId(string userID)
        {
            //bool alreadyExists = false;
            if (htUsers_ConIds.Count == 0)
            {
                htUsers_ConIds.Add(userID, Context.ConnectionId);
            }
            else
            {
                if (!htUsers_ConIds.ContainsKey(userID))
                {
                    htUsers_ConIds.Add(userID, Context.ConnectionId);
                }
            }
        }
        public void Send(string name, string message, string user)
        {
            // Call the broadcastMessage method to update clients.
            if (!String.IsNullOrEmpty(user))
            {
                string clienteFuente = htUsers_ConIds[name].ToString();
                string clienteDestino = htUsers_ConIds[user].ToString();
                List<string> l = new List<string>();
                l.Add(htUsers_ConIds[name].ToString());
                l.Add(htUsers_ConIds[user].ToString());
                Clients.Clients(l).broadcastMessage(name, message);
                //Clients.Client(cliente).broadcastMessage(name, message);
            }
            else
            {
                Clients.All.broadcastMessage(name, message);
            }

        }
    }
}