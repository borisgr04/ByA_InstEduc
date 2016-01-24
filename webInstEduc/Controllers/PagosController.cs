using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entidades.Vistas;
using ByA;
using Entidades.Consultas;
using System.Net.Http.Headers;

namespace Skeleton.WebAPI.Controllers
{
    [RoutePrefix("api/Pagos")]
    public class PagosController : ApiController
    {
        [Route("Liquidaciones/Estudiante/{id_estudiante}/grupo/{id_grupo}")]
        public List<pagosDto> GetLiquidacionesEstudiante(string id_estudiante, int id_grupo)
        {
            mPagos o = new mPagos();
            return o.GetsLiquidacionesEstudiante(id_estudiante, id_grupo);
        }
        [Route("Liquidaciones/Estudiante/{id_estudiante}")]
        public List<pagosDto> GetLiquidacionesEstudiante(string id_estudiante)
        {
            mPagos o = new mPagos();
            return o.GetsLiquidacionesEstudiante(id_estudiante);
        }
        [Route("{id_liquidacion}")]
        public pagosDto GetLiquidacion(int id_liquidacion)
        {
            mPagos o = new mPagos();
            return o.GetLiquidacion(id_liquidacion);
        }
        [Route("NotasCredito/Estudiante/{id_estudiante}")]
        public List<notas_creditoDto> GetEstudiantes(string id_estudiante)
        {
            mPagos o = new mPagos();
            return o.GetNotasCredito(id_estudiante);
        }
        [Route("NotasCredito/Nota/{idNota}")]
        public notas_creditoDto GetEstudiantes(int idNota)
        {
            mPagos o = new mPagos();
            return o.GetNotaCredito(idNota);
        }
        [Route("Anular/{id_pago}")]
        public ByARpt PostAnularPago(int id_pago)
        {
            mPagos o = new mPagos();
            return o.AnularPago(id_pago, GetUser());
        }
        [Route("Anular/Liquidacion/{id_pago}")]
        public ByARpt PostAnularLiquidacion(int id_pago)
        {
            mPagos o = new mPagos();
            return o.AnularLiquidacion(id_pago, GetUser());
        } 
        [Route("Liquidar")]
        public ByARpt PostLiquidacion(pagosDto Reg)
        {
            Reg.usu = GetUser();
            mPagos o = new mPagos();
            return o.InsertLiquidacion(Reg);
        }
        [Route("Pagar")]
        public ByARpt PostPagar(pagosDto Reg)
        {
            Reg.usu = GetUser();
            mPagos o = new mPagos();
            return o.Pagar(Reg);
        }
        [Route("PagarLiquidacion")]
        public ByARpt PostPagarLiquidacion(pagosDto Reg)
        {
            Reg.usu = GetUser();
            mPagos o = new mPagos();
            return o.PagarLiquidacion(Reg);
        }
        [Route("NotaCredito")]
        public ByARpt PostPagarLiquidacion(notas_creditoDto Reg)
        {
            Reg.usu = GetUser();
            mPagos o = new mPagos();
            return o.InsertNotaCredito(Reg);
        }
        [Route("Estudiante")]
        public List<pagosDto> PostPagosEstudiante(bPagosEstudiante Reg)
        {
            mPagos o = new mPagos();
            return o.GetPagosEstudiante(Reg);
        }
        private string GetUser()
        {
            string sessionId = "";
            CookieHeaderValue cookie = Request.Headers.GetCookies("fc_user").FirstOrDefault();
            if (cookie != null) sessionId = cookie["fc_user"].Value;
            return sessionId;
        }
    }
}
