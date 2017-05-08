using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginMVC2.Interfaces
{
    public interface IFormsAuthenticationService
    {
        void SignIn(string userName, bool createPersistentCookie);
        void SignOut();
    }
}