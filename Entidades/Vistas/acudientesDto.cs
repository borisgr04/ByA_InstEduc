using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Vistas
{
    public class acudientesDto
    {
        public string tipo_identificacion { get; set; }
        public string identificacion { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string celular { get; set; }
        public string email { get; set; }
        public string ocupacion { get; set; }
        public string direccion_trabajo { get; set; }
        public int id { get; set; }

        public string nombre_completo { get; set; }
    }
}
