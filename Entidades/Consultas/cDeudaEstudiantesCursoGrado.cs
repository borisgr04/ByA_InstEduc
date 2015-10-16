using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Consultas
{
    public class cDeudaEstudiantesCursoGrado
    {
        public int id_grado { get; set; }
        public string nombre_grado { get; set; }
        public int id_curso { get; set; }
        public string nombre_curso { get; set; }
        public int id_est { get; set; } 
        public string id_estudiante { get; set; }
        public string nombre_estudiante { get; set; }
        public float valor_deuda { get; set; }
    }
}
