using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades.Consultas;
using System.Threading.Tasks;

namespace BLL
{
    public class mConsultaCartera
    {
        public List<cCarteraEstudianteConcepto> GetCarteraxConcepto()
        {
            //mCausacion.Causar("");
            List<CarteraxConceptoDto> lista;
            using (ieEntities db = new ieEntities())
            {

                DateTime FechaCausacion = mCausacion.FechaCausacion();
                int VigPerAct = int.Parse(FechaCausacion.Year.ToString() + FechaCausacion.Month.ToString().PadLeft(2, '0'));

                lista = db.carterap.
                    Where(t => (t.estado == "PR" || t.estado == "CA") && (t.vigencia * 100 + t.periodo) <= VigPerAct && t.pagado < t.valor)
                        .GroupBy(t => new { t.estudiantes, t.vigencias, t.conceptos })
                        .Select(
                                x => new CarteraxConceptoDto
                                {
                                    NombreEstudiante = x.Key.estudiantes.terceros.nombre + " " + x.Key.estudiantes.terceros.apellido,
                                    NombreConcepto = x.Key.conceptos.nombre,
                                    id_estudiante = x.Key.estudiantes.terceros.identificacion,
                                    id_concepto = x.Key.conceptos.id,
                                    Vigencia = x.Key.vigencias.vigencia,
                                    Valor = x.Sum(t => t.valor),
                                    Pagado = x.Sum(t => t.pagado),
                                    Saldo = x.Sum(t => (t.valor - t.pagado)),
                                    Cantidad = x.Count()
                                }
                        ).ToList();
            }
            List<cCarteraEstudianteConcepto> lr = new List<cCarteraEstudianteConcepto>();
            foreach (CarteraxConceptoDto item in lista)
            {
                if (lr.Where(t => t.id_estudiante == item.id_estudiante).FirstOrDefault() == null)
                {
                    cCarteraEstudianteConcepto o = new cCarteraEstudianteConcepto();
                    o.id_estudiante = item.id_estudiante;
                    o.nombre_estudiante = item.NombreEstudiante;
                    o.total_deuda = 0;

                    // Matricula
                    if (lista.Where(t => t.id_concepto == 1 && t.id_estudiante == o.id_estudiante).FirstOrDefault() != null)
                    {
                        o.per_matricula = lista.Where(t => t.id_concepto == 1 && t.id_estudiante == o.id_estudiante).FirstOrDefault().Cantidad;
                        o.valor_matricula = lista.Where(t => t.id_concepto == 1 && t.id_estudiante == o.id_estudiante).FirstOrDefault().Saldo;
                    }
                    else
                    {
                        o.per_matricula = 0;
                        o.valor_matricula = 0;
                    }                    

                    // Proyectos pedagogicos
                    if (lista.Where(t => t.id_concepto == 2 && t.id_estudiante == o.id_estudiante).FirstOrDefault() != null)
                    {
                        o.per_propeda = lista.Where(t => t.id_concepto == 2 && t.id_estudiante == o.id_estudiante).FirstOrDefault().Cantidad;
                        o.valor_propeda = lista.Where(t => t.id_concepto == 2 && t.id_estudiante == o.id_estudiante).FirstOrDefault().Saldo;
                    }
                    else
                    {
                        o.per_propeda = 0;
                        o.valor_propeda = 0;
                    }

                    // Sistematizacion
                    if (lista.Where(t => t.id_concepto == 3 && t.id_estudiante == o.id_estudiante).FirstOrDefault() != null)
                    {
                        o.per_sistematizacion = lista.Where(t => t.id_concepto == 3 && t.id_estudiante == o.id_estudiante).FirstOrDefault().Cantidad;
                        o.valor_sistematizacion = lista.Where(t => t.id_concepto == 3 && t.id_estudiante == o.id_estudiante).FirstOrDefault().Saldo;
                    }
                    else
                    {
                        o.per_sistematizacion = 0;
                        o.valor_sistematizacion = 0;
                    }

                    // Seguro Estudiantil
                    if (lista.Where(t => t.id_concepto == 4 && t.id_estudiante == o.id_estudiante).FirstOrDefault() != null)
                    {
                        o.per_seguro = lista.Where(t => t.id_concepto == 4 && t.id_estudiante == o.id_estudiante).FirstOrDefault().Cantidad;
                        o.valor_seguro = lista.Where(t => t.id_concepto == 4 && t.id_estudiante == o.id_estudiante).FirstOrDefault().Saldo;
                    }
                    else
                    {
                        o.per_seguro = 0;
                        o.valor_seguro = 0;
                    }

                    // Pension
                    if (lista.Where(t => t.id_concepto == 5 && t.id_estudiante == o.id_estudiante).FirstOrDefault() != null)
                    {
                        o.per_pension = lista.Where(t => t.id_concepto == 5 && t.id_estudiante == o.id_estudiante).FirstOrDefault().Cantidad;
                        o.valor_pension = lista.Where(t => t.id_concepto == 5 && t.id_estudiante == o.id_estudiante).FirstOrDefault().Saldo;
                    }
                    else
                    {
                        o.per_pension = 0;
                        o.valor_pension = 0;
                    }

                    // Cartera venciada
                    if (lista.Where(t => t.id_concepto == 7 && t.id_estudiante == o.id_estudiante).FirstOrDefault() != null)
                    {
                        o.per_carteravencida = lista.Where(t => t.id_concepto == 7 && t.id_estudiante == o.id_estudiante).FirstOrDefault().Cantidad;
                        o.valor_carteravencida = lista.Where(t => t.id_concepto == 7 && t.id_estudiante == o.id_estudiante).FirstOrDefault().Saldo;
                    }
                    else
                    {
                        o.per_carteravencida = 0;
                        o.valor_carteravencida = 0;
                    }

                    o.total_deuda += o.valor_matricula;
                    o.total_deuda += o.valor_carteravencida;
                    o.total_deuda += o.valor_pension;
                    o.total_deuda += o.valor_propeda;
                    o.total_deuda += o.valor_sistematizacion;
                    o.total_deuda += o.valor_seguro;

                    lr.Add(o);
                }
            }
            return lr.OrderByDescending(t => t.total_deuda).ToList();
        }
    }
}
