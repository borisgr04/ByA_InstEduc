using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;
using ByA;
using Entidades.Vistas;
using Entidades.Consultas;
using System.Web.Http.Cors;

namespace Skeleton.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize]
    [RoutePrefix("api/MensajeAcudiente")]
    public class MensajeAcudienteController : ApiController
    {
        [Route("{id_mensaje_acudiente}")]
        public ByARpt GetCambiarEstado(int id_mensaje_acudiente)
        {
            mMensajesAcudiente msje = new mMensajesAcudiente();
            return msje.cambiarEstado(id_mensaje_acudiente);
        }
    }
}
