using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entidades.Vistas;
using AutoMapper;
using ByA;
namespace BLL
{
    public class mMensajesAcudiente
    {
        ieEntities ctx;
        public mMensajesAcudiente()
        {
            Mapper.CreateMap<mensajes, mensajesDto>();
            Mapper.CreateMap<mensajesDto,mensajes>();
        }

        public List<mensajesDto> GetMensajes(int id_acudiente)
        {
            using(ctx = new ieEntities())
            {
                List<mensajesDto> ListMsjeDto = new List<mensajesDto>();
                List<mensajes> ListMsje = ctx.mensajes.Where(t => t.mensajes_acudientes.Where(c => c.id_acudiente == id_acudiente && c.estado != "IN").ToList().Count() > 0).ToList();                
                Mapper.Map(ListMsje, ListMsjeDto);
                foreach (mensajesDto mens in ListMsjeDto)
                {
                    mensajes_acudientes mensaje = ListMsje.Where(t => t.id == mens.id).FirstOrDefault().mensajes_acudientes.Where(t => t.id_acudiente == id_acudiente).FirstOrDefault();
                    mens.estado_mensaje_acudiente = mensaje.estado;
                    mens.id_mensaje_acudiente = mensaje.id;
                }
                return ListMsjeDto;
            }
        }

        public ByARpt cambiarEstado(int id_mensaje_acudiente)
        {
            cmdUpdate o = new cmdUpdate();
            o.id_mensaje = id_mensaje_acudiente;
            return o.Enviar();
        }

        public ByARpt cambiarMensajeInactivo(List<mensajesDto> ListMsjeDto)
        {
            try
            {
                cmdUpdateMensajeInactivo o = new cmdUpdateMensajeInactivo();
                foreach (mensajesDto msjeDto in ListMsjeDto)
                {
                    o.id_mensaje = msjeDto.id;
                    o.Enviar();
                }
                ByARpt res = new ByARpt();
                res.Error = false;
                res.Mensaje = "Se eliminaron los mensajes correctamente";
                return res;
            }
            catch
            {
                ByARpt res = new ByARpt();
                res.Error = true;
                res.Mensaje = "Ha ocurrido un error al intentar Eliminar los mensajes";
                return res;
            }
        }

        class cmdUpdate : absTemplate
        {
            public int id_mensaje { get; set; }
            private mensajes_acudientes Dto { get; set; }
            #region ImplementaciónMetodosAbstractos
            protected internal override bool esValido()
            {
                Dto = ctx.mensajes_acudientes.Where(t => t.id == id_mensaje).FirstOrDefault();
                if (Dto != null) return true;
                else
                {
                    byaRpt.Error = true;
                    byaRpt.Mensaje = "El Mensaje no se encuentra registrado";
                    return false;
                }
            }
            protected internal override void Antes()
            {
                Dto.estado = "Revisado";
            }
            #endregion
        }

        class cmdUpdateMensajeInactivo : absTemplate
        {
            public int id_mensaje { get; set; }
            private mensajes_acudientes Dto { get; set; }
            protected internal override bool esValido()
            {
                Dto = ctx.mensajes_acudientes.Where(t => t.id == id_mensaje).FirstOrDefault();
                if (Dto != null) return true;
                else
                {
                    byaRpt.Error = true;
                    byaRpt.Mensaje = "El Mensaje no se encuentra registrado";
                    return false;
                }
            }

            protected internal override void Antes()
            {
                Dto.estado = "IN";
            }

        }

    }
}
