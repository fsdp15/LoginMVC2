using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoginMVC2.Models.Persistence;

namespace LoginMVC2.Models.Persistence
{
    public class ComprasDAL
    {
        EntidadeLojasFelipe db = new EntidadeLojasFelipe();

        public EntidadeLojasFelipe Db { get => db; set => db = value; }

        public void Insert(Compras c)
        {
            Db.Compras.Add(c);
            Db.SaveChanges();
        }

        public void InsertAll(int IDUsuario, int IDDetalhes)
        {
            foreach (var c in new CarrinhoDAL().GetAllItems(IDUsuario))
            {
                Insert(new Compras()
                {
                    Produto = c.Produto,
                    Quantidade = c.Quantidade,
                    Detalhes = IDDetalhes
                });
            }

        }

        public List<Compras> RetornaCompras(int IDDetalhes)
        {
            return Db.Compras.Where(c => c.Detalhes == IDDetalhes).ToList();
        }
    }
}