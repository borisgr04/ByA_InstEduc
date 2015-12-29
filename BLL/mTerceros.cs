using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades.Vistas;
using Entidades.Consultas;
using Entidades.Security;
using DAL;
using ByA;
using AutoMapper;
using BLL.Security;

namespace BLL
{
    public class mTerceros
    {
        ieEntities ctx;
        public mTerceros()
        {
            Mapper.CreateMap<terceros,tercerosDto>();
            Mapper.CreateMap<tercerosDto,terceros>();

            Mapper.CreateMap<estudiantes, estudiantesDto>()
                .ForMember(dest => dest.nombre_grado, obj => obj.MapFrom(src => src.id_ultima_matricula != null ? src.matriculas.Where(t => t.id == src.id_ultima_matricula).FirstOrDefault().cursos.grados.nombre : "Ninguno"))
                .ForMember(dest => dest.nombre_curso, obj => obj.MapFrom(src => src.id_ultima_matricula != null ? src.matriculas.Where(t => t.id == src.id_ultima_matricula).FirstOrDefault().cursos.nombre : "Ninguno"))
                .ForMember(dest => dest.nombre_completo, obj => obj.MapFrom(src => src.terceros.apellido + " " + src.terceros.nombre));
            Mapper.CreateMap<estudiantesDto, estudiantes>();
        }

        public List<estudiantesDto> GetEstudiantesAcudientes(string identificacionAcudiente)
        {
            
            using (ctx = new ieEntities())
            {
                terceros acudiente = ctx.terceros.Where(t => t.identificacion == identificacionAcudiente).FirstOrDefault();
                List<estudiantesDto> listEstudiantesDto = new List<estudiantesDto>();
                if(acudiente != null)
                {
                    List<estudiantes> listEstudiantes = ctx.estudiantes.Where(t => t.id_acudiente == acudiente.id).ToList();
                    Mapper.Map(listEstudiantes, listEstudiantesDto);
                    return listEstudiantesDto;
                }
                else
                {
                    listEstudiantesDto = null;
                    return listEstudiantesDto;
                }
            }
        }

        public List<tercerosDto> GetsAcudientes()
        {
            using(ctx = new ieEntities())
            {
                List<tercerosDto> listTercerosDto = new List<tercerosDto>();
                List<terceros> listTerceros = ctx.terceros.Where(t => t.estudiantes3.Count() > 0).ToList();
                Mapper.Map(listTerceros, listTercerosDto);
               
                gesUsuarios gUsuarios = new gesUsuarios();
                List<USUARIOS_DTO> listUsuariosDto = gUsuarios.GetUsuarios("");

                List<tercerosDto> newListTercerosDto = new List<tercerosDto>();
                
                foreach (var item in listTercerosDto) 
                {
                    if(listUsuariosDto.Where(t=> t.USERNAME == item.identificacion).FirstOrDefault() == null)
                    {
                        newListTercerosDto.Add(item);
                    }
                }
                return newListTercerosDto;
            }
        }
        public ByARpt CrearUsuariosAcudientes(List<tercerosDto> lReg)
        {
            try
            {
                gesUsuarios GestionUsuario = new gesUsuarios();
                foreach (tercerosDto tercero in lReg)
                {
                    if (tercero.identificacion != null && tercero.identificacion != "")
                    {

                        Entidades.Security.USUARIOS_DTO Usuario = new Entidades.Security.USUARIOS_DTO()
                        {
                            EMAIL = tercero.email,
                            USERNAME = tercero.identificacion,
                            PASSWORD = tercero.identificacion + ".",
                            TERCERO = tercero.nombre + " " + tercero.apellido
                        };
                        GestionUsuario.InsUsuarios(Usuario);
                        GestionUsuario.GuardarRoles(GetRolesAcudientes(), Usuario.USERNAME);
                    }
                }
                ByARpt res = new ByARpt();
                res.Error = false;
                res.Mensaje = "Se asignaron los usuarios correctamente";
                return res;
            }
            catch
            {
                ByARpt res = new ByARpt();
                res.Error = true;
                res.Mensaje = "Ha ocurrido un error al intentar asignar los usuarios";
                return res;
            }
        }
        public List<Entidades.Security.ModuloRoles> GetRolesAcudientes()
        {
            List<Entidades.Security.ModuloRoles> Permisos = new List<Entidades.Security.ModuloRoles>() 
            { 
                new Entidades.Security.ModuloRoles() { hasRol = true, Modulo = "INICI", Roles = "INICIAcudientes" },
                new Entidades.Security.ModuloRoles() { hasRol = true, Modulo = "ACUDI", Roles = "ACUDIEstudiantes" },
                new Entidades.Security.ModuloRoles() { hasRol = true, Modulo = "ACUDI", Roles = "ACUDIMatriculas" },
                new Entidades.Security.ModuloRoles() { hasRol = true, Modulo = "ACUDI", Roles = "ACUDILiquidaciones" },
                new Entidades.Security.ModuloRoles() { hasRol = true, Modulo = "ACUDI", Roles = "ACUDIEstadoCuenta" },
                new Entidades.Security.ModuloRoles() { hasRol = true, Modulo = "ACUDI", Roles = "ACUDIPagosEstudiante" },
                new Entidades.Security.ModuloRoles() { hasRol = true, Modulo = "ACUDI", Roles = "ACUDISeguridad" }
            };
            return Permisos;
        }

