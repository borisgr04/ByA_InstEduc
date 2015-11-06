using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;
using Entidades.Consultas;
using Entidades.Vistas;

namespace AspIdentity.Controllers
{
    [RoutePrefix("api/DeudaGrados")]
    public class DeudaGradosController : ApiController
    {
        [Route("{vigencia}")]
        public List<cDeudaGrados> GetDeudasGrados(int vigencia)
        {
            mDuedaGrados o = new mDuedaGrados();
            return o.GetDeudaGrados(vigencia);
        }
        [Route("{id_grado}/{vigencia}")]
        public List<cDeudaCursosGrado> GetDeudasCursosGrado(int id_grado, int vigencia)
        {
            mDuedaGrados o = new mDuedaGrados();
            return o.GetDeudaCursosGrado(id_grado, vigencia);
        }
        [Route("Curso/{id_curso}/{vigencia}")]
        public List<cDeudaEstudiantesCursoGrado> GetDeudasEstudiantesCursoGrado(int id_curso, int vigencia)
        {
            mDuedaGrados o = new mDuedaGrados();
            return o.GetDeudaEstudiantesCursoGrado(id_curso,vigencia);
        }
        [Route("Estudiante/{id_estudiante}")]
        public List<detalles_pagoDto> GetDeudasEstudiantesCursoGrado(string id_estudiante)
        {
            mDuedaGrados o = new mDuedaGrados();
            return o.GetDeudaEstudiante(id_estudiante);
        }
    }
}