using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades.Vistas
{
    public class tarifasDto
    {
        public Nullable<int> id { get; set; }
        public int vigencia { get; set; }
        public int id_grado { get; set; }
        public int id_concepto { get; set; }
        public double valor { get; set; }
        public int periodo_desde { get; set; }
        public int periodo_hasta { get; set; }
    }
}
