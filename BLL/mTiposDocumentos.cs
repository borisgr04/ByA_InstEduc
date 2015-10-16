using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades.Vistas;
using DAL;
using ByA;
using AutoMapper;

namespace BLL
{
    public class mTiposDocumentos
    {
        ieEntities ctx;
        public mTiposDocumentos()
        {
            Mapper.CreateMap<tiposDocumentosDto, tipos_documentos>();
            Mapper.CreateMap<tipos_documentos, tiposDocumentosDto>();
        }
        public tiposDocumentosDto Get(string id_tipo_documento)
        {
            using(ctx = new ieEntities())
            {
                tiposDocumentosDto r = new tiposDocumentosDto();
                tipos_documentos o = ctx.tipos_documentos.Where(t => t.id == id_tipo_documento).FirstOrDefault();
                Mapper.Map(o,r);
                return r;
            }
        }
        public List<tiposDocumentosDto> Gets()
        {
            using (ctx = new ieEntities())
            {
                List<tiposDocumentosDto> r = new List<tiposDocumentosDto>();
                List<tipos_documentos> o = ctx.tipos_documentos.ToList();
                Mapper.Map(o, r);
                foreach (tiposDocumentosDto item in r)
                    item.idOld = item.id;

                return r;
            }
        }
        public List<ByARpt> InsertsOrUpdates(List<tiposDocumentosDto> lReg)
        {
            List<ByARpt> lResp = new List<ByARpt>();
            foreach (tiposDocumentosDto item in lReg)
            {
                ByARpt res = new ByARpt();
                if (item.idOld == null)
                {
                    cmdInsert o = new cmdInsert();
                    o.oDto = item;
                    res = o.Enviar();
                }
                else
                {
                    cmdUpdate o = new cmdUpdate();
                    o.oDto = item;
                    res = o.Enviar();
                }
                lResp.Add(res);
            }
            return lResp;
        }
        class cmdInsert : absTemplate
        {
            tipos_documentos Dto { get; set; }
            public tiposDocumentosDto oDto { get; set; }
            int ultid_conceptos = 0;
            #region ImplementaciónMetodosAbstractos
            protected internal override bool esValido()
            {
                Dto = ctx.tipos_documentos.Where(t => t.nombre == oDto.nombre).FirstOrDefault();
                if (Dto == null) return true;
                else
                {
                    byaRpt.Mensaje = "Existe un tipo de documento con un nombre exactamente igual";
                    byaRpt.Error = true;
                    return false;
                }
            }
            protected internal override void Antes()
            {
                //UltIdTiposDocumentos();
                //ultid_conceptos++;
                //oDto.id = ultid_conceptos;
                Dto = new tipos_documentos();
                Mapper.Map(oDto,Dto);
                ctx.tipos_documentos.Add(Dto);
            }
            protected override void Despues()
            {
                byaRpt.Mensaje = "Operación Realizada Satisfactoriamente";
                byaRpt.id = Dto.id.ToString();
            }
            //private void UltIdTiposDocumentos()
            //{
            //    try
            //    {
            //        ultid_tipos_docuemntos = ctx.tipos.Max(t => t.id);
            //    }
            //    catch { }
            //}
            #endregion
        }
        class cmdUpdate : absTemplate
        {
            tipos_documentos Dto { get; set; }
            public tiposDocumentosDto oDto { get; set; }
            #region ImplementaciónMetodosAbstractos
            protected internal override bool esValido()
            {
                Dto = ctx.tipos_documentos.Where(t => t.id.Equals(oDto.idOld)).FirstOrDefault();
                if (Dto != null) return true;
                else
                {
                    byaRpt.Error = true;
                    byaRpt.Mensaje = "No existe ningun tipo de documento con este id";
                    return false;
                }
            }
            protected internal override void Antes()
            {
                Mapper.Map(oDto, Dto);
                
            }
            protected override void Despues()
            {
                byaRpt.Mensaje = "Operación Realizada Satisfactoriamente";
                byaRpt.id = Dto.id.ToString();
            }
            #endregion
        }
    }
}
