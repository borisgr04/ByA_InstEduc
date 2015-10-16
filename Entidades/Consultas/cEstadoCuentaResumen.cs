using Entidades.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Consultas
{
    public class cEstadoCuentaResumen
    {
        public cEstadoCuentaResumen()
        {
            this.matricula = new cConceptoEstadoCuenta();
            this.otros = new cConceptoEstadoCuenta();
            this.pension1 = new cConceptoEstadoCuenta();
            this.pension2 = new cConceptoEstadoCuenta();
            this.pension3 = new cConceptoEstadoCuenta();
            this.pension4 = new cConceptoEstadoCuenta();
            this.pension5 = new cConceptoEstadoCuenta();
            this.pension6 = new cConceptoEstadoCuenta();
            this.pension7 = new cConceptoEstadoCuenta();
            this.pension8 = new cConceptoEstadoCuenta();
            this.pension9 = new cConceptoEstadoCuenta();
            this.pension10 = new cConceptoEstadoCuenta();
            this.pension11 = new cConceptoEstadoCuenta();
            this.pension12 = new cConceptoEstadoCuenta();
        }

        public string id_matricula { get; set; }
        public string nombre_grado { get; set; }
        public string nombre_curso { get; set; }

        public cConceptoEstadoCuenta matricula { get; set; }
        public cConceptoEstadoCuenta otros { get; set; }
        public cConceptoEstadoCuenta pension1 { get; set; }
        public cConceptoEstadoCuenta pension2 { get; set; }
        public cConceptoEstadoCuenta pension3 { get; set; }
        public cConceptoEstadoCuenta pension4 { get; set; }
        public cConceptoEstadoCuenta pension5 { get; set; }
        public cConceptoEstadoCuenta pension6 { get; set; }
        public cConceptoEstadoCuenta pension7 { get; set; }
        public cConceptoEstadoCuenta pension8 { get; set; }
        public cConceptoEstadoCuenta pension9 { get; set; }
        public cConceptoEstadoCuenta pension10 { get; set; }
        public cConceptoEstadoCuenta pension11 { get; set; }
        public cConceptoEstadoCuenta pension12 { get; set; }
    }

    public class cConceptoEstadoCuenta
    {
        public int valor { get; set; }
        public int pagado { get; set; }
    }
}
