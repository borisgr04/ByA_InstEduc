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
    [RoutePrefix("api/TipoDocumentos")]
    public class TiposDocumentosController : ApiController
    {
        [Route("")]
        public List<tiposDocumentosDto> Gets()
        {
            mTiposDocumentos o = new mTiposDocumentos();
            return o.Gets();
        }
        [Route("")]
        public List<ByARpt> Post(List<tiposDocumentosDto> lReg)
        {
            mTiposDocumentos o = new mTiposDocumentos();
            return o.InsertsOrUpdates(lReg);
        }
    }
}
