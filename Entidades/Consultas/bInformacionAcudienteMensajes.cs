using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Vistas;

namespace Entidades.Consultas
{
    public class bInformacionAcudienteMensajes
    {
        public tercerosDto acudiente { get; set; }
        public List<estudiantesDto> estudiantes { get; set; }
        public List<mensajesDto> mensajes { get; set; }
    }
}
