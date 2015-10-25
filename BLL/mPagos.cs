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
    public class mPagos
    {
        ieEntities ctx;
        public mPagos()
        {
            Mapper.CreateMap<detalles_pago,detalles_pagoDto>();
            Mapper.CreateMap<pagos, pagosDto>()
                .ForMember(dest => dest.detalles_pago, opt => opt.MapFrom(src => src.detalles_pago))
                .ForMember(dest => dest.nombre_estudiante, opt => opt.MapFrom(src => src.estudiantes.terceros.nombre + " " + src.estudiantes.terceros.apellido))
                .ForMember(dest => dest.nombre_grupo, opt => opt.MapFrom(src => src.grupos_pagos != null ? src.grupos_pagos.nombre : "Ninguno")); 
            Mapper.CreateMap<pagosDto, pagos>();


        }
        public pagosDto GetLiquidacion(int id_liquidacion)
        {
            using (ctx = new ieEntities())
            {
                pagosDto r = new pagosDto();
                pagos o = ctx.pagos.Where(t => t.id == id_liquidacion).FirstOrDefault();
                Mapper.Map(o, r);
                return r;
            }
        }
        public List<pagosDto> GetsLiquidacionesEstudiante(string id_estudiante, int? id_grupo = null)
        {
            using (ctx = new ieEntities())
            {
                List<pagosDto> lr = new List<pagosDto>();
                List<pagos> l;
                if (id_grupo != null) l = ctx.pagos.Where(t => t.id_estudiante == id_estudiante && t.id_grupo == id_grupo && t.estado != "AN").OrderByDescending(t => t.id).ToList();
                else l = ctx.pagos.Where(t => t.id_estudiante == id_estudiante && t.estado != "AN").OrderByDescending(t => t.id).ToList();
                foreach (pagos item in l)
                {
                    int VT = 0;
                    pagosDto r = new pagosDto();
                    foreach (detalles_pago item2 in item.detalles_pago)
                    {
                        VT += (int) item2.valor;
                    }
                    item.detalles_pago = null;
                    Mapper.Map(item, r);
                    r.ValorTotal = VT;
                    lr.Add(r);
                }
                return lr;
            }
        }
        public List<pagosDto> GetPagosEstudiante(bPagosEstudiante Reg)
        {
            using (ctx = new ieEntities())
            {
                Reg.FechaInicial = new DateTime(Reg.FechaInicial.Year, Reg.FechaInicial.Month, Reg.FechaInicial.Day, 0, 0, 0);
                Reg.FechaFinal = new DateTime(Reg.FechaFinal.Year, Reg.FechaFinal.Month, Reg.FechaFinal.Day, 0, 0, 0);
                List<pagosDto> lr = new List<pagosDto>();
                List<pagos> l = new List<pagos>();
                if(Reg.id_estudiante != "") l = ctx.pagos.Where(t => t.id_estudiante == Reg.id_estudiante && t.estado == "PA" && t.fecha_pago >= Reg.FechaInicial && t.fecha_pago <= Reg.FechaFinal).OrderByDescending(t=> t.id).ToList();
                else l = ctx.pagos.Where(t => t.estado == "PA" && t.fecha_pago >= Reg.FechaInicial && t.fecha_pago <= Reg.FechaFinal).OrderByDescending(t => t.id).ToList();
                Mapper.Map(l, lr);
                return lr;
            }
        }
        public ByARpt AnularPago(int id_pago, string usu)
        {
            cmdAnularPago o = new cmdAnularPago();
            o.id_pago = id_pago;
            o.usu = usu;
            return o.Enviar();
        }
        public ByARpt AnularLiquidacion(int id_pago, string usu)
        {
            cmdAnularLiquidacion o = new cmdAnularLiquidacion();
            o.id_pago = id_pago;
            o.usu = usu;
            return o.Enviar();
        }
        public ByARpt InsertLiquidacion(pagosDto Reg)
        {
            cmdInsertLiquidacion o = new cmdInsertLiquidacion();
            o.oDto = Reg;
            return o.Enviar();
        }
        public ByARpt Pagar(pagosDto Reg)
        {
            mCausacion.Causar(Reg.id_estudiante);
            cmdInsertPago o = new cmdInsertPago();
            o.oDto = Reg;
            return o.Enviar();
        }
        public ByARpt PagarLiquidacion(pagosDto Reg)
        {
            cmdInsertPagar o = new cmdInsertPagar();
            o.oDto = Reg;
            return o.Enviar();
        }
        class cmdInsertLiquidacion : absTemplate
        {
            public pagosDto oDto { get; set; }
            private List<detalles_pagoDto> detalles_pago { get; set; }
            private pagos Dto { get; set; }
            private estudiantes estudiante { get; set; }
            private grupos_pagos grupo_pago { get; set; }
            private pagos UltLiqui { get; set; }
            private int ultIdFechaCalculoIntereses { get; set; }
            #region ImplementaciónMetodosAbstractos
            protected internal override bool esValido()
            {
                estudiante = ctx.estudiantes.Where(t => t.identificacion == oDto.id_estudiante).FirstOrDefault();
                if (estudiante != null)
                {
                    mCausacion.Causar(estudiante.identificacion);
                    UltLiqui = ctx.pagos.Where(t => t.estado == "LI" && t.id_estudiante == oDto.id_estudiante).FirstOrDefault();
                    if (UltLiqui == null)
                    {
                        pagos pagoMax = ctx.pagos.Where(t => t.estado == "PA").OrderByDescending(t => t.id).FirstOrDefault();
                        if (pagoMax == null) return true;
                        {
                            if (pagoMax.fecha_pago.Value.Date <= oDto.fecha.Date) return true;
                            else
                            {
                                byaRpt.Error = true;
                                byaRpt.Mensaje = "No se puede realizar la liquidación en la fecha indicada, porque hay un pago con una fecha mayor registrada";
                                return false;
                            }
                        }
                    }
                    else
                    {
                        byaRpt.Error = true;
                        byaRpt.Mensaje = "El estudiante tiene una liquidación sin pagar";
                        return false;
                    }
                }
                else
                {
                    byaRpt.Error = true;
                    byaRpt.Mensaje = "La identificación del estudiante no se encuantra registrada";
                    return false;
                }
            }
            protected internal override void Antes()
            {
                int UltId = 0;
                try
                {
                    UltId = ctx.pagos.Max(t => t.id);
                }
                catch { }

                ultIdFechaCalculoIntereses = 0;
                try
                {
                    ultIdFechaCalculoIntereses = ctx.fechas_calculo_intereses.Max(t => t.id);
                }
                catch { }

                UltId++;
                oDto.id = UltId;
                oDto.fecha_pago = null;
                oDto.id_forma_pago = 1;
                oDto.estado = "LI";
                oDto.id_est = estudiante.id;
                oDto.tipo = "PL";
                oDto.fecha = oDto.fecha.Date;

                oDto.fecha_max_pago = CalcularFechaVencimientoLiquidacion();

                detalles_pago = oDto.detalles_pago;
                oDto.detalles_pago = null;                

                Dto = new pagos();
                Mapper.Map(oDto,Dto);
                _cmpReg();
                ctx.pagos.Add(Dto);
                InsertDetallesPagos();
            }
            protected override void Despues()
            {
                byaRpt.id = oDto.id.ToString();
                byaRpt.Mensaje = "Operación realizada satisfactoriamente";
            }
            private void InsertDetallesPagos()
            {
                int UltId = 0;
                try
                {
                    UltId = ctx.detalles_pago.Max(t => t.id);
                }
                catch { }

                foreach (detalles_pagoDto item in detalles_pago)
                {
                    UltId++;
                    detalles_pago detalle = new detalles_pago();
                    detalle.id = UltId;
                    detalle.id_pago = oDto.id;

                    if (item.tipo != "IN") detalle.id_concepto = (int)item.id_concepto;
                    else
                    {
                        /// Se le asigna el codigo de concepto de intereses = 6
                        detalle.id_concepto = item.id_concepto;
                        InsertFechaUltimoCalculoInteresesCartera(item);
                    }

                    detalle.periodo = item.periodo;
                    detalle.vigencia = item.vigencia;
                    detalle.valor = item.valor;
                    detalle.tipo = item.tipo;
                    detalle.id_cartera = item.id_cartera;
                    detalle.nombre_concepto = item.nombre_concepto;

                    //movimientosDto mv = new movimientosDto();
                    //mv.id_cartera = detalle.id_cartera;
                    //mMovimientos m = new mMovimientos(ctx);
                    //m.Insert(mv);


                    ctx.detalles_pago.Add(detalle);
                }
            }
            private void InsertFechaUltimoCalculoInteresesCartera(detalles_pagoDto detalle)
            {
                ultIdFechaCalculoIntereses++;

                fechas_calculo_intereses fecha_intereses = new fechas_calculo_intereses();
                fecha_intereses.id = ultIdFechaCalculoIntereses;
                fecha_intereses.id_cartera = detalle.id_cartera;
                fecha_intereses.fecha = detalle.fecha_calculo_intereses;
                fecha_intereses.estado = "LI";
                ctx.fechas_calculo_intereses.Add(fecha_intereses);
            }
            private DateTime? CalcularFechaVencimientoLiquidacion()
            {
                DateTime FechaActual = oDto.fecha.Date;
                mIntereses oTI = new mIntereses();
                bool Ok = false;
                foreach (detalles_pagoDto item in oDto.detalles_pago.Where(t => t.tipo == "CA").ToList())
                {
                    config_grupos_pagos config = ctx.config_grupos_pagos.Where(t => t.id_concepto == item.id_concepto && t.vigencia == item.vigencia).FirstOrDefault();
                    if ((config != null) && (config.intereses == "SI")) Ok = true;
                }
                if (Ok == false) return null;
                else
                {
                    bool Seguir = true;

                    DateTime FechaExtrema = new DateTime(2100, 1, 1);
                    if (VerificarInteresesDetallesPago(FechaExtrema)) return null;
                    else
                    {
                        DateTime FechaVencimiento = oDto.fecha;
                        while (Seguir)
                        {
                            FechaVencimiento = FechaVencimiento.AddDays(1);
                            bool InteresesIguales = VerificarInteresesDetallesPago(FechaVencimiento);
                            if (InteresesIguales) Seguir = true;
                            else Seguir = false;
                        }
                        return FechaVencimiento.AddDays(-1);
                    }
                }
            }
            private bool VerificarInteresesDetallesPago(DateTime FechaCalcular)
            {
                DateTime FechaActual = FechaCalcular;
                mIntereses oTI = new mIntereses();
                bool Ok = true;
                foreach (detalles_pagoDto item in oDto.detalles_pago.Where(t => t.tipo == "CA").ToList())
                {
                    int ValorIntereses = 0;
                    config_grupos_pagos config = ctx.config_grupos_pagos.Where(t => t.id_concepto == item.id_concepto && t.vigencia == item.vigencia).FirstOrDefault();
                    if ((config != null) && (config.intereses == "SI"))
                    {
                        periodos periodo = ctx.periodos.Where(t => t.periodo == item.periodo && t.vigencia == item.vigencia).FirstOrDefault();
                        DateTime FechaVencimientoPeriodo = new DateTime((int)periodo.vigencia, (int)periodo.periodo, (int)periodo.vence_dia);

                        carterap cart = ctx.carterap.Where(t => t.id == item.id_cartera).FirstOrDefault();

                        DateTime FechaUltimoCalculoIntereses = cart.fechas_calculo_intereses.Where(t => t.estado == "PA").OrderByDescending(t => t.fecha).FirstOrDefault().fecha;

                        if (FechaActual > FechaVencimientoPeriodo)
                        {
                            ValorIntereses = oTI.GetValorIntereses(FechaUltimoCalculoIntereses, FechaActual, (float)item.valor, item.vigencia, item.periodo, item.id);
                        }
                    }

                    if (ValorIntereses > 0)
                    {
                        detalles_pagoDto detailsPaymentOld = oDto.detalles_pago.Where(t => t.tipo == "IN" && t.id_cartera == item.id_cartera).FirstOrDefault();
                        if ((detailsPaymentOld == null) || (detailsPaymentOld.valor != ValorIntereses))
                        {
                            Ok = false;
                            break;
                        }
                    }
                }
                return Ok;
            }
            private void _cmpReg()
            {
                Dto.fec_reg = DateTime.Now;
                Dto.fec_mod = DateTime.Now;
                Dto.usu_mod = oDto.usu;
                Dto.usu_reg = oDto.usu;
            }
            #endregion
        }
        class cmdInsertPago : absTemplate
        {
            public pagosDto oDto { get; set; }
            private pagos Dto { get; set; }
            private estudiantes estudiante { get; set; }
            private grupos_pagos grupo_pago { get; set; }
            private List<detalles_pagoDto> detalles_pago { get; set; }
            private pagos UltLiqui { get; set; }
            private int ultIdFechaCalculoIntereses { get; set; }
            #region ImplementaciónMetodosAbstractos
            protected internal override bool esValido()
            {
                estudiante = ctx.estudiantes.Where(t => t.identificacion == oDto.id_estudiante).FirstOrDefault();
                if (estudiante != null)
                {                    
                    UltLiqui = ctx.pagos.Where(t => t.estado == "LI" && t.id_estudiante == oDto.id_estudiante).FirstOrDefault();
                    if (UltLiqui == null)
                    {
                        pagos pagoMax = ctx.pagos.Where(t => t.estado == "PA").OrderByDescending(t => t.id).FirstOrDefault();
                        if (pagoMax == null) return true;
                        {
                            if (pagoMax.fecha_pago.Value.Date <= oDto.fecha_pago.Value.Date) return true;
                            else
                            {
                                byaRpt.Error = true;
                                byaRpt.Mensaje = "No se puede realizar el pago en la fecha de pago indicada, porque hay un pago con una fecha mayor registrada";
                                return false;
                            }
                        }
                    }
                    else
                    {
                        byaRpt.Error = true;
                        byaRpt.Mensaje = "El estudiante tiene una liquidación sin pagar";
                        return false;
                    }
                }
                else
                {
                    byaRpt.Error = true;
                    byaRpt.Mensaje = "La identificación del estudiante no se encuantra registrada";
                    return false;
                }
            }            
            protected internal override void Antes()
            {
                int UltId = UltPIdPago();

                UltIdFechaIntereses();

                UltId++;
                oDto.id = UltId;
                oDto.id_forma_pago = 1;
                oDto.estado = "PA";
                oDto.id_est = estudiante.id;
                oDto.tipo = "PS";

                detalles_pago = oDto.detalles_pago;
                oDto.detalles_pago = null;

                Dto = new pagos();
                Mapper.Map(oDto, Dto);

                _cmpReg();

                ctx.pagos.Add(Dto);

                InsertDetallesPagos();
            }
            private void _cmpReg()
            {
                Dto.fec_reg = DateTime.Now;
                Dto.fec_mod = DateTime.Now;
                Dto.usu_mod = oDto.usu;
                Dto.usu_reg = oDto.usu;
            }
            private void UltIdFechaIntereses()
            {
                ultIdFechaCalculoIntereses = 0;
                try
                {
                    ultIdFechaCalculoIntereses = ctx.fechas_calculo_intereses.Max(t => t.id);
                }
                catch { }
            }
            private int UltPIdPago()
            {
                int UltId = 0;
                try
                {
                    UltId = ctx.pagos.Max(t => t.id);
                }
                catch { }
                return UltId;
            }
            protected override void Despues()
            {
                byaRpt.id = oDto.id.ToString();
                byaRpt.Mensaje = "Operación realizada satisfactoriamente";
            }
            private void InsertDetallesPagos()
            {
                int UltId = UltDetallePago();
                mMovimientos mm = new mMovimientos(this.ctx);
                var id_mov = mm.GetMaxId(); 
                
                foreach (detalles_pagoDto item in detalles_pago)
                {
                    UltId++;
                    InsDetallePago(UltId, item);

                    
                    if (item.tipo == "IN")
                    {
                        id_mov++;
                        InsMovimiento(mm, id_mov, item,"DB");
                    }
                    id_mov++;
                    InsMovimiento(mm, id_mov, item);

                    
                }
            }
            private void InsMovimiento(mMovimientos mm, int id_mov, detalles_pagoDto item, string tipo = "CR")
            {
                movimientosDto m = new movimientosDto();
                m.estado = "AC";
                m.fecha_movimiento = oDto.fecha_pago;
                m.fecha_novedad = DateTime.Now;
                m.fecha_registro = DateTime.Now;
                m.id_cartera = item.id_cartera;
                m.id_concepto = item.id_concepto;
                m.id_estudiante = oDto.id_estudiante;
                m.numero_documento = oDto.id;
                m.id_est = oDto.id_est;
                
                if (tipo == "CR")
                {
                    m.tipo_documento = "PAGOS";
                    m.valor_debito = 0;
                    m.valor_credito = item.valor;
                }
                else
                {
                    m.tipo_documento = "PAGOS";
                    m.valor_debito = item.valor ;
                    m.valor_credito = 0;
                }
                
                m.vigencia = item.vigencia;
                m.id = id_mov;
                m.periodo = item.periodo;
                mm.Insert(m);
            }
            private void InsDetallePago(int UltId, detalles_pagoDto item)
            {
                detalles_pago detalle = new detalles_pago();
                detalle.id = UltId;
                detalle.id_pago = oDto.id;

                if (item.tipo != "IN")
                {
                    detalle.id_concepto = (int)item.id_concepto;
                    carterap cartera = ctx.carterap.Where(t => t.id == item.id_cartera).FirstOrDefault();
                    if (cartera != null) cartera.pagado += item.valor;
                }
                else
                {
                    // se asigna el codigo de concepto de intereses = 6
                    detalle.id_concepto = item.id_concepto;
                    InsertFechaUltimoCalculoInteresesCartera(item);
                }

                detalle.periodo = item.periodo;
                detalle.vigencia = item.vigencia;
                detalle.valor = item.valor;
                detalle.tipo = item.tipo;
                detalle.id_cartera = item.id_cartera;
                detalle.nombre_concepto = item.nombre_concepto;

                ctx.detalles_pago.Add(detalle);
            }
            private int UltDetallePago()
            {
                int UltId = 0;
                try
                {
                    UltId = ctx.detalles_pago.Max(t => t.id);
                }
                catch { }
                
                return UltId;
            }
            private void InsertFechaUltimoCalculoInteresesCartera(detalles_pagoDto detalle)
            {
                ultIdFechaCalculoIntereses++;

                fechas_calculo_intereses fecha_intereses = new fechas_calculo_intereses();
                fecha_intereses.id = ultIdFechaCalculoIntereses;
                fecha_intereses.id_cartera = detalle.id_cartera;
                fecha_intereses.fecha = detalle.fecha_calculo_intereses;
                fecha_intereses.estado = "PA";
                ctx.fechas_calculo_intereses.Add(fecha_intereses);

            }
            #endregion
        }
        class cmdInsertPagar : absTemplate
        {
            private pagos Dto { get; set; }
            public pagosDto oDto { get; set; }
            #region ImplementaciónMetodosAbstractos
            protected internal override bool esValido()
            {
                Dto = ctx.pagos.Where(t => t.id == oDto.id && t.estado == "LI").FirstOrDefault();
                if (Dto != null)
                {
                    if (Dto.fecha <= oDto.fecha_pago)
                    {
                        if ((oDto.fecha_pago <= Dto.fecha_max_pago) || (Dto.fecha_max_pago == null) || (oDto.VerificadoIntereses == true))
                        {
                            pagos pagoMax = ctx.pagos.Where(t => t.estado == "PA").OrderByDescending(t => t.id).FirstOrDefault();
                            if (pagoMax == null) return true;
                            else
                            {
                                if (pagoMax.fecha_pago.Value.Date <= oDto.fecha_pago.Value.Date) return true;
                                else
                                {
                                    byaRpt.id = "false";
                                    byaRpt.Error = true;
                                    byaRpt.Mensaje = "No se puede realizar el pago en la fecha de pago indicada, porque hay un pago con una fecha mayor registrada";
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            byaRpt.id = "true";
                            byaRpt.Error = true;
                            byaRpt.Mensaje = "El pago fue realizado despues de la fecha de vencimiento, por lo cual se han generado nuevos " +
                                             "intereses como se muestra a continuación: </br></br>" +
                                             VerificarInteresesDetallesPago() +
                                             "¿Que desea hacer con los intereses?</br>"+
                                             "- <strong>Obviar:</strong> se registrara el pago ignorando los intereses.</br>" +
                                             "- <strong>Causar:</strong> se causaran los intereses para pagarlos despues, y se registrata el pago.";

                            return false;
                        }                        
                    }
                    else
                    {
                        byaRpt.id = "false";
                        byaRpt.Error = true;
                        byaRpt.Mensaje = "No se puede realizar el pago en la fecha de pago indicada, porque la fecha de liquidación es menor";
                        return false;
                    }
                }
                else
                {
                    byaRpt.id = "false";
                    byaRpt.Error = true;
                    byaRpt.Mensaje = "No se encuentra la liquidación";
                    return false;
                }
            }
            private string VerificarInteresesDetallesPago()
            {
                string CadenaTabla = "<table class='table table-bordered table-hover table-striped tablesorter'>" +
                                        "<thead>" +
                                            "<tr>" +
                                                "<th>Concepto <i class='fa fa-sort'></i></th>" +
                                                "<th style='text-align:right'>Valor Antes <i class='fa fa-sort'></i></th>" +
                                                "<th style='text-align:right'>Valor Ahora <i class='fa fa-sort'></i></th>" +
                                            "</tr>" +
                                        "</thead>" +
                                        "<tbody>";
                DateTime FechaActual = oDto.fecha_pago.Value.Date;
                mIntereses oTI = new mIntereses();
                foreach (detalles_pago item in Dto.detalles_pago.Where(t => t.tipo == "CA").ToList())
                {
                    int ValorIntereses = 0;
                    config_grupos_pagos config = ctx.config_grupos_pagos.Where(t => t.id_concepto == item.id_concepto && t.vigencia == item.vigencia).FirstOrDefault();
                    if ((config != null) && (config.intereses == "SI"))
                    {
                        periodos periodo = ctx.periodos.Where(t => t.periodo == item.periodo && t.vigencia == item.vigencia).FirstOrDefault();
                        DateTime FechaVencimientoPeriodo = new DateTime((int)periodo.vigencia, (int)periodo.periodo, (int)periodo.vence_dia);
                        DateTime FechaUltimoCalculoIntereses = item.carterap.fechas_calculo_intereses.Where(t => t.estado == "PA").OrderByDescending(t => t.fecha).FirstOrDefault().fecha;

                        if (FechaActual > FechaVencimientoPeriodo)
                        {
                            ValorIntereses = oTI.GetValorIntereses(FechaUltimoCalculoIntereses, FechaActual, (float)item.valor, item.vigencia, item.periodo, item.id);
                        }
                    }

                    if (ValorIntereses > 0)
                    {
                        detalles_pago detailsPaymentOld = Dto.detalles_pago.Where(t => t.tipo == "IN" && t.id_cartera == item.id_cartera).FirstOrDefault();
                        if ((detailsPaymentOld == null) || (detailsPaymentOld.valor != ValorIntereses))
                        {
                            detalles_pago detPag = Dto.detalles_pago.Where(t => t.tipo == "CA" && t.id_cartera == item.id_cartera).FirstOrDefault();
                            carterap itemCart = detPag.carterap;
                            if (itemCart.valor - itemCart.pagado - detPag.valor == 0)
                            {
                                int interesesPri = 0;
                                if (detailsPaymentOld != null) interesesPri = (int)detailsPaymentOld.valor;

                                detalles_pago det = Dto.detalles_pago.Where(t => t.tipo == "CA" && t.id_cartera == item.id_cartera).FirstOrDefault();
                                CadenaTabla += "<tr>" +
                                                    "<td>" + det.conceptos.nombre + ", periodo: " + det.periodo + "</td>" +
                                                    "<td style='text-align:right'>" + interesesPri.ToString("C") + "</td>" +
                                                    "<td style='text-align:right'>" + ValorIntereses.ToString("C") + "</td>" +
                                                "</tr>";
                            }
                        }
                    }
                }
                 CadenaTabla += "</tbody></table>";
                 return CadenaTabla;
            }
            protected override void Despues()
            {
                base.Despues();
                byaRpt.id = "false";
            }
            protected internal override void Antes()
            {
                Dto.observacion = oDto.observacion;
                Dto.estado = "PA";
                Dto.fecha_pago = oDto.fecha_pago.Value.Date;
                _cmpReg();
                AfectarCarteraDetallesPago();
               
                if(oDto.VerificadoIntereses) OperacionIntereses();
            }

            private void _cmpReg()
            {
                Dto.usu_mod = oDto.usu;
                Dto.fec_mod = DateTime.Now;
            }
            private void AfectarCarteraDetallesPago()
            {
                mMovimientos mm = new mMovimientos(this.ctx);
                var id_mov = mm.GetMaxId(); 
                foreach (detalles_pago item in Dto.detalles_pago.ToList())
                {

                    if (item.tipo == "CA")
                    {
                        item.carterap.pagado += item.valor;
                        fechas_calculo_intereses FechaOld = ctx.fechas_calculo_intereses.Where(t => t.id_cartera == item.id_cartera && t.estado == "LI").OrderByDescending(t => t.fecha).FirstOrDefault();
                        if (FechaOld != null) FechaOld.estado = "PA";
                    }

                    if (item.tipo == "IN")
                    {
                        id_mov++;
                        InsMovimiento(mm, id_mov, item, "DB");
                    }
                    id_mov++;
                    InsMovimiento(mm, id_mov, item);
                }
            }            
            private void InsMovimiento(mMovimientos mm, int id_mov, detalles_pago item, string tipo = "CR")
            {
                movimientosDto m = new movimientosDto();
                m.estado = "AC";
                m.fecha_movimiento = Dto.fecha_pago;
                m.fecha_novedad = DateTime.Now;
                m.fecha_registro = DateTime.Now;
                m.id_cartera = item.id_cartera;
                m.id_concepto = item.id_concepto;
                m.id_estudiante = Dto.id_estudiante;
                m.numero_documento = Dto.id;
                m.id_est = Dto.id_est;
                
                if (tipo == "CR")
                {
                    m.tipo_documento = "PAGOS-L";
                    m.valor_debito = 0;
                    m.valor_credito = item.valor;
                }
                else
                {
                    m.tipo_documento = "PAGOS-L";
                    m.valor_debito = item.valor ;
                    m.valor_credito = 0;
                }
                
                m.vigencia = item.vigencia;
                m.id = id_mov;
                m.periodo = item.periodo;
                mm.Insert(m);                
            }
            public void OperacionIntereses()
            {
                if (oDto.causar_intereses)
                {
                    int ultId = 0;
                    try
                    {
                        ultId = ctx.carterap.Max(t => t.id);
                    }
                    catch { }

                    DateTime FechaActual = oDto.fecha_pago.Value.Date;
                    mIntereses oTI = new mIntereses();
                    foreach (detalles_pago item in Dto.detalles_pago.Where(t => t.tipo == "CA").ToList())
                    {
                        int ValorIntereses = 0;
                        config_grupos_pagos config = ctx.config_grupos_pagos.Where(t => t.id_concepto == item.id_concepto && t.vigencia == item.vigencia).FirstOrDefault();
                        if ((config != null) && (config.intereses == "SI"))
                        {
                            periodos periodo = ctx.periodos.Where(t => t.periodo == item.periodo && t.vigencia == item.vigencia).FirstOrDefault();
                            DateTime FechaVencimientoPeriodo = new DateTime((int)periodo.vigencia, (int)periodo.periodo, (int)periodo.vence_dia);
                            DateTime FechaUltimoCalculoIntereses = item.carterap.fechas_calculo_intereses.Where(t => t.estado == "PA").OrderByDescending(t => t.fecha).FirstOrDefault().fecha;

                            if (FechaActual > FechaVencimientoPeriodo)
                            {
                                ValorIntereses = oTI.GetValorIntereses(FechaUltimoCalculoIntereses, FechaActual, (float)item.valor, item.vigencia, item.periodo, item.id);
                            }
                        }

                        if (ValorIntereses > 0)
                        {
                            detalles_pago detailsPaymentOld = Dto.detalles_pago.Where(t => t.tipo == "IN" && t.id_cartera == item.id_cartera).FirstOrDefault();
                            if ((detailsPaymentOld == null) || (detailsPaymentOld.valor != ValorIntereses))
                            {
                                detalles_pago detPag = Dto.detalles_pago.Where(t => t.tipo == "CA" && t.id_cartera == item.id_cartera).FirstOrDefault();
                                carterap itemCart = detPag.carterap;
                                if (itemCart.valor - itemCart.pagado == 0)
                                {
                                    ultId++;
                                    carterap itemCartera = new carterap();
                                    itemCartera.id = ultId;
                                    itemCartera.vigencia = item.vigencia;
                                    itemCartera.id_concepto = 6;
                                    itemCartera.periodo = item.periodo;
                                    itemCartera.valor = ValorIntereses;
                                    itemCartera.id_matricula = item.carterap.id_matricula;
                                    itemCartera.id_estudiante = item.carterap.id_estudiante;
                                    itemCartera.pagado = 0;
                                    itemCartera.id_est = item.carterap.id_est;
                                    itemCartera.id_grupo = item.carterap.id_grupo;
                                    itemCartera.estado = "PR";
                                    itemCartera.pago_genero_intereses = Dto.id;
                                    ctx.carterap.Add(itemCartera);
                                }                                
                            }
                        }
                    }
                }
            }
            #endregion
        }
        class cmdAnularPago : absTemplate
        {
            public int id_pago { get; set; }
            public string usu { get; set; }
            int ultIdMov { get; set; }
            pagos pago { get; set; }
            DateTime fecha_pago { get; set; }
            documentos doc { get; set; }
            #region ImplementaciónMetodosAbstractos
            protected internal override bool esValido()
            {
                pago = ctx.pagos.Where(t => t.id == id_pago).FirstOrDefault();
                if (pago != null) {
                    if (pago.estado == "PA"){
                        pagos pagoLiq = ctx.pagos.Where(t => t.id_estudiante == pago.id_estudiante && t.estado == "LI").FirstOrDefault();
                        if (pagoLiq == null)
                        {
                            pagos pag = ctx.pagos.Where(t => t.id_estudiante == pago.id_estudiante && t.estado != "AN").OrderByDescending(t => t.fecha).ThenByDescending(t=> t.id).FirstOrDefault();
                            if (pag.id == pago.id) return true;
                            else
                            {
                                byaRpt.Error = true;
                                byaRpt.Mensaje = "Solo se puede anular el último pago realizado";
                                return false;
                            }
                        }
                        else
                        {
                            byaRpt.Error = true;
                            byaRpt.Mensaje = "Existe otra liquidación sin pagar para este estudiante. Nota: Solo se puede tener una liquidación sin pagar.";
                            return false;
                        }                        
                    }
                    else
                    {
                        byaRpt.Error = true;
                        byaRpt.Mensaje = "Esta liquidación no ha sido pagada";
                        return false;
                    }
                }
                else
                {
                    byaRpt.Error = true;
                    byaRpt.Mensaje = "El número de pago no puede ser encontrado";
                    return false;
                }
            }
            protected internal override void Antes()
            {
                ultIdMovimientos();
                int ultIdDoc = ultIdDocumentos();
                ultIdDoc++;
                doc = new documentos();
                doc.id = ultIdDoc;
                doc.fecha = DateTime.Now;
                doc.tipo_documento = "ANPAGO";
                doc.descripcion = "Se genero por anulación del pago No. " + id_pago;
                ctx.documentos.Add(doc);
                if(pago.tipo == "PL") pago.estado = "LI";
                else pago.estado = "AN";
                fecha_pago = (DateTime) pago.fecha_pago;
                pago.fecha_pago = null;
                AfectarCartera();
                VerificarSiPagoVencido();

                _cmpReg();
            }

            private void _cmpReg()
            {
                pago.usu_mod = usu;
                pago.fec_mod = DateTime.Now;
            }
            private void VerificarSiPagoVencido()
            {
                if (fecha_pago > pago.fecha_max_pago)
                {
                    List<carterap> lCarteras = ctx.carterap.Where(t => t.pago_genero_intereses == pago.id).ToList();
                    foreach (carterap item in lCarteras)
                    {
                        List<movimientos> lMovi = item.movimientos.ToList();
                        foreach (movimientos item2 in lMovi)
                        {
                            ctx.movimientos.Remove(item2);
                        }
                        ctx.carterap.Remove(item);
                    }
                }
            }
            private void AfectarCartera()
            {
                foreach (detalles_pago item in pago.detalles_pago)
                {
                    if (item.tipo == "CA") AfectarCapital(item);
                    if (item.tipo == "IN") AfectarIntereses(item);
                }
            }
            private void AfectarIntereses(detalles_pago item)
            {
                fechas_calculo_intereses ult_fecha = item.carterap.fechas_calculo_intereses.Where(t => t.estado == "PA").OrderByDescending(t => t.fecha).FirstOrDefault();
                ult_fecha.estado = "LI";

                ultIdMov++;
                movimientos movIN = new movimientos();
                movIN.id = ultIdMov;
                movIN.id_estudiante = pago.id_estudiante;
                movIN.vigencia = item.vigencia;
                movIN.periodo = item.periodo;
                movIN.id_cartera = item.id_cartera;
                movIN.id_concepto = item.id_concepto;
                movIN.valor_debito = item.valor;
                movIN.valor_credito = 0;
                movIN.fecha_movimiento = DateTime.Now;
                movIN.estado = "AC";
                movIN.fecha_novedad = DateTime.Now;
                movIN.fecha_registro = DateTime.Now;
                movIN.tipo_documento = "ANPAGO";
                movIN.numero_documento = doc.id;
                movIN.id_est = pago.id_est;
                ctx.movimientos.Add(movIN);

                ultIdMov++;
                movimientos movIN2 = new movimientos();
                movIN2.id = ultIdMov;
                movIN2.id_estudiante = pago.id_estudiante;
                movIN2.vigencia = item.vigencia;
                movIN2.periodo = item.periodo;
                movIN2.id_cartera = item.id_cartera;
                movIN2.id_concepto = item.id_concepto;
                movIN2.valor_debito = 0;
                movIN2.valor_credito = item.valor;
                movIN2.fecha_movimiento = DateTime.Now;
                movIN2.estado = "AC";
                movIN2.fecha_novedad = DateTime.Now;
                movIN2.fecha_registro = DateTime.Now;
                movIN2.tipo_documento = "ANPAGO";
                movIN2.numero_documento = doc.id;
                movIN2.id_est = pago.id_est;
                ctx.movimientos.Add(movIN2);
            }
            private void AfectarCapital(detalles_pago item)
            {
                ultIdMov++;
                movimientos movCA = new movimientos();
                movCA.id = ultIdMov;
                movCA.id_estudiante = pago.id_estudiante;
                movCA.vigencia = item.vigencia;
                movCA.periodo = item.periodo;
                movCA.id_cartera = item.id_cartera;
                movCA.id_concepto = item.id_concepto;
                movCA.valor_debito = item.valor;
                movCA.valor_credito = 0;
                movCA.fecha_movimiento = DateTime.Now;
                movCA.estado = "AC";
                movCA.fecha_novedad = DateTime.Now;
                movCA.fecha_registro = DateTime.Now;
                movCA.tipo_documento = "ANPAGO";
                movCA.numero_documento = doc.id;
                movCA.id_est = pago.id_est;
                ctx.movimientos.Add(movCA);

                item.carterap.pagado -= item.valor;
            }
            private void ultIdMovimientos()
            {
                ultIdMov = 0;
                try
                {
                    ultIdMov = ctx.movimientos.Max(t => t.id);
                }
                catch { }
            }
            private int ultIdDocumentos()
            {
                int ultid_doc = 0;
                try
                {
                    ultid_doc = ctx.documentos.Max(t => t.id);
                }
                catch { }
                return ultid_doc;
            }
            #endregion
        }
        class cmdAnularLiquidacion : absTemplate
        {
            public string usu { get; set; }
            public int id_pago { get; set; }
            pagos pago { get; set; }
            protected internal override bool esValido()
            {
                pago = ctx.pagos.Where(t => t.id == id_pago).FirstOrDefault();
                if (pago != null)
                {
                    if (pago.estado == "LI")
                    {
                        pagos pag = ctx.pagos.Where(t => t.id_estudiante == pago.id_estudiante && t.estado != "AN").OrderByDescending(t => t.fecha).ThenByDescending(t=> t.id).FirstOrDefault();
                        if (pag.id == pago.id) return true;
                        else
                        {
                            byaRpt.Error = true;
                            byaRpt.Mensaje = "Solo se puede anular la última liqudiación realizada";
                            return false;
                        }
                    }
                    else
                    {
                        byaRpt.Error = true;
                        byaRpt.Mensaje = "La liquidación no esta en estado Liquidado";
                        return false;
                    }
                }
                else
                {
                    byaRpt.Error = true;
                    byaRpt.Mensaje = "El número de liquidación no puede ser encontrada";
                    return false;
                }
            }
            protected internal override void Antes()
            {
                pago.estado = "AN";

                foreach (detalles_pago item in pago.detalles_pago.Where(t=> t.tipo == "CA").ToList())
                {

                    fechas_calculo_intereses ult_fecha = item.carterap.fechas_calculo_intereses.Where(t=> t.estado == "LI").OrderByDescending(t => t.fecha).FirstOrDefault();
                    if(ult_fecha != null) ult_fecha.estado = "AN";
                }
                _cmpReg();
            }
            private void _cmpReg()
            {
                pago.usu_mod = usu;
                pago.fec_mod = DateTime.Now;
            }
        }
    }
}
