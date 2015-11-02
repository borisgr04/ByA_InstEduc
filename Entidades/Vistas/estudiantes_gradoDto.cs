using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Vistas
{
    public class estudiante_contactoDto
    {
        public string id_estudiante { get; set; }
        public string codigo_estudiante { get; set; }
        public string nombre_estudiante { get; set; }
        public string vive_con_estudiante { get; set; }
        public string nombre_acudiente { get; set; }
        public string direccion_acudiente { get; set; }
        public string telefono_acudiente { get; set; }
        public string celular_acudiente { get; set; }
        public string correo_acudiente { get; set; }
    }
    public class grado_contactoDto
    {
        public string nombre_grado { get; set; }
        public string nombre_curso { get; set; }
        public int vigencia { get; set; }
        public List<estudiante_contactoDto> l_estudiantes { get; set; }
    }
}
