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
    [RoutePrefix("api/Mensajes")]
    public class MensajesController : ApiController
    {
        [Route("idAcudiente/{id_acudiente}/idMensaje/{id_mensaje}")]
        public ByARpt PostCambiarEstado(int id_acudiente, int id_mensaje)
        {
            mMensajes msje = new mMensajes();
            return msje.PostCambiarEstado(id_acudiente, id_mensaje);
        }

        [Route("EliminarMensajes")]
        public ByARpt PostCambiarMensajeInactivo(List<mensajesDto> ListMsjeDto)
        {
            mMensajes msje = new mMensajes();
            return msje.PostCambiarEstadoInactivo(ListMsjeDto);
        }
    }
}
