using Entidades.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class mConceptosACartera
    {
        public ConceptosPeriodosDto GetConfiguracionPosible(int vigencia)
        {
            ConceptosPeriodosDto objRes = new ConceptosPeriodosDto();
            objRes.lConceptos = new List<conceptosDto>();
            objRes.lPeriodos = new List<periodosDto>();
            mConfigGruposPagos oConfigGru = new mConfigGruposPagos();
            mConceptos oConceptos = new mConceptos();
            mPeriodos oPeriodos = new mPeriodos();

            List<configGruposPagosDto> lConfig = oConfigGru.Gets(vigencia);
            foreach (configGruposPagosDto config in lConfig)
            {
                conceptosDto concepto = oConceptos.Get(config.id_concepto);
                objRes.lConceptos.Add(concepto);
            }

            objRes.lPeriodos = oPeriodos.Gets(vigencia);

            return objRes;
        }
    }
}
