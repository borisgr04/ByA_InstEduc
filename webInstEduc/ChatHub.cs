using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Entidades.Vistas;
using BLL;

namespace AspIdentity
{
    public class ChatHub : Hub
    {
        //private static Hashtable htUsers_ConIds = new Hashtable(20);
        private static Dictionary<string, string> htUsers_ConIds = new Dictionary<string, string>();

        public int numId()
        {
            return htUsers_ConIds.Count;
        }
        /*public void registerIdRemitente(string identificacion)
        {
            mTerceros mterce = new mTerceros();
            tercerosDto terce = mterce.idTercero(identificacion);
            if(terce != null)
            {
                if(htUsers_ConIds.Count == 0)
                {
                    htUsers_ConIds.Add(terce.id.ToString(), Context.ConnectionId);
                }
                else
                {
                    if(!htUsers_ConIds.ContainsKey(terce.id.ToString()))
                    {
                        htUsers_ConIds.Add(terce.id.ToString(), Context.ConnectionId);
                    }
                }
            }
        }
        
        public void registrarIdAcudientes(List<estudiantesDto> listEstuDto)
        {
            foreach (estudiantesDto estuDto in listEstuDto)
	        {   
		        if(htUsers_ConIds.Count == 0 )
                {
                    htUsers_ConIds.Add(estuDto.id_acudiente.ToString(), Context.ConnectionId);
                }
                else
                {
                    if(!htUsers_ConIds.ContainsKey(estuDto.id_acudiente.ToString()))
                    {
                        htUsers_ConIds.Add(estuDto.id_acudiente.ToString(), Context.ConnectionId);
                    }
                }
	        }
        }
        public void Send(List<estudiantesDto> ListEstudianteDto, string remitente)
        {
            mTerceros mTercero = new mTerceros();
            tercerosDto terceroDto = mTercero.idTercero(remitente);
            if(terceroDto != null)
            {
                string clienteFuente = htUsers_ConIds[terceroDto.id.ToString()].ToString();
                foreach (estudiantesDto estuDto in ListEstudianteDto)
                {
                    string clienteDestino = htUsers_ConIds[estuDto.id_acudiente.ToString()].ToString();
                    List<string> l = new List<string>();
                    l.Add(htUsers_ConIds[terceroDto.id.ToString()]);
                    l.Add(htUsers_ConIds[estuDto.id_acudiente.ToString()]);
                    Clients.Clients(l).broadcastMessage(terceroDto.nombre,estuDto.mensaje);
                }
            }
        }*/

        //Chat//
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