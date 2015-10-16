using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspIdentity
{
    /// <summary>
    /// Descripción breve de wsFoto
    /// </summary>
    public class wsFoto : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "image/png";
                mEntidad objfoto = new mEntidad();
                context.Response.BinaryWrite(objfoto.getImagen());
            }
            catch (Exception e)
            {
                context.Response.Write(e.Message);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}