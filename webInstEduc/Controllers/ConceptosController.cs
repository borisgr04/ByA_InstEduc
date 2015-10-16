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
    [RoutePrefix("api/Conceptos")]
    public class ConceptosController : ApiController
    {
        [Route("")]
        public List<conceptosDto> Gets()
        {
            mConceptos o = new mConceptos();
            return o.Gets();
        }
        [Route("")]
        public List<ByARpt> Post(List<conceptosDto> lReg)
        {
            mConceptos o = new mConceptos();
            return o.InsertsOrUpdates(lReg);
        }
    }
}
