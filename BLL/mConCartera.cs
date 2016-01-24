using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entidades;
using Entidades.Vistas;
using AutoMapper;
using BLL.DatosBasicos;

namespace BLL
{
    public class vmCarteraxSaldos {
        public int Item { get; set; }
        public double Valor {get; set;}
        public double Pagado { get; set; }
    }

    public class vmCarteraxSaldosxConceptos
    {
        public conceptosDto Item { get; set; }
        public double Valor { get; set; }
        public double Pagado { get; set; }
    }


    public class mConCartera
    {
        ieEntities db;


        public List<vmCarteraxSaldos> GetSaldos(string ide_est)
        {
            List<vmCarteraxSaldos> lst = new List<vmCarteraxSaldos>();
            DateTime FechaCausacion = mCausacion.FechaCausacion();
            List<vigenciasDto> lVigencias = new List<vigenciasDto>();
            int VigPerAct = int.Parse(FechaCausacion.Year.ToString() + FechaCausacion.Month.ToString().PadLeft(2, '0'));
            lVigencias = GetsVigencias(lVigencias);
            using (db = new ieEntities())            
            {                
                List<carterap> lCarteras = db.carterap.Where(t => t.id_estudiante == ide_est && (t.estado == "PR" || t.estado == "CA") && (t.vigencia * 100 + t.periodo) <= VigPerAct).ToList();                
                foreach (vigenciasDto vigencia in lVigencias)
                {
                    int ValorCausado = 0;
                    int ValorPagado = 0;

                    ValorCausado = CalcularValorCausado(FechaCausacion, lCarteras, vigencia, ValorCausado, db);

                    List<detalles_pago> lDetallesPago = db.detalles_pago.Where(t => t.vigencia == vigencia.vigencia && t.pagos.estado == "PA" && t.pagos.id_estudiante == ide_est).ToList();
                    lDetallesPago.ForEach(t => ValorPagado += (int) t.valor);

                    List<detalles_nota_credito> lDetallesNotas = db.detalles_nota_credito.Where(t => t.vigencia == vigencia.vigencia && t.notas_credito.estado == "PA" && t.notas_credito.id_estudiante == ide_est).ToList();
                    lDetallesNotas.ForEach(t => ValorPagado += (int)t.valor);

                    if ((ValorCausado > 0) || (ValorPagado > 0))
                    {
                        vmCarteraxSaldos objRes = new vmCarteraxSaldos();
                        objRes.Item = vigencia.vigencia;
                        objRes.Pagado = ValorPagado;
                        objRes.Valor = ValorCausado;

                        lst.Add(objRes);
                    }
                }                
                return lst;
            }
        }    
        public List<vmCarteraxSaldos> GetSaldos(string ide_est, int r_vigencia)
        {
            List<vmCarteraxSaldos> lst = new List<vmCarteraxSaldos>();
            DateTime FechaCausacion = mCausacion.FechaCausacion();
            List<periodosDto> lPeriodos = new List<periodosDto>();
            int VigPerAct = int.Parse(FechaCausacion.Year.ToString() + FechaCausacion.Month.ToString().PadLeft(2, '0'));

            mPeriodos oPeriodos = new mPeriodos();
            lPeriodos = oPeriodos.Gets(r_vigencia);

            using (db = new ieEntities())
            {
                List<carterap> lCarteras = db.carterap.Where(t => t.id_estudiante == ide_est && (t.estado == "PR" || t.estado == "CA") && (t.vigencia * 100 + t.periodo) <= VigPerAct && t.vigencia == r_vigencia).ToList();
                foreach (periodosDto periodo in lPeriodos)
                {
                    int ValorCausado = 0;
                    int ValorPagado = 0;

                    mVigencias objVigencias = new mVigencias();
                    vigenciasDto vigencia = objVigencias.Get(r_vigencia);

                    ValorCausado = CalcularValorCausadoPeriodo(FechaCausacion, lCarteras, vigencia, ValorCausado, (int) periodo.periodo, db);

                    List<detalles_pago> lDetallesPago = db.detalles_pago.Where(t => t.vigencia == vigencia.vigencia && t.periodo == periodo.periodo && t.pagos.estado == "PA" && t.pagos.id_estudiante == ide_est).ToList();
                    lDetallesPago.ForEach(t => ValorPagado += (int)t.valor);

                    List<detalles_nota_credito> lDetallesNotas = db.detalles_nota_credito.Where(t => t.vigencia == vigencia.vigencia && t.periodo == periodo.periodo && t.notas_credito.estado == "PA" && t.notas_credito.id_estudiante == ide_est).ToList();
                    lDetallesNotas.ForEach(t => ValorPagado += (int)t.valor);

                    if ((ValorCausado > 0) || (ValorPagado > 0))
                    {
                        vmCarteraxSaldos objRes = new vmCarteraxSaldos();
                        objRes.Item = (int) periodo.periodo;
                        objRes.Pagado = ValorPagado;
                        objRes.Valor = ValorCausado;

                        lst.Add(objRes);
                    }
                }
                return lst;
            }
        }
        public List<vmCarteraxSaldosxConceptos> GetSaldos(string ide_est, int r_vigencia, int periodo)
        {
            List<vmCarteraxSaldosxConceptos> lst = new List<vmCarteraxSaldosxConceptos>();
            DateTime FechaCausacion = mCausacion.FechaCausacion();
            List<periodosDto> lPeriodos = new List<periodosDto>();
            int VigPerAct = int.Parse(FechaCausacion.Year.ToString() + FechaCausacion.Month.ToString().PadLeft(2, '0'));

            using (db = new ieEntities())
            {
                mVigencias objVigencias = new mVigencias();
                vigenciasDto vigencia = objVigencias.Get(r_vigencia);

                List<carterap> lCarteras = db.carterap.Where(t => t.id_estudiante == ide_est && (t.estado == "PR" || t.estado == "CA") && (t.vigencia * 100 + t.periodo) <= VigPerAct && t.vigencia == r_vigencia && t.periodo == periodo).ToList();
                foreach (carterap cartera in lCarteras)
                {
                    vmCarteraxSaldosxConceptos objValorC = new vmCarteraxSaldosxConceptos();
                    objValorC.Item = new conceptosDto();
                    objValorC.Item.id = cartera.id_concepto;
                    objValorC.Item.nombre = cartera.conceptos.nombre;
                    objValorC.Valor = cartera.valor;
                    objValorC.Pagado = cartera.pagado;
                    lst.Add(objValorC); 

                    int ValorIntereses = 0;
                    int ValorPagadoIntereses = 0;
                    ValorIntereses = PreCalcularInteresesCartera(FechaCausacion, cartera, ValorIntereses);

                    List<detalles_pago> lDet = db.detalles_pago.Where(t => t.id_cartera == cartera.id && t.tipo == "IN" && t.pagos.estado == "PA").ToList();
                    lDet.ForEach(t => ValorPagadoIntereses += (int)t.valor);

                    List<detalles_nota_credito> lDetNota = db.detalles_nota_credito.Where(t => t.id_cartera == cartera.id && t.tipo == "IN" && t.notas_credito.estado == "PA").ToList();
                    lDetNota.ForEach(t => ValorPagadoIntereses += (int)t.valor);

                    vmCarteraxSaldosxConceptos objValorI = new vmCarteraxSaldosxConceptos();
                    objValorI.Item = new conceptosDto();
                    objValorI.Item.id = 6;
                    objValorI.Item.nombre = "Intereses: Pensión, Periodo: " + cartera.periodo;
                    objValorI.Valor = ValorIntereses;
                    objValorI.Pagado = ValorPagadoIntereses;
                    lst.Add(objValorI);
                }
                List<detalles_pago> lPagos = db.detalles_pago.Where(t => t.vigencia == vigencia.vigencia && t.periodo == periodo && t.pagos.estado == "PA" && t.pagos.id_estudiante == ide_est && (t.vigencia * 100 + t.periodo) > VigPerAct).ToList();
                foreach (detalles_pago item in lPagos)
                {
                    vmCarteraxSaldosxConceptos objValor = new vmCarteraxSaldosxConceptos();
                    objValor.Item = new conceptosDto();
                    objValor.Item.id = item.id_concepto;
                    objValor.Item.nombre = item.nombre_concepto;
                    objValor.Valor = 0;
                    objValor.Pagado = item.valor;
                    lst.Add(objValor);
                }

                List<detalles_nota_credito> lNotas = db.detalles_nota_credito.Where(t => t.vigencia == vigencia.vigencia && t.periodo == periodo && t.notas_credito.estado == "PA" && t.notas_credito.id_estudiante == ide_est && (t.vigencia * 100 + t.periodo) > VigPerAct).ToList();
                foreach (detalles_nota_credito item in lNotas)
                {
                    vmCarteraxSaldosxConceptos objValor = new vmCarteraxSaldosxConceptos();
                    objValor.Item = new conceptosDto();
                    objValor.Item.id = item.id_concepto;
                    objValor.Item.nombre = item.nombre_concepto;
                    objValor.Valor = 0;
                    objValor.Pagado = item.valor;
                    lst.Add(objValor);
                }
                return lst;
            }
        }

        

