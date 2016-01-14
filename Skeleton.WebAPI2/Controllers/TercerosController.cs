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
    [RoutePrefix("api/terceros")]
    public class TercerosController : ApiController
    {
        //[Route("InformacionAcudientesMensajes/username/{username}")]
        //public bInformacionAcudienteMensajes GetInformacionAcudienteMensajes(string username)
        //{
        //    mTerceros m = new mTerceros();
        //    return m.GetInformacionAcudienteMensajes(username);
        //}
    }
}
