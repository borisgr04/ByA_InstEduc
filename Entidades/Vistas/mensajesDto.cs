using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Vistas
{
    public class mensajesDto
    {
        public int id { get; set; }
        public string asunto { get; set; }
        public string mensaje { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public string tipo { get; set; }
        public int id_remitente { get; set; }
    }
}
