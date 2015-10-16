using BLL.DatosBasicos;
using Entidades.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ByA;

namespace Skeleton.WebAPI.Controllers
{
    [RoutePrefix("api/Vigencias")]
    public class VigenciasController : ApiController
    {
        [Route("Gets")]
        public List<vigenciasDto> Gets()
        {
            mVigencias o = new mVigencias();
            return o.Gets();
        }
        [Route("")]
        public List<vigenciasDto> GetsAll()
        {
            mVigencias o = new mVigencias();
            return o.Gets();
        }
        [Route("")]
        public List<ByARpt> Post(List<vigenciasDto> lReg)
        {
            mVigencias o = new mVigencias();
            return o.InsertOrUpdate(lReg);
        }
    }
}
