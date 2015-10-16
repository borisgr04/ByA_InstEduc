using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades.Vistas
{
    public class periodosDto
    {
        public int id { get; set; }
        public Nullable<int> periodo { get; set; }
        public string estado { get; set; }
        public Nullable<int> vigencia { get; set; }
        public Nullable<int> vence_dia { get; set; }
    }
}
