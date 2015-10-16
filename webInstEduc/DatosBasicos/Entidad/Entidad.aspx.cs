using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AspIdentity.DatosBasicos.Entidad
{
    public partial class Entidad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void BtnGuardarImagen_Click(object sender, EventArgs e)
        {
            mEntidad me = new mEntidad();
            me.actualizarImagen(FileUpload1.FileBytes);
        }

        protected void BtnModificarEntidad(object sender, EventArgs e) {
            
            mEntidad me = new mEntidad();
            Entidades.Vistas.entidadDto edto = new Entidades.Vistas.entidadDto() {
                id = int.Parse( id.Value),
                nombre = nombre.Value,
                direccion = direccion.Value,
                telefono = telefono.Value,
                logo = null
            };
            me.Update(edto);
            string message = "Order Placed Successfully.";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(message);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            mEntidad me = new mEntidad();
            Entidades.Vistas.entidadDto edto = new Entidades.Vistas.entidadDto()
            {
                id = int.Parse(id.Value),
                nombre = nombre.Value,
                direccion = direccion.Value,
                telefono = telefono.Value,
                logo = null
            };
            me.Update(edto);
/*            string message = "Order Placed Successfully.";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(message);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());*/
        }
    }
}