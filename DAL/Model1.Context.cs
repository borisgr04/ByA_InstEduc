﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ieEntities : DbContext
    {
        public ieEntities()
            : base("name=ieEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<carterap> carterap { get; set; }
        public DbSet<conceptos> conceptos { get; set; }
        public DbSet<config_grupos_pagos> config_grupos_pagos { get; set; }
        public DbSet<cursos> cursos { get; set; }
        public DbSet<detalles_pago> detalles_pago { get; set; }
        public DbSet<detalles_tipos_tercero> detalles_tipos_tercero { get; set; }
        public DbSet<documentos> documentos { get; set; }
        public DbSet<entidad> entidad { get; set; }
        public DbSet<estudiantes> estudiantes { get; set; }
        public DbSet<fc_menu> fc_menu { get; set; }
        public DbSet<fechas_calculo_intereses> fechas_calculo_intereses { get; set; }
        public DbSet<formas_pago> formas_pago { get; set; }
        public DbSet<grados> grados { get; set; }
        public DbSet<grupos_pagos> grupos_pagos { get; set; }
        public DbSet<intereses_manuales> intereses_manuales { get; set; }
        public DbSet<matriculas> matriculas { get; set; }
        public DbSet<mensajes> mensajes { get; set; }
        public DbSet<movimientos> movimientos { get; set; }
        public DbSet<pagos> pagos { get; set; }
        public DbSet<parametros> parametros { get; set; }
        public DbSet<periodos> periodos { get; set; }
        public DbSet<saldos_a_favor> saldos_a_favor { get; set; }
        public DbSet<tarifas> tarifas { get; set; }
        public DbSet<tasas> tasas { get; set; }
        public DbSet<terceros> terceros { get; set; }
        public DbSet<tipos_documentos> tipos_documentos { get; set; }
        public DbSet<tipos_terceros> tipos_terceros { get; set; }
        public DbSet<vigencias> vigencias { get; set; }
        public DbSet<tokens_notificaciones> tokens_notificaciones { get; set; }
        public DbSet<detalles_nota_credito> detalles_nota_credito { get; set; }
        public DbSet<notas_credito> notas_credito { get; set; }
    }
}
