using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades.Vistas
{
    public class pagosDto
    {
        public int id { get; set; }
        public System.DateTime fecha { get; set; }
        public Nullable<int> id_forma_pago { get; set; }
        public string estado { get; set; }
        public Nullable<System.DateTime> fecha_pago { get; set; }
        public string id_estudiante { get; set; }
        public Nullable<int> id_grupo { get; set; }
        public int id_est { get; set; }
        public string tipo { get; set; }
        public string observacion { get; set; }
        public Nullable<System.DateTime> fecha_max_pago { get; set; }

        public Nullable<int> ValorTotal { get; set; }
        public List<detalles_pagoDto> detalles_pago { get; set; }
        public string nombre_estudiante { get; set; }
        public string nombre_grupo { get; set; }
        public bool VerificadoIntereses { get; set; }
        public bool causar_intereses { get; set; }
    }
}
