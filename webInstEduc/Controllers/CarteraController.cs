using BLL;
using Entidades.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ByA;
using Entidades.Consultas;
using AspIdentity.Models;
using System.Security.Claims;
using System.Web;
using System.Net.Http.Headers;

namespace Skeleton.WebAPI.Controllers
{
    [RoutePrefix("api/Cartera")]
    public class CarteraController : ApiController
    {
        [Route("Visualizacion/Grado/{grado}/Vigencia/{vigencia}/VigenciaActual/{vigenciaActal}/PeriodoActual/{periodoActual}")]
        public List<carteraDto> GetVisualizacionCarteraAntes(int grado, int vigencia, int vigenciaActal, int periodoActual)
        {
            mCartera o = new mCartera();
            return o.GetVisualizacionCarteraAntes(grado, vigencia, vigenciaActal, periodoActual);
        }
        [Route("EstadoCuenta/Estudiante/{id_estudiante}")]
        public cEstadoCuentaEstudiante GetEstadoCuenta(string id_estudiante)
        {
            mCartera o = new mCartera();
            return o.GetEstadoCuentaEstudiante(id_estudiante);
        }
        [Route("{id_estudiante}/vigencia/{vigencia}")]
        public List<carterapDto> GetCarteraEstudiante(string id_estudiante, int vigencia)
        {
            mCartera o = new mCartera();
            return o.GetCarteraEstudiante(id_estudiante, vigencia);
        }
        [Route("Conceptos")]
        public List<cCarteraEstudianteConcepto> GetCarteraConceptos()
        {
            mConsultaCartera o = new mConsultaCartera();
            return o.GetCarteraxConcepto();
        }
        [Route("")]
        public ByARpt PostCarteraEstudiante(List<carterapDto> lReg)
        {
            mCartera o = new mCartera();
            return o.UpdateCarteraEstudiante(lReg);
        }

        // las dos apis siguientes son las de el registrar liquidacion que se esta haciendo de manera provisional

        [Route("Causado/Estudiante/Liquidacion")]
        public List<detalles_pagoDto> PostObtenerDeudaEstudianteL(bDeudaEstudianteFecha reg)
        {            
            mCartera o = new mCartera();
            return o.GetDeudaEstudianteL(reg);
        }

        [Route("Causado/Estudiante/Valor/Liquidacion")]
        public List<detalles_pagoDto> PostObtenerDeudaEstudianteValorL(bDeudaEstudianteFecha reg)
        {
            mCartera o = new mCartera();
            return o.GetDeudaEstudianteValorL(reg);
        }
    }
}
