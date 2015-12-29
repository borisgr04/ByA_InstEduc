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
    [RoutePrefix("api/Cursos")]
    public class CursosController : ApiController
    {
        [Route("")]
        public string GetSaludo()
        {
            return "Hola Mundo!!!";
        }
    }
}
