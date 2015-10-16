using BLL;
using Entidades.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspIdentity.Controllers
{
    [RoutePrefix("api/GruposPagos")]
    public class GruposPagosController : ApiController
    {
        [Route("")]
        public List<grupos_pagosDto> Gets()
        {
            mGruposPagos o = new mGruposPagos();
            return o.Gets();
        }
    }
}
