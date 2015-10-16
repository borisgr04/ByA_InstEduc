using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ByA;
using Entidades;
using System.Web.Security;
using DAL;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace BLL.Security
{
    public class genRoles
    {

        public static string _ADMIN = "admin";
        public string GenerarRoles()
        {
            var roleStore = new RoleStore<IdentityRole>();
            var roleMgr = new RoleManager<IdentityRole>(roleStore);

            var userStore = new UserStore<IdentityUser>();
            var userMgr = new UserManager<IdentityUser>(userStore);
            var user = userMgr.FindByName(_ADMIN);

            IdentityResult IdRoleResult;
            IdentityResult IdUserResult;

            using (ieEntities ctx = new ieEntities())
            {
                string strrole = "";
                List<fc_menu> lm = ctx.fc_menu.Where(t => !String.IsNullOrEmpty(t.fc_roles)).ToList();
                foreach (fc_menu im in lm)
                {
                    string rol = im.fc_roles;
                    //!String.IsNullOrEmpty(rol) &&
                    bool RolExiste = roleMgr.RoleExists(rol);

                    if ((!RolExiste))
                    {
                        IdRoleResult = roleMgr.Create(new IdentityRole(rol));
                        IdUserResult = userMgr.AddToRole(user.Id, rol);
                        strrole = strrole + rol + "<br>";
                    }
                    else
                    {
                        bool adminTieneElRol = userMgr.IsInRole(user.Id, rol);
                        if (!adminTieneElRol)
                        {
                            IdUserResult = userMgr.AddToRole(user.Id, rol);
                            strrole = strrole + rol + "<br>";
                        }
                    }
                }
                    return strrole;
            }

        }
    }
}