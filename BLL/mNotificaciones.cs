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
                    if (objNoti.token_notificacion != null && objNoti.token_notificacion != "")
                    {
                        return true;
                    }
                    else
                    {
                        byaRpt.Error = true;
                        byaRpt.Mensaje = "No hay token";
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
                tokens_notificaciones tokenAcu = ctx.tokens_notificaciones.Where(t => t.id_tercero == acudiente.id).FirstOrDefault();
                if (tokenAcu == null)
                {
                    int id = calcularConsecutivo();
                    tokens_notificaciones token_noti = new tokens_notificaciones();
                    token_noti.id = id;
                    token_noti.id_tercero = acudiente.id;
                    token_noti.token = objNoti.token_notificacion;
                    ctx.tokens_notificaciones.Add(token_noti);
                }
                else
                {
                    tokenAcu.token = objNoti.token_notificacion;
                }
            }

            private int calcularConsecutivo()
            {
                tokens_notificaciones token = ctx.tokens_notificaciones.OrderByDescending(t => t.id).FirstOrDefault();
                if (token == null) return 1;
                return token.id + 1;
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
