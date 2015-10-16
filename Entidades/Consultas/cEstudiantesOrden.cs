using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Vistas;

namespace Entidades.Consultas
{
    public class cEstudiantesOrden
    {
        public bool Ultimo { get; set; }
        public List<estudiantesDto> lEstudiantes { get; set; }
    }
}
