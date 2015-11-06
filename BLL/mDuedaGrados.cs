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
    public class mDuedaGrados
    {
        ieEntities ctx;
        public List<cDeudaGrados> GetDeudaGrados(int vigencia)
        {
            //mCausacion.Causar("");
            using (ctx = new ieEntities())
            {
                List<cDeudaGrados> lDeuda = new List<cDeudaGrados>();
                List<grados> lGrados = ctx.grados.OrderBy(t => t.id).ToList();
                foreach (grados item in lGrados)
                {
                    cDeudaGrados oDeuda = new cDeudaGrados();
                    oDeuda.id_grado = item.id;
                    oDeuda.nombre_grado = item.nombre;
                    oDeuda.valor_deuda = 0;

                    List<matriculas> lMatriculas = ctx.matriculas.Where(t => t.vigencia == vigencia && t.id_grado == item.id && t.estado == "AC").ToList();
                    List<estudiantes> lEstudiantesGrados = new List<estudiantes>();
                    lMatriculas.ForEach(t => lEstudiantesGrados.Add(t.estudiantes));

                    foreach (estudiantes item2 in lEstudiantesGrados)
                    {
                        oDeuda.valor_deuda += GetDeudaTotalEstudiante(ctx, item2.identificacion);
                    }
                    lDeuda.Add(oDeuda);
                }
                return lDeuda.OrderByDescending(t => t.valor_deuda).ToList();
            }
        }
        private int GetDeudaTotalEstudiante(ieEntities ctx, string id_estudiante)
        {
            int ValorDeuda = 0;
            DateTime FechaCausacion = mCausacion.FechaCausacion();
            int VigPerAct = int.Parse(FechaCausacion.Year.ToString() + FechaCausacion.Month.ToString().PadLeft(2, '0'));
            List<detalles_pagoDto> lDeuda = new List<detalles_pagoDto>();
            List<carterap> lCartera = ctx.carterap.Where(t => t.id_estudiante == id_estudiante && (t.estado == "PR" || t.estado == "CA") && (t.vigencia * 100 + t.periodo) <= VigPerAct && t.pagado < t.valor).OrderBy(t => t.vigencia).ThenBy(t => t.periodo).ThenBy(t => t.id_concepto).ToList();
            foreach (carterap item in lCartera)
            {
                int PagoCapital = (int)(item.valor - item.pagado);
                ValorDeuda += PagoCapital;
                config_grupos_pagos config = ctx.config_grupos_pagos.Where(t => t.id_concepto == item.id_concepto && t.vigencia == item.vigencia).FirstOrDefault();
                if ((config != null) && (config.intereses == "SI"))
                {
                    periodos periodo = ctx.periodos.Where(t => t.periodo == item.periodo && t.vigencia == item.vigencia).FirstOrDefault();
                    DateTime FechaVencimientoPeriodo = new DateTime((int)periodo.vigencia, (int)periodo.periodo, (int)periodo.vence_dia);
                    if (FechaCausacion > FechaVencimientoPeriodo)
                    {
                        mIntereses oTI = new mIntereses();
                        DateTime FechaUltimoCalculoIntereses = item.fechas_calculo_intereses.Where(t => t.estado == "PA").OrderByDescending(t => t.fecha).FirstOrDefault().fecha;
                        int ValorIntereses = oTI.GetValorIntereses(FechaUltimoCalculoIntereses, FechaCausacion, PagoCapital,item.vigencia,item.periodo,item.id);
                        if (ValorIntereses > 0)
                        {
                            ValorDeuda += ValorIntereses;
                        }
                    }
                }
            }
            return ValorDeuda;
        }
        public List<cDeudaCursosGrado> GetDeudaCursosGrado(int id_grado, int vigencia)
        {
            using (ctx = new ieEntities())
            {
                List<cDeudaCursosGrado> lDeuda = new List<cDeudaCursosGrado>();
                grados grado = ctx.grados.Where(t => t.id == id_grado).FirstOrDefault();
                if (grado != null)
                {
                    List<cursos> cursos = grado.cursos.ToList();
                    foreach (cursos item in cursos)
                    {
                        cDeudaCursosGrado oDeuda = new cDeudaCursosGrado();
                        oDeuda.id_curso = item.id;
                        oDeuda.nombre_curso = item.nombre;
                        oDeuda.id_grado = item.id_grado;
                        oDeuda.nombre_grado = item.grados.nombre;
                        oDeuda.valor_deuda = 0;

                        List<matriculas> lMatriculas = item.matriculas.Where(t => t.estado == "AC" && t.vigencia == vigencia && t.id_curso == item.id).ToList();

                        foreach (matriculas item2 in lMatriculas)
                        {
                            oDeuda.valor_deuda += GetDeudaTotalEstudiante(ctx, item2.estudiantes.identificacion);                       
                        }
                        lDeuda.Add(oDeuda);
                    }
                }
                return lDeuda.OrderByDescending(t => t.valor_deuda).ToList();
            }
        }
        public List<cDeudaEstudiantesCursoGrado> GetDeudaEstudiantesCursoGrado(int id_curso, int vigencia)
        {
            using (ctx = new ieEntities())
            {
                List<cDeudaEstudiantesCursoGrado> lDeuda = new List<cDeudaEstudiantesCursoGrado>();
                cursos curso = ctx.cursos.Where(t => t.id == id_curso).FirstOrDefault();
                if (curso != null)
                {
                    List<matriculas> lMatriculas = curso.matriculas.Where(t => t.estado == "AC" && t.id_curso == id_curso && t.vigencia == vigencia).ToList();
                    foreach (matriculas item2 in lMatriculas)
                    {
                        cDeudaEstudiantesCursoGrado oDeuda = new cDeudaEstudiantesCursoGrado();
                        oDeuda.id_curso = curso.id;
                        oDeuda.nombre_curso = curso.nombre;
                        oDeuda.id_grado = curso.grados.id;
                        oDeuda.nombre_grado = curso.grados.nombre;
                        oDeuda.id_est = item2.id_est;
                        oDeuda.id_estudiante = item2.id_estudiante;
                        oDeuda.nombre_estudiante = item2.estudiantes.terceros.nombre + " " + item2.estudiantes.terceros.apellido;
                        oDeuda.valor_deuda += GetDeudaTotalEstudiante(ctx, item2.estudiantes.identificacion);
                        lDeuda.Add(oDeuda);
                    }
                }
                return lDeuda.OrderByDescending(t => t.valor_deuda).ToList();
            }
        }
        public List<detalles_pagoDto> GetDeudaEstudiante(string id_estudiante)
        {
            bDeudaEstudianteFecha reg = new bDeudaEstudianteFecha();
            DateTime FechaCausacion = mCausacion.FechaCausacion();
            reg.fecha = FechaCausacion;
            reg.id_estudiante = id_estudiante;
            reg.id_grupo = null;

            mCartera oCartera = new mCartera();
            List<detalles_pagoDto> lDetallesPago = oCartera.GetDeudaEstudianteL(reg);
            return lDetallesPago;
        }
    }
}
