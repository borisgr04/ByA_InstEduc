using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades.Vistas;
using DAL;
using ByA;
using AutoMapper;
using Entidades.Consultas;

namespace BLL
{
    public class mEstudiantes
    {
        ieEntities ctx;
        public mEstudiantes()
        {
            Mapper.CreateMap<terceros, tercerosDto>();
            Mapper.CreateMap<estudiantesDto, estudiantes>();
            Mapper.CreateMap<estudiantes, estudiantesDto>()
                .ForMember(dest => dest.nombre_completo, obj => obj.MapFrom(src => src.terceros.apellido + " " + src.terceros.nombre))
                .ForMember(dest => dest.nombre_grado, obj => obj.MapFrom(src => src.id_ultima_matricula != null ? src.matriculas.Where(t=> t.id == src.id_ultima_matricula).FirstOrDefault().cursos.grados.nombre : "Ninguno"))
                .ForMember(dest => dest.nombre_curso, obj => obj.MapFrom(src => src.id_ultima_matricula != null ? src.matriculas.Where(t => t.id == src.id_ultima_matricula).FirstOrDefault().cursos.nombre : "Ninguno"));
        }
        public estudiantesDto Get(string Id)
        {
            using (ctx = new ieEntities())
            {
                estudiantesDto r = new estudiantesDto();
                estudiantes o = ctx.estudiantes.Where(t => t.identificacion == Id).FirstOrDefault();
                if (o != null)
                {
                    Mapper.Map(o, r);

                    if (r.id_ultima_matricula != null)
                    {
                        r.nombre_curso = ctx.matriculas.Where(t => t.id == r.id_ultima_matricula).FirstOrDefault().cursos.nombre;
                        r.nombre_grado = ctx.matriculas.Where(t => t.id == r.id_ultima_matricula).FirstOrDefault().cursos.grados.nombre;
                    }

                    Mapper.Map(o.terceros, r.terceros);
                    Mapper.Map(o.terceros1, r.terceros1);
                    Mapper.Map(o.terceros2, r.terceros2);
                    Mapper.Map(o.terceros3, r.terceros3);                    
                }
                return r;
            }
        }
        public estudiantesDto GetXId(int Id)
        {
            using (ctx = new ieEntities())
            {
                estudiantesDto r = new estudiantesDto();
                estudiantes o = ctx.estudiantes.Where(t => t.id == Id).FirstOrDefault();
                if (o != null)
                {
                    Mapper.Map(o, r);

                    if (r.id_ultima_matricula != null)
                    {
                        r.nombre_curso = ctx.matriculas.Where(t => t.id == r.id_ultima_matricula).FirstOrDefault().cursos.nombre;
                        r.nombre_grado = ctx.matriculas.Where(t => t.id == r.id_ultima_matricula).FirstOrDefault().cursos.grados.nombre;
                    }

                    Mapper.Map(o.terceros, r.terceros);
                    Mapper.Map(o.terceros1, r.terceros1);
                    Mapper.Map(o.terceros2, r.terceros2);
                    Mapper.Map(o.terceros3, r.terceros3);
                }
                return r;
            }
        }
        public List<estudiantesDto> GetsFiltro(string Filtro)
        {
            using (ctx = new ieEntities())
            {
                List<estudiantesDto> lr = new List<estudiantesDto>();
                List<estudiantes> l = ctx.estudiantes.Where(t => t.codigo.Contains(Filtro) || t.identificacion.Contains(Filtro) || (t.terceros.nombre.Trim().ToUpper() + " " + t.terceros.apellido.Trim().ToUpper()).Contains(Filtro.Trim().ToUpper())).ToList();
                Mapper.Map(l,lr);
                return lr;
            }
        }
        public cEstudiantesOrden GetsOrden(int Orden, string filtro = "")
        {
            using (ctx = new ieEntities())
            {
                List<estudiantesDto> lr = new List<estudiantesDto>();
                List<estudiantes> l;
                if(filtro == "") l = ctx.estudiantes.ToList();
                else l = ctx.estudiantes.Where(t => t.codigo.ToUpper().Contains(filtro.ToUpper()) || t.identificacion.ToUpper().Contains(filtro.ToUpper()) || t.terceros.nombre.ToUpper().Contains(filtro.ToUpper()) || t.terceros.apellido.ToUpper().Contains(filtro.ToUpper())).ToList();
                if (l.Count() > 0)
                {
                    int Inicial = Orden * 20;
                    int Final = Inicial + 19;
                    int i = Inicial;
                    bool ult = false;
                    while (i < l.Count() && i <= Final)
                    {
                        estudiantesDto o = new estudiantesDto();
                        Mapper.Map(l[i], o);
                        if (i == l.Count() - 1) ult = true;
                        lr.Add(o);
                        i++;
                    }
                    cEstudiantesOrden objResEstudiantes = new cEstudiantesOrden();
                    objResEstudiantes.Ultimo = ult;
                    objResEstudiantes.lEstudiantes = lr;
                    return objResEstudiantes;
                }
                else
                {
                    cEstudiantesOrden objResEstudiantes = new cEstudiantesOrden();
                    objResEstudiantes.Ultimo = true;
                    objResEstudiantes.lEstudiantes = lr;
                    return objResEstudiantes;
                }
            }
        }
        public int GetGrupoPago(string id_estudiante)
        {
            mCausacion.Causar(id_estudiante);
            using (ctx = new ieEntities())
            {
                int id_grupo = 0;
                List<grupos_pagos> lGrupos = ctx.grupos_pagos.OrderBy(t => t.prioridad).ToList();
                foreach (grupos_pagos item in lGrupos)
                {
                    List<carterap> lCartera = ctx.carterap.Where(t => t.id_estudiante == id_estudiante && t.id_grupo == item.id && t.pagado < t.valor && t.estado == "CA").ToList();
                    if (lCartera.Count() > 0)
                    {
                        id_grupo = item.id;
                        return item.id;
                    }
                }
                return id_grupo;
            }
        }
        public List<estudiantesDto> Gets()
        {
            using (ctx = new ieEntities())
            {
                List<estudiantesDto> r = new List<estudiantesDto>();
                List<estudiantes> o = ctx.estudiantes.ToList();
                Mapper.Map(o,r);
                return r;
            }
        }
        public ByARpt Insert(estudiantesDto Reg)
        {
            cmdInsert o = new cmdInsert();
            o.oDto = Reg;
            return o.Enviar();
        }
        public ByARpt Update(estudiantesDto Reg)
        {
            cmdUpdate o = new cmdUpdate();
            o.oDto = Reg;
            return o.Enviar();
        }
        class cmdInsert : absTemplate
        {
            public estudiantesDto oDto { get; set; }
            estudiantes Dto { get; set; }
            acudientesDto acudiente { get; set; }            
            #region ImplementaciónMetodosAbstractos
            protected internal override bool esValido()
            {
                Dto = ctx.estudiantes.Where(t => t.identificacion == oDto.identificacion).FirstOrDefault();
                if (Dto == null)
                {
                    Dto = ctx.estudiantes.Where(t => t.codigo == oDto.codigo).FirstOrDefault();
                    if (Dto == null) return true;
                    else
                    {
                        byaRpt.Error = true;
                        byaRpt.Mensaje = "Ya se encuentra registrado un estudiante con este código";
                        return false;
                    }
                }
                else
                {
                    byaRpt.Error = true;
                    byaRpt.Mensaje = "Ya se encuentra registrado un estudiante con esta identificación";
                    return false;
                }
            }
            protected internal override void Antes()
            {
                int ultId_est;
                int ultId_ter;
                int ultId_tip_ter;
                UltsIds(out ultId_est, out ultId_ter, out ultId_tip_ter);

                Dto = new estudiantes();
                oDto.terceros.identificacion = oDto.identificacion;
                // Creo o modifico el tercero del estudiante
                mTerceros objTer = new mTerceros();
                terceros tercero_acudiente = null;
                terceros tercero_madre = null;
                terceros tercero_padre = null;
                bool rep = false;
                string acudi = "";

                rptNewTercero obj_Res_Ter = new rptNewTercero();

                List<string> lTipos;
                terceros tercero_estudiante;
                CrearEstudiante(ref ultId_ter, ref ultId_tip_ter, objTer, ref rep, ref obj_Res_Ter, out lTipos, out tercero_estudiante);

                CrearOModificarMadre(ref ultId_ter, ref ultId_tip_ter, objTer, ref tercero_madre, ref rep, ref acudi, ref obj_Res_Ter, ref lTipos);

                CrearOModificarPadre(ref ultId_ter, ref ultId_tip_ter, objTer, ref tercero_padre, ref rep, ref acudi, ref obj_Res_Ter, ref lTipos);

                CreoOModificoAcudiente(ref ultId_ter, ref ultId_tip_ter, objTer, ref tercero_acudiente, tercero_madre, tercero_padre, rep, acudi, ref obj_Res_Ter, ref lTipos, tercero_estudiante);

                LimpioTercerosParaMapear();

                ultId_est++;
                oDto.id = ultId_est;
                Mapper.Map(oDto, Dto);

                AsignoTerceros(tercero_acudiente, tercero_madre, tercero_padre, tercero_estudiante);

                ctx.estudiantes.Add(Dto);
            }
            private void AsignoTerceros(terceros tercero_acudiente, terceros tercero_madre, terceros tercero_padre, terceros tercero_estudiante)
            {
                Dto.terceros = tercero_estudiante;
                Dto.terceros1 = tercero_madre;
                Dto.terceros2 = tercero_padre;
                Dto.terceros3 = tercero_acudiente;
            }
            private void LimpioTercerosParaMapear()
            {
                oDto.terceros = null;
                oDto.terceros1 = null;
                oDto.terceros2 = null;
                oDto.terceros3 = null;
            }
            private void CreoOModificoAcudiente(ref int ultId_ter, ref int ultId_tip_ter, mTerceros objTer, ref terceros tercero_acudiente, terceros tercero_madre, terceros tercero_padre, bool rep, string acudi, ref rptNewTercero obj_Res_Ter, ref List<string> lTipos, terceros tercero_estudiante)
            {
                if (rep == false)
                {
                    // Creo o modifico el tercero del acudiente
                    lTipos = new List<string>();
                    lTipos.Add("ACUDIENTE");
                    obj_Res_Ter = objTer.InsertOrUpdate(ctx, oDto.terceros3, ultId_ter, ultId_tip_ter, lTipos);
                    tercero_acudiente = obj_Res_Ter.tercero;
                    ultId_ter = obj_Res_Ter.ultid_ter;
                    ultId_tip_ter = obj_Res_Ter.ultid_tip_ter;
                }
                else
                {
                    if (acudi == "ESTUDIANTE") tercero_acudiente = tercero_estudiante;
                    if (acudi == "MADRE") tercero_acudiente = tercero_madre;
                    if (acudi == "PADRE") tercero_acudiente = tercero_padre;
                }
            }
            private void CrearOModificarPadre(ref int ultId_ter, ref int ultId_tip_ter, mTerceros objTer, ref terceros tercero_padre, ref bool rep, ref string acudi, ref rptNewTercero obj_Res_Ter, ref List<string> lTipos)
            {
                if (oDto.terceros2 != null)
                {
                    // Creo o modifico el tercero del padre
                    lTipos = new List<string>();
                    lTipos.Add("PADRE");
                    if (oDto.terceros2.identificacion == oDto.terceros3.identificacion)
                    {
                        rep = true;
                        acudi = "PADRE";
                        lTipos.Add("ACUDIENTE");
                    }
                    if ((oDto.terceros2.identificacion != null) && (oDto.terceros2.identificacion != "") && (oDto.terceros2.identificacion != "0")) obj_Res_Ter = objTer.InsertOrUpdate(ctx, oDto.terceros2, ultId_ter, ultId_tip_ter, lTipos);
                    else obj_Res_Ter = objTer.InsertSinIdentificacion(ctx, oDto.terceros2, ultId_ter, ultId_tip_ter, lTipos);
                    tercero_padre = obj_Res_Ter.tercero;
                    ultId_ter = obj_Res_Ter.ultid_ter;
                    ultId_tip_ter = obj_Res_Ter.ultid_tip_ter;
                }
            }
            private void CrearOModificarMadre(ref int ultId_ter, ref int ultId_tip_ter, mTerceros objTer, ref terceros tercero_madre, ref bool rep, ref string acudi, ref rptNewTercero obj_Res_Ter, ref List<string> lTipos)
            {
                if (oDto.terceros1 != null)
                {
                    // Creo o modifico el tercero de la madre
                    lTipos = new List<string>();
                    lTipos.Add("MADRE");
                    if (oDto.terceros1.identificacion == oDto.terceros3.identificacion)
                    {
                        rep = true;
                        acudi = "MADRE";
                        lTipos.Add("ACUDIENTE");
                    }
                    if ((oDto.terceros1.identificacion != null) && (oDto.terceros1.identificacion != "") && (oDto.terceros1.identificacion != "0")) obj_Res_Ter = objTer.InsertOrUpdate(ctx, oDto.terceros1, ultId_ter, ultId_tip_ter, lTipos);
                    else obj_Res_Ter = objTer.InsertSinIdentificacion(ctx, oDto.terceros1, ultId_ter, ultId_tip_ter, lTipos);
                    tercero_madre = obj_Res_Ter.tercero;
                    ultId_ter = obj_Res_Ter.ultid_ter;
                    ultId_tip_ter = obj_Res_Ter.ultid_tip_ter;
                }
            }
            private void CrearEstudiante(ref int ultId_ter, ref int ultId_tip_ter, mTerceros objTer, ref bool rep, ref rptNewTercero obj_Res_Ter, out List<string> lTipos, out terceros tercero_estudiante)
            {
                lTipos = new List<string>();
                lTipos = new List<string>();
                lTipos.Add("ESTUDIANTE");
                if (oDto.identificacion == oDto.terceros3.identificacion)
                {
                    rep = true;
                    lTipos.Add("ACUDIENTE");
                }
                obj_Res_Ter = objTer.InsertOrUpdate(ctx, oDto.terceros, ultId_ter, ultId_tip_ter, lTipos);
                tercero_estudiante = obj_Res_Ter.tercero;
                ultId_ter = obj_Res_Ter.ultid_ter;
                ultId_tip_ter = obj_Res_Ter.ultid_tip_ter;
            }
            private void UltsIds(out int ultId_est, out int ultId_ter, out int ultId_tip_ter)
            {
                ultId_est = 0;
                ultId_ter = 0;
                ultId_tip_ter = 0;
                try { ultId_est = ctx.estudiantes.Max(t => t.id); }
                catch { }
                try { ultId_ter = ctx.terceros.Max(t => t.id); }
                catch { }
                try { ultId_tip_ter = ctx.detalles_tipos_tercero.Max(t => t.id); }
                catch { }
            }            
            #endregion
        }
        class cmdUpdate : absTemplate
        {
            public estudiantesDto oDto { get; set; }
            estudiantes Dto { get; set; }
            acudientesDto acudiente { get; set; }

            #region ImplementaciónMetodosAbstractos
            protected internal override bool esValido()
            {
                Dto = ctx.estudiantes.Where(t => t.terceros.id == oDto.terceros.id).FirstOrDefault();
                if (Dto != null)
                {
                    if (Dto.terceros.identificacion != oDto.terceros.identificacion)
                    {
                        terceros terceroIde = ctx.terceros.Where(t => t.identificacion == oDto.terceros.identificacion).FirstOrDefault();
                        if (terceroIde == null)
                        {
                            if (oDto.codigo != Dto.codigo)
                            {
                                estudiantes Est = ctx.estudiantes.Where(t => t.codigo == oDto.codigo).FirstOrDefault();
                                if (Est == null) return true;
                                else
                                {
                                    byaRpt.Error = true;
                                    byaRpt.Mensaje = "No se puede asignar ese código al estudiante porque ya le pertenece a otro estudiante";
                                    return false;
                                }
                            }
                            else return true;
                        }
                        else
                        {
                            byaRpt.Error = true;
                            byaRpt.Mensaje = "Esta intentando cambiar la identificación por otra que ya esta asignada a otra persona";
                            return false;
                        }
                    }
                    else
                    {
                        if (oDto.codigo != Dto.codigo)
                        {
                            estudiantes Est = ctx.estudiantes.Where(t => t.codigo == oDto.codigo).FirstOrDefault();
                            if (Est == null) return true;
                            else
                            {
                                byaRpt.Error = true;
                                byaRpt.Mensaje = "No se puede asignar ese código al estudiante porque ya le pertenece a otro estudiante";
                                return false;
                            }
                        }
                        else return true;
                    }
                }
                else
                {
                    byaRpt.Error = true;
                    byaRpt.Mensaje = "No se encuentra registrado un estudiante con esta identificación";
                    return false;
                }
            }
            protected internal override void Antes()
            {
                int ultId_ter;
                int ultId_tip_ter;
                UltsIds(out ultId_ter, out ultId_tip_ter);

                oDto.terceros.identificacion = oDto.identificacion;

                mTerceros objTer;
                bool rep;
                string acudi;
                rptNewTercero obj_Res_Ter;
                terceros tercero_acudiente;
                terceros tercero_padre;
                terceros tercero_madre;
                List<string> lTipos;
                terceros tercero_estudiante;
                CrearOModificarEstudiante(ref ultId_ter, ref ultId_tip_ter, out objTer, out rep, out acudi, out obj_Res_Ter, out tercero_acudiente, out tercero_padre, out tercero_madre, out lTipos, out tercero_estudiante);

                CrearOModificarMadre(ref ultId_ter, ref ultId_tip_ter, objTer, ref rep, ref acudi, ref obj_Res_Ter, ref tercero_madre, ref lTipos);

                CrearOModificarPadre(ref ultId_ter, ref ultId_tip_ter, objTer, ref rep, ref acudi, ref obj_Res_Ter, ref tercero_padre, ref lTipos);

                CrearOModificarAcudiente(ref ultId_ter, ref ultId_tip_ter, objTer, rep, acudi, ref obj_Res_Ter, ref tercero_acudiente, tercero_padre, tercero_madre, ref lTipos, tercero_estudiante);

                LimpiarTercerosParaMapear();

                oDto.id = Dto.id;
                Mapper.Map(oDto, Dto);

                AsignoTerceros(tercero_acudiente, tercero_padre, tercero_madre, tercero_estudiante);

                AsignarIdsTercerosAEstudiante();
            }
            private void AsignarIdsTercerosAEstudiante()
            {
                if (Dto.terceros != null) Dto.id_tercero_estudiante = Dto.terceros.id;

                if (Dto.terceros1 != null) Dto.id_tercero_madre = Dto.terceros1.id;
                else Dto.id_tercero_madre = null;

                if (Dto.terceros2 != null) Dto.id_tercero_padre = Dto.terceros2.id;
                else Dto.id_tercero_padre = null;

                if (Dto.terceros3 != null) Dto.id_acudiente = Dto.terceros3.id;
            }
            private void AsignoTerceros(terceros tercero_acudiente, terceros tercero_padre, terceros tercero_madre, terceros tercero_estudiante)
            {
                Dto.terceros = tercero_estudiante;
                Dto.terceros1 = tercero_madre;
                Dto.terceros2 = tercero_padre;
                Dto.terceros3 = tercero_acudiente;
            }
            private void LimpiarTercerosParaMapear()
            {
                oDto.terceros = null;
                oDto.terceros1 = null;
                oDto.terceros2 = null;
                oDto.terceros3 = null;
            }
            private void CrearOModificarAcudiente(ref int ultId_ter, ref int ultId_tip_ter, mTerceros objTer, bool rep, string acudi, ref rptNewTercero obj_Res_Ter, ref terceros tercero_acudiente, terceros tercero_padre, terceros tercero_madre, ref List<string> lTipos, terceros tercero_estudiante)
            {
                if (rep == false)
                {
                    // Creo o modifico el tercero del acudiente
                    lTipos = new List<string>();
                    lTipos.Add("ACUDIENTE");
                    obj_Res_Ter = objTer.InsertOrUpdateXId(ctx, oDto.terceros3, ultId_ter, ultId_tip_ter, lTipos);
                    tercero_acudiente = obj_Res_Ter.tercero;
                    ultId_ter = obj_Res_Ter.ultid_ter;
                    ultId_tip_ter = obj_Res_Ter.ultid_tip_ter;
                }
                else
                {
                    if (acudi == "ESTUDIANTE") tercero_acudiente = tercero_estudiante;
                    if (acudi == "MADRE") tercero_acudiente = tercero_madre;
                    if (acudi == "PADRE") tercero_acudiente = tercero_padre;
                }
            }
            private void CrearOModificarPadre(ref int ultId_ter, ref int ultId_tip_ter, mTerceros objTer, ref bool rep, ref string acudi, ref rptNewTercero obj_Res_Ter, ref terceros tercero_padre, ref List<string> lTipos)
            {
                if (oDto.terceros2 != null)
                {
                    // Creo o modifico el tercero del padre
                    lTipos = new List<string>();
                    lTipos.Add("PADRE");
                    if (oDto.terceros2.identificacion == oDto.terceros3.identificacion)
                    {
                        rep = true;
                        acudi = "PADRE";
                        lTipos.Add("ACUDIENTE");
                    }
                    obj_Res_Ter = objTer.InsertOrUpdateXId(ctx, oDto.terceros2, ultId_ter, ultId_tip_ter, lTipos);
                    tercero_padre = obj_Res_Ter.tercero;
                    ultId_ter = obj_Res_Ter.ultid_ter;
                    ultId_tip_ter = obj_Res_Ter.ultid_tip_ter;
                }
            }
            private void CrearOModificarMadre(ref int ultId_ter, ref int ultId_tip_ter, mTerceros objTer, ref bool rep, ref string acudi, ref rptNewTercero obj_Res_Ter, ref terceros tercero_madre, ref List<string> lTipos)
            {
                if (oDto.terceros1 != null)
                {
                    // Creo o modifico el tercero de la madre
                    lTipos = new List<string>();
                    lTipos.Add("MADRE");
                    if (oDto.terceros1.identificacion == oDto.terceros3.identificacion)
                    {
                        rep = true;
                        acudi = "MADRE";
                        lTipos.Add("ACUDIENTE");
                    }
                    obj_Res_Ter = objTer.InsertOrUpdateXId(ctx, oDto.terceros1, ultId_ter, ultId_tip_ter, lTipos);
                    tercero_madre = obj_Res_Ter.tercero;
                    ultId_ter = obj_Res_Ter.ultid_ter;
                    ultId_tip_ter = obj_Res_Ter.ultid_tip_ter;
                }
            }
            private void CrearOModificarEstudiante(ref int ultId_ter, ref int ultId_tip_ter, out mTerceros objTer, out bool rep, out string acudi, out rptNewTercero obj_Res_Ter, out terceros tercero_acudiente, out terceros tercero_padre, out terceros tercero_madre, out List<string> lTipos, out terceros tercero_estudiante)
            {
                // Creo o modifico el tercero del estudiante
                objTer = new mTerceros();
                rep = false;
                acudi = "";

                obj_Res_Ter = new rptNewTercero();
                tercero_acudiente = new terceros();
                tercero_padre = null;
                tercero_madre = null;
                lTipos = new List<string>();
                lTipos = new List<string>();
                lTipos.Add("ESTUDIANTE");
                if (oDto.identificacion == oDto.terceros3.identificacion)
                {
                    rep = true;
                    acudi = "ESTUDIANTE";
                    lTipos.Add("ACUDIENTE");
                }
                obj_Res_Ter = objTer.InsertOrUpdateXId(ctx, oDto.terceros, ultId_ter, ultId_tip_ter, lTipos);
                tercero_estudiante = obj_Res_Ter.tercero;
                ultId_ter = obj_Res_Ter.ultid_ter;
                ultId_tip_ter = obj_Res_Ter.ultid_tip_ter;
            }
            private void UltsIds(out int ultId_ter, out int ultId_tip_ter)
            {
                ultId_ter = 0;
                ultId_tip_ter = 0;
                try { ultId_ter = ctx.terceros.Max(t => t.id); }
                catch { }
                try { ultId_tip_ter = ctx.detalles_tipos_tercero.Max(t => t.id); }
                catch { }
            }
            #endregion
        }
    }
}
