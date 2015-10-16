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
        [Route("")]
        public List<cDeudaGrados> GetDeudasGrados()
        {
            mDuedaGrados o = new mDuedaGrados();
            return o.GetDeudaGrados();
        }
        [Route("{id_grado}")]
        public List<cDeudaCursosGrado> GetDeudasCursosGrado(int id_grado)
        {
            mDuedaGrados o = new mDuedaGrados();
            return o.GetDeudaCursosGrado(id_grado);
        }
        [Route("Curso/{id_curso}")]
        public List<cDeudaEstudiantesCursoGrado> GetDeudasEstudiantesCursoGrado(int id_curso)
        {
            mDuedaGrados o = new mDuedaGrados();
            return o.GetDeudaEstudiantesCursoGrado(id_curso);
        }
        [Route("Estudiante/{id_estudiante}")]
        public List<detalles_pagoDto> GetDeudasEstudiantesCursoGrado(string id_estudiante)
        {
            mDuedaGrados o = new mDuedaGrados();
            return o.GetDeudaEstudiante(id_estudiante);
        }
    }
}