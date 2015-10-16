using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Vistas
{
    public class ConsultaMatriculasDto
    {
        public string Filtro { get; set; }
        public int? Curso { get; set; }
        public int Vigencia { get; set; }
    }
}
