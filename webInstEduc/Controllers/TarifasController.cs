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
    [RoutePrefix("api/Tarifa")]
    public class TarifasController : ApiController
    {

        [Route("")]
        public List<tarifasDto> GetAll()
        {
            mTarifas o = new mTarifas();
            return o.Gets();
        }
        [Route("{id}")]
        public tarifasDto Get(int id)
        {
            mTarifas o = new mTarifas();
            return o.Get(id);
        }
        [Route("Vigencia/{vigencia}")]
        public List<tarifasDto> GetVigencia(int vigencia)
        {
            mTarifas o = new mTarifas();
            return o.Gets(vigencia);
        }
        [Route("Vigencia/{vigencia}/Grado/{grado}")]
        public List<tarifasDto> GetVigencia(int vigencia,int grado)
        {
            mTarifas o = new mTarifas();
            return o.GetTarifas(vigencia,grado);
        }
        [Route("")]
        public List<ByARpt> Post(List<tarifasDto> lReg)
        {
            mTarifas o = new mTarifas();
            return o.InsertsOrUpdates(lReg);
        }
    }
}
