using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades.Vistas
{
    public class estudiantesDto
    {
        public int id { get; set; }
        public Nullable<int> id_ultima_matricula { get; set; }
        public Nullable<int> id_ultimo_grado { get; set; }
        public string lugar_nacimiento { get; set; }
        public Nullable<System.DateTime> fecha_nacimiento { get; set; }
        public string colegio_procedencia { get; set; }
        public string estado_civil_padres { get; set; }
        public string parentesco_acudiente { get; set; }
        public int id_acudiente { get; set; }
        public Nullable<int> id_tercero_madre { get; set; }
        public Nullable<int> id_tercero_padre { get; set; }
        public int id_tercero_estudiante { get; set; }
        public string identificacion { get; set; }
        public string vive_con { get; set; }
        public string codigo { get; set; }
        public string usu { get; set; }

        public string nombre_curso { get; set; }
        public string nombre_grado { get; set; }
        public string nombre_completo { get; set; }
        public string nombre_completo_padre { get; set; }
        public string nombre_completo_madre { get; set; }
        public string nombre_completo_acudiente { get; set; }
        public double? saldo { get; set;}
        public string mensaje { get; set; }
        public string asunto { get; set; }
        public string tipo_mensaje { get; set; }
        
        public tercerosDto terceros { get; set; }
        public tercerosDto terceros1 { get; set; }
        public tercerosDto terceros2 { get; set; }
        public tercerosDto terceros3 { get; set; }
    }
}
