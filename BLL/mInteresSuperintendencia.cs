using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Entidades.Vistas;
using AutoMapper;
using ByA;
using BLL;

namespace BLL
{
    public class mInteresSuperintendencia
    {
        ieEntities ctx;
        public mInteresSuperintendencia()
        {
            Mapper.CreateMap<tasasDto, tasas>();
            Mapper.CreateMap<tasas, tasasDto>();
        }
        public int GetValorIntereses(DateTime FechaInicial, DateTime FechaFinal, double Capital)
        {
            using (ctx = new ieEntities())
            {
                TimeSpan Diferencia = FechaFinal.Date - FechaInicial.Date;
                int NumeroDias = Diferencia.Days;
                decimal TasaVigente = GetTasaVigenteNominalDiario();
                int Intereses = (int)((decimal)Capital  * TasaVigente * NumeroDias);
                //int Intereses =(int)(Capital * (Math.Pow(((double)(1 + TasaVigente)), ((double)NumeroDias)) - 1));
                return Intereses;
            }
        }
        public int GetNumeroDiasPagoIntereses(float Capital, float ValorDisponible)
        {
            using (ctx = new ieEntities())
            {
                try
                {
                    decimal TasaVigente = GetTasaVigenteNominalDiario();
                    int NumeroDias = (int)((decimal)ValorDisponible / ((decimal)Capital * TasaVigente));
                    return NumeroDias;
                }
                catch
                {
                    return 0;
                }
            }
        }
        public decimal GetTasaVigenteNominalDiario()
        {
            using (ctx = new ieEntities())
            {
                DateTime FechaActual = DateTime.Now.Date;
                tasas tasaActual = ctx.tasas.Where(t => t.fecha_inicio <= FechaActual && t.fecha_fin >= FechaActual).FirstOrDefault();
                decimal Tasa = (decimal)((tasaActual.tasa / 365) / 100);
                //decimal Tasa = (decimal) Math.Pow((1 + (tasaActual.tasa / 100)), (1 / 365)) - 1;
                return Tasa;
            }
        }
        public tasasDto Get(int id_tasa)
        {
            using (ctx = new ieEntities())
            {
                tasasDto r = new tasasDto();
                tasas o = ctx.tasas.Where(t => t.id == id_tasa).FirstOrDefault();
                Mapper.Map(o, r);
                return r;
            }
        }
        public List<tasasDto> Gets(int vigencia)
        {
            using (ctx = new ieEntities())
            {
                List<tasasDto> r = new List<tasasDto>();
                List<tasas> o = ctx.tasas.Where(t=>t.fecha_inicio.Year==vigencia).ToList();
                Mapper.Map(o, r);
                return r;
            }
        }
        public List<tasasDto> Gets()
        {
            using (ctx = new ieEntities())
            {
                List<tasasDto> r = new List<tasasDto>();
                List<tasas> o = ctx.tasas.ToList();
                Mapper.Map(o, r);
                return r;
            }
        }
        public List<ByARpt> InsertsOrUpdates(List<tasasDto> lReg)
        {
            List<ByARpt> lResp = new List<ByARpt>();
            foreach (tasasDto item in lReg)
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
            tasas Dto { get; set; }
            public tasasDto oDto { get; set; }
            int ultid_tasas = 0;
            #region ImplementaciónMetodosAbstractos
            protected internal override bool esValido()
            {

                Dto = ctx.tasas.Where(t => (t.fecha_inicio <= oDto.fecha_inicio && t.fecha_fin >= oDto.fecha_inicio) || (t.fecha_inicio <= oDto.fecha_fin && t.fecha_fin >= oDto.fecha_fin)).FirstOrDefault();
                if (Dto == null) return true;
                else
                {
                    byaRpt.Mensaje = "Fecha inicio o feha fin esta en un rango de fecha ya registrado";
                    byaRpt.Error = true;
                    return false;
                }
            }
            protected internal override void Antes()
            {
                UltIdTasas();
                ultid_tasas++;
                oDto.id = ultid_tasas;
                Dto = new tasas();
                Mapper.Map(oDto, Dto);
                ctx.tasas.Add(Dto);
            }
            protected override void Despues()
            {
                byaRpt.Mensaje = "Operación Realizada Satisfactoriamente";
                byaRpt.id = Dto.id.ToString();
            }
            private void UltIdTasas()
            {
                try
                {
                    ultid_tasas = ctx.tasas.Max(t => t.id);
                }
                catch { }
            }
            #endregion
        }
        class cmdUpdate : absTemplate
        {
            tasas Dto { get; set; }
            public tasasDto oDto { get; set; }
            #region ImplementaciónMetodosAbstractos
            protected internal override bool esValido()
            {
                Dto = ctx.tasas.Where(t => t.id == oDto.id).FirstOrDefault();
                if (Dto != null) return true;
                else
                {
                    byaRpt.Error = true;
                    byaRpt.Mensaje = "No existe ninguna tasa con este id";
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
