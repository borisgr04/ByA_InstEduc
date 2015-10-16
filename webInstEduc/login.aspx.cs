using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.OAuth;
using System.Net;
using AspIdentity;
using System.Configuration;


namespace Skeleton.WebAPI
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["opt"] == "logout")
                {
                    var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                    authenticationManager.SignOut();
                    Response.Redirect("~/Login.aspx");
                    lbMsg.Text = "Su sesión se ha cerrado correctamente.";
                    lbMsg.ForeColor = System.Drawing.Color.Green;
                }
                if (!String.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                {
                    lbMsg.Text = "No tiene autorización a la opción seleccionada ";
                    lbMsg.Text += "<a href='javascript:history.go(-1)'>Atrás</a>";
                    lbMsg.ForeColor = System.Drawing.Color.Red;
                }


            }

        }
        protected void SignOut(object sender, EventArgs e)
        {
            
        }
        private void SetCookieUser(string usuario, string vig)
        {
            DateTime now = DateTime.Now;

            HttpCookie myCookie;

            myCookie = new HttpCookie("fc_user");
            myCookie.Value = usuario;
            myCookie.Expires = now.AddHours(8);
            HttpContext.Current.Response.Cookies.Add(myCookie);

            myCookie = new HttpCookie("fc_vig");
            myCookie.Value = vig;
            myCookie.Expires = now.AddHours(8);
            HttpContext.Current.Response.Cookies.Add(myCookie);

        }
        protected void BtnIniciar_Click1(object sender, EventArgs e)
        {
            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);
            //var user = userManager.Find(UserName.Text, Password.Text);

            var user = userManager.FindByName(UserName.Text);

            if (user != null)
            {
                var validCredentials = userManager.Find(UserName.Text, Password.Text);

                if (userManager.IsLockedOut(user.Id))
                {
                    ModelState.AddModelError("", string.Format("Your account has been locked out for {0} minutes due to multiple failed login attempts.", ConfigurationManager.AppSettings["DefaultAccountLockoutTimeSpan"].ToString()));
                    StatusText.Text = string.Format("Your account has been locked out for {0} minutes due to multiple failed login attempts.", ConfigurationManager.AppSettings["DefaultAccountLockoutTimeSpan"].ToString());
                }
                else if (userManager.GetLockoutEnabled(user.Id) && validCredentials == null)
                {
                    userManager.AccessFailed(user.Id);
                    string message;
                    if (userManager.IsLockedOut(user.Id))
                    {
                        message = string.Format("Your account has been locked out for {0} minutes due to multiple failed login attempts.", ConfigurationManager.AppSettings["DefaultAccountLockoutTimeSpan"].ToString());
                        StatusText.Text = message;
                    }
                    else
                    {
                        int accessFailedCount = userManager.GetAccessFailedCount(user.Id);
                        int attemptsLeft =
                            Convert.ToInt32(
                                ConfigurationManager.AppSettings["MaxFailedAccessAttemptsBeforeLockout"].ToString()) -
                            accessFailedCount;
                        message = string.Format(
                            "Invalid credentials. You have {0} more attempt(s) before your account gets locked out.", attemptsLeft);
                        StatusText.Text = message;
                    }

                    ModelState.AddModelError("", message);
                }
                else if (validCredentials == null)
                {
                    ModelState.AddModelError("", "Invalid credentials. Please try again.");
                    StatusText.Text = "Invalid credentials. Please try again.";
                }
                else
                {

                    var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                    var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    
                    authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, userIdentity);
                    userManager.ResetAccessFailedCount(user.Id);


                    string vig = DateTime.Now.Year.ToString();
                    SetCookieUser(UserName.Text, vig);

                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);

                    //Response.Redirect("default.aspx");

                    
                }


                
            }
            else
            {
                StatusText.Text = "Invalid username or password.";
            }
        }
        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }
    }
}