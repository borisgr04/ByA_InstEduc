using AutoMapper;
using ByA;
using DAL;
using Entidades.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class mCursos
    {
        ieEntities ctx;
        public mCursos()
        {
            Mapper.CreateMap<cursosDto, cursos>();
            Mapper.CreateMap<cursos, cursosDto>();
        }
        public List<cursosDto> GetCursosGrado(int id_grado)
        {
            using (ctx = new ieEntities())
            {
                List<cursosDto> lr = new List<cursosDto>();
                List<cursos> l = ctx.cursos.Where(t => t.id_grado == id_grado).OrderBy(t=> t.id).ToList();
                foreach (cursos item in l)
                {
                    cursosDto o = new cursosDto();
                    o.id = item.id;
                    o.id_grado = item.id_grado;
                    o.nombre = item.nombre;
                    lr.Add(o);
                }
                return lr;
            }
        }
        public cursosDto Get(int id_curso)
        {
            using (ctx = new ieEntities())
            {
                cursosDto lr = new cursosDto();
                cursos l = ctx.cursos.Where(t => t.id == id_curso).OrderBy(t => t.id).FirstOrDefault();
                Mapper.Map(l, lr);
                return lr;
            }
        }
        public List<ByARpt> InsertsOrUpdates(List<cursosDto> lReg)
        {
            List<ByARpt> lResp = new List<ByARpt>();
            foreach (cursosDto item in lReg)
            {
                ByARpt res = new ByARpt();
                if (item.id == null)
                {
                    cmdInsert o = new cmdInsert();
                    o.oDto = item;
                    res = o.Enviar();
                }
                else
                {
                    cmdUpdate o = new cmdUpdate();
                    o.oDto = item;
                    res = o.Enviar();
                }
                lResp.Add(res);
            }
            return lResp;
        }
        class cmdInsert : absTemplate
        {
            cursos Dto { get; set; }
            public cursosDto oDto { get; set; }
            int ultid = 0;
            #region ImplementaciónMetodosAbstractos
            protected internal override bool esValido()
            {
                Dto = ctx.cursos.Where(t => t.nombre == oDto.nombre && t.id_grado == oDto.id_grado).FirstOrDefault();
                if (Dto == null) return true;
                else
                {
                    byaRpt.Mensaje = "Existe un curso con un nombre y grado exactamente igual";
                    byaRpt.Error = true;
                    return false;
                }
            }
            protected internal override void Antes()
            {
                UltIdConceptos();
                ultid++;
                oDto.id = ultid;
                Dto = new cursos();
                Mapper.Map(oDto, Dto);
                ctx.cursos.Add(Dto);
            }
            protected override void Despues()
            {
                byaRpt.Mensaje = "Operación Realizada Satisfactoriamente";
                byaRpt.id = Dto.id.ToString();
            }
            private void UltIdConceptos()
            {
                try
                {
                    ultid = ctx.cursos.Max(t => t.id);
                }
                catch { }
            }
            #endregion
        }
        class cmdUpdate : absTemplate
        {
            cursos Dto { get; set; }
            public cursosDto oDto { get; set; }
            #region ImplementaciónMetodosAbstractos
            protected internal override bool esValido()
            {
                Dto = ctx.cursos.Where(t => t.id == oDto.id).FirstOrDefault();
                if (Dto != null) return true;
                else
                {
                    byaRpt.Error = true;
                    byaRpt.Mensaje = "No existe ningun curso con este id";
                    return false;
                }
            }
            protected internal override void Antes()
            {
                Mapper.Map(oDto, Dto);
            }
            protected override void Despues()
            {
                byaRpt.Mensaje = "Operación Realizada Satisfactoriamente";
                byaRpt.id = Dto.id.ToString();
            }
            #endregion
        }
    }
}
