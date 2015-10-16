using BLL;
using Entidades.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ByA;

namespace AspIdentity.Controllers
{
    [RoutePrefix("api/Tasas")]
    public class TasasController : ApiController
    {
        [Route("")]
        public List<tasasDto> Gets()
        {
            mInteresSuperintendencia o = new mInteresSuperintendencia();
            return o.Gets();
        }
        [Route("")]
        public List<ByARpt> Post(List<tasasDto> lReg)
        {
            mInteresSuperintendencia o = new mInteresSuperintendencia();
            return o.InsertsOrUpdates(lReg);
        }
    }
}
