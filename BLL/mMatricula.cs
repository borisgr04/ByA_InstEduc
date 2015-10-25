using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades.Vistas;
using DAL;
using ByA;
using AutoMapper;

namespace BLL
{
    public class mMatricula
    {
        ieEntities ctx;
        public mMatricula()
        {
            Mapper.CreateMap<matriculasDto, matriculas>();
            Mapper.CreateMap<matriculas, matriculasDto>()
                .ForMember(dest => dest.nombre_estudiante, obj => obj.MapFrom(src => src.estudiantes.terceros.apellido + " " + src.estudiantes.terceros.nombre))
                .ForMember(dest => dest.nombre_curso, obj => obj.MapFrom(src => src.cursos.nombre))
                .ForMember(dest => dest.codigo_estudiante, obj => obj.MapFrom(src => src.estudiantes.codigo))
                .ForMember(dest => dest.nombre_grado, obj => obj.MapFrom(src => src.cursos.grados.nombre));
        }
        public List<matriculasDto> Gets(ConsultaMatriculasDto objCon)
        {
            using (ctx = new ieEntities())
            {
                List<matriculasDto> lr = new List<matriculasDto>();
                List<matriculas> l;
                if(objCon.Curso == null) l = ctx.matriculas.Where(t => t.vigencia == objCon.Vigencia && t.estado != "AN" && (t.id_matricula.ToUpper().Contains(objCon.Filtro.ToUpper()) || t.estudiantes.terceros.nombre.ToUpper().Contains(objCon.Filtro) || t.estudiantes.terceros.apellido.ToUpper().Contains(objCon.Filtro))).ToList();
                else l = ctx.matriculas.Where(t => t.vigencia == objCon.Vigencia && t.estado != "AN" && (t.id_matricula.ToUpper().Contains(objCon.Filtro.ToUpper()) || t.estudiantes.terceros.nombre.ToUpper().Contains(objCon.Filtro) || t.estudiantes.terceros.apellido.ToUpper().Contains(objCon.Filtro)) && t.id_curso == objCon.Curso).ToList();

                Mapper.Map(l,lr);

                return lr;
            }
        }
        public matriculasDto Get(string id_estudiante, int vigencia)
        {
            using (ctx = new ieEntities())
            {
                matriculasDto r = new matriculasDto();
                matriculas o = ctx.matriculas.Where(t => t.id_estudiante == id_estudiante && t.vigencia == vigencia && t.estado == "AC").FirstOrDefault();
                if (o != null) Mapper.Map(o, r);
                else r = null;
                return r;
            }
        }
        public ByARpt MatricularEstudiante(matriculasDto matricula)
        {
            ByARpt res = new ByARpt();
            cmdInsert o = new cmdInsert();
            o.oDto = matricula;
            res = o.Enviar();
            if (!res.Error) mCausacion.Causar(matricula.id_estudiante, int.Parse(res.id));
            return res;
        }
        public ByARpt RetirarEstudiante(string id_estudiante)
        {
            mCausacion.Causar(id_estudiante);
            cmdRetirarEstudiante o = new cmdRetirarEstudiante();
            o.id_estudiante = id_estudiante;
            return o.Enviar();
        }
        public ByARpt AnularMatricula(int id_matricula)
        {
            cmdAnularMatricula o = new cmdAnularMatricula();
            o.id_matricula = id_matricula;
            return o.Enviar();
        }
        class cmdInsert : absTemplate
        {
            public matriculasDto oDto { get; set; }
            matriculas Dto { get; set; }
            estudiantes estudiante;
            vigencias vigencia;
            cursos curso;
            matriculas matricula3;
            matriculas matricula2;
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
                        curso = ctx.cursos.Where(t => t.id == oDto.id_curso).FirstOrDefault();
                        if (curso != null)
                        {
                            matricula = ctx.matriculas.Where(t => t.id_estudiante == oDto.id_estudiante && t.vigencia == oDto.vigencia && t.estado == "AC").FirstOrDefault();
                            if (matricula == null)
                            {
                                matricula2 = ctx.matriculas.Where(t => t.id_estudiante == oDto.id_estudiante && t.vigencia > oDto.vigencia && t.id_grado <= curso.id_grado && t.estado == "AC").FirstOrDefault();
                                if (matricula2 == null)
                                {
                                    matricula3 = ctx.matriculas.Where(t => t.id_estudiante == oDto.id_estudiante && t.vigencia < oDto.vigencia && t.id_grado > curso.id_grado && t.estado == "AC").FirstOrDefault();
                                    if (matricula3 == null) return true;
                                    else
                                    {
                                        byaRpt.Error = true;
                                        byaRpt.Mensaje = "El estudiante ya tiene una matrícula de un grado superior en una vigencia inferior";
                                        return false;
                                    }
                                }
                                else
                                {
                                    byaRpt.Error = true;
                                    byaRpt.Mensaje = "El estudiante ya tiene una matrícula de un grado inferior en una vigencia superior";
                                    return false;
                                }
                            }
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
                            byaRpt.Mensaje = "No ha indicado un curso valido";
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
                Dto = new matriculas();
                int ultId = 0;
                try
                {
                    ultId = ctx.matriculas.Max(t => t.id);
                }
                catch { }
                ultId++;
                oDto.id = ultId;
                oDto.id_est = estudiante.id;
                oDto.id_grado = (int)curso.id_grado;
                oDto.estado = "AC";
                oDto.folio = GetFolio();
                oDto.id_matricula = CalcularConsecutivoMatricula();
                Mapper.Map(oDto, Dto);

                _cmpReg();

                ctx.matriculas.Add(Dto);

                AsignarGradoEstudiante();

                ArmarCartera();
            }

