using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Vistas
{
    public class detalles_nota_creditoDto
    {
        public int id { get; set; }
        public int id_nota_credito { get; set; }
        public Nullable<int> id_concepto { get; set; }
        public int periodo { get; set; }
        public int vigencia { get; set; }
        public double valor { get; set; }
        public string tipo { get; set; }
        public int id_cartera { get; set; }
        public string nombre_concepto { get; set; }
        public DateTime fecha_calculo_intereses { get; set; }
    }
}
