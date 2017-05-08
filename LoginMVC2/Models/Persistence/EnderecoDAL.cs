using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LoginMVC2.Models.Persistence
{
    public class EnderecoDAL
    {
        private EntidadeLojasFelipe db = new EntidadeLojasFelipe();

        public EntidadeLojasFelipe Db { get => db; set => db = value; }

        public void Insert(Enderecos e)
        {
            Db.Enderecos.Add(e);
            Db.SaveChanges();
        }

        public int Remove(int id)
        {
            try
            {
                Db.Enderecos.Remove(Db.Enderecos.Find(id));
                Db.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }

        }

        public void Edit(Enderecos e)
        {
            Db.Entry(e).State = EntityState.Modified;
            Db.SaveChanges();
        }

        public Enderecos FindById(int id)
        {
            return Db.Enderecos.Find(id);
        }

        public List<Enderecos> FindByUser(int IDUsuario)
        {
            return Db.Enderecos.Where(e => e.USUARIOS.IDUsuario == IDUsuario).ToList();
        }
    }
}