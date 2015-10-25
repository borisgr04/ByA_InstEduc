using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades.Vistas
{
    public class matriculasDto
    {
        public int id { get; set; }
        public int vigencia { get; set; }
        public int id_curso { get; set; }
        public System.DateTime fecha { get; set; }
        public string id_estudiante { get; set; }
        public int id_grado { get; set; }
        public int id_est { get; set; }
        public List<carteraDto> lCartera { get; set; }
        public string estado { get; set; }
        public string folio { get; set; }
        public string id_matricula { get; set; }
        public string usu { get; set; }

        public string codigo_estudiante { get; set; }
        public string nombre_estudiante { get; set; }
        public string nombre_curso { get; set; }
        public string nombre_grado { get; set; }
    }
}
