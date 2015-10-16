using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Consultas
{
    public class cCarteraEstudianteConcepto
    {
        public string id_estudiante { get; set; }
        public string nombre_estudiante { get; set; }

        public int per_pension { get; set; }
        public double valor_pension { get; set; }
        public int per_matricula { get; set; }
        public double valor_matricula { get; set; }
        public int per_sistematizacion { get; set; }
        public double valor_sistematizacion { get; set; }
        public int per_seguro { get; set; }
        public double valor_seguro { get; set; }
        public int per_propeda { get; set; }
        public double valor_propeda { get; set; }
        public int per_carteravencida { get; set; }
        public double valor_carteravencida { get; set; }
        public double total_deuda;
    }
}
