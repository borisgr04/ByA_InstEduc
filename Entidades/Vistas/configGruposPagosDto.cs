using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Vistas
{
    public class configGruposPagosDto
    {
        public Nullable<int> id { get; set; }
        public int id_grupo { get; set; }
        public int id_concepto { get; set; }
        public int vigencia { get; set; }
        public string intereses { get; set; }
    }
}
