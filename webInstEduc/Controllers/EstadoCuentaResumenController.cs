using BLL;
using Entidades.Consultas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspIdentity.Controllers
{
    [RoutePrefix("api/EstadoCuentaResumen")]
    public class EstadoCuentaResumenController : ApiController
    {
        [Route("{id_estudiante}/Vigencia/{vigencia}")]
        public List<cEstadoCuenta> GetEstadoCuenta(string id_estudiante, int vigencia)
        {
            mEstadoCuenta oEstCuenta = new mEstadoCuenta();
            return oEstCuenta.GetEstadoCuentaResumido(id_estudiante);
        }
    }
}
