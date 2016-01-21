using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades.Vistas;
using Entidades.Consultas;
using Entidades.Security;
using DAL;
using ByA;
using AutoMapper;
using BLL.Security;
using RestSharp;

namespace BLL
{
    public class mMensajes
    {
        ieEntities ctx;
        public mMensajes()
        {
            Mapper.CreateMap<mensajes, mensajesDto>();
            Mapper.CreateMap<mensajesDto, mensajes>();
        }

        public List<mensajesDto> GetMensajes(int id_acudiente)
        {
            using(ctx = new ieEntities())
            {
                List<mensajesDto> ListMsjeDto = new List<mensajesDto>();
                List<mensajes> ListMsje = ctx.mensajes.Where(t => t.id_destinatario == id_acudiente && t.estado != "IN").OrderByDescending(t=> t.fecha).ToList();
                Mapper.Map(ListMsje, ListMsjeDto);
                return ListMsjeDto;
            }
        }

        public ByARpt PostCambiarEstado(int id_acudiente, int id_mensaje)
        {
            cmdUpdateEstado o = new cmdUpdateEstado();
            o.id_acudiente = id_acudiente;
            o.id_mensaje = id_mensaje;
            return o.Enviar();
        }

        public ByARpt PostMensajes(List<estudiantesDto> ListEstudiantesDto, string identificacion)
        {
            cmdInsert o = new cmdInsert();
            o.ListEstDto = ListEstudiantesDto;
            o.identificacion_remitente = identificacion;
            return o.Enviar();
        }

        public ByARpt PostCambiarEstadoInactivo(List<mensajesDto> listMsgDto)
        {
            cmdUpdateEstadoInactivo o = new cmdUpdateEstadoInactivo();
            o.ListMsjeDto = listMsgDto;
            return o.Enviar();
        }

        class cmdUpdateEstadoInactivo : absTemplate
        {
            public List<mensajesDto> ListMsjeDto { get; set; }

            protected internal override bool esValido()
            {
                if(ListMsjeDto == null)
                {
                    byaRpt.Error = true;
                    byaRpt.Mensaje = "Listado de mensajes vacío";
                    return false;
                }
                else
                {
                    return true;
                }
            }
            protected internal override void Antes()
            {
                foreach (mensajesDto msjeDto in ListMsjeDto)
                {
                    mensajes msg = ctx.mensajes.Where(t => t.id == msjeDto.id).FirstOrDefault();
                    msg.estado = "IN";
                }
            }
        }

        class cmdUpdateEstado : absTemplate
        {
            public int id_acudiente { get; set; }
            public int id_mensaje { get; set; }
            public mensajes msje { get; set; }
            protected internal override bool esValido()
            {
                terceros acudiente = ctx.terceros.Where(t => t.id == id_acudiente).FirstOrDefault();
                if(acudiente == null)
                {
                    byaRpt.Error = true;
                    byaRpt.Mensaje = "El Acudiente no existe";
                    return false;
                }
                else
                {
                    msje = ctx.mensajes.Where(t => t.id == id_mensaje && t.id_destinatario == id_acudiente).FirstOrDefault();
                    if(msje == null)
                    {
                        byaRpt.Error = true;
                        byaRpt.Mensaje = "El mensaje no existe o no fue enviado al acudiente";
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            protected internal override void Antes()
            {
                msje.estado = "Revisado";

            }
        }

        class cmdInsert : absTemplate
        {
            public List<estudiantesDto> ListEstDto { get; set; }
            public string identificacion_remitente { get; set; }
            public terceros remitente { get; set; }

            #region ImplementaciónMetodosAbstractos
            protected internal override bool esValido()
            {
                remitente = ctx.terceros.Where(t => t.identificacion == identificacion_remitente).FirstOrDefault();
                if(remitente == null)
                {
                    byaRpt.Error = true;
                    byaRpt.Mensaje = "El remitente no existe";
                    return false;
                }
                else
                {
                    foreach (estudiantesDto estuDto in ListEstDto)
                    {
                        if (estuDto.mensaje == "" || estuDto.mensaje == null)
                        {
                            byaRpt.Error = true;
                            byaRpt.Mensaje = "Algún mensaje está vacío";
                            return false;
                        }
                    }
                    return true;
                }
            }
            protected internal override void Antes()
            {
                List<string> lTokens = new List<string>();
                int id = calcularConsecutivo();
                foreach(estudiantesDto estuDto in ListEstDto)
                {
                    id++;
                    mensajes msje = new mensajes();
                    msje.id = id;
                    msje.asunto = estuDto.asunto;
                    msje.mensaje = estuDto.mensaje;
                    msje.tipo = estuDto.tipo_mensaje;
                    msje.id_remitente = remitente.id;
                    msje.estado = "Sin Revisar";
                    msje.id_destinatario = estuDto.id_acudiente;
                    msje.fecha = DateTime.Now;
                    ctx.mensajes.Add(msje);

                   
                    tokens_notificaciones token = ctx.tokens_notificaciones.Where(t => t.id_tercero == estuDto.id_acudiente).FirstOrDefault();
                    if (token != null && token.token != null && token.token != "")
                    {
                        lTokens.Add(token.token);
                    }
                }
                EnviarNotificaciones(lTokens);
            }
            private void EnviarNotificaciones(List<string> ltokens)
            {
                string url = "https://push.ionic.io/api/v1/push";

                var client = new RestClient(url);
                var request = new RestRequest("", Method.POST);

                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("X-Ionic-Application-Id", "569d145a"); // Numero del proyecto Ionic
                request.AddHeader("Authorization", "NzVhZWIwNjlmNjMwZDAwZTk3ZDdlZjA3MGIzZTc2ODFlMjI3NDVlMTM4YTYxMDFj"); // Clave privada en Base4

                objRequestPushApi obj = new objRequestPushApi()
                {
                    tokens = ltokens,
                    notification = new obj_notification
                    {
                        alert = "Tiene nuevos mensajes por leer!!",
                        ios = new obj_ios()
                        {
                            badge = "1",
                            sound = "ping.aiff",
                            expiry = "1423238641",
                            priority = "10",
                            contentAvailable = "1",
                            payload = new obj_payload
                            {
                                key1 = "value",
                                key2 = "value"
                            }
                        },
                        android = new obj_android
                        {
                            collapseKey = "foo",
                            delayWhileIdle = "true",
                            timeToLive = "300",
                            payload = new obj_payload
                            {
                                key1 = "value",
                                key2 = "value"
                            }
                        }
                    }
                };
                request.AddJsonBody(obj);

                respuestaApiRest queryResult = client.Execute<respuestaApiRest>(request).Data;
            }
            private int calcularConsecutivo()
            {
                int id;
                mensajes mensaje = ctx.mensajes.OrderByDescending(t=> t.id).FirstOrDefault();
                if(mensaje == null){
                    id = 0;
                    return id;
                }
                return id = mensaje.id;
            }
            #endregion
        }
    }
}
