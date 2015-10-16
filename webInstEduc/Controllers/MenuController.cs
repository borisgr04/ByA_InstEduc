using BLL.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Skeleton.WebAPI.Controllers
{
    [RoutePrefix("api/Menu")]
    public class MenuController : ApiController
    {
        [Route("GetMenu/{modulo}/{usuario}")]
        public List<dataTree> GetMenu(string modulo, string usuario)
        {
            gesMenuAdapter mg = new gesMenuAdapter();
            return mg.getOpciones(modulo, usuario);
        }
    }
}
