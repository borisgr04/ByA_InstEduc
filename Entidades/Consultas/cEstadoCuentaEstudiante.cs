using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Vistas;

namespace Entidades.Consultas
{
    public class cEstadoCuentaEstudiante
    {
        public List<detalles_pagoDto> lDeuda { get; set; }
        public List<detalles_pagoDto> lAdelantos { get; set; }
    }
}
