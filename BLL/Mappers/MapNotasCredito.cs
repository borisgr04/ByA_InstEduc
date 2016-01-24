using DAL;
using Entidades.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public class MapNotasCredito
    {
        public static System.Linq.Expressions.Expression<Func<notas_credito, notas_creditoDto>> Map()
        {
            return t => new notas_creditoDto()
            {
                id = t.id,
                fecha = t.fecha,
                estado = t.estado,
                id_estudiante = t.id_estudiante,
                id_grupo = t.id_grupo,
                id_est = t.id_est,
                observacion = t.observacion,
                fec_reg = t.fec_reg,
                fec_mov = t.fec_mov,
                usu_reg = t.usu_reg,
                usu_mod = t.usu_mod,
                nombre_estudiante = t.estudiantes.terceros.apellido + " " + t.estudiantes.terceros.nombre,
                nombre_grupo = t.grupos_pagos.nombre,
                detalles_nota_credito = t.detalles_nota_credito.AsQueryable().Select(MapDetallesNotasCredito.Map()).ToList()
            };
        }
    }
}
