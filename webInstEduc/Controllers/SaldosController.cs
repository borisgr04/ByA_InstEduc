using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspIdentity.Controllers
{
    //http://elbauldelprogramador.com/buenas-practicas-para-el-diseno-de-una-api-restful-pragmatica/
    [RoutePrefix("api/Estudiantes/{ide_est}/Saldos")]
    public class SaldosController : ApiController
    {
        mConCartera mc = new mConCartera();

        [Route("")]
        public List<vmCarteraxSaldos> GetSaldos(string ide_est) {
            return mc.GetSaldos(ide_est);
        }

        [Route("vigencia/{vigencia}")]
        public List<vmCarteraxSaldos> GetSaldos(string ide_est, int vigencia) {
            return mc.GetSaldos(ide_est, vigencia);
        }

        [Route("vigencia/{vigencia}/periodo/{periodo}")]
        public List<vmCarteraxSaldosxConceptos> GetSaldos(string ide_est, int vigencia, int periodo)
        {
            return mc.GetSaldos(ide_est, vigencia, periodo);
        }
        
    }
}
