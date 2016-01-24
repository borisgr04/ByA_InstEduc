using DAL;
using Entidades.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public class MapDetallesNotasCredito
    {
        public static System.Linq.Expressions.Expression<Func<detalles_nota_credito, detalles_nota_creditoDto>> Map()
        {
            return u => new detalles_nota_creditoDto
            {
                id = u.id,
                id_nota_credito = u.id_nota_credito,
                id_concepto = u.id_concepto,
                periodo = u.periodo,
                vigencia = u.vigencia,
                valor = u.valor,
                tipo = u.tipo,
                id_cartera = u.id_cartera,
                nombre_concepto = u.nombre_concepto
            };
        }
    }
}
