using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades.Vistas
{
    public class carteraDto
    {
        public int id { get; set; }
        public Nullable<int> vigencia { get; set; }
        public Nullable<int> id_concepto { get; set; }
        public Nullable<int> periodo_desde { get; set; }
        public Nullable<int> periodo_hasta { get; set; }
        public Nullable<float> valor { get; set; }
        public Nullable<int> id_matricula { get; set; }

        public string nombre_concepto { get; set; }
    }
}
