using System;
using BLL;
using Entidades.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ByA;
using Entidades.Consultas;
using AspIdentity.Models;
using System.Security.Claims;
using System.Web;
using System.Net.Http.Headers;
using Entidades.Vistas;

namespace AspIdentity.Controllers
{
    [RoutePrefix("api/Mensajes")]
    public class MensajesController : ApiController
    {
        [Route("{identificacion}")]
        public ByARpt PostMensajes(List<estudiantesDto> listEstuDto, string identificacion)
        {
            mMensajes mMsje = new mMensajes();
            return mMsje.PostMensajes(listEstuDto, identificacion);
        }
    }
}
