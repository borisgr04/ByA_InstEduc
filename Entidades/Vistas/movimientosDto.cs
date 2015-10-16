using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Vistas
{
    public class movimientosDto
    {
        public int id { get; set; }
        public string id_estudiante { get; set; }
        public Nullable<int> vigencia { get; set; }
        public Nullable<int> periodo { get; set; }
        public Nullable<int> id_cartera { get; set; }
        public Nullable<int> id_concepto { get; set; }
        public Nullable<double> valor_debito { get; set; } 
        public Nullable<double> valor_credito { get; set; }
        public Nullable<System.DateTime> fecha_movimiento { get; set; }
        public string estado { get; set; }
        public Nullable<System.DateTime> fecha_novedad { get; set; }
        public Nullable<System.DateTime> fecha_registro { get; set; }
        public string tipo_documento { get; set; }
        public Nullable<int> numero_documento { get; set; }
        public Nullable<int> id_est { get; set; }
        public string nombre_concepto { get; set; }
    }
}
