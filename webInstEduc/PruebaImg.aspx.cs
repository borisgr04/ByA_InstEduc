using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace AspIdentity
{
    public partial class PruebaImg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void BtnGuardarImagen_Click(object sender, EventArgs e)
        {
            mEntidad me = new mEntidad();
            
            me.actualizarImagen(FileUpload1.FileBytes);

        }

        protected void BtnImprimirImagen_Click(object sender, EventArgs e)
        {
            mEntidad me = new mEntidad();
            Response.ContentType = "image/png";

            Response.BinaryWrite(me.getImagen());
        }
    }
}