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
    [RoutePrefix("api/ConfiguracionGrupoPago")]
    public class ConfiguracionGrupoPagoController: ApiController
    {

        [Route("")]
        public List<configGruposPagosDto> GetAll()
        {
            mConfigGruposPagos o = new mConfigGruposPagos();
            return o.Gets();
        }
        [Route("{id}")]
        public configGruposPagosDto Get(int id)
        {
            mConfigGruposPagos o = new mConfigGruposPagos();
            return o.Get(id);
        }
        [Route("Vigencia/{vigencia}")]
        public List<configGruposPagosDto> GetVigencia(int vigencia)
        {
            mConfigGruposPagos o = new mConfigGruposPagos();
            return o.Gets(vigencia);
        }
        [Route("")]
        public List<ByARpt> Post(List<configGruposPagosDto> lReg)
        {
            mConfigGruposPagos o = new mConfigGruposPagos();
            return o.InsertsOrUpdates(lReg);
        }
    }
}
