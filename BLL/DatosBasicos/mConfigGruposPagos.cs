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
    public class mConfigGruposPagos
    {
        ieEntities ctx;
        public mConfigGruposPagos()
        {
            Mapper.CreateMap<configGruposPagosDto, config_grupos_pagos>();
            Mapper.CreateMap<config_grupos_pagos, configGruposPagosDto>();
        }
        public configGruposPagosDto Get(int id)
        {
            using(ctx = new ieEntities())
            {
                configGruposPagosDto r = new configGruposPagosDto();
                config_grupos_pagos o = ctx.config_grupos_pagos.Where(t => t.id == id).FirstOrDefault();
                Mapper.Map(o,r);
                return r;
            }
        }
        public List<configGruposPagosDto> Gets()
        {
            using (ctx = new ieEntities())
            {
                List<configGruposPagosDto> r = new List<configGruposPagosDto>();
                List<config_grupos_pagos> o = ctx.config_grupos_pagos.ToList();
                Mapper.Map(o, r);
                return r;
            }
        }
        public List<configGruposPagosDto> Gets(int vigencia)
        {
            using (ctx = new ieEntities())
            {
                List<configGruposPagosDto> r = new List<configGruposPagosDto>();
                List<config_grupos_pagos> o = ctx.config_grupos_pagos.Where(t=>t.vigencia==vigencia).ToList();
                Mapper.Map(o, r);
                return r;
            }
        }
        public List<ByARpt> InsertsOrUpdates(List<configGruposPagosDto> lReg)
        {
            List<ByARpt> lResp = new List<ByARpt>();
            foreach (configGruposPagosDto item in lReg)
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
            config_grupos_pagos Dto { get; set; }
            public configGruposPagosDto oDto { get; set; }
            int ultid = 0;
            #region ImplementaciónMetodosAbstractos
            protected internal override bool esValido()
            {
                /*if (ctx.conceptos.Where(t => t.id == oDto.id_concepto).FirstOrDefault() == null) {
                    byaRpt.Mensaje = "No existe un concepto asociado a este registro";
                    byaRpt.Error = true;
                    return false;
                }
                if (ctx.grupos_pagos.Where(t => t.id == oDto.id_grupo).FirstOrDefault() == null)
                {
                    byaRpt.Mensaje = "No existe un grupo de pago asociado a este registro";
                    byaRpt.Error = true;
                    return false;
                }
                if (ctx.vigencias.Where(t => t.vigencia == oDto.vigencia).FirstOrDefault() == null)
                {
                    byaRpt.Mensaje = "No existe una vigencia asociada a este registro";
                    byaRpt.Error = true;
                    return false;
                }*/

                Dto = ctx.config_grupos_pagos.Where(t => t.id_concepto == oDto.id_concepto && t.id_grupo == oDto.id_grupo && t.vigencia == oDto.vigencia).FirstOrDefault();
                if (Dto == null) return true;
                else
                {
                    byaRpt.Mensaje = "Existe una configuracion de grupos de pago exactamente igual";
                    byaRpt.Error = true;
                    return false;
                }
            }
            protected internal override void Antes()
            {
                UltId();
                ultid++;
                oDto.id = ultid;
                Dto = new config_grupos_pagos();
                Mapper.Map(oDto,Dto);
                ctx.config_grupos_pagos.Add(Dto);
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
                    ultid = ctx.config_grupos_pagos.Max(t => t.id);
                }
                catch { }
            }
            #endregion
        }
        class cmdUpdate : absTemplate
        {
            config_grupos_pagos Dto { get; set; }
            public configGruposPagosDto oDto { get; set; }
            #region ImplementaciónMetodosAbstractos
            protected internal override bool esValido()
            {
                Dto = ctx.config_grupos_pagos.Where(t => t.id == oDto.id).FirstOrDefault();
                if (Dto != null) return true;
                else
                {
                    byaRpt.Error = true;
                    byaRpt.Mensaje = "No existe ningun registro con este id";
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
