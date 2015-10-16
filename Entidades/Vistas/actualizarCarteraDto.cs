using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades.Vistas
{
    public class actualizarCarteraDto
    {
        public string id_estudiante { get; set; }
        public List<carteraDto> lCartera { get; set; }
    }
}
