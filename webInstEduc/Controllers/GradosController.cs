using BLL;
using ByA;
using Entidades.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Skeleton.WebAPI.Controllers
{
    [RoutePrefix("api/Grados")]
    public class GradosController : ApiController
    {
        [Route("")]
        public List<gradosDto> Gets()
        {
            mGrados o = new mGrados();
            return o.Gets();
        }
        [Route("")]
        public List<ByARpt> Post(List<gradosDto> lReg)
        {
            mGrados o = new mGrados();
            return o.InsertsOrUpdates(lReg);
        }
    }
}
