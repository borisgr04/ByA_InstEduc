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
    
    public partial class cursos
    {
        public cursos()
        {
            this.matriculas = new HashSet<matriculas>();
        }
    
        public int id { get; set; }
        public string nombre { get; set; }
        public int id_grado { get; set; }
        public Nullable<System.DateTime> fec_reg { get; set; }
        public Nullable<System.DateTime> fec_mod { get; set; }
        public string usu_reg { get; set; }
        public string usu_mod { get; set; }
    
        public virtual grados grados { get; set; }
        public virtual ICollection<matriculas> matriculas { get; set; }
    }
}
