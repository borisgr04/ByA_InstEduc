using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Entidades.Vistas;
using AutoMapper;
using ByA;
using BLL;

namespace BLL
{
    public class mInteresesManuales
    {
        ieEntities ctx;
        public int GetValorIntereses(DateTime FechaFinal, int Vigencia, int Periodo, int id_cartera)
        {
            using (ctx = new ieEntities())
            {
                periodos objPeriodo = ctx.periodos.Where(t => t.vigencia == Vigencia && t.periodo == Periodo).FirstOrDefault();
                int FPP = int.Parse(objPeriodo.vigencia.ToString() + objPeriodo.periodo.ToString().PadLeft(2, '0') + objPeriodo.vence_dia.ToString().PadLeft(2, '0'));
                int FP = int.Parse(FechaFinal.Year.ToString() + FechaFinal.Month.ToString().PadLeft(2,'0') + FechaFinal.Day.ToString().PadLeft(2,'0'));
                int valorIntereses = 0;
                if (FP <= FPP) valorIntereses = 0;
                else
                {
                    int MPP = int.Parse(FPP.ToString().Substring(0, 6));
                    int MP = int.Parse(FP.ToString().Substring(0, 6));
                    if (MPP == MP)
                    {
                        intereses_manuales intereses = ctx.intereses_manuales.Where(t => t.inicio <= FechaFinal.Day && t.fin >= FechaFinal.Day && t.en_mes == "SI" && t.vigencia == Vigencia).FirstOrDefault();
                        if (intereses != null) valorIntereses = intereses.valor;
                        else valorIntereses = 0;
                    }
                    else
                    {
                        intereses_manuales intereses = ctx.intereses_manuales.Where(t => t.en_mes == "NO" && t.vigencia == Vigencia).FirstOrDefault();
                        if (intereses != null) valorIntereses = intereses.valor;
                        else valorIntereses = 0;
                    }
                }

                int pagado = 0;
                List<detalles_pago> lDetallesPago = ctx.detalles_pago.Where(t => t.tipo == "IN" && t.id_cartera == id_cartera && t.pagos.estado == "PA").ToList();
                foreach (detalles_pago item in lDetallesPago)
                {
                    pagado += (int) item.valor;
                }

                List<detalles_nota_credito> lDetallesNotas = ctx.detalles_nota_credito.Where(t => t.tipo == "IN" && t.id_cartera == id_cartera && t.notas_credito.estado == "PA").ToList();
                foreach (detalles_nota_credito item in lDetallesNotas)
                {
                    pagado += (int)item.valor;
                }

                int ValorPorPagar = valorIntereses - pagado;

                return ValorPorPagar;
            }
        }
    }
}
