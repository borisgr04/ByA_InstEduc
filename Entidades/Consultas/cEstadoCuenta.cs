using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Consultas
{
    public class itemPorVigencia
    {
        public int id_concepto { get; set; }
        public string nombre_concepto { get; set; }
        public int periodo { get; set; }
        public int causado { get; set; }
        public int intereses { get; set; }
        public int pagado { get; set; }
        public int saldo { get; set; }
    }
    public class cEstadoCuenta
    {
        public int vigencia { get; set; }
        public List<itemPorVigencia> l_items { get; set; }
        public int causado_vigencia { get; set; }
        public int intereses_vigencia { get; set; }
        public int pagado_vigencia { get; set; }
        public int saldo_vigencia { get; set; }

        public bool ban_agregar { get; set; }
    }
}
