using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PagedList.Mvc;

namespace LoginMVC2.Models.Persistence
{
    public class ProdutoDAL
    {
        private EntidadeLojasFelipe db;

        public ProdutoDAL()
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

        public void Insert(Produtos produto)
        {
            produto.DataCadastro = DateTime.Now;
            this.Db.Produtos.Add(produto);
            this.Db.SaveChanges();
        }

        public void Edit(Produtos produto)
        {
            this.Db.Entry(produto).State = EntityState.Modified;
            this.Db.SaveChanges();
        }

        public string Delete(long id)
        {
            try
            {
                this.Db.Produtos.Remove(Db.Produtos.Find(id));
                this.Db.SaveChanges();
                return Boolean.TrueString;
            }
            catch
            {
                return Boolean.FalseString;
            }
        }

        public Produtos FindById(long id)
        {
            return this.Db.Produtos.Find(id);
        }

        public List<Produtos> FindAll()
        {
            return this.Db.Produtos.ToList();
        }


    }
}