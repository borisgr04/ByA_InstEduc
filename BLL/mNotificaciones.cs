using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ByA;
using Entidades.Vistas;
using DAL;
using Entidades.Consultas;

namespace BLL
{
    public class mNotificaciones
    {
        ieEntities ctx;   
        public ByARpt EnviarNotificaciones(List<estudiantesDto> lEstudiantes)
        {
            // recorrer ls lista
            /* por cada mensaje buscar el token del acudiente (crear tabla)
             * enviar la notificacion y ya!
             */
            ByARpt r = new ByARpt();
            try
            {
                foreach (estudiantesDto estuDto in lEstudiantes)
                {
                    tokens_notificaciones token = ctx.tokens_notificaciones.Where(t => t.id_tercero == estuDto.id).FirstOrDefault();
                    if (token != null)
                    {
                        //enviar notificacion
                    }
                }
                r.Error = false;
                r.Mensaje = "Alertas enviadas exitosamente!";
            }
            catch
            {
                r.Error = true;
                r.Mensaje = "Error al enviar las Alertas";
            }
            return r;
        }
        public ByARpt PostTokenNotificaciones(bObjetoNotificaciones objNotificaciones)
        {
            cmdInsert o = new cmdInsert();
            o.objNoti = objNotificaciones;
            return o.Enviar();
        }
        public ByARpt DeleteTokenNotificaciones(string identificacion_acudiente)
        {
            cmdDelete o = new cmdDelete();
            o.identificacion_tercero = identificacion_acudiente;
            return o.Enviar();
        }
        class cmdInsert : absTemplate
        {
            public bObjetoNotificaciones objNoti { set; get; }
            public terceros acudiente { set; get; }
            protected internal override bool esValido()
            {
                acudiente = ctx.terceros.Where(t => t.identificacion == objNoti.identificacion_acudiente).FirstOrDefault();
                if(acudiente != null)
                {
                    if (objNoti.token_notificacion != null || objNoti.token_notificacion != "")
                    {
                        return true;
                    }
                    else
                    {
                        byaRpt.Error = true;
                        byaRpt.Mensaje = "El token está vacío";
                        return false;
                    }
                }
                else
                {
                    byaRpt.Error = true;
                    byaRpt.Mensaje = "El Acudiente no existe";
                    return false;
                }
            }

            protected internal override void Antes()
            {
                int id = calcularConsecutivo();
                tokens_notificaciones token_noti = new tokens_notificaciones();
                token_noti.id = id;
                token_noti.id_tercero = acudiente.id;
                token_noti.token = objNoti.token_notificacion;
                ctx.tokens_notificaciones.Add(token_noti);
            }

            private int calcularConsecutivo()
            {
                int id;
                tokens_notificaciones token = ctx.tokens_notificaciones.OrderByDescending(t => t.id).FirstOrDefault();
                if (token == null)
                {
                    id = 0;
                    return id;
                }
                return id = token.id;
            }
        }
        class cmdDelete : absTemplate
        {
            public string identificacion_tercero { set; get; }
            public terceros acudiente { set; get; }
            protected internal override bool esValido()
            {
                acudiente = ctx.terceros.Where(t => t.identificacion == identificacion_tercero).FirstOrDefault();
                if(acudiente != null)
                {
                    return true;
                }
                else
                {
                    byaRpt.Error = true;
                    byaRpt.Mensaje = "El Acudiente no existe";
                    return false;
                }
            }

            protected internal override void Antes()
            {
                tokens_notificaciones token_noti = ctx.tokens_notificaciones.Where(t=> t.id_tercero == acudiente.id).FirstOrDefault();
                if(token_noti != null)
                {
                    ctx.tokens_notificaciones.Remove(token_noti);
                }
            }
        }
    }
}
