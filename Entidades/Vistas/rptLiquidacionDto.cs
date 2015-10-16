using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ByA;

namespace Entidades.Vistas
{
    public class rptLiquidacionDto
    {
        public ByARpt RespuestaGenerarLiquidacion { get; set; }
        public pagosDto Liquidacion { get; set; }
    }
}
