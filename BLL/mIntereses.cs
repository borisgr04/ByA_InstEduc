using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades.Vistas;
using DAL;
using ByA;
using AutoMapper;
using Entidades.Consultas;

namespace BLL
{
    public class mIntereses
    {
        ieEntities ctx;
        public int GetValorIntereses(DateTime FechaInicial, DateTime FechaFinal, double Capital, int Vigencia, int Periodo, int id_cartera)
        {
            using (ctx = new ieEntities())
            {
                parametros parametro = ctx.parametros.Where(t => t.vigencia == Vigencia && t.nombre == "INTERESES").FirstOrDefault();
                if ((parametro != null) && (parametro.valor == "M"))
                {
                    mInteresesManuales intMa = new mInteresesManuales();
                    return intMa.GetValorIntereses(FechaFinal, Vigencia, Periodo, id_cartera);
                }
                else
                {
                    mInteresSuperintendencia intSuperInt = new mInteresSuperintendencia();
                    return intSuperInt.GetValorIntereses(FechaInicial, FechaFinal, Capital);
                }
            }
        }
        public DiasInteresesDto GetNumeroDiasPagoIntereses(float Capital, float ValorDisponible, int Vigencia)
        {
            using (ctx = new ieEntities())
            {
                DiasInteresesDto diasIntereses = new DiasInteresesDto();
                parametros parametro = ctx.parametros.Where(t => t.vigencia == Vigencia && t.nombre == "INTERESES").FirstOrDefault();
                if ((parametro != null) && (parametro.valor == "S"))
                {
                    mInteresSuperintendencia intSuperInt = new mInteresSuperintendencia();
                    diasIntereses.NumeroDias = intSuperInt.GetNumeroDiasPagoIntereses(Capital, ValorDisponible);
                    diasIntereses.TiposIntereses = "S";
                }
                else
                {
                    diasIntereses.NumeroDias = 0;
                    diasIntereses.TiposIntereses = "M";
                }
                return diasIntereses;
            }
        }
    }
}
