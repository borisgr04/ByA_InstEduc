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
    [RoutePrefix("api/ConceptosACartera")]
    public class ConceptosACarteraController : ApiController
    {
        [Route("Configuracion/{vigencia}")]
        public ConceptosPeriodosDto GetConfiguracionPosible(int vigencia)
        {
            mConceptosACartera oConcpCart = new mConceptosACartera();
            return oConcpCart.GetConfiguracionPosible(vigencia);
        }
        [Route("")]
        public ByARpt Post(oNuevoConceptoDto Reg)
        {
            mConceptosACartera oConcpCart = new mConceptosACartera();
            return oConcpCart.NuevoConceptoACartera(Reg);
        }
    }
}
