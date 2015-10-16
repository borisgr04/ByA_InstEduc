using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades.Vistas;
using ByA;
using AutoMapper;
using DAL;

namespace BLL.DatosBasicos
{
    public class mVigencias
    {
        ieEntities ctx;
        public mVigencias()
        {
            Mapper.CreateMap<vigenciasDto, vigencias>();
            Mapper.CreateMap<vigencias, vigenciasDto>();
        }
        public List<vigenciasDto> Gets()
        {
            using (ctx = new ieEntities())
            {
                List<vigenciasDto> lr = new List<vigenciasDto>();
                List<vigencias> l = ctx.vigencias.OrderByDescending(t=> t.vigencia).ToList();
                Mapper.Map(l,lr);
                return lr;
            }
        }
        public vigenciasDto Get(int vigencia)
        {
            using (ctx = new ieEntities())
            {
                vigenciasDto lr = new vigenciasDto();
                vigencias l = ctx.vigencias.Where(t => t.vigencia == vigencia).OrderByDescending(t => t.vigencia).FirstOrDefault();
                Mapper.Map(l, lr);
                return lr;
            }
        }
        public List<vigenciasDto> GetsActivas()
        {
            using (ctx = new ieEntities())
            {
                List<vigenciasDto> lr = new List<vigenciasDto>();
                List<vigencias> l = ctx.vigencias.Where(t=> t.estado == "AC").OrderByDescending(t => t.vigencia).ToList();
                Mapper.Map(l, lr);
                return lr;
            }
        }
        public List<ByARpt> InsertOrUpdate(List<vigenciasDto> lReg)
        {
            using (ctx = new ieEntities())
            {
                List<ByARpt> lResp = new List<ByARpt>();
                foreach (vigenciasDto item in lReg)
                {
                    ByARpt res = new ByARpt();
                    vigencias obj = ctx.vigencias.Where(t => t.vigencia == item.vigencia).FirstOrDefault();
                    if (obj == null)
                    {
                        cmdInicializarVigencia o = new cmdInicializarVigencia();
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
        public ByARpt Insert(vigenciasDto Reg)
        {
            cmdInicializarVigencia o = new cmdInicializarVigencia();
            o.oDto = Reg;
            return o.Enviar();
        }
        class cmdInicializarVigencia : absTemplate
        {
            public vigenciasDto oDto { get; set; }
            public int ultid_periodos = 0;
            public int ultid_config = 0;
            public int ultid_tarifas = 0;
            private vigencias vigenciaOld { get; set; }

            protected internal override bool esValido()
            {
                vigenciaOld = ctx.vigencias.Where(t=> t.vigencia < oDto.vigencia).OrderByDescending(t => t.vigencia).FirstOrDefault();
                if (vigenciaOld != null)
                {
                    if (oDto.vigencia > vigenciaOld.vigencia) return true;
                    else
                    {                        
                        byaRpt.Mensaje = "No se puede crear una vigencia menor a las vigencias ya creadas";
                        byaRpt.Error = true;
                        byaRpt.id = oDto.vigencia.ToString();
                        return false;
                    }
                }
                else
                {
                    byaRpt.Mensaje = "No se puede crear la vigencia";
                    byaRpt.Error = true;
                    byaRpt.id = oDto.vigencia.ToString();
                    return false;
                }

            }
            protected internal override void Antes()
            {
                UltIdPeriodos();
                UltIdConfig();
                UltIdTarifas();

                vigencias NVigencia = new vigencias();
                NVigencia.vigencia = oDto.vigencia;
                NVigencia.estado = oDto.estado;
                ctx.vigencias.Add(NVigencia);
                InsertPeriodos(NVigencia);
                InsertConfigGruposPagos(NVigencia);
                InsertTarifas(NVigencia);
            }
            protected override void Despues()
            {
                byaRpt.Mensaje = "Operación Realizada Satisfactoriamente<br/>Se crearon tambien los periodos y tarifas en base al año anterior";
                byaRpt.id = oDto.vigencia.ToString();
            }
            private void InsertTarifas(vigencias NVigencia)
            {
                foreach (tarifas item in vigenciaOld.tarifas.OrderBy(t => t.id).ToList())
                {
                    ultid_tarifas++;
                    tarifas tar = new tarifas();
                    tar.id = ultid_tarifas;
                    tar.vigencia = NVigencia.vigencia;
                    tar.id_grado = item.id_grado;
                    tar.id_concepto = item.id_concepto;
                    tar.valor = item.valor;
                    tar.periodo_desde = item.periodo_desde;
                    tar.periodo_hasta = item.periodo_hasta;
                    ctx.tarifas.Add(tar);
                }
            }
            private void InsertConfigGruposPagos(vigencias NVigencia)
            {
                foreach (config_grupos_pagos item in vigenciaOld.config_grupos_pagos.OrderBy(t => t.id).ToList())
                {
                    ultid_config++;
                    config_grupos_pagos conf = new config_grupos_pagos();
                    conf.id = ultid_config;
                    conf.id_grupo = item.id_grupo;
                    conf.id_concepto = item.id_concepto;
                    conf.vigencia = NVigencia.vigencia;
                    conf.intereses = item.intereses;
                    ctx.config_grupos_pagos.Add(conf);
                }
            }
            private void InsertPeriodos(vigencias NVigencia)
            {
                foreach (periodos item in vigenciaOld.periodos.OrderBy(t => t.id).ToList())
                {
                    ultid_periodos++;
                    periodos per = new periodos();
                    per.id = ultid_periodos;
                    per.periodo = item.periodo;
                    per.vigencia = NVigencia.vigencia;
                    per.vence_dia = item.vence_dia;
                    per.estado = "AC";
                    ctx.periodos.Add(per);
                }
            }
            private void UltIdPeriodos()
            {
                try
                {
                    ultid_periodos = ctx.periodos.Max(t => t.id);
                }
                catch { }
            }
            private void UltIdConfig()
            {
                try
                {
                    ultid_config = ctx.config_grupos_pagos.Max(t => t.id);
                }
                catch { }
            }
            private void UltIdTarifas()
            {
                try
                {
                    ultid_tarifas = ctx.tarifas.Max(t => t.id);
                }
                catch { }
            }
        }
        class cmdUpdate : absTemplate
        {
            public vigenciasDto oDto { get; set; }
            private vigencias Dto { get; set; }
            protected internal override bool esValido()
            {
                Dto = ctx.vigencias.Where(t=> t.vigencia == oDto.vigencia).FirstOrDefault();
                if (Dto != null) return true;
                else
                {
                    byaRpt.Mensaje = "No se puede encontrar la vigencia";
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
