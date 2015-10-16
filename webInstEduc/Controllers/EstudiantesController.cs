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

namespace Skeleton.WebAPI.Controllers
{
    [RoutePrefix("api/Estudiantes")]
    public class EstudiantesController : ApiController
    {
        [Route("{Id}")]
        public estudiantesDto Get(string Id)
        {
            mEstudiantes o = new mEstudiantes();
            return o.Get(Id);
        }
        [Route("Id/{Id}")]
        public estudiantesDto GetXId(int Id)
        {
            mEstudiantes o = new mEstudiantes();
            return o.GetXId(Id);
        }
        [Route("Orden/{Orden}")]
        public cEstudiantesOrden GetsOrden(int Orden)
        {
            mEstudiantes o = new mEstudiantes();
            return o.GetsOrden(Orden);
        }
        [Route("Orden/{Orden}/{Filtro}")]
        public cEstudiantesOrden GetsOrden(int Orden, string Filtro)
        {
            mEstudiantes o = new mEstudiantes();
            return o.GetsOrden(Orden, Filtro);
        }
        [Route("GrupoPagar/{id_estudiante}")]
        public int GetGrupoPagar(string id_estudiante)
        {
            mEstudiantes o = new mEstudiantes();
            return o.GetGrupoPago(id_estudiante);
        }
        [Route("Filtro/{Filtro}")]
        public List<estudiantesDto> GetsFiltro(string Filtro)
        {
            mEstudiantes o = new mEstudiantes();
            return o.GetsFiltro(Filtro);
        }
        [Route("")]
        public ByARpt Post(estudiantesDto Reg)
        {
            mEstudiantes o = new mEstudiantes();
            return o.Insert(Reg);
        }
        [Route("")]
        public ByARpt Put(estudiantesDto Reg)
        {
            mEstudiantes o = new mEstudiantes();
            return o.Update(Reg);
        }

    }
}
