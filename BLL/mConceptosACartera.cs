using Entidades.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ByA;
using DAL;
using AutoMapper;

namespace BLL
{
    public class mConceptosACartera
    {
        public ConceptosPeriodosDto GetConfiguracionPosible(int vigencia)
        {
            ConceptosPeriodosDto objRes = new ConceptosPeriodosDto();
            objRes.lConceptos = new List<conceptosDto>();
            objRes.lPeriodos = new List<periodosDto>();
            mConfigGruposPagos oConfigGru = new mConfigGruposPagos();
            mConceptos oConceptos = new mConceptos();
            mPeriodos oPeriodos = new mPeriodos();

            List<configGruposPagosDto> lConfig = oConfigGru.Gets(vigencia);
            foreach (configGruposPagosDto config in lConfig)
            {
                conceptosDto concepto = oConceptos.Get(config.id_concepto);
                objRes.lConceptos.Add(concepto);
            }

            objRes.lPeriodos = oPeriodos.Gets(vigencia);

            return objRes;
        }
        public ByARpt NuevoConceptoACartera(oNuevoConceptoDto Reg)
        {
            cmdInsert o = new cmdInsert();
            o.oDto = Reg;
            return o.Enviar();
        }
        class cmdInsert : absTemplate
        {
            public oNuevoConceptoDto oDto { get; set; }
            estudiantes estudiante;
            vigencias vigencia;
            cursos curso;
            matriculas matricula;
            List<tarifas> lTarifas = new List<tarifas>();
            int ultIDFechas;
            #region ImplementaciónMetodosAbstractos
            protected internal override bool esValido()
            {
                estudiante = ctx.estudiantes.Where(t => t.identificacion == oDto.id_estudiante).FirstOrDefault();
                if (estudiante != null)
                {
                    mCausacion.Causar(estudiante.identificacion);
                    vigencia = ctx.vigencias.Where(t => t.vigencia == oDto.vigencia).FirstOrDefault();
                    if (vigencia != null)
                    {
                        matricula = ctx.matriculas.Where(t => t.id_estudiante == oDto.id_estudiante && t.vigencia == oDto.vigencia && t.estado == "AC").FirstOrDefault();
                        if (matricula != null) return true;
                        else
                        {
                                
                            byaRpt.Error = true;
                            byaRpt.Mensaje = "El estudiante ya se encuentra matriculado en la vigencia actual";
                            return false;
                        }
                    }
                    else
                    {
                        byaRpt.Error = true;
                        byaRpt.Mensaje = "No ha indicado una vigencia valida";
                        return false;
                    }
                }
                else
                {
                    byaRpt.Error = true;
                    byaRpt.Mensaje = "El estudiante no se encuentra registrado";
                    return false;
                }
            }
            protected internal override void Antes()
            {
                int ultId = 0;
                try
                {
                    ultId = ctx.carterap.Max(t => t.id);
                }
                catch { }

                ultIDFechas = 0;
                try
                {
                    ultIDFechas = ctx.fechas_calculo_intereses.Max(t => t.id);
                }
                catch { }

                for (int i = (int)oDto.perido_desde_seleccionado; i <= oDto.perido_hasta_seleccionado; i++)
                {
                    ultId++;
                    carterap itemCartera = new carterap();
                    itemCartera.id = ultId;
                    itemCartera.vigencia = (int)oDto.vigencia;
                    itemCartera.id_concepto = (int)oDto.concepto_seleccionado;
                    itemCartera.periodo = i;
                    itemCartera.valor = oDto.valor;
                    itemCartera.id_matricula = matricula.id;
                    itemCartera.id_estudiante = oDto.id_estudiante;
                    itemCartera.pagado = 0;
                    itemCartera.id_est = estudiante.id;
                    itemCartera.id_grupo = (int)ctx.config_grupos_pagos.Where(t => t.id_concepto == itemCartera.id_concepto && t.vigencia == itemCartera.vigencia).FirstOrDefault().id_grupo;
                    itemCartera.estado = "PR";

                    itemCartera.usu_mod = oDto.usu;
                    itemCartera.usu_reg = oDto.usu;
                    itemCartera.fec_mod = DateTime.Now;
                    itemCartera.fec_reg = DateTime.Now;

                    ctx.carterap.Add(itemCartera);
                    InicializarCalculoIntereses(itemCartera.id, itemCartera.periodo, itemCartera.vigencia);
                }
            }
            private void InicializarCalculoIntereses(int id_cartera, int id_periodo, int vigencia)
            {
                periodos periodo = ctx.periodos.Where(t => t.vigencia == vigencia && t.periodo == id_periodo).FirstOrDefault();
                DateTime FechaVencimiento = new DateTime((int)periodo.vigencia, (int)periodo.periodo, (int)periodo.vence_dia);

                ultIDFechas++;
                fechas_calculo_intereses fecha = new fechas_calculo_intereses();
                fecha.id = ultIDFechas;
                fecha.id_cartera = id_cartera;
                fecha.fecha = FechaVencimiento;
                fecha.estado = "PA";
                ctx.fechas_calculo_intereses.Add(fecha);
            }
            #endregion
        }
    }
}
