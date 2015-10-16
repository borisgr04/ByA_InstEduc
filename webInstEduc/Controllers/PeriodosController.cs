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
    [RoutePrefix("api/Periodos")]
    public class PeriodosController : ApiController
    {
        [Route("Gets/{vigencia}")]
        public List<periodosDto> GetCarteraEstudiante(int vigencia)
        {
            mPeriodos o = new mPeriodos();
            return o.Gets(vigencia);
        }
        [Route("")]
        public List<periodosDto> GetAll()
        {
            mPeriodos o = new mPeriodos();
            return o.Gets();
        }
        [Route("{vigencia}")]
        public List<periodosDto> GetVigencia(int vigencia)
        {
            mPeriodos o = new mPeriodos();
            return o.Gets(vigencia);
        }
        [Route("")]
        public List<ByARpt> Post(List<periodosDto> lReg)
        {
            mPeriodos o = new mPeriodos();
            return o.InsertOrUpdate(lReg);
        }
    }
}
