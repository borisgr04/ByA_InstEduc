using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Consultas
{
    public class CarteraxConceptoDto
    {
        public string id_estudiante { get; set; }
        public string NombreEstudiante { get; set; }
        public int id_concepto { get; set; }
        public string NombreConcepto { get; set; }
        public double Valor { get; set; }
        public double Pagado { get; set; }
        public double Saldo { get; set; }
        public int Vigencia { get; set; }
        public int Cantidad { get; set; }
    }
}
