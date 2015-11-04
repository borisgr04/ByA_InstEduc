using ByA;
using DAL;
using Entidades.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class mCausacion
    {
        public static ByARpt Causar(string id_est, int? id_mat = null)
        {
            cmdCausar o = new cmdCausar();
            o.id_est = id_est;
            o.id_mat = id_mat;
            return o.Enviar();
        }
        public static DateTime FechaCausacion()
        {
            ieEntities ctx = new ieEntities();

            parametros strFechaCausacion = ctx.parametros.Where(t => t.nombre == "FECCAUSA").FirstOrDefault();
            int year = int.Parse(strFechaCausacion.valor.Substring(0, 4));
            int month = int.Parse(strFechaCausacion.valor.Substring(4, 2));
            int day = int.Parse(strFechaCausacion.valor.Substring(6, 2));
            DateTime FechaCausacion = new DateTime(year, month, day);
            return FechaCausacion;
        }
        public ByARpt CambiarFechaCausacion(DateTime FechaCausacion)
        {
            cmdCambiarFechaCausacion o = new cmdCambiarFechaCausacion();
            o.FechaCausacionNueva = FechaCausacion;
            return o.Enviar();
        }
    }
    class cmdCambiarFechaCausacion : absTemplate
    {
        parametros strFechaCausacion { get; set; }
        public DateTime FechaCausacionNueva { get; set; }
        protected internal override bool esValido()
        {
            strFechaCausacion = ctx.parametros.Where(t => t.nombre == "FECCAUSA").FirstOrDefault();
            string fech = strFechaCausacion.valor;
            DateTime FechaCausacion = new DateTime(int.Parse(fech.Substring(0, 4)), int.Parse(fech.Substring(4, 2)), int.Parse(fech.Substring(6, 2)));
            if (FechaCausacion < FechaCausacionNueva) return true;
            else
            {
                byaRpt.Error = true;
                byaRpt.Mensaje = "La fecha de causación nueva no puede ser menor a la fecha de causación actual";
                return false;
            }
        }
        protected internal override void Antes()
        {
            strFechaCausacion.valor = FechaCausacionNueva.Year.ToString() + FechaCausacionNueva.Month.ToString().PadLeft(2, '0') + FechaCausacionNueva.Day.ToString().PadLeft(2, '0');
        }
    }
    class cmdCausar : absTemplate
        {
            mMovimientos mm;
            public string id_est { get; set; }
            public int? id_mat { get; set; }
            DateTime FechaCausacion { get; set; }
            List<carterap> cart_sin_causar = new List<carterap>();            
            protected internal override bool esValido()
            {
                FechaCausacion = mCausacion.FechaCausacion();
                int VigPerAct = int.Parse(FechaCausacion.Year.ToString() + FechaCausacion.Month.ToString().PadLeft(2, '0'));
                if (id_est == "")
                {
                    cart_sin_causar = ctx.carterap.Where(t => t.estado == "PR" && (t.vigencia * 100 + t.periodo) <= VigPerAct).ToList();
                }
                else {
                    cart_sin_causar = ctx.carterap.Where(t =>t.id_estudiante == id_est && t.estado == "PR" && (t.vigencia * 100 + t.periodo) <= VigPerAct).ToList();
                }

                if (cart_sin_causar.Count == 0) return false;
                else return true;
                
            }
            protected internal override void Antes()
            {
                int ultidDoc = 0;
                try { ultidDoc = ctx.documentos.Max(t => t.id); } catch { }
                ultidDoc++;

                if (id_mat == null) InsertDocumento(ultidDoc);
                else ultidDoc = (int) id_mat;


                int IdMov=0;
                mm = new mMovimientos(this.ctx);
                IdMov = mm.GetMaxId();
                cart_sin_causar.ForEach(t => casusarItem(t, ref IdMov, ultidDoc));
            }
            private void InsertDocumento(int ultidDoc)
            {
                documentos doc = new documentos();
                doc.id = ultidDoc;
                doc.tipo_documento = "NOTDB";
                doc.fecha = FechaCausacion;
                doc.descripcion = "Se genero por causacion hasta la fecha: " + doc.fecha.Value.ToShortDateString();
                ctx.documentos.Add(doc);
            }
            private void casusarItem(carterap cart,ref int IdMov, int idDoc)
            {
                IdMov++;
                cart.estado = "CA";
                movimientosDto m = new movimientosDto();
                m.estado = "AC";
                m.fecha_movimiento = FechaCausacion;
                m.fecha_novedad = DateTime.Now;
                m.fecha_registro = DateTime.Now;
                m.id_cartera = cart.id;
                m.id_concepto = cart.id_concepto;
                m.id_estudiante = cart.id_estudiante;
                m.numero_documento = idDoc;
                m.id_est = cart.id_est;

                if (id_mat == null) m.tipo_documento = "NOTDB";
                else m.tipo_documento = "MATRI";

                m.valor_debito = cart.valor;
                m.valor_credito = 0;
                m.vigencia = cart.vigencia;
                m.id = IdMov;
                m.periodo = cart.periodo;
                mm.Insert(m);
            }
        }
}
