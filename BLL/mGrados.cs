using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades.Vistas;
using DAL;
using ByA;
using AutoMapper;

namespace BLL
{
    public class mGrados
    {
        ieEntities ctx;
        public mGrados()
        {
            Mapper.CreateMap<gradosDto, grados>();
            Mapper.CreateMap<grados, gradosDto>();
        }
        public List<gradosDto> Gets()
        {
            using (ctx = new ieEntities())
            {
                List<gradosDto> lr = new List<gradosDto>();
                List<grados> l = ctx.grados.OrderBy(t=> t.id).ToList();
                Mapper.Map(l, lr);
                return lr;
            }
        }
        public List<ByARpt> InsertsOrUpdates(List<gradosDto> lReg)
        {
            List<ByARpt> lResp = new List<ByARpt>();
            foreach (gradosDto item in lReg)
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
            grados Dto { get; set; }
            public gradosDto oDto { get; set; }
            int ultid = 0;
            #region ImplementaciónMetodosAbstractos
            protected internal override bool esValido()
            {
                Dto = ctx.grados.Where(t => t.nombre == oDto.nombre).FirstOrDefault();
                if (Dto == null) return true;
                else
                {
                    byaRpt.Mensaje = "Existe un grado con un nombre exactamente igual";
                    byaRpt.Error = true;
                    return false;
                }
            }
            protected internal override void Antes()
            {
                UltIdConceptos();
                ultid++;
                oDto.id = ultid;
                Dto = new grados();
                Mapper.Map(oDto, Dto);
                ctx.grados.Add(Dto);
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
                    ultid = ctx.grados.Max(t => t.id);
                }
                catch { }
            }
            #endregion
        }
        class cmdUpdate : absTemplate
        {
            grados Dto { get; set; }
            public gradosDto oDto { get; set; }
            #region ImplementaciónMetodosAbstractos
            protected internal override bool esValido()
            {
                Dto = ctx.grados.Where(t => t.id == oDto.id).FirstOrDefault();
                if (Dto != null) return true;
                else
                {
                    byaRpt.Error = true;
                    byaRpt.Mensaje = "No existe ningun grado con este id";
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
