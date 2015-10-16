using Entidades.Consultas;
using Entidades.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class mEstadoCuenta
    {
        public cEstadoCuentaResumen GetEstadoCuentaResumido(string id_estudiante, int vigencia)
        {
            cEstadoCuentaResumen objEstadoCuenta = new cEstadoCuentaResumen();
            mCartera oCartera = new mCartera();
            mMatricula oMatriculas = new mMatricula();
            matriculasDto objMatricula = oMatriculas.Get(id_estudiante, vigencia);
            List<carterapDto> lCarteras = oCartera.GetCarteraEstudiante(id_estudiante, vigencia);

            objEstadoCuenta.id_matricula = objMatricula != null ? objMatricula.id_matricula : "";
            objEstadoCuenta.nombre_grado = objMatricula != null ? objMatricula.nombre_grado : "";
            objEstadoCuenta.nombre_curso = objMatricula != null ? objMatricula.nombre_curso : "";

            objEstadoCuenta.matricula.valor = (int) lCarteras.Where(t => t.id_concepto == 1).FirstOrDefault().valor;
            objEstadoCuenta.matricula.pagado = (int)lCarteras.Where(t => t.id_concepto == 1).FirstOrDefault().pagado;

            objEstadoCuenta.otros.valor = 0;
            objEstadoCuenta.otros.pagado = 0;
            objEstadoCuenta.otros.valor += (int)lCarteras.Where(t => t.id_concepto == 2).FirstOrDefault().valor;
            objEstadoCuenta.otros.pagado += (int)lCarteras.Where(t => t.id_concepto == 2).FirstOrDefault().pagado;
            objEstadoCuenta.otros.valor += (int)lCarteras.Where(t => t.id_concepto == 3).FirstOrDefault().valor;
            objEstadoCuenta.otros.pagado += (int)lCarteras.Where(t => t.id_concepto == 3).FirstOrDefault().pagado;
            objEstadoCuenta.otros.valor += (int)lCarteras.Where(t => t.id_concepto == 4).FirstOrDefault().valor;
            objEstadoCuenta.otros.pagado += (int)lCarteras.Where(t => t.id_concepto == 4).FirstOrDefault().pagado;

            objEstadoCuenta.pension2.valor = (int)lCarteras.Where(t => t.id_concepto == 5 && t.periodo == 2).FirstOrDefault().valor;
            objEstadoCuenta.pension2.pagado = (int)lCarteras.Where(t => t.id_concepto == 5 && t.periodo == 2).FirstOrDefault().pagado;

            objEstadoCuenta.pension3.valor = (int)lCarteras.Where(t => t.id_concepto == 5 && t.periodo == 3).FirstOrDefault().valor;
            objEstadoCuenta.pension3.pagado = (int)lCarteras.Where(t => t.id_concepto == 5 && t.periodo == 3).FirstOrDefault().pagado;

            objEstadoCuenta.pension4.valor = (int)lCarteras.Where(t => t.id_concepto == 5 && t.periodo == 4).FirstOrDefault().valor;
            objEstadoCuenta.pension4.pagado = (int)lCarteras.Where(t => t.id_concepto == 5 && t.periodo == 4).FirstOrDefault().pagado;

            objEstadoCuenta.pension5.valor = (int)lCarteras.Where(t => t.id_concepto == 5 && t.periodo == 5).FirstOrDefault().valor;
            objEstadoCuenta.pension5.pagado = (int)lCarteras.Where(t => t.id_concepto == 5 && t.periodo == 5).FirstOrDefault().pagado;

            objEstadoCuenta.pension6.valor = (int)lCarteras.Where(t => t.id_concepto == 5 && t.periodo == 6).FirstOrDefault().valor;
            objEstadoCuenta.pension6.pagado = (int)lCarteras.Where(t => t.id_concepto == 5 && t.periodo == 6).FirstOrDefault().pagado;

            objEstadoCuenta.pension7.valor = (int)lCarteras.Where(t => t.id_concepto == 5 && t.periodo == 7).FirstOrDefault().valor;
            objEstadoCuenta.pension7.pagado = (int)lCarteras.Where(t => t.id_concepto == 5 && t.periodo == 7).FirstOrDefault().pagado;

            objEstadoCuenta.pension8.valor = (int)lCarteras.Where(t => t.id_concepto == 5 && t.periodo == 8).FirstOrDefault().valor;
            objEstadoCuenta.pension8.pagado = (int)lCarteras.Where(t => t.id_concepto == 5 && t.periodo == 8).FirstOrDefault().pagado;

            objEstadoCuenta.pension9.valor = (int)lCarteras.Where(t => t.id_concepto == 5 && t.periodo == 9).FirstOrDefault().valor;
            objEstadoCuenta.pension9.pagado = (int)lCarteras.Where(t => t.id_concepto == 5 && t.periodo == 9).FirstOrDefault().pagado;

            objEstadoCuenta.pension10.valor = (int)lCarteras.Where(t => t.id_concepto == 5 && t.periodo == 10).FirstOrDefault().valor;
            objEstadoCuenta.pension10.pagado = (int)lCarteras.Where(t => t.id_concepto == 5 && t.periodo == 10).FirstOrDefault().pagado;

            objEstadoCuenta.pension11.valor = (int)lCarteras.Where(t => t.id_concepto == 5 && t.periodo == 11).FirstOrDefault().valor;
            objEstadoCuenta.pension11.pagado = (int)lCarteras.Where(t => t.id_concepto == 5 && t.periodo == 11).FirstOrDefault().pagado;

            return objEstadoCuenta;
        }
    }
}
