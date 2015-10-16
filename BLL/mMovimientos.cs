using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades.Vistas;
using DAL;
using ByA;
using Entidades.Consultas;
using AutoMapper;

namespace BLL
{
    public class mMovimientos
    {
        private ieEntities ctx { get; set;  }
        public mMovimientos(ieEntities ctx)
        {
            this.ctx = ctx;
            Mapper.CreateMap<movimientos, movimientosDto>();
            Mapper.CreateMap<movimientosDto, movimientos>();
        }
        public mMovimientos()
        {
            Mapper.CreateMap<movimientos, movimientosDto>()
                .ForMember(dest => dest.nombre_concepto, obj => obj.MapFrom(src => src.conceptos.nombre));
            Mapper.CreateMap<movimientosDto, movimientos>();
        }
        public List<movimientosDto> GetsMovimientosEstudiante(bTransaccionesEstudiante Reg)
        {
            using (ctx = new ieEntities())
            {
                List<movimientosDto> lrMovimientos = new List<movimientosDto>();
                List<movimientos> lMovimientos = ctx.movimientos.Where(t => t.id_estudiante == Reg.id_estudiante && t.fecha_movimiento >= Reg.fecha_inicio && t.fecha_movimiento <= Reg.fecha_final).ToList();
                Mapper.Map(lMovimientos, lrMovimientos);
                return lrMovimientos;
            }
        }
        public void VerificarCausado(string id_estudiante)
        {
            using (ctx = new ieEntities())
            {
                DateTime FechaActual = DateTime.Now;
                List<detalles_pagoDto> lDeuda = new List<detalles_pagoDto>();
                List<carterap> lCartera = ctx.carterap.Where(t => t.id_estudiante == id_estudiante && t.pagado < t.valor && t.vigencia <= FechaActual.Year && t.periodo <= FechaActual.Month).OrderBy(t => t.vigencia).ThenBy(t => t.periodo).ThenBy(t => t.id_concepto).ToList();
            }
        }
        public void Insert( movimientosDto Reg)
        {
            //int ultId = 0;
            //try
            //{
            //    ultId = ctx.movimientos.Max(t => t.id);
            //}
            //catch {  }
            //ultId++;

            //Reg.id = ultId;
            movimientos mov = new movimientos();
            Mapper.Map(Reg,mov);

            ctx.movimientos.Add(mov);
        }
        public int GetMaxId() {
            int ultId = 0;
            try
            {
                ultId = ctx.movimientos.Max(t => t.id);
            }
            catch { }
            return ultId;
        }
    }
}