        public List<tercerosDto> Gets()
        {
            using (ctx = new ieEntities())
            {
                List<tercerosDto> lr = new List<tercerosDto>();
                List<terceros> l = ctx.terceros.ToList();
                Mapper.Map(l, lr);
                return lr;
            }
        }
        public tercerosDto GetIdentificacion(string Id)
        {
            using (ctx = new ieEntities())
            {
                tercerosDto r = new tercerosDto();
                terceros o = ctx.terceros.Where(t => t.identificacion == Id).FirstOrDefault();
                if (o != null) Mapper.Map(o, r);
                else r = null;
                return r;
            }
        }
        public tercerosDto GetId(int Id)
        {
            using (ctx = new ieEntities())
            {
                tercerosDto r = new tercerosDto();
                terceros o = ctx.terceros.Where(t => t.id == Id).FirstOrDefault();
                Mapper.Map(o, r);
                return r;
            }
        }
        public ByARpt Insert(tercerosDto Reg)
        {
            cmdInsert o = new cmdInsert();
            o.oDto = Reg;
            return o.Enviar();
        }
        public ByARpt Update(tercerosDto Reg)
        {
            cmdUpdate o = new cmdUpdate();
            o.oDto = Reg;
            return o.Enviar();
        }
        public rptNewTercero InsertOrUpdate(ieEntities ctx, tercerosDto Reg, int idult_ter, int idult_tip_ter, List<string> tipos_tercero)
        {
            rptNewTercero rpt = new rptNewTercero();
            terceros tercero = ctx.terceros.Where(t => t.identificacion == Reg.identificacion).FirstOrDefault();
            if (tercero == null)
            {
                idult_ter++;                
                Reg.id = idult_ter;
                tercero = new terceros();
                Mapper.Map(Reg, tercero);

                foreach (string tipo_tercero in tipos_tercero)
                {
                    idult_tip_ter++;
                    detalles_tipos_tercero tp_ter = new detalles_tipos_tercero();
                    tp_ter.id = idult_tip_ter;
                    tp_ter.id_tercero = tercero.id;
                    tp_ter.nombre_tipo = tipo_tercero;
                    tercero.detalles_tipos_tercero.Add(tp_ter);
                }

                rpt.tercero = tercero;
                rpt.ultid_ter = idult_ter;
                rpt.ultid_tip_ter = idult_tip_ter;

                return rpt;
            }
            else
            {
                Reg.id = tercero.id;
                Mapper.Map(Reg,tercero);

                foreach (string tipo_tercero in tipos_tercero)
                {
                    detalles_tipos_tercero tipo = tercero.detalles_tipos_tercero.Where(t => t.nombre_tipo == tipo_tercero).FirstOrDefault();
                    if (tipo == null)
                    {
                        idult_tip_ter++;
                        detalles_tipos_tercero tp_ter = new detalles_tipos_tercero();
                        tp_ter.id = idult_tip_ter;
                        tp_ter.id_tercero = tercero.id;
                        tp_ter.nombre_tipo = tipo_tercero;
                        tercero.detalles_tipos_tercero.Add(tp_ter);
                    }
                }
                rpt.tercero = tercero;
                rpt.ultid_ter = idult_ter;
                rpt.ultid_tip_ter = idult_tip_ter;

                return rpt;
            }
        }
        public rptNewTercero InsertOrUpdateXId(ieEntities ctx, tercerosDto Reg, int idult_ter, int idult_tip_ter, List<string> tipos_tercero)
        {
            rptNewTercero rpt = new rptNewTercero();
            terceros tercero;
            if (Reg.id == 0)
            {
                idult_ter++;
                Reg.id = idult_ter;
                tercero = new terceros();
                Mapper.Map(Reg, tercero);

                foreach (string tipo_tercero in tipos_tercero)
                {
                    idult_tip_ter++;
                    detalles_tipos_tercero tp_ter = new detalles_tipos_tercero();
                    tp_ter.id = idult_tip_ter;
                    tp_ter.id_tercero = tercero.id;
                    tp_ter.nombre_tipo = tipo_tercero;
                    tercero.detalles_tipos_tercero.Add(tp_ter);
                }

                rpt.tercero = tercero;
                rpt.ultid_ter = idult_ter;
                rpt.ultid_tip_ter = idult_tip_ter;

                return rpt;
            }else{
                tercero = ctx.terceros.Where(t => t.id == Reg.id).FirstOrDefault();
                Reg.id = tercero.id;
                Mapper.Map(Reg, tercero);

                foreach (string tipo_tercero in tipos_tercero)
                {
                    detalles_tipos_tercero tipo = tercero.detalles_tipos_tercero.Where(t => t.nombre_tipo == tipo_tercero).FirstOrDefault();
                    if (tipo == null)
                    {
                        idult_tip_ter++;
                        detalles_tipos_tercero tp_ter = new detalles_tipos_tercero();
                        tp_ter.id = idult_tip_ter;
                        tp_ter.id_tercero = tercero.id;
                        tp_ter.nombre_tipo = tipo_tercero;
                        tercero.detalles_tipos_tercero.Add(tp_ter);
                    }
                }
                rpt.tercero = tercero;
                rpt.ultid_ter = idult_ter;
                rpt.ultid_tip_ter = idult_tip_ter;

                return rpt;
            }
        }
        public rptNewTercero InsertSinIdentificacion(ieEntities ctx, tercerosDto Reg, int idult_ter, int idult_tip_ter, List<string> tipos_tercero)
        {
            rptNewTercero rpt = new rptNewTercero();
            terceros tercero;
            idult_ter++;
            Reg.id = idult_ter;
            tercero = new terceros();
            Mapper.Map(Reg, tercero);
            
            foreach (string tipo_tercero in tipos_tercero)
            {
                idult_tip_ter++;
                detalles_tipos_tercero tp_ter = new detalles_tipos_tercero();
                tp_ter.id = idult_tip_ter;
                tp_ter.id_tercero = tercero.id;
                tp_ter.nombre_tipo = tipo_tercero;
                tercero.detalles_tipos_tercero.Add(tp_ter);
            }

            rpt.tercero = tercero;
            rpt.ultid_ter = idult_ter;
            rpt.ultid_tip_ter = idult_tip_ter;
            return rpt;
        }
        public ByARpt InsertOrUpdate(tercerosDto Reg)
        {
            using (ctx = new ieEntities())
            {

                terceros tercero = ctx.terceros.Where(t => t.identificacion == Reg.identificacion).FirstOrDefault();
                if (tercero == null)
                {
                    cmdInsert o = new cmdInsert();
                    o.oDto = Reg;
                    return o.Enviar();
                }
                else
                {
                    cmdUpdate o = new cmdUpdate();
                    o.oDto = Reg;
                    return o.Enviar();
                }
            }
        }
        class cmdInsert : absTemplate
        {
            public tercerosDto oDto { get; set; }
            private terceros Dto { get; set; }

