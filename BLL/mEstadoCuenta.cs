using Entidades.Consultas;
using Entidades.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BLL.DatosBasicos;

namespace BLL
{
    public class mEstadoCuenta
    {
        ieEntities ctx;
        public List<cEstadoCuenta> GetEstadoCuentaResumido(string id_estudiante)
        {
            List<cEstadoCuenta> lEstadoCuenta = new List<cEstadoCuenta>();
            DateTime FechaCausacion = mCausacion.FechaCausacion();            
            int VigPerAct = int.Parse(FechaCausacion.Year.ToString() + FechaCausacion.Month.ToString().PadLeft(2, '0'));
            mVigencias objVigencias = new mVigencias();
            List<vigenciasDto> lVigencias = objVigencias.GetsActivas();

            using (ctx = new ieEntities())
            {
                foreach (vigenciasDto vigencia in lVigencias)
                {
                    cEstadoCuenta objEstCuenta = new cEstadoCuenta();
                    objEstCuenta.vigencia = vigencia.vigencia;
                    objEstCuenta.saldo_vigencia = 0;
                    objEstCuenta.intereses_vigencia = 0;
                    objEstCuenta.pagado_vigencia = 0;
                    objEstCuenta.saldo_vigencia = 0;
                    objEstCuenta.ban_agregar = false;
                    objEstCuenta.l_items = new List<itemPorVigencia>();

                    List<carterap> lCarteras = ctx.carterap.Where(t => t.id_estudiante == id_estudiante && (t.estado == "PR" || t.estado == "CA") && (t.vigencia * 100 + t.periodo) <= VigPerAct && t.vigencia == vigencia.vigencia).ToList();
                    foreach (carterap cartera in lCarteras)
                    {
                        itemPorVigencia item = new itemPorVigencia();
                        item.id_concepto = cartera.id_concepto;
                        item.nombre_concepto = cartera.conceptos.nombre;
                        item.periodo = cartera.periodo;
                        item.causado = (int) cartera.valor;

                        int ValorIntereses = 0;
                        int ValorPagadoIntereses = 0;
                        ValorIntereses = PreCalcularInteresesCartera(FechaCausacion, cartera, ValorIntereses);
                        List<detalles_pago> lDet = ctx.detalles_pago.Where(t => t.id_cartera == cartera.id && t.tipo == "IN" && t.pagos.estado == "PA").ToList();
                        lDet.ForEach(t => ValorPagadoIntereses += (int)t.valor);

                        item.intereses = ValorIntereses;
                        item.pagado = (int) cartera.pagado + ValorPagadoIntereses;
                        item.saldo = item.causado + item.intereses - item.pagado;

                        detalles_pago detalle = ctx.detalles_pago.Where(t => t.pagos.estado == "PA" && t.id_cartera == cartera.id).OrderByDescending(t => t.pagos.fecha_pago).FirstOrDefault();
                        if(detalle != null){
                            if(detalle.pagos.fecha_pago != null) item.fecha_pago = detalle.pagos.fecha_pago;
                            else item.fecha_pago = null;
                        } else item.fecha_pago = null;

                        objEstCuenta.causado_vigencia += item.causado;
                        objEstCuenta.intereses_vigencia += item.intereses;
                        objEstCuenta.pagado_vigencia += item.pagado;
                        objEstCuenta.saldo_vigencia += item.saldo;

                        objEstCuenta.l_items.Add(item);
                    }
                    List<carterap> lCarterasNoCausadas = ctx.carterap.Where(t => t.id_estudiante == id_estudiante && (t.estado == "PR" || t.estado == "CA") && (t.vigencia * 100 + t.periodo) > VigPerAct && t.vigencia == vigencia.vigencia).ToList();
                    if (lCarteras.Count() > 0) objEstCuenta.ban_agregar = true;
                    foreach (carterap itemC in lCarterasNoCausadas)
                    {
                        itemPorVigencia item = new itemPorVigencia();
                        item.id_concepto = itemC.id_concepto;
                        item.nombre_concepto = itemC.conceptos.nombre;
                        item.periodo = itemC.periodo;
                        item.causado = 0;
                        item.intereses = 0;
                        item.pagado = 0;
                        List<detalles_pago> lDet = ctx.detalles_pago.Where(t => t.id_cartera == itemC.id && t.pagos.estado == "PA").ToList();
                        lDet.ForEach(t => item.pagado += (int) t.valor);
                        item.saldo = item.causado + item.intereses - item.pagado;

                        detalles_pago detalle = ctx.detalles_pago.Where(t => t.pagos.estado == "PA" && t.id_cartera == itemC.id).OrderByDescending(t => t.pagos.fecha_pago).FirstOrDefault();
                        if(detalle != null){
                            if(detalle.pagos.fecha_pago != null) item.fecha_pago = detalle.pagos.fecha_pago;
                            else item.fecha_pago = null;
                        } else item.fecha_pago = null;

                        objEstCuenta.causado_vigencia += item.causado;
                        objEstCuenta.intereses_vigencia += item.intereses;
                        objEstCuenta.pagado_vigencia += item.pagado;
                        objEstCuenta.saldo_vigencia += item.saldo;

                        objEstCuenta.l_items.Add(item);
                    }
                    objEstCuenta.l_items = objEstCuenta.l_items.OrderBy(t => t.periodo).ToList();
                    if(objEstCuenta.l_items.Count() > 0) lEstadoCuenta.Add(objEstCuenta);
                }
            }

            int MaxVig = lEstadoCuenta.Max(t => t.vigencia);

            return lEstadoCuenta.Where(t=> t.saldo_vigencia != 0 || t.vigencia == MaxVig || t.ban_agregar).OrderBy(t => t.vigencia).ToList();
        }
        private int PreCalcularInteresesCartera(DateTime FechaCausacion, carterap cartera, int ValorIntereses)
        {
            if (cartera.pagado == cartera.valor)
            {
                List<detalles_pago> lDet = ctx.detalles_pago.Where(t => t.id_cartera == cartera.id && t.tipo == "IN" && t.pagos.estado == "PA").ToList();
                lDet.ForEach(t => ValorIntereses += (int)t.valor);
            }
            else
            {
                int ValorAdicional = 0;
                ValorIntereses = CalcularValorInteresesCartera(FechaCausacion, cartera, ValorIntereses);
                List<detalles_pago> lDet = ctx.detalles_pago.Where(t => t.id_cartera == cartera.id && t.tipo == "IN" && t.pagos.estado == "PA").ToList();
                lDet.ForEach(t => ValorAdicional += (int)t.valor);
                ValorIntereses += ValorAdicional;
            }
            return ValorIntereses;
        }
        private int CalcularValorInteresesCartera(DateTime FechaCausacion, carterap cartera, int ValorIntereses)
        {
            config_grupos_pagos config = ctx.config_grupos_pagos.Where(t => t.id_concepto == cartera.id_concepto && t.vigencia == cartera.vigencia).FirstOrDefault();
            if ((config != null) && (config.intereses == "SI"))
            {
                periodos periodo = ctx.periodos.Where(t => t.periodo == cartera.periodo && t.vigencia == cartera.vigencia).FirstOrDefault();
                DateTime FechaVencimientoPeriodo = new DateTime((int)periodo.vigencia, (int)periodo.periodo, (int)periodo.vence_dia);
                if (FechaCausacion > FechaVencimientoPeriodo)
                {
                    mIntereses oTI = new mIntereses();
                    DiasInteresesDto DiasTipo = oTI.GetNumeroDiasPagoIntereses(0, 0, cartera.vigencia);
                    DateTime FechaUltimoCalculoIntereses = cartera.fechas_calculo_intereses.Where(t => t.estado == "PA").OrderByDescending(t => t.fecha).FirstOrDefault().fecha;
                    ValorIntereses = oTI.GetValorIntereses(FechaUltimoCalculoIntereses, FechaCausacion, cartera.valor, cartera.vigencia, cartera.periodo, cartera.id);
                }
            }
            return ValorIntereses;
        }
    }
}
