//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class mensajes_acudientes
    {
        public int id { get; set; }
        public int id_mensaje { get; set; }
        public int id_acudiente { get; set; }
        public string estado { get; set; }
    
        public virtual terceros terceros { get; set; }
        public virtual mensajes mensajes { get; set; }
    }
}
