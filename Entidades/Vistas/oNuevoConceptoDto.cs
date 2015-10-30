using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Vistas
{
    public class oNuevoConceptoDto
    {
        public int vigencia { get; set; }
        public string id_estudiante { get; set; }
        public int concepto_seleccionado { get; set; }
        public int perido_desde_seleccionado { get; set; }
        public int perido_hasta_seleccionado { get; set; }
        public int valor { get; set; }
        public string usu { get; set; }
    }
}
