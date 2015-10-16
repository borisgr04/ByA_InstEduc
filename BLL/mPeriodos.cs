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
    public class mPeriodos
    {
        ieEntities ctx;
        public mPeriodos()
        {
            Mapper.CreateMap<periodosDto, periodos>();
            Mapper.CreateMap<periodos, periodosDto>();
        }
        public List<periodosDto> Gets(int vigencia)
        {
            using (ctx = new ieEntities())
            {
                List<periodosDto> lr = new List<periodosDto>();
                List<periodos> l = ctx.periodos.Where(t=> t.vigencia == vigencia).OrderBy(t => t.periodo).ToList();
                Mapper.Map(l, lr);
                return lr;
            }
        }
        public List<periodosDto> Gets()
        {
            using (ctx = new ieEntities())
            {
                List<periodosDto> lr = new List<periodosDto>();
                List<periodos> l = ctx.periodos.OrderByDescending(t => t.id).ToList();
                Mapper.Map(l, lr);
                return lr;
            }
        }
        public List<ByARpt> InsertOrUpdate(List<periodosDto> lReg)
        {
            using (ctx = new ieEntities())
            {
                List<ByARpt> lResp = new List<ByARpt>();
                foreach (periodosDto item in lReg)
                {
                    ByARpt res = new ByARpt();
                    periodos obj = ctx.periodos.Where(t => t.id == item.id).FirstOrDefault();
                    if (obj == null)
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
        }

        class cmdInsert : absTemplate
        {

            periodos Dto { get; set; }
            public periodosDto oDto { get; set; }
            int ultid_periodos = 0;
            #region ImplementaciónMetodosAbstractos
            protected internal override bool esValido()
            {
                if (oDto.periodo >= 1 && oDto.periodo <= 12)
                {
                    Dto = ctx.periodos.Where(t => t.periodo == oDto.periodo && t.vigencia == oDto.vigencia).FirstOrDefault();
                    if (Dto == null) return true;
                    else
                    {
                        byaRpt.Mensaje = "Existe un mismo periodo registrado en la vigencia de " + oDto.vigencia.ToString();
                        byaRpt.Error = true;
                        return false;
                    }
                }
                else
                {
                    byaRpt.Mensaje = "El valor para periodo esta por fuera del rango";
                    byaRpt.Error = true;
                    return false;
                }

            }

            protected internal override void Antes()
            {
                UltIdPeriodos();
                ultid_periodos++;
                oDto.id = ultid_periodos;
                Dto = new periodos();
                Mapper.Map(oDto, Dto);
                ctx.periodos.Add(Dto);
            }

            protected override void Despues()
            {
                byaRpt.Mensaje = "Operación Realizada Satisfactoriamente";
                byaRpt.id = oDto.id.ToString();
            }
            #endregion
            private void UltIdPeriodos()
            {
                try
                {
                    ultid_periodos = ctx.periodos.Max(t => t.id);
                }
                catch { }
            }

        }
        class cmdUpdate : absTemplate
        {
            public periodosDto oDto { get; set; }
            private periodos Dto { get; set; }
            protected internal override bool esValido()
            {
                if (oDto.periodo >= 1 && oDto.periodo <= 12)
                {
                    Dto = ctx.periodos.Where(t => t.id == oDto.id).FirstOrDefault();
                    if (Dto != null) return true;
                    else
                    {
                        byaRpt.Mensaje = "No se puede encontrar el periodo";
                        byaRpt.Error = true;
                        return false;
                    }
                }
                else
                {
                    byaRpt.Mensaje = "El valor para periodo esta por fuera del rango";
                    byaRpt.Error = true;
                    return false;
                }
            }
            protected internal override void Antes()
            {
                Mapper.Map(oDto, Dto);
            }
        }
    }
}
