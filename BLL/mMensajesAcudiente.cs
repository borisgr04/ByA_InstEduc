using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entidades.Vistas;
using AutoMapper;
namespace BLL
{
    public class mMensajesAcudiente
    {
        ieEntities ctx;
        public mMensajesAcudiente()
        {
            Mapper.CreateMap<mensajes,mensajesDto>();
            Mapper.CreateMap<mensajesDto,mensajes>();
        }

        public List<mensajesDto> GetMensajes(int id_acudiente)
        {
            using(ctx = new ieEntities())
            {
                List<mensajesDto> ListMsjeDto = new List<mensajesDto>();
                List<mensajes> ListMsje = ctx.mensajes.Where(t => t.mensajes_acudientes.Where(c => c.id_acudiente == id_acudiente).ToList().Count() > 0).ToList();
                Mapper.Map(ListMsje, ListMsjeDto);
                return ListMsjeDto;
            }
        }
    }
}
