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

        public ByARpt PostMensajes(List<estudiantesDto> ListEstudiantesDto, string identificacion)
        {
            cmdInsert o = new cmdInsert();
            o.ListEstDto = ListEstudiantesDto;
            o.identificacion_remitente = identificacion;
            return o.Enviar();
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
                }
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
