using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades.Vistas
{
    public class carterapDto
    {
        public int id { get; set; }
        public int vigencia { get; set; }
        public int id_concepto { get; set; }
        public int periodo { get; set; }
        public double valor { get; set; }
        public int id_matricula { get; set; }
        public string id_estudiante { get; set; }
        public double pagado { get; set; }
        public int id_grupo { get; set; }
        public int id_est { get; set; }
        public string estado { get; set; }
        public Nullable<int> pago_genero_intereses { get; set; }
        public string nombre_concepto { get; set; }
        public string causado { get; set; }
        public string generar_nota { get; set; }
    }
}
