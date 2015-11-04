using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ByA;
using Entidades.Vistas;

namespace AspIdentity.Controllers
{
    [RoutePrefix("api/FechaCausacion")]
    public class FechaCausacionController : ApiController
    {
        [Route("")]
        public DateTime Get()
        {
            return mCausacion.FechaCausacion();
        }
        [Route("")]
        public ByARpt Post(fechaCausacionDto Reg)
        {
            mCausacion o = new mCausacion();
            return o.CambiarFechaCausacion(Reg.Fecha);
        }
    }
}
