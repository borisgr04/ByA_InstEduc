using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ByA;
using Entidades.Vistas;
using BLL;

namespace Skeleton.WebAPI.Controllers
{
    [RoutePrefix("api/Matriculas")]
    public class MatriculasController : ApiController
    {
        [Route("Vigencia/{vigencia}/Estudiante/{id_estudiante}")]
        public matriculasDto Get(int vigencia, string id_estudiante)
        {
            mMatricula o = new mMatricula();
            return o.Get(id_estudiante, vigencia);
        }
        [Route("Vigencia/Grado/Curso")]
        public List<matriculasDto> PostObtenerMatriculas(ConsultaMatriculasDto Reg)
        {
            mMatricula o = new mMatricula();
            return o.Gets(Reg);
        }
        [Route("")]
        public ByARpt Post(matriculasDto m)
        {
            mMatricula o = new mMatricula();
            return o.MatricularEstudiante(m);
        }
        [Route("Anular/{id_matricula}")]
        public ByARpt PostAnular(int id_matricula)
        {
            mMatricula o = new mMatricula();
            return o.AnularMatricula(id_matricula);
        }
        [Route("Retirar/Estudiante/{id_estudiante}")]
        public ByARpt PostRetirar(string id_estudiante)
        {
            mMatricula o = new mMatricula();
            return o.RetirarEstudiante(id_estudiante);
        }
    }
}