            private void _cmpReg()
            {
                Dto.fec_reg = DateTime.Now;
                Dto.fec_mod = DateTime.Now;
                Dto.usu_mod = oDto.usu;
                Dto.usu_reg = oDto.usu;
            }
            protected override void Despues()
            {
                byaRpt.Mensaje = "Operación Realizada Satisfactoriamente";
                byaRpt.id = Dto.id.ToString();
            }
            private void AsignarGradoEstudiante()
            {
                matriculas matriculaMax = ctx.matriculas.Where(t => t.id_est == estudiante.id && t.estado == "AC").OrderByDescending(t => t.vigencia).FirstOrDefault();
                if ((matriculaMax == null) || (matriculaMax.vigencia < Dto.vigencia))
                {
                    estudiante.id_ultima_matricula = Dto.id;
                    estudiante.id_ultimo_grado = curso.id_grado;
                }
            }
            private string GetFolio()
            {
                int ult_folio = 0;
                try
                {
                    matriculas Matri = ctx.matriculas.OrderByDescending(t=> t.id).FirstOrDefault();
                    ult_folio = int.Parse(Matri.folio);
                }
                catch { }
                string FolioProximo = (ult_folio + 1).ToString().PadLeft(8,'0');
                return FolioProximo;
            }
            private string CalcularConsecutivoMatricula()
            {
                string vigencia = oDto.vigencia.ToString();
                int ultCons = 0;
                try
                {
                    matriculas Matri = ctx.matriculas.Where(t => t.vigencia == oDto.vigencia).OrderByDescending(t => t.id).FirstOrDefault();
                    string consUltimo = Matri.id_matricula;
                    ultCons = int.Parse(consUltimo.Substring(4, 4));
                }
                catch { }
                string ConsecutivoProximo = vigencia + (ultCons + 1).ToString().PadLeft(4, '0');
                return ConsecutivoProximo;
            }
            private void ArmarCartera()
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

                foreach (carteraDto item in oDto.lCartera)
                {
                    for (int i = (int)item.periodo_desde; i <= item.periodo_hasta; i++)
                    {
                        ultId++;
                        carterap itemCartera = new carterap();
                        itemCartera.id = ultId;
                        itemCartera.vigencia = (int)oDto.vigencia;
                        itemCartera.id_concepto = (int)item.id_concepto;
                        itemCartera.periodo = i;
                        itemCartera.valor = (int)item.valor;
                        itemCartera.id_matricula = oDto.id;
                        itemCartera.id_estudiante = oDto.id_estudiante;
                        itemCartera.pagado = 0;
                        itemCartera.id_est = estudiante.id;
                        itemCartera.id_grupo = (int)ctx.config_grupos_pagos.Where(t => t.id_concepto == itemCartera.id_concepto && t.vigencia == itemCartera.vigencia).FirstOrDefault().id_grupo;
                        itemCartera.estado = "PR";
                        ctx.carterap.Add(itemCartera);
                        InicializarCalculoIntereses(itemCartera.id, itemCartera.periodo, itemCartera.vigencia);
                    }
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
        class cmdAnularMatricula : absTemplate
        {
            public int id_matricula { get; set; }
            matriculas Dto { get; set; }
            int ultid_documentos = 0;
            int ultid_movimientos = 0;
            documentos doc { get; set; }
            #region ImplementaciónMetodosAbstractos
            protected internal override bool esValido()
            {
                Dto = ctx.matriculas.Where(t => t.id == id_matricula && t.estado == "AC").FirstOrDefault();
                if (Dto != null)
                {
                    bool error = false;
                    error = _VerificarSiPagos(error);
                    if (!error) return true;
                    else
                    {
                        byaRpt.Error = true;
                        byaRpt.Mensaje = "La matrícula que desea cancelar ya tiene registrados pagos o liquidaciones, por lo cual no puede realizar la operación.";
                        return false;
                    }
                }
                else
                {
                    byaRpt.Error = true;
                    byaRpt.Mensaje = "No existe una matricula con esta identificación";
                    return false;
                }
            }
            private bool _VerificarSiPagos(bool error)
            {
                List<carterap> carteras = Dto.carterap.ToList();
                foreach (carterap item in carteras)
                {
                    List<detalles_pago> detalles = item.detalles_pago.ToList();
                    foreach (detalles_pago item2 in detalles)
                    {
                        if (item2.pagos.estado != "AN")
                        {
                            error = true;
                        }
                    }
                }
                return error;
            }
            protected internal override void Antes()
            {
                UltIdDocumentos();
                UltIdMovimientos();
                Dto.estado = "AN";

                ultid_documentos++;
                doc = new documentos();
                doc.id = ultid_documentos;
                doc.fecha = DateTime.Now;
                doc.tipo_documento = "ANMAT";
                doc.descripcion = "Se genero por la anulación de la matricula No. " + Dto.id;
                ctx.documentos.Add(doc);
                AnularCarteras();
                BorrarIdMatriculaEstudiante();
            }
            private void BorrarIdMatriculaEstudiante()
            {
                estudiantes estudiante = ctx.estudiantes.Where(t => t.id == Dto.id_est).FirstOrDefault();
                if ((estudiante != null) && (estudiante.id_ultima_matricula == Dto.id)) 
                {
                    matriculas matricula = estudiante.matriculas.Where(t=> t.estado == "AC").OrderByDescending(t => t.vigencia).FirstOrDefault();
                    if (matricula != null)
                    {
                        estudiante.id_ultima_matricula = matricula.id;
                        estudiante.id_ultimo_grado = matricula.id_grado;
                    }
                    else
                    {
                        estudiante.id_ultima_matricula = null;
                        estudiante.id_ultimo_grado = null;
                    }
                }
            }
            private void AnularCarteras()
            {
                foreach (carterap item in Dto.carterap.ToList())
                {
                    item.estado = "AN";
                    foreach (movimientos item2 in item.movimientos.ToList())
                    {
                        ultid_movimientos++;
                        movimientos movCA = new movimientos();
                        movCA.id = ultid_movimientos;
                        movCA.id_estudiante = item.id_estudiante;
                        movCA.vigencia = item.vigencia;
                        movCA.periodo = item.periodo;
                        movCA.id_cartera = item.id;
                        movCA.id_concepto = item.id_concepto;
                        movCA.valor_debito = 0;
                        movCA.valor_credito = item.valor;
                        movCA.fecha_movimiento = DateTime.Now;
                        movCA.estado = "AC";
                        movCA.fecha_novedad = DateTime.Now;
                        movCA.fecha_registro = DateTime.Now;
                        movCA.tipo_documento = doc.tipo_documento;
                        movCA.numero_documento = doc.id;
                        movCA.id_est = item.id_est;
                        ctx.movimientos.Add(movCA);
                    }
                }
            }
            private void UltIdDocumentos()
            {
                try
                {
                    ultid_documentos = ctx.documentos.Max(t => t.id);
                }
                catch { }
            }
            private void UltIdMovimientos()
            {
                try
                {
                    ultid_movimientos = ctx.movimientos.Max(t => t.id);
                }
                catch { }
            }
            #endregion
        }
        class cmdRetirarEstudiante : absTemplate
        {
            public string id_estudiante { get; set; }
            estudiantes estudiante { get; set; }
            List<carterap> lCarteras { get; set; }
            float valorDevolucion = 0;
            int ultid_movimientos = 0;
            int ultid_devoluciones = 0;
            #region ImplementaciónMetodosAbstractos
            protected internal override bool esValido()
            {
                estudiante = ctx.estudiantes.Where(t => t.identificacion == id_estudiante).FirstOrDefault();
                if (estudiante != null) return true;
                else
                {
                    byaRpt.Error = true;
                    byaRpt.Mensaje = "El estudiante no esta registrado en el sistema";
                    return false;
                }
            }
            protected internal override void Antes()
            {
                UltIdMovimientos();
                UltIdDevoluciones();

                ultid_devoluciones++;
                lCarteras = estudiante.carterap.Where(t => t.estado == "PR").ToList();
                foreach (carterap item in lCarteras)
                {
                    item.estado = "RT";
                    if (item.pagado > 0)
                    {
                        valorDevolucion += (int)item.pagado;
                        ultid_movimientos++;
                        movimientos movCA = new movimientos();
                        movCA.id = ultid_movimientos;
                        movCA.id_estudiante = item.id_estudiante;
                        movCA.vigencia = item.vigencia;
                        movCA.periodo = item.periodo;
                        movCA.id_cartera = item.id;
                        movCA.id_concepto = item.id_concepto;
                        movCA.valor_debito = item.valor;
                        movCA.valor_credito = 0;
                        movCA.fecha_movimiento = DateTime.Now;
                        movCA.estado = "AC";
                        movCA.fecha_novedad = DateTime.Now;
                        movCA.fecha_registro = DateTime.Now;
                        movCA.tipo_documento = "DEVOL";
                        movCA.numero_documento = ultid_devoluciones;
                        movCA.id_est = item.id_est;
                        ctx.movimientos.Add(movCA);
                    }
                }

                if (valorDevolucion > 0)
                {
                    saldos_a_favor dev = new saldos_a_favor();
                    dev.id = ultid_devoluciones;
                    dev.valor = valorDevolucion;
                    dev.id_est = estudiante.id;
                    dev.id_estudiante = estudiante.identificacion;
                    dev.fecha = DateTime.Now;
                    dev.estado = "AC";
                    ctx.saldos_a_favor.Add(dev);
                }
            }
            protected override void Despues()
            {
                byaRpt.Mensaje = "Operación Realizada Satisfactoriamente, el estudiante fue retirado";

            }
            private void UltIdMovimientos()
            {
                try
                {
                    ultid_movimientos = ctx.movimientos.Max(t => t.id);
                }
                catch { }
            }
            private void UltIdDevoluciones()
            {
                try
                {
                    ultid_devoluciones = ctx.saldos_a_favor.Max(t => t.id);
                }
                catch { }
            }
            #endregion
        }
    }
}
