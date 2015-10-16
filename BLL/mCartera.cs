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
    public class mCartera
    {
        ieEntities ctx;
        public List<carteraDto> GetVisualizacionCarteraAntes(int grado, int vigencia, int vigenciaActual, int periodoActual)
        {
            using (ctx = new ieEntities())
            {
                mConceptos oConceptos = new mConceptos();
                mTarifas oTarifas = new mTarifas();
                int MaxPeriodo = ctx.vigencias.Where(t => t.vigencia == vigencia).FirstOrDefault().periodos.Max(t => t.periodo);
                List<tarifasDto> lTarifas = oTarifas.GetTarifas(vigencia,grado);

                List<carteraDto> lrCartera = new List<carteraDto>();

                foreach (tarifasDto item in lTarifas)
                {
                    carteraDto itemCartera = new carteraDto();
                    itemCartera.vigencia = vigencia;
                    itemCartera.id_concepto = (int)item.id_concepto;
                    itemCartera.nombre_concepto = oConceptos.Get((int)item.id_concepto).nombre;

                    if ((item.vigencia == vigenciaActual) && (item.periodo_desde < periodoActual))
                    {
                        itemCartera.periodo_desde = periodoActual;
                        int PeriodosPaga = (int)(item.periodo_hasta - item.periodo_desde + 1);
                        int PeriodosRestantes = (int)(MaxPeriodo - periodoActual + 1);
                        if (PeriodosPaga >= PeriodosRestantes) itemCartera.periodo_hasta = MaxPeriodo;
                        else itemCartera.periodo_hasta = periodoActual + PeriodosPaga - 1;
                    }
                    else
                    {                        
                        itemCartera.periodo_desde = (int)item.periodo_desde;
                        itemCartera.periodo_hasta = (int)item.periodo_hasta;
                    }

                    itemCartera.valor = (int)item.valor;
                    lrCartera.Add(itemCartera);
                }
                return lrCartera;
            }
        }
        public List<tarifas> TraerTarifas(int grado, int vigencia)
        {
            using (ctx = new ieEntities())
            {
                return ctx.tarifas.Where(t => t.vigencia == vigencia && t.id_grado == grado).ToList();
            }            
        }
        public cEstadoCuentaEstudiante GetEstadoCuentaEstudiante(string id_estudiante)
        {            
            cEstadoCuentaEstudiante objRes = new cEstadoCuentaEstudiante();
            objRes.lDeuda = GetDeudaTotal(id_estudiante);
            objRes.lAdelantos = GetPagosAdelantados(id_estudiante);
            return objRes;
        }
        private List<detalles_pagoDto> GetPagosAdelantados(string id_estudiante)
        {
            mCausacion.Causar(id_estudiante);
            using (ctx = new ieEntities())
            {
                List<detalles_pagoDto> lr = new List<detalles_pagoDto>();
                List<carterap> lcartera = ctx.carterap.Where(t => t.estado == "PR" && t.pagado > 0 && t.id_estudiante == id_estudiante).ToList();
                foreach (carterap item in lcartera)
                {
                    detalles_pagoDto pagoCapital = new detalles_pagoDto();
                    pagoCapital.id_concepto = item.id_concepto;
                    pagoCapital.valor = (int)(item.pagado);
                    pagoCapital.nombre_concepto = item.conceptos.nombre;
                    pagoCapital.periodo = item.periodo;
                    pagoCapital.id_cartera = item.id;
                    pagoCapital.vigencia = item.vigencia;
                    pagoCapital.tipo = "CA";
                    lr.Add(pagoCapital);
                }
                return lr;
            }
        }
        private List<detalles_pagoDto> GetDeudaTotal(string id_estudiante)
        {
            mCausacion.Causar(id_estudiante);
            using (ctx = new ieEntities())
            {
                DateTime FechaActual = DateTime.Now;
                List<detalles_pagoDto> lDeuda = new List<detalles_pagoDto>();
                List<carterap> lCartera = ctx.carterap.Where(t => t.id_estudiante == id_estudiante && t.estado == "CA" && t.pagado < t.valor).OrderBy(t => t.vigencia).ThenBy(t => t.periodo).ThenBy(t => t.id_concepto).ToList();
                foreach (carterap item in lCartera)
                {
                    detalles_pagoDto pagoCapital = new detalles_pagoDto();
                    detalles_pagoDto pagoIntereses = new detalles_pagoDto();

                    pagoCapital.id_concepto = item.id_concepto;
                    pagoCapital.valor = (int)(item.valor - item.pagado);
                    pagoCapital.nombre_concepto = item.conceptos.nombre;
                    pagoCapital.periodo = item.periodo;
                    pagoCapital.id_cartera = item.id;
                    pagoCapital.vigencia = item.vigencia;
                    pagoCapital.tipo = "CA";
                    lDeuda.Add(pagoCapital);

                    config_grupos_pagos config = ctx.config_grupos_pagos.Where(t => t.id_concepto == item.id_concepto && t.vigencia == item.vigencia).FirstOrDefault();
                    if ((config != null) && (config.intereses == "SI"))
                    {
                        periodos periodo = ctx.periodos.Where(t => t.periodo == item.periodo && t.vigencia == item.vigencia).FirstOrDefault();
                        DateTime FechaVencimientoPeriodo = new DateTime((int)periodo.vigencia, (int)periodo.periodo, (int)periodo.vence_dia);
                        if (FechaActual > FechaVencimientoPeriodo)
                        {
                            mIntereses oTI = new mIntereses();
                            DateTime FechaUltimoCalculoIntereses = item.fechas_calculo_intereses.Where(t => t.estado == "PA").OrderByDescending(t => t.fecha).FirstOrDefault().fecha;
                            int ValorIntereses = oTI.GetValorIntereses(FechaUltimoCalculoIntereses, FechaActual, pagoCapital.valor, pagoCapital.vigencia, pagoCapital.periodo, pagoCapital.id_cartera);
                            if (ValorIntereses > 0)
                            {
                                pagoIntereses.id_cartera = item.id;
                                pagoIntereses.fecha_calculo_intereses = FechaActual;
                                pagoIntereses.id_concepto = 6;
                                pagoIntereses.valor = ValorIntereses;
                                pagoIntereses.nombre_concepto = "Intereses: " + pagoCapital.nombre_concepto + ", desde " + FechaUltimoCalculoIntereses.ToShortDateString() + " hasta " + FechaActual.ToShortDateString();
                                pagoIntereses.periodo = pagoCapital.periodo;
                                pagoIntereses.vigencia = pagoCapital.vigencia;
                                pagoIntereses.tipo = "IN";
                                lDeuda.Add(pagoIntereses);
                            }
                        }
                    }
                }
                return lDeuda;
            }
        }
        public List<carterapDto> GetCarteraEstudiante(string id_estudiante, int vigencia)
        {
            using (ctx = new ieEntities())
            {
                MaperCarterap();
                List<carterapDto> lrCartera = new List<carterapDto>();
                List<carterap> lCartera = ctx.carterap.Where(t => t.id_estudiante == id_estudiante && t.estado != "AN" && t.vigencia == vigencia).ToList();
                Mapper.Map(lCartera, lrCartera);
                return lrCartera;
            }
        }
        private static void MaperCarterap()
        {
            Mapper.CreateMap<carterap, carterapDto>()
                .ForMember(dest => dest.nombre_concepto, obj => obj.MapFrom(src => src.conceptos.nombre))
                .ForMember(dest => dest.causado, obj => obj.MapFrom(src => src.estado == "CA" ? "SI" : "NO"))
                .ForMember(dest => dest.generar_nota, obj => obj.MapFrom(src => "NO"));
        }
        public ByARpt UpdateCarteraEstudiante(List<carterapDto> lReg)
        {
            cmdUpdateL o = new cmdUpdateL();
            o.olDto = lReg;
            return o.Enviar();
        }
        class cmdUpdateL : absTemplate
        {
            public List<carterapDto> olDto { get; set; }
            int ultIdDocumentos { get; set; }
            int ultIdMovimientos { get; set; }
            #region ImplementaciónMetodosAbstractos
            protected internal override bool esValido()
            {
                return true;
            }
            protected internal override void Antes()
            {
                UltIdDocumentos();
                UltIdMovimientos();
                foreach (carterapDto cartera in olDto)
                {
                    if (cartera.causado == "NO") ModificarNoCausada(cartera);
                    else ModificarCausada(cartera);                    
                }
            }

            private void UltIdDocumentos()
            {
                ultIdDocumentos = 0;
                try { ultIdDocumentos = ctx.documentos.Max(t => t.id); }
                catch { }
            }
            private void UltIdMovimientos()
            {
                ultIdMovimientos = 0;
                try { ultIdMovimientos = ctx.movimientos.Max(t => t.id); }
                catch { }
            }
            private void ModificarNoCausada(carterapDto cartera)
            {
                carterap ctxCartera = ctx.carterap.Where(t => t.id == cartera.id).FirstOrDefault();
                if (ctxCartera != null)
                {
                    ctxCartera.valor = cartera.valor;
                }
            }
            private void ModificarCausada(carterapDto cartera)
            {
                carterap ctxCartera = ctx.carterap.Where(t => t.id == cartera.id).FirstOrDefault();
                if (ctxCartera != null)
                {
                    if (ctxCartera.valor < cartera.valor)
                    {
                        //Movimiento Debito
                        int NumDoc = InsertDocumento("NOTDB", "Aumento un valor cartera, Id. Estudiante: " + cartera.id_estudiante + ", Valor Antes: " + ctxCartera.valor + ", Valor Ahora: " + cartera.valor);

                        ultIdMovimientos++;
                        movimientos m = new movimientos();
                        m.id = ultIdMovimientos;
                        m.estado = "AC";
                        m.fecha_movimiento = DateTime.Now;
                        m.fecha_novedad = DateTime.Now;
                        m.fecha_registro = DateTime.Now;
                        m.id_cartera = cartera.id;
                        m.id_concepto = cartera.id_concepto;
                        m.id_estudiante = cartera.id_estudiante;
                        m.numero_documento = NumDoc;
                        m.id_est = cartera.id_est;
                        m.tipo_documento = "NOTDB";
                        m.valor_debito = ctxCartera.valor - cartera.valor;
                        m.valor_credito = 0;
                        m.vigencia = cartera.vigencia;
                        m.periodo = cartera.periodo;
                        ctx.movimientos.Add(m);                        
                    }
                    if (ctxCartera.valor > cartera.valor)
                    {
                        //Movimiento Credito
                        int NumDoc = InsertDocumento("NOTCR", "Disminuyó un valor cartera, Id. Estudiante: " + cartera.id_estudiante + ", Valor Antes: " + ctxCartera.valor + ", Valor Ahora: " + cartera.valor);

                        ultIdMovimientos++;
                        movimientos m = new movimientos();
                        m.id = ultIdMovimientos;
                        m.estado = "AC";
                        m.fecha_movimiento = DateTime.Now;
                        m.fecha_novedad = DateTime.Now;
                        m.fecha_registro = DateTime.Now;
                        m.id_cartera = cartera.id;
                        m.id_concepto = cartera.id_concepto;
                        m.id_estudiante = cartera.id_estudiante;
                        m.numero_documento = NumDoc;
                        m.id_est = cartera.id_est;
                        m.tipo_documento = "NOTCR";
                        m.valor_debito = 0;
                        m.valor_credito = cartera.valor - ctxCartera.valor;
                        m.vigencia = cartera.vigencia;
                        m.periodo = cartera.periodo;
                        ctx.movimientos.Add(m);
                    }
                    ctxCartera.valor = cartera.valor;
                }
            }
            private int InsertDocumento(string Tipo, string Descripcion)
            {
                ultIdDocumentos++;
                documentos doc = new documentos();
                doc.id = ultIdDocumentos;
                doc.tipo_documento = Tipo;
                doc.fecha = DateTime.Now;
                doc.descripcion = Descripcion;
                ctx.documentos.Add(doc);
                return ultIdDocumentos;
            }
            #endregion
        }
        // los dos siguientes metodos son los de liquidar de manera provisional
        public List<detalles_pagoDto> GetDeudaEstudianteL(bDeudaEstudianteFecha reg)
        {
            mCausacion.Causar(reg.id_estudiante);
            using (ctx = new ieEntities())
            {
                DateTime FechaActual = reg.fecha;
                List<detalles_pagoDto> lDeuda = new List<detalles_pagoDto>();
                int VigPerAct = int.Parse(FechaActual.Year.ToString() + FechaActual.Month.ToString().PadLeft(2, '0'));
                List<carterap> lCartera = new List<carterap>();
                if (reg.id_grupo == null) lCartera = ctx.carterap.Where(t => t.id_estudiante == reg.id_estudiante && (t.estado == "PR" || t.estado == "CA") && (t.vigencia * 100 + t.periodo) <= VigPerAct && t.pagado < t.valor).OrderBy(t => t.vigencia).ThenBy(t => t.periodo).ThenBy(t => t.grupos_pagos.prioridad).ThenBy(t => t.id_concepto).ToList();
                else lCartera = ctx.carterap.Where(t => t.id_estudiante == reg.id_estudiante && t.id_grupo == reg.id_grupo && (t.estado == "PR" || t.estado == "CA") && (t.vigencia * 100 + t.periodo) <= VigPerAct && t.pagado < t.valor).OrderBy(t => t.vigencia).ThenBy(t => t.periodo).ThenBy(t => t.grupos_pagos.prioridad).ThenBy(t => t.id_concepto).ToList();
                
                bool ban = false;
                foreach (carterap item in lCartera)
                {
                    if ((item.id_grupo == lCartera[0].id_grupo) && (ban == false))
                    {
                        detalles_pagoDto pagoCapital = new detalles_pagoDto();
                        detalles_pagoDto pagoIntereses = new detalles_pagoDto();

                        pagoCapital.id_concepto = item.id_concepto;
                        pagoCapital.valor = (int)(item.valor - item.pagado);
                        pagoCapital.nombre_concepto = item.conceptos.nombre;
                        pagoCapital.periodo = item.periodo;
                        pagoCapital.id_cartera = item.id;
                        pagoCapital.vigencia = item.vigencia;
                        pagoCapital.id_grupo = item.id_grupo;
                        pagoCapital.tipo = "CA";
                        lDeuda.Add(pagoCapital);

                        config_grupos_pagos config = ctx.config_grupos_pagos.Where(t => t.id_concepto == item.id_concepto && t.vigencia == item.vigencia).FirstOrDefault();
                        if ((config != null) && (config.intereses == "SI"))
                        {
                            periodos periodo = ctx.periodos.Where(t => t.periodo == item.periodo && t.vigencia == item.vigencia).FirstOrDefault();
                            DateTime FechaVencimientoPeriodo = new DateTime((int)periodo.vigencia, (int)periodo.periodo, (int)periodo.vence_dia);
                            if (FechaActual > FechaVencimientoPeriodo)
                            {
                                mIntereses oTI = new mIntereses();
                                DiasInteresesDto DiasTipo = oTI.GetNumeroDiasPagoIntereses(0, 0, pagoCapital.vigencia); 
                                DateTime FechaUltimoCalculoIntereses = item.fechas_calculo_intereses.Where(t => t.estado == "PA").OrderByDescending(t => t.fecha).FirstOrDefault().fecha;
                                int ValorIntereses = oTI.GetValorIntereses(FechaUltimoCalculoIntereses, FechaActual, pagoCapital.valor, pagoCapital.vigencia, pagoCapital.periodo, pagoCapital.id_cartera);
                                if (ValorIntereses > 0)
                                {
                                    pagoIntereses.id_cartera = item.id;
                                    pagoIntereses.fecha_calculo_intereses = FechaActual;
                                    pagoIntereses.id_concepto = 6;
                                    pagoIntereses.valor = ValorIntereses;
                                    if (DiasTipo.TiposIntereses == "S") pagoIntereses.nombre_concepto = "Intereses: " + pagoCapital.nombre_concepto + ", desde " + FechaUltimoCalculoIntereses.ToShortDateString() + " hasta " + FechaActual.ToShortDateString();
                                    else pagoIntereses.nombre_concepto = "Intereses: " + pagoCapital.nombre_concepto;
                                    pagoIntereses.periodo = pagoCapital.periodo;
                                    pagoIntereses.vigencia = pagoCapital.vigencia;
                                    pagoIntereses.id_grupo = item.id_grupo;
                                    pagoIntereses.tipo = "IN";
                                    lDeuda.Add(pagoIntereses);
                                }
                            }
                        }
                    }
                    else
                    {
                        ban = true;
                    }
                }
                return lDeuda;
            }
        }
        public List<detalles_pagoDto> GetDeudaEstudianteValorL(bDeudaEstudianteFecha reg)
        {
            mCausacion.Causar(reg.id_estudiante);
            using (ctx = new ieEntities())
            {
                int ValorLiquidado = 0;
                int i = 0;
                DateTime FechaActual = reg.fecha;
                List<detalles_pagoDto> lDeuda = new List<detalles_pagoDto>();
                int VigPerAct = int.Parse(FechaActual.Year.ToString() + FechaActual.Month.ToString().PadLeft(2, '0'));
                List<carterap> lCartera = new List<carterap>();
                if(reg.id_grupo == null) lCartera = ctx.carterap.Where(t => t.id_estudiante == reg.id_estudiante && (t.estado == "PR" || t.estado == "CA") && t.pagado < t.valor).OrderBy(t => t.vigencia).ThenBy(t => t.periodo).ThenBy(t => t.grupos_pagos.prioridad).ThenBy(t => t.id_concepto).ToList();
                else lCartera = ctx.carterap.Where(t => t.id_estudiante == reg.id_estudiante && t.id_grupo == reg.id_grupo && (t.estado == "PR" || t.estado == "CA") && t.pagado < t.valor).OrderBy(t => t.vigencia).ThenBy(t => t.periodo).ThenBy(t => t.grupos_pagos.prioridad).ThenBy(t => t.id_concepto).ToList();
                //List<carterap> lCartera = ctx.carterap.Where(t => t.id_estudiante == reg.id_estudiante && (t.estado == "CA" || t.estado == "PR") && t.pagado < t.valor).OrderBy(t => t.vigencia).ThenBy(t => t.periodo).ThenBy(t => t.grupos_pagos.prioridad).ThenBy(t => t.id_concepto).ToList();
                bool ban = false;
                while ((i <= lCartera.Count() - 1) && (ValorLiquidado < reg.ValorPagar) && (!ban))
                {
                    if ((lCartera[i].id_grupo == lCartera[0].id_grupo) && (ban == false))
                    {
                        detalles_pagoDto pagoCapital = new detalles_pagoDto();
                        detalles_pagoDto pagoIntereses = new detalles_pagoDto();

                        int ValorPeriodo = (int)(lCartera[i].valor - lCartera[i].pagado);
                        int ValorIntereses = 0;

                        int vigenciaCartera = lCartera[i].vigencia;
                        int periodoCartera = lCartera[i].periodo;
                        int id_concepto = lCartera[i].id_concepto;

                        config_grupos_pagos config = ctx.config_grupos_pagos.Where(t => t.id_concepto == id_concepto && t.vigencia == vigenciaCartera).FirstOrDefault();
                        if ((config != null) && (config.intereses == "SI"))
                        {    
                            periodos periodo = ctx.periodos.Where(t => t.periodo == periodoCartera && t.vigencia == vigenciaCartera).FirstOrDefault();
                            DateTime FechaVencimientoPeriodo = new DateTime((int)periodo.vigencia, (int)periodo.periodo, (int)periodo.vence_dia);
                            DateTime FechaUltimoCalculoIntereses = lCartera[i].fechas_calculo_intereses.Where(t => t.estado == "PA").OrderByDescending(t => t.fecha).FirstOrDefault().fecha;
                            mIntereses oTI = new mIntereses();
                            DiasInteresesDto DiasTipo = oTI.GetNumeroDiasPagoIntereses(ValorPeriodo, reg.ValorPagar - ValorLiquidado, lCartera[i].vigencia);
                            if (FechaActual > FechaVencimientoPeriodo)
                            {
                                ValorIntereses = oTI.GetValorIntereses(FechaUltimoCalculoIntereses, FechaActual, ValorPeriodo, lCartera[i].vigencia, lCartera[i].periodo, lCartera[i].id);
                            }

                            if ((ValorIntereses + ValorPeriodo) <= (reg.ValorPagar - ValorLiquidado))
                            {
                                pagoCapital.id_cartera = lCartera[i].id;
                                pagoCapital.id_concepto = lCartera[i].id_concepto;
                                pagoCapital.valor = (int)(ValorPeriodo);
                                pagoCapital.nombre_concepto = lCartera[i].conceptos.nombre;
                                pagoCapital.periodo = lCartera[i].periodo;
                                pagoCapital.vigencia = lCartera[i].vigencia;
                                pagoCapital.id_grupo = lCartera[i].id_grupo;
                                pagoCapital.tipo = "CA";
                                lDeuda.Add(pagoCapital);
                                ValorLiquidado += (int)pagoCapital.valor;

                                if (ValorIntereses > 0)
                                {
                                    pagoIntereses.id_concepto = 6;

                                    pagoIntereses.valor = ValorIntereses;

                                    if (DiasTipo.TiposIntereses == "S") pagoIntereses.nombre_concepto = "Intereses: " + lCartera[i].conceptos.nombre + ", desde " + FechaUltimoCalculoIntereses.ToShortDateString() + " hasta " + FechaUltimoCalculoIntereses.AddDays(DiasTipo.NumeroDias).ToShortDateString();
                                    else pagoIntereses.nombre_concepto = "Intereses: " + lCartera[i].conceptos.nombre;

                                    pagoIntereses.id_cartera = lCartera[i].id;
                                    pagoIntereses.fecha_calculo_intereses = FechaActual;
                                    pagoIntereses.periodo = pagoCapital.periodo;
                                    pagoIntereses.vigencia = pagoCapital.vigencia;
                                    pagoIntereses.fecha_calculo_intereses = FechaActual.AddDays(1);
                                    pagoIntereses.id_grupo = lCartera[i].id_grupo;
                                    pagoIntereses.tipo = "IN";
                                    lDeuda.Add(pagoIntereses);
                                    ValorLiquidado += (int)pagoIntereses.valor;
                                }
                            }
                            else
                            {
                                if (ValorIntereses == (reg.ValorPagar - ValorLiquidado))
                                {
                                    pagoIntereses.id_concepto = 6;
                                    pagoIntereses.valor = ValorIntereses;

                                    if (DiasTipo.TiposIntereses == "S") pagoIntereses.nombre_concepto = "Intereses: " + lCartera[i].conceptos.nombre + ", desde " + FechaUltimoCalculoIntereses.ToShortDateString() + " hasta " + FechaUltimoCalculoIntereses.AddDays(DiasTipo.NumeroDias).ToShortDateString();
                                    else pagoIntereses.nombre_concepto = "Intereses: " + lCartera[i].conceptos.nombre;

                                    pagoIntereses.id_cartera = lCartera[i].id;
                                    pagoIntereses.fecha_calculo_intereses = FechaActual;
                                    pagoIntereses.periodo = lCartera[i].periodo;
                                    pagoIntereses.vigencia = lCartera[i].vigencia;
                                    pagoIntereses.fecha_calculo_intereses = FechaActual.AddDays(1);
                                    pagoIntereses.id_grupo = lCartera[i].id_grupo;
                                    pagoIntereses.tipo = "IN";
                                    lDeuda.Add(pagoIntereses);
                                    ValorLiquidado += (int)pagoIntereses.valor;
                                }
                                else
                                {
                                    if (ValorIntereses < (reg.ValorPagar - ValorLiquidado))
                                    {
                                        pagoCapital.id_cartera = lCartera[i].id;
                                        pagoCapital.id_concepto = lCartera[i].id_concepto;
                                        pagoCapital.valor = (int)((reg.ValorPagar - ValorLiquidado) - ValorIntereses);
                                        pagoCapital.nombre_concepto = lCartera[i].conceptos.nombre;
                                        pagoCapital.periodo = lCartera[i].periodo;
                                        pagoCapital.vigencia = lCartera[i].vigencia;
                                        pagoCapital.id_grupo = lCartera[i].id_grupo;
                                        pagoCapital.tipo = "CA";
                                        lDeuda.Add(pagoCapital);
                                        ValorLiquidado += (int)pagoCapital.valor;

                                        if (ValorIntereses > 0)
                                        {
                                            pagoIntereses.id_concepto = 6;
                                            pagoIntereses.valor = ValorIntereses;

                                            if (DiasTipo.TiposIntereses == "S") pagoIntereses.nombre_concepto = "Intereses: " + lCartera[i].conceptos.nombre + ", desde " + FechaUltimoCalculoIntereses.ToShortDateString() + " hasta " + FechaUltimoCalculoIntereses.AddDays(DiasTipo.NumeroDias).ToShortDateString();
                                            else pagoIntereses.nombre_concepto = "Intereses: " + lCartera[i].conceptos.nombre;

                                            pagoIntereses.id_cartera = lCartera[i].id;
                                            pagoIntereses.fecha_calculo_intereses = FechaActual;
                                            pagoIntereses.periodo = lCartera[i].periodo;
                                            pagoIntereses.vigencia = lCartera[i].vigencia;
                                            pagoIntereses.fecha_calculo_intereses = FechaActual.AddDays(1);
                                            pagoIntereses.id_grupo = lCartera[i].id_grupo;
                                            pagoIntereses.tipo = "IN";
                                            lDeuda.Add(pagoIntereses);
                                            ValorLiquidado += (int)pagoIntereses.valor;
                                        }
                                    }
                                    else
                                    {
                                        pagoIntereses.id_concepto = 6;
                                        pagoIntereses.valor = reg.ValorPagar - ValorLiquidado;

                                        if (DiasTipo.TiposIntereses == "S") pagoIntereses.nombre_concepto = "Intereses: " + lCartera[i].conceptos.nombre + ", desde " + FechaUltimoCalculoIntereses.ToShortDateString() + " hasta " + FechaUltimoCalculoIntereses.AddDays(DiasTipo.NumeroDias).ToShortDateString();
                                        else pagoIntereses.nombre_concepto = "Intereses: " + lCartera[i].conceptos.nombre;

                                        pagoIntereses.id_cartera = lCartera[i].id;
                                        pagoIntereses.fecha_calculo_intereses = FechaUltimoCalculoIntereses.AddDays(DiasTipo.NumeroDias);
                                        pagoIntereses.periodo = lCartera[i].periodo;
                                        pagoIntereses.vigencia = lCartera[i].vigencia;
                                        pagoIntereses.fecha_calculo_intereses = FechaActual.AddDays(DiasTipo.NumeroDias + 1);
                                        pagoIntereses.id_grupo = lCartera[i].id_grupo;
                                        pagoIntereses.tipo = "IN";
                                        lDeuda.Add(pagoIntereses);
                                        ValorLiquidado += (int)pagoIntereses.valor;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (ValorPeriodo <= (reg.ValorPagar - ValorLiquidado))
                            {
                                pagoCapital.id_cartera = lCartera[i].id;
                                pagoCapital.id_concepto = lCartera[i].id_concepto;
                                pagoCapital.valor = (int)(ValorPeriodo);
                                pagoCapital.nombre_concepto = lCartera[i].conceptos.nombre;
                                pagoCapital.periodo = lCartera[i].periodo;
                                pagoCapital.vigencia = lCartera[i].vigencia;
                                pagoCapital.id_grupo = lCartera[i].id_grupo;
                                pagoCapital.tipo = "CA";
                                lDeuda.Add(pagoCapital);
                                ValorLiquidado += (int)pagoCapital.valor;
                            }
                            else
                            {
                                pagoCapital.id_cartera = lCartera[i].id;
                                pagoCapital.id_concepto = lCartera[i].id_concepto;
                                pagoCapital.valor = (int)(reg.ValorPagar - ValorLiquidado);
                                pagoCapital.nombre_concepto = lCartera[i].conceptos.nombre;
                                pagoCapital.periodo = lCartera[i].periodo;
                                pagoCapital.vigencia = lCartera[i].vigencia;
                                pagoCapital.id_grupo = lCartera[i].id_grupo;
                                pagoCapital.tipo = "CA";
                                lDeuda.Add(pagoCapital);
                                ValorLiquidado += (int)pagoCapital.valor;
                            }
                        }
                        i++;
                    }
                    else
                    {
                        ban = true;
                    }
                }
                return lDeuda;
            }
        }
    }
}