        private int CalcularValorInteresesCartera(DateTime FechaCausacion, carterap cartera, int ValorIntereses)
        {
            config_grupos_pagos config = db.config_grupos_pagos.Where(t => t.id_concepto == cartera.id_concepto && t.vigencia == cartera.vigencia).FirstOrDefault();
            if ((config != null) && (config.intereses == "SI"))
            {
                periodos periodo = db.periodos.Where(t => t.periodo == cartera.periodo && t.vigencia == cartera.vigencia).FirstOrDefault();
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
        private static List<vigenciasDto> GetsVigencias(List<vigenciasDto> lVigencias)
        {
            mVigencias oVigencias = new mVigencias();
            lVigencias = oVigencias.GetsActivas();
            return lVigencias;
        }
        private int CalcularValorCausado(DateTime FechaCausacion, List<carterap> lCarteras, vigenciasDto vigencia, int ValorCausado, ieEntities db)
        {
            foreach (carterap cartera in lCarteras.Where(t => t.vigencia == vigencia.vigencia).ToList())
            {
                int ValorIntereses = 0;
                ValorCausado += (int)cartera.valor;
                ValorIntereses = PreCalcularInteresesCartera(FechaCausacion, cartera, ValorIntereses); 
                ValorCausado += ValorIntereses;
            }
            return ValorCausado;
        }
        private int CalcularValorCausadoPeriodo(DateTime FechaCausacion, List<carterap> lCarteras, vigenciasDto vigencia, int ValorCausado, int Periodo, ieEntities db)
        {
            foreach (carterap cartera in lCarteras.Where(t => t.vigencia == vigencia.vigencia && t.periodo == Periodo).ToList())
            {
                int ValorIntereses = 0;
                ValorCausado += (int)cartera.valor;
                ValorIntereses = PreCalcularInteresesCartera(FechaCausacion, cartera, ValorIntereses);
                ValorCausado += ValorIntereses;
            }
            return ValorCausado;
        }
        private int PreCalcularInteresesCartera(DateTime FechaCausacion, carterap cartera, int ValorIntereses)
        {
            if (cartera.pagado == cartera.valor)
            {
                List<detalles_pago> lDet = db.detalles_pago.Where(t => t.id_cartera == cartera.id && t.tipo == "IN" && t.pagos.estado == "PA").ToList();
                lDet.ForEach(t => ValorIntereses += (int)t.valor);

                List<detalles_nota_credito> lDetno = db.detalles_nota_credito.Where(t => t.id_cartera == cartera.id && t.tipo == "IN" && t.notas_credito.estado == "PA").ToList();
                lDetno.ForEach(t => ValorIntereses += (int)t.valor);
            }
            else
            {
                int ValorAdicional = 0;
                ValorIntereses = CalcularValorInteresesCartera(FechaCausacion, cartera, ValorIntereses);
                List<detalles_pago> lDet = db.detalles_pago.Where(t => t.id_cartera == cartera.id && t.tipo == "IN" && t.pagos.estado == "PA").ToList();
                lDet.ForEach(t => ValorAdicional += (int)t.valor);

                List<detalles_nota_credito> lDetno = db.detalles_nota_credito.Where(t => t.id_cartera == cartera.id && t.tipo == "IN" && t.notas_credito.estado == "PA").ToList();
                lDetno.ForEach(t => ValorAdicional += (int)t.valor);

                ValorIntereses += ValorAdicional;
            }
            return ValorIntereses;
        }
    }
}
