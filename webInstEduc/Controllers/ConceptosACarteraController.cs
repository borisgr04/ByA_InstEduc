using BLL;
using Entidades.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ByA;
using System.Net.Http.Headers;

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
            Reg.usu = GetUser();
            mConceptosACartera oConcpCart = new mConceptosACartera();
            return oConcpCart.NuevoConceptoACartera(Reg);
        }
        private string GetUser()
        {
            string sessionId = "";
            CookieHeaderValue cookie = Request.Headers.GetCookies("fc_user").FirstOrDefault();
            if (cookie != null) sessionId = cookie["fc_user"].Value;
            return sessionId;
        }
    }
}
