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
    
    public partial class matriculas
    {
        public matriculas()
        {
            this.carterap = new HashSet<carterap>();
        }
    
        public int id { get; set; }
        public int vigencia { get; set; }
        public int id_curso { get; set; }
        public System.DateTime fecha { get; set; }
        public string id_estudiante { get; set; }
        public int id_grado { get; set; }
        public int id_est { get; set; }
        public string estado { get; set; }
        public string folio { get; set; }
        public string id_matricula { get; set; }
    
        public virtual ICollection<carterap> carterap { get; set; }
        public virtual cursos cursos { get; set; }
        public virtual estudiantes estudiantes { get; set; }
        public virtual vigencias vigencias { get; set; }
    }
}
