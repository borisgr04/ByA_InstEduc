using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Vistas
{
    public class mensajesAcudientesDto
    {
        public int id { get; set; }
        public int id_mensaje { get; set; }
        public int id_acudiente { get; set; }
        public string estado { get; set; }
    }
}
