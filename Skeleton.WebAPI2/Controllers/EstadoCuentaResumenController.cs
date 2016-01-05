using BLL;
using Entidades.Consultas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Skeleton.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize]
    [RoutePrefix("api/EstadoCuentaResumen")]
    public class EstadoCuentaResumenController : ApiController
    {
        [Route("{id_estudiante}")]
        public List<cEstadoCuenta> GetEstadoCuenta(string id_estudiante)
        {
            mEstadoCuenta oEstCuenta = new mEstadoCuenta();
            return oEstCuenta.GetEstadoCuentaResumido(id_estudiante);
        }
    }
}
