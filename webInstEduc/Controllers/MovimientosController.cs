using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entidades.Vistas;
using Entidades.Consultas;
using BLL;

namespace AspIdentity.Controllers
{
    [RoutePrefix("api/Movimientos")]
    public class MovimientosController : ApiController
    {
        [Route("Estudiante")]
        public List<movimientosDto> PostMovimientosEstudiante(bTransaccionesEstudiante Reg)
        {
            mMovimientos o = new mMovimientos();
            return o.GetsMovimientosEstudiante(Reg);
        }
    }
}