            #region ImplementaciónMetodosAbstractos
            protected internal override bool esValido()
            {
                Dto = ctx.terceros.Where(t => t.identificacion == oDto.identificacion).FirstOrDefault();
                if (Dto == null) return true;
                else
                {
                    byaRpt.Error = true;
                    byaRpt.Mensaje = "El tercero ya se encuentra registrado";
                    return false;
                }
            }
            protected internal override void Antes()
            {
                int ultId = 0;
                try
                {
                    ultId = ctx.terceros.Max(t => t.id);
                }
                catch { }
                ultId++;

                oDto.id = ultId;

                Dto = new terceros();
                Mapper.Map(oDto, Dto);
                ctx.terceros.Add(Dto);
            }
            #endregion
        }
        class cmdUpdate : absTemplate
        {
            public tercerosDto oDto { get; set; }
            private terceros Dto { get; set; }
            #region ImplementaciónMetodosAbstractos
            protected internal override bool esValido()
            {
                Dto = ctx.terceros.Where(t => t.identificacion == oDto.identificacion).FirstOrDefault();
                if (Dto != null) return true;
                else
                {
                    byaRpt.Error = true;
                    byaRpt.Mensaje = "El tercero no se encuentra registrado";
                    return false;
                }
            }
            protected internal override void Antes()
            {
                Mapper.Map(oDto,Dto);
            }
            #endregion
        }
    }
}
