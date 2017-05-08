using System;
using System.Web.Security;
using LoginMVC2.Interfaces;

namespace LoginMVC2.Services
{
    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        public void SignIn(string userName, bool createPersistentCookie)
        {
            if (String.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("O valor não pode ser vazio", "userName");
            }

            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}