using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ByA;
using Entidades;
using Entidades.Security;
using System.Web.Security;
using DAL;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace BLL.Security
{
    public class gesUsuarios
    {
        ByARpt byaRpt = new ByARpt();

        public ieEntities ctx { get; set; }

        public ByARpt InsUsuarios(USUARIOS_DTO Reg)
        {

            string Msg = "";
            try
            {
                var userStore = new UserStore<IdentityUser>();
                var manager = new UserManager<IdentityUser>(userStore);
                var user = new IdentityUser() { UserName = Reg.USERNAME };

                IdentityResult result = manager.Create(user, Reg.PASSWORD);

                if (result.Succeeded)
                {
                    byaRpt.Mensaje = string.Format("User {0} was created successfully!", user.UserName);
                }
                else
                {
                    byaRpt.Mensaje = result.Errors.FirstOrDefault();
                }
                byaRpt.Mensaje = Msg;
            }
            catch (Exception ex)
            {
                Msg = ex.Message;
                byaRpt.Error = true;
                byaRpt.Mensaje = Msg;
                return byaRpt;
            }
            return byaRpt;
        }

        public List<USUARIOS_DTO> GetUsuarios(string filtro)
        {
            filtro = filtro.ToUpper();
            List<USUARIOS_DTO> lst = new List<USUARIOS_DTO>();
            ctx = new ieEntities();
            var userStore = new UserStore<IdentityUser>();
            List<IdentityUser> lstO = userStore.Users.ToList();
            Mapper.CreateMap<IdentityUser, USUARIOS_DTO>()
                   .ForMember(dest => dest.USERNAME, opt => opt.MapFrom(src => src.UserName))
                   .ForMember(dest => dest.TERCERO, opt => opt.MapFrom(src => GetTercero(src.UserName)));
            Mapper.Map(lstO, lst);


            List<USUARIOS_DTO> lstR = new List<USUARIOS_DTO>();
            
            if (filtro == "") lstR = lst;
            else foreach (var t in lst)
                {
                    if (t.TERCERO != null && t.USERNAME != null)
                    {
                        if (t.USERNAME.Contains(filtro) || t.TERCERO.ToUpper().Contains(filtro))

                            lstR.Add(t);
                    }
                }
            return lstR;
        }


        private string GetTercero(string username)
        {
            terceros t = ctx.terceros.Where(ter => ter.identificacion == username).FirstOrDefault();
            if (t != null)
            {
                return t.nombre;
            }
            //Faltaría agregar el estudiante
            else return "Sin Tercero";
        }

        public IdentityUser Validar(string username, string password)
        {
            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);
            IdentityUser user = userManager.Find(username, password);
            return user;
        }

        public ByARpt Forzar_Cambio_Clave(USUARIOS_DTO Reg)
        {
            try
            {
                var userStore = new UserStore<IdentityUser>();
                var manager = new UserManager<IdentityUser>(userStore);
                var user = manager.FindByName(Reg.USERNAME);
                manager.RemovePassword(user.Id);
                manager.AddPassword(user.Id, Reg.PASSWORD);
                byaRpt.Mensaje = "Se realizó el cambio de contraseña";
                byaRpt.Error = false;
            }
            catch (System.Exception ex)
            {
                byaRpt.Mensaje = "Error de App:" + ex.Message;
                byaRpt.Error = true;
            }
            return byaRpt;

        }

        public List<ModuloRoles> GetRoles(string Modulo, string UserName)
        {
            var userStore = new UserStore<IdentityUser>();
            var userMgr = new UserManager<IdentityUser>(userStore);
            var user = userMgr.FindByName(UserName);

            ctx = new ieEntities();

            List<ModuloRoles> lm = ctx.fc_menu.Where(t => t.fc_modulo == Modulo && t.fc_menuid != t.fc_padreid && t.fc_roles != null).OrderBy(t => t.fc_posicion)
                .Select(t => new ModuloRoles
                {
                    Modulo = t.fc_modulo,
                    Roles = t.fc_roles,
                    Titulo = t.fc_titulo
                }).Distinct().ToList();
            foreach (ModuloRoles item in lm)
            {
                if (item.Roles != null)
                {
                    item.hasRol = (userMgr.IsInRole(user.Id, item.Roles));
                }

            }
            return lm;
        }

        public ByARpt GuardarRoles(List<ModuloRoles> lst, string UserName)
        {
            string msg = "";

            var userStore = new UserStore<IdentityUser>();
            var userMgr = new UserManager<IdentityUser>(userStore);
            var user = userMgr.FindByName(UserName);
            IdentityResult IdUserResult;


            foreach (ModuloRoles item in lst)
            {
                bool hasRoolAnt = userMgr.IsInRole(user.Id, item.Roles);
                if (item.hasRol != hasRoolAnt)//Si cambió
                {
                    try
                    {
                        if (item.hasRol)
                        {
                            IdUserResult = userMgr.AddToRole(user.Id, item.Roles);
                            msg += String.Format("Se Asignó el Rol {0} - [ {1} ]</br>", item.Titulo, item.Roles);
                        }
                        else
                        {
                            IdUserResult = userMgr.RemoveFromRole(user.Id, item.Roles);
                            msg += String.Format("Se Retiró el Rol {0} - [ {1} ]</br>", item.Titulo, item.Roles);
                        }
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;

                    }
                }
            }
            if (String.IsNullOrEmpty(msg))
            {
                byaRpt.Mensaje = "No realizó ningun cambio de Roles al usuario";
            }
            else
            {
                byaRpt.Mensaje = msg;
            }

            byaRpt.Error = false;
            //GuardarRolesUsuarios
            return byaRpt;
        }

    }

}
