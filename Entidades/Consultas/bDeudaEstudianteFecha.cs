using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Consultas
{
    public class bDeudaEstudianteFecha
    {
        public Nullable<int> id_grupo { get; set; }
        public string id_estudiante { get; set; }
        public int ValorPagar { get; set; }
        public DateTime fecha { get; set; }
    }
}
