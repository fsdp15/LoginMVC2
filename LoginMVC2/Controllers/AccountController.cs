using System.Web.Mvc;
using System.Web.Routing;
using LoginMVC2.Interfaces;
using LoginMVC2.Services;
using LoginMVC2.Models;
using System.Web.Security;
using System;
using LoginMVC2.Models.Persistence;
using System.Web;
using System.Globalization;

namespace LoginMVC2.Controllers
{
    public class AccountController : Controller
    {
        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }

        public ActionResult Index()
        {
            if (!User.IsInRole("Administrador") && !User.IsInRole("Funcionario") && !User.IsInRole("Cliente"))
            {
                return View();
            }
            else return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Index(USUARIOS Usuario, bool RememberMe = false)
        {
            if (!User.IsInRole("Administrador") && !User.IsInRole("Funcionario") && !User.IsInRole("Cliente"))
            {
                if (ModelState.IsValid)
                {
                    if (MembershipService.ValidateUser(Usuario.UserName, Usuario.Senha))
                    {
                        SetupFormsAuthTicket(Usuario.UserName, RememberMe);
                        string[] roles = Roles.GetRolesForUser(Usuario.UserName);
                        if (roles[0] == "Administrador") {
                            roles[0] = roles[0] + "es";
                        }
                        else {
                            roles[0] = roles[0] + "s";
                        }
                        return RedirectToAction("Index", roles[0]);
                    }
                    ModelState.AddModelError("", "Usuário ou senha incorretos");
                }
                return View(User);
            }
            else return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            if (!User.IsInRole("Administrador") && !User.IsInRole("Funcionario") && !User.IsInRole("Cliente"))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Register(Register NovoUsuario)
        {
            if (!User.IsInRole("Administrador") && !User.IsInRole("Funcionario") && !User.IsInRole("Cliente"))
            {
                if (ModelState.IsValid)
                {
                    if (MembershipService.CreateUser(NovoUsuario.UserName, NovoUsuario.Senha, NovoUsuario.EmailUsuario) == MembershipCreateStatus.Success)
                    {
                        ViewBag.CriouUsuario = "Usuário cadastrado com sucesso!";
                        return RedirectToAction("Index", "Account");
                    }
                    ModelState.AddModelError("", "Falha na criação do usuário");
                }
                return View(NovoUsuario);
            }
            else return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private USUARIOS SetupFormsAuthTicket(string userName, bool persistanceFlag)
        {
            if (!User.IsInRole("Administrador") && !User.IsInRole("Funcionario") && !User.IsInRole("Cliente"))
            {
                USUARIOS user = new UsuariosDAL().GetUser(userName);

                this.Response.Cookies.Add(
                     new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(new FormsAuthenticationTicket(1,
                                        userName,
                                        DateTime.Now,
                                        DateTime.Now.AddMinutes(30),
                                        persistanceFlag,
                                        user.IDUsuario.ToString(CultureInfo.InvariantCulture)))));
                return user;
            }
            else
            {
                return null;
            }
        }

    }
}
