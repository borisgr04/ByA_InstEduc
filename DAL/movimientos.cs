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
    
    public partial class movimientos
    {
        public int id { get; set; }
        public string id_estudiante { get; set; }
        public Nullable<int> vigencia { get; set; }
        public Nullable<int> periodo { get; set; }
        public Nullable<int> id_cartera { get; set; }
        public Nullable<int> id_concepto { get; set; }
        public Nullable<double> valor_debito { get; set; }
        public Nullable<double> valor_credito { get; set; }
        public Nullable<System.DateTime> fecha_movimiento { get; set; }
        public string estado { get; set; }
        public Nullable<System.DateTime> fecha_novedad { get; set; }
        public Nullable<System.DateTime> fecha_registro { get; set; }
        public string tipo_documento { get; set; }
        public Nullable<int> numero_documento { get; set; }
        public Nullable<int> id_est { get; set; }
    
        public virtual carterap carterap { get; set; }
        public virtual conceptos conceptos { get; set; }
        public virtual estudiantes estudiantes { get; set; }
        public virtual tipos_documentos tipos_documentos { get; set; }
        public virtual vigencias vigencias { get; set; }
    }
}
