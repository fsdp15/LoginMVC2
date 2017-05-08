using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LoginMVC2.Models.Persistence
{
    public class CarrinhoDAL
    {
        private EntidadeLojasFelipe db;

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

        public CarrinhoDAL()
        {
            this.Db = new EntidadeLojasFelipe();
        }

        public int AddToCart(int IdProduto, int IDCliente, int quantidade)
        {
            var cartItem = Db.Carrinho.SingleOrDefault(c => c.Cliente == IDCliente && c.Produto == IdProduto);
            if (cartItem == null)
            {
                cartItem = new Carrinho();
                cartItem.Cliente = IDCliente;
                cartItem.Produto = IdProduto;
                cartItem.Quantidade = quantidade;
                cartItem.DataCriacao = DateTime.Now;
                Db.Carrinho.Add(cartItem);
            }
            else
            {
                cartItem.Quantidade += quantidade;
            }
            Db.SaveChanges();
            return 1;


        }

        public int RemoveFromCart(int IDCliente, int IdProduto)
        {
            var cartItem = Db.Carrinho.SingleOrDefault(c => c.Cliente == IDCliente && c.Produto == IdProduto);
            int itemCount = 0;
            if (cartItem != null)
            {
                if (cartItem.Quantidade > 1)
                {
                    cartItem.Quantidade--;
                    itemCount = cartItem.Quantidade;
                }
                else
                {
                    this.Db.Carrinho.Remove(cartItem);
                }
                this.Db.SaveChanges();
                return itemCount;
            }
            return -1;
        }

        public void EmptyCart(int IDCliente)
        {
            var cartItems = Db.Carrinho.Where(c => c.Cliente == IDCliente);
            foreach (Carrinho c in cartItems)
            {
                Db.Carrinho.Remove(c);
            }
            Db.SaveChanges();
        }

        public double GetTotalPrice(int IDCliente)
        {
            double price = 0;
            var cartItems = Db.Carrinho.Where(c => c.Cliente == IDCliente);
            foreach (Carrinho c in cartItems)
            {
                for (int i = 0; i < c.Quantidade; i++)
                {
                    price += c.Produtos.Preco;
                }
            }
            return price;
        }

        public int GetCount(int IDCliente)
        {
            var cartItems = Db.Carrinho.Where(c => c.Cliente == IDCliente);
            int count = 0;
            foreach (Carrinho c in cartItems)
            {
                count += c.Quantidade;
            }
            return count;
        }

        public List<Carrinho> GetAllItems(int IDCliente)
        {
            return Db.Carrinho.Where(c => c.Cliente == IDCliente).ToList();
        }


    }
}