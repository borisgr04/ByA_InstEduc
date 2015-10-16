using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Entidades.Vistas;
using BLL;
using ByA;
using System.IO;

namespace AspIdentity.Controllers
{
    [RoutePrefix("api/Entidad")]
    public class EntidadController : ApiController
    {
        [Route("")]
        public entidadDto Get()
        {
            mEntidad o = new mEntidad();
            return o.Get();
        }
        [Route("Logo")]
        public HttpResponseMessage GetLogo()
        {
            mEntidad objfoto = new mEntidad();
            byte[] imgData = objfoto.getImagen();
            MemoryStream ms = new MemoryStream(imgData);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(ms);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
            return response;
        }

        [AcceptVerbs("PUT")]
        [Route("")]
        public ByARpt Put(entidadDto Reg)
        {
            mEntidad o = new mEntidad();
            return o.Update(Reg);
        }
        
        /*[AcceptVerbs("PATCH")]
        [Route("Logo")]
        public ByARpt Patch()
        {
            if (Request.Content.IsMimeMultipartContent())
            {
                var streamProvider = new MultipartMemoryStreamProvider();
                var task = Request.Content.ReadAsMultipartAsync(streamProvider).ContinueWith(t =>
                {
                    foreach (var item in streamProvider.Contents)
                    {
                        mEntidad o = new mEntidad();
                        byte[] x = item.ReadAsByteArrayAsync().Result;
                        o.actualizarImagen(x);
                    }
                });

                return new ByARpt() { Error = false, Filas = 0, id = "0", Mensaje = "File uploaded successfully!" };
            }
            else
            {
                return new ByARpt() { Error = true, Filas = 0, id = "0", Mensaje = "Formato invalido de archivo" };

            }
        }*/
    }
}
