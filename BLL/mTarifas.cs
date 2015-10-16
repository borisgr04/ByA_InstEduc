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
    public class mTarifas
    {
        ieEntities ctx;
        public mTarifas()
        {
            Mapper.CreateMap<tarifasDto, tarifas>();
            Mapper.CreateMap<tarifas, tarifasDto>();
        }
        public List<tarifasDto> GetTarifas(int vigencia,int grado)
        {
            using (ctx = new ieEntities())
            {
                List<tarifas> o = new List<tarifas>();
                List<tarifasDto> r = new List<tarifasDto>();
                o = ctx.tarifas.Where(t => t.vigencia == vigencia && t.id_grado == grado).ToList();
                Mapper.Map(o, r);
                return r;
            }
        }
        public tarifasDto Get(int id)
        {
            using (ctx = new ieEntities())
            {
                tarifasDto r = new tarifasDto();
                tarifas o = ctx.tarifas.Where(t => t.id == id).FirstOrDefault();
                Mapper.Map(o, r);
                return r;
            }
        }
        public List<tarifasDto> Gets()
        {
            using (ctx = new ieEntities())
            {
                List<tarifasDto> r = new List<tarifasDto>();
                List<tarifas> o = ctx.tarifas.ToList();
                Mapper.Map(o, r);
                return r;
            }
        }
        public List<tarifasDto> Gets(int vigencia)
        {
            using (ctx = new ieEntities())
            {
                List<tarifasDto> r = new List<tarifasDto>();
                List<tarifas> o = ctx.tarifas.Where(t => t.vigencia == vigencia).ToList();
                Mapper.Map(o, r);
                return r;
            }
        }

        public List<ByARpt> InsertsOrUpdates(List<tarifasDto> lReg)
        {
            List<ByARpt> lResp = new List<ByARpt>();
            foreach (tarifasDto item in lReg)
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
            tarifas Dto { get; set; }
            public tarifasDto oDto { get; set; }
            int ultid = 0;
            #region ImplementaciónMetodosAbstractos
            protected internal override bool esValido()
            {
                if (oDto.periodo_desde <= oDto.periodo_hasta)
                {
                    Dto = ctx.tarifas.Where(t => t.id_concepto == oDto.id_concepto && t.id_grado == oDto.id_grado && t.vigencia == oDto.vigencia).FirstOrDefault();
                    if (Dto == null) return true;
                    else
                    {
                        byaRpt.Mensaje = "Existe una tarifa exactamente igual";
                        byaRpt.Error = true;
                        return false;
                    }
                }
                else {
                    byaRpt.Mensaje = "Periodo inicial debe ser mayor que el periodo final";
                    byaRpt.Error = true;
                    return false;
                }
            }
            protected internal override void Antes()
            {
                UltId();
                ultid++;
                oDto.id = ultid;
                Dto = new tarifas();
                Mapper.Map(oDto, Dto);
                ctx.tarifas.Add(Dto);
            }
            protected override void Despues()
            {
                byaRpt.Mensaje = "Operación Realizada Satisfactoriamente";
                byaRpt.id = Dto.id.ToString();
            }
            private void UltId()
            {
                try
                {
                    ultid = ctx.tarifas.Max(t => t.id);
                }
                catch { }
            }
            #endregion
        }
        class cmdUpdate : absTemplate
        {
            tarifas Dto { get; set; }
            public tarifasDto oDto { get; set; }
            #region ImplementaciónMetodosAbstractos
            protected internal override bool esValido()
            {
                if (oDto.periodo_desde <= oDto.periodo_hasta)
                {
                    Dto = ctx.tarifas.Where(t => t.id == oDto.id).FirstOrDefault();
                    if (Dto != null) return true;
                    else
                    {
                        byaRpt.Error = true;
                        byaRpt.Mensaje = "No existe ninguna tarifa con este id";
                        return false;
                    }
                }
                else
                {
                    byaRpt.Mensaje = "Periodo inicial debe ser mayor que el periodo final";
                    byaRpt.Error = true;
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
