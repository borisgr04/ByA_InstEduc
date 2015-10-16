using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Vistas;
using AutoMapper;
using ByA;

namespace BLL
{
    public class mEntidad
    {
        ieEntities ctx;
        public mEntidad()
        {
            Mapper.CreateMap<entidad, entidadDto>();
            Mapper.CreateMap<entidadDto, entidad>();
        }
        public void actualizarImagen(byte[] imagen){
            using (ieEntities db = new ieEntities())
            {
                entidad ic = db.entidad.Single();
                ic.logo = imagen;
                db.SaveChanges();
            }
        }
        public entidadDto Get()
        {
            using(ctx = new ieEntities())
            {
                entidadDto r = new entidadDto();
                entidad o = ctx.entidad.FirstOrDefault();
                o.logo = null;
                Mapper.Map(o,r);
                return r;
            }
        }
        public byte[] getImagen()
        {
            using (ieEntities db = new ieEntities())
            {
                entidad ic = db.entidad.Single();
                return ic.logo;
            }
        }


        public ByARpt Update(entidadDto Reg)
        {
            ByARpt Resp = new ByARpt();
            cmdUpdate o = new cmdUpdate();
            o.oDto = Reg;
            o.id = Reg.id;
            Resp = o.Enviar();
            return Resp;
        }
        
        class cmdUpdate : absTemplate
        {
            entidad Dto { get; set; }
            public entidadDto oDto { get; set; }
            public int id { get; set; }
            #region ImplementaciónMetodosAbstractos
            protected internal override bool esValido()
            {
                Dto = ctx.entidad.Where(t => t.id == id).FirstOrDefault();
                if (Dto != null) return true;
                else
                {
                    byaRpt.Error = true;
                    byaRpt.Mensaje = "No existe ninguna entidad con este id";
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
