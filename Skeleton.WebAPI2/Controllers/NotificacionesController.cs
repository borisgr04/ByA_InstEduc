using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ByA;
using Entidades.Vistas;
using BLL;
using Entidades.Consultas;
using System.Web.Http.Cors;

namespace Skeleton.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    //[Authorize]
    [RoutePrefix("api/Notificaciones")]
    public class NotificacionesController : ApiController
    {
        [Route("")]
        public ByARpt Post(bObjetoNotificaciones obj)
        {
            mNotificaciones o = new mNotificaciones();
            return o.PostTokenNotificaciones(obj);
        }
    }
}
