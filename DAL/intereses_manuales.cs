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
    
    public partial class intereses_manuales
    {
        public int id { get; set; }
        public int vigencia { get; set; }
        public string en_mes { get; set; }
        public Nullable<int> inicio { get; set; }
        public Nullable<int> fin { get; set; }
        public int valor { get; set; }
        public Nullable<System.DateTime> fec_reg { get; set; }
        public Nullable<System.DateTime> fec_mod { get; set; }
        public string usu_reg { get; set; }
        public string usu_mod { get; set; }
    
        public virtual vigencias vigencias { get; set; }
    }
}
