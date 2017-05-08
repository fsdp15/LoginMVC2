using System.Linq;
using System.Collections.Generic;

namespace LoginMVC2.Models.Persistence
{
    public class UsuariosDAL
    {
        private EntidadeLojasFelipe db;

        public UsuariosDAL()
        {
            this.Db = new EntidadeLojasFelipe();
        }

        public EntidadeLojasFelipe Db
        {
            get
            {
                return db;
            }
            set
            {
                db = value;
            }
        }

        public void AddUser(USUARIOS usuario)
        {
            this.Db.USUARIOS.Add(usuario);
            this.Db.SaveChanges();
        }

        public USUARIOS GetUser(string userName)
        {
            return this.Db.USUARIOS.SingleOrDefault(u => u.UserName == userName);
        }

        public List<USUARIOS> GetAllUsers()
        {
            return this.Db.USUARIOS.ToList();
        }

        public void AddUserRole(RolesUsuarios userRole)
        {
            var roleEntry = Db.RolesUsuarios.SingleOrDefault(r => r.IDUsuario == userRole.IDUsuario);
            if (roleEntry != null)
            {
                this.Db.RolesUsuarios.Remove(roleEntry);
            }
            this.Db.RolesUsuarios.Add(userRole);
            this.Db.SaveChanges();
        }


    }
}