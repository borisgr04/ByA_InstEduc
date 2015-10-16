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
    public class mConceptos
    {
        ieEntities ctx;
        public mConceptos()
        {
            Mapper.CreateMap<conceptosDto, conceptos>();
            Mapper.CreateMap<conceptos, conceptosDto>();
        }
        public conceptosDto Get(int id_concepto)
        {
            using(ctx = new ieEntities())
            {
                conceptosDto r = new conceptosDto();
                conceptos o = ctx.conceptos.Where(t => t.id == id_concepto).FirstOrDefault();
                Mapper.Map(o,r);
                return r;
            }
        }
        public List<conceptosDto> Gets()
        {
            using (ctx = new ieEntities())
            {
                List<conceptosDto> r = new List<conceptosDto>();
                List<conceptos> o = ctx.conceptos.ToList();
                Mapper.Map(o, r);
                return r;
            }
        }
        public List<ByARpt> InsertsOrUpdates(List<conceptosDto> lReg)
        {
            List<ByARpt> lResp = new List<ByARpt>();
            foreach (conceptosDto item in lReg)
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
            conceptos Dto { get; set; }
            public conceptosDto oDto { get; set; }
            int ultid_conceptos = 0;
            #region ImplementaciónMetodosAbstractos
            protected internal override bool esValido()
            {
                Dto = ctx.conceptos.Where(t => t.nombre == oDto.nombre).FirstOrDefault();
                if (Dto == null) return true;
                else
                {
                    byaRpt.Mensaje = "Existe un concepto con un nombre exactamente igual";
                    byaRpt.Error = true;
                    return false;
                }
            }
            protected internal override void Antes()
            {
                UltIdConceptos();
                ultid_conceptos++;
                oDto.id = ultid_conceptos;
                Dto = new conceptos();
                Mapper.Map(oDto,Dto);
                ctx.conceptos.Add(Dto);
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
                    ultid_conceptos = ctx.conceptos.Max(t => t.id);
                }
                catch { }
            }
            #endregion
        }
        class cmdUpdate : absTemplate
        {
            conceptos Dto { get; set; }
            public conceptosDto oDto { get; set; }
            #region ImplementaciónMetodosAbstractos
            protected internal override bool esValido()
            {
                Dto = ctx.conceptos.Where(t => t.id == oDto.id).FirstOrDefault();
                if (Dto != null) return true;
                else
                {
                    byaRpt.Error = true;
                    byaRpt.Mensaje = "No existe ningun concepto con este id";
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
