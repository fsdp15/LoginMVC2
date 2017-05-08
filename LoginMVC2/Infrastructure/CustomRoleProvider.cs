using LoginMVC2.Models;
using LoginMVC2.Models.Persistence;
using System;
using System.Linq;
using System.Web.Security;

namespace LoginMVC2.Infrastructure
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override bool IsUserInRole(string username, string roleNome)
        {
            using (EntidadeLojasFelipe db = new EntidadeLojasFelipe())
            {
                var user = new UsuariosDAL().GetUser(username);
                if (user != null)
                {
                    return user.RolesUsuarios != null && user.RolesUsuarios.Select(u => u.ROLES).Any(r => r.RoleNome == roleNome);
                }
                else return false;
            }
        }

        public override string[] GetRolesForUser(string username)
        {
            using (EntidadeLojasFelipe db = new EntidadeLojasFelipe())
            {
                var user = new UsuariosDAL().GetUser(username);
                if (user != null)
                {
                    if (user.RolesUsuarios != null)
                    {
                        return user.RolesUsuarios.Select(u => u.ROLES).Select(u => u.RoleNome).ToArray();
                    }
                    return new string[] { };
                }
                return new string[] { };
            }
        }

        // -- Snip --

        public override string[] GetAllRoles()
        {
            using (EntidadeLojasFelipe db = new EntidadeLojasFelipe())
            {
                return db.ROLES.Select(r => r.RoleNome).ToArray();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }
    }
}