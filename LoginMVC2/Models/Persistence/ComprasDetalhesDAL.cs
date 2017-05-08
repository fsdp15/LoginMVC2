using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoginMVC2.Models.Persistence;
using LoginMVC2.Models;
using System.Data.Entity;

namespace LoginMVC2.Models.Persistence
{
    public class ComprasDetalhesDAL
    {
        EntidadeLojasFelipe db = new EntidadeLojasFelipe();

        public EntidadeLojasFelipe Db { get => db; set => db = value; }

        public void Insert(Compras_Detalhes cd)
        {
            cd.DataCompra = DateTime.Now;
            Db.Compras_Detalhes.Add(cd);
            Db.SaveChanges();
        }

        public void Edit(Compras_Detalhes cd)
        {
            Db.Entry(cd).State = EntityState.Modified;
            Db.SaveChanges();
        }

        public List<Compras_Detalhes> FindAll()
        {
            return Db.Compras_Detalhes.ToList();
        }

        public Compras_Detalhes FindByID(int IDCompra_Detalhes)
        {
            return Db.Compras_Detalhes.Find(IDCompra_Detalhes);
        }
         


    }
}