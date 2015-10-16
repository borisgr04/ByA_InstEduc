using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades.Vistas;
using ByA;
using AutoMapper;
using DAL;

namespace BLL
{
    public class mGruposPagos
    {
        ieEntities ctx;
        public mGruposPagos()
        {
            Mapper.CreateMap<grupos_pagosDto, grupos_pagos>();
            Mapper.CreateMap<grupos_pagos, grupos_pagosDto>();
        }
        public List<grupos_pagosDto> Gets()
        {
            using (ctx = new ieEntities())
            {
                List<grupos_pagosDto> lr = new List<grupos_pagosDto>();
                List<grupos_pagos> l = ctx.grupos_pagos.OrderBy(t=> t.prioridad).ToList();
                Mapper.Map(l,lr);
                return lr;
            }
        }
    }
}
