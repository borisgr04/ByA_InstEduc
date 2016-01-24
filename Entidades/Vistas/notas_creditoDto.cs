using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Vistas
{
    public class notas_creditoDto
    {
        public int id { get; set; }
        public System.DateTime fecha { get; set; }
        public string estado { get; set; }
        public string id_estudiante { get; set; }
        public int id_grupo { get; set; }
        public int id_est { get; set; }
        public string observacion { get; set; }
        public Nullable<System.DateTime> fec_reg { get; set; }
        public Nullable<System.DateTime> fec_mov { get; set; }
        public string usu_reg { get; set; }
        public string usu_mod { get; set; }
        public string usu { get; set; }

        public string nombre_estudiante { get; set; }
        public string nombre_grupo { get; set; }


        public List<detalles_nota_creditoDto> detalles_nota_credito { get; set; }
    }
}
