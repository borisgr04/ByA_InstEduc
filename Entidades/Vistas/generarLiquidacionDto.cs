using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades.Vistas
{
    public class generarLiquidacionDto
    {
        public string id_estudiante { get; set; }
        public int periodo_desde { get; set; }
        public int periodo_hasta { get; set; }
    }
}
