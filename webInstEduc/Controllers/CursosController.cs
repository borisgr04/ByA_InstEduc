using BLL;
using ByA;
using Entidades.Vistas;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Skeleton.WebAPI.Controllers
{
    [RoutePrefix("api/Cursos")]
    public class CursosController : ApiController
    {
        [Route("Grado/{id_grado}")]
        public List<cursosDto> GetCursosGrado(int id_grado)
        {
            mCursos o = new mCursos();
            return o.GetCursosGrado(id_grado);
        }
        [Route("")]
        public List<ByARpt> Post(List<cursosDto> lReg)
        {
            mCursos o = new mCursos();
            return o.InsertsOrUpdates(lReg);
        }
    }
}
