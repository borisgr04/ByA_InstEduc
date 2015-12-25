﻿using BLL;
using ByA;
using Entidades.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspIdentity.Controllers
{
    [RoutePrefix("api/Terceros")]
    public class TercerosController : ApiController
    {
        [Route("Acudientes")]
        public List<tercerosDto> GetsAcudientes()
        {
            mTerceros o = new mTerceros();
            return o.GetsAcudientes();
        }

        [Route("CrearUsuariosAcudientes")]
        public ByARpt PostCrearUsuariosAcudientes(List<tercerosDto> lReg)
        {
            mTerceros o = new mTerceros();
            return o.CrearUsuariosAcudientes(lReg);
        }

        [Route("EstudiantesAcudientes/{identificacionAcudiente}")]
        public List<estudiantesDto> GetEstudiantesAcudientes(string identificacionAcudiente)
        {
            mTerceros o = new mTerceros();
            return o.GetEstudiantesAcudientes(identificacionAcudiente);
        }

        [Route("")]
        public List<tercerosDto> Gets()
        {
            mTerceros o = new mTerceros();
            return o.Gets();
        }
        [Route("")]
        public ByARpt Post(tercerosDto Reg)
        {
            mTerceros o = new mTerceros();
            return o.Insert(Reg);
        }
        [Route("")]
        public ByARpt Put(tercerosDto Reg)
        {
            mTerceros o = new mTerceros();
            return o.Update(Reg);
        }
        [Route("{identificacion}")]
        public tercerosDto Get(string identificacion)
        {
            if (identificacion == "admin")
            {
                tercerosDto t = new tercerosDto();
                t.identificacion = "admin";
                t.nombre = "ADMINISTRADOR";
                t.ocupacion = "ADMINISTRADOR";
                return t;
            }
            else
            {
                mTerceros o = new mTerceros();
                return o.GetIdentificacion(identificacion);
            }
        }
    }
}
