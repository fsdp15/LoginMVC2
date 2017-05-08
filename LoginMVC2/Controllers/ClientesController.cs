using System.Web.Mvc;
using System;
using LoginMVC2.Models.Persistence;
using System.Linq;
using PagedList;
using LoginMVC2.Models;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace LoginMVC2.Controllers
{
    public class ClientesController : Controller
    {
        private ProdutoDAL p = new ProdutoDAL();
        private CarrinhoDAL c = new CarrinhoDAL();
        private UsuariosDAL u = new UsuariosDAL();
        private EnderecoDAL e = new EnderecoDAL();

        public UsuariosDAL U { get => u; set => u = value; }
        public CarrinhoDAL C { get => c; set => c = value; }
        public ProdutoDAL P { get => p; set => p = value; }
        public EnderecoDAL E { get => e; set => e = value; }


        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.NomeCliente = HttpContext.User.Identity.Name;
            ViewBag.Alerta = (string)TempData["alerta"];
            ViewBag.CurrentSort = sortOrder;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            int pageNumber = (page ?? 1);

            var produtos = from p in P.Db.Produtos
                           select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                produtos = produtos.Where(p => p.Nome.Contains(searchString));
            }

            if (String.IsNullOrEmpty(sortOrder))
            {
                ViewBag.NameSortParm = "name_desc";
                ViewBag.PriceSortParm = "price_desc";
                return View(produtos.OrderBy(p => p.Nome).ToPagedList(pageNumber, 10));
            }
            else
            {
                if (sortOrder.Equals("Price"))
                {
                    ViewBag.PriceSortParm = "price_desc";
                    return View(produtos.OrderBy(p => p.Preco).ToPagedList(pageNumber, 10));
                }
                else if (sortOrder.Equals("price_desc"))
                {
                    ViewBag.PriceSortParm = "Price";
                    return View(produtos.OrderByDescending(p => p.Preco).ToPagedList(pageNumber, 10));
                }
                else
                {
                    ViewBag.NameSortParm = "";
                    return View(produtos.OrderByDescending(p => p.Nome).ToPagedList(pageNumber, 10));
                }
            }
        }

        public ActionResult Detalhes(long id)
        {
            return PartialView("Detalhes", P.FindById(id));
        }

        [Authorize(Roles = "Administrador, Funcionario, Cliente")]
        public ActionResult Carrinho()
        {
            int ID = U.GetUser(HttpContext.User.Identity.Name).IDUsuario;
            ViewBag.Preco = C.GetTotalPrice(ID);
            return View(C.GetAllItems(ID));
        }

        public ActionResult Checkout(Carrinho ca)
        {
            if (User.IsInRole("Cliente") || User.IsInRole("Funcionario"))
            {
                if (c.GetCount(U.GetUser(HttpContext.User.Identity.Name).IDUsuario) != 0)
                {
                    Enderecos_Lista lista = new Enderecos_Lista();
                    lista.ListaEnderecos = E.Db.Enderecos.ToList();
                    ViewBag.IDUsuario = U.GetUser(HttpContext.User.Identity.Name).IDUsuario;
                    return PartialView("Checkout", lista);
                }
                else return RedirectToAction("Clientes", "Carrinho");
            }
            return RedirectToAction("Account", "Index");
        }

        private void EnviaEmail(int detalhes, double precoTotal)
        {
            var body = "<p>Email de: {0} ({1})</p><p>Mensagem:</p><p>{2}</p>";
            var message = new MailMessage();
            USUARIOS u = U.GetUser(HttpContext.User.Identity.Name);
            message.To.Add(new MailAddress(u.EmailUsuario));
            message.From = new MailAddress("lojasfelipe@outlook.com.br");
            message.Subject = "Sua compra nas Lojas Felipe";
            StringBuilder builder = new StringBuilder();
            builder.Append("Olá, ");
            builder.Append(u.UserName);
            builder.Append("!<br /><br />Você realizou a compra dos seguintes produtos nas Lojas Felipe: <br /><br />");
            foreach (var item in new ComprasDAL().RetornaCompras(detalhes))
            {
                builder.Append("Produto: " + item.Produtos.Nome);
                builder.Append("<br />Quantidade: " + item.Quantidade);
                builder.Append("<br />Preço: R$ " + item.Produtos.Preco * item.Quantidade);
                builder.Append("<br /><br />");
            }

            builder.Append("Preço total da compra: R$ " + precoTotal);
            builder.Append("<br /><br />Este e-mail é automático, favor não responder.");
            message.Body = string.Format(body, "Lojas Felipe", "felipe.dprado@live.com", builder.ToString());
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "lojasfelipe@outlook.com.br",
                    Password = "Jwoa0RGD"
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.live.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(message);
            }
        }

        private void Enviado()
        {
            return;
        }

        [HttpPost]
        public ActionResult Checkout(Enderecos_Lista lista)
        {
            if (User.IsInRole("Cliente") || User.IsInRole("Funcionario"))
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Alerta = null;
                    try
                    {
                        int IDUsuario = U.GetUser(HttpContext.User.Identity.Name).IDUsuario;
                        Compras_Detalhes cd = new Compras_Detalhes();
                        cd.Cliente = IDUsuario;
                        cd.Endereco = lista.EnderecoSelecionado;
                        ComprasDetalhesDAL cdd = new ComprasDetalhesDAL();
                        cdd.Insert(cd);
                        ComprasDAL c = new ComprasDAL();
                        c.InsertAll(IDUsuario, cd.IDCompra_Detalhes);
                        double precoTotal = C.GetTotalPrice(IDUsuario);
                        C.EmptyCart(IDUsuario);
                        EnviaEmail(cd.IDCompra_Detalhes, precoTotal);
                        return RedirectToAction("Compra");
                    }
                    catch
                    {
                        ViewBag.Alerta = "Você não selecionou um endereço!";
                        return RedirectToAction("Index");
                    }
                }
                ViewBag.Alerta = "Você não selecionou um endereço!";
            }
            return RedirectToAction("Carrinho");
        }

        [Authorize(Roles = "Administrador, Funcionario, Cliente")]
        public ActionResult Compra()
        {
            return View();
        }

        [Authorize(Roles = "Administrador, Funcionario, Cliente")]
        public ActionResult Endereco()
        {
            ViewBag.Estado = new SelectList(e.Db.Estado, "Id", "Nome", null);
            return View();
        }

        [ValidateAntiForgeryToken()]
        [Authorize(Roles = "Administrador, Funcionario, Cliente")]
        [HttpPost]
        public ActionResult Endereco(Enderecos e)
        {
            var x = e.Complemento;
            var y = e.Endereco;
            var z = e.Estado;
 // valores da drop down list estão vindo zerados
            if (ModelState.IsValid)
            {
                e.Cliente = U.GetUser(HttpContext.User.Identity.Name).IDUsuario;
                E.Insert(e);
                return RedirectToAction("Carrinho");
            }
            return null;

        }

        [Authorize(Roles = "Administrador, Funcionario, Cliente")]
        public ActionResult DropDownCidades(long val)
        {
            var estado = e.Db.Estado.Find(val);
            ViewBag.Cidade = new SelectList(e.Db.Municipio.Where(m => m.Uf == estado.Uf), "Id", "Nome", "--SELECIONE--");
            return PartialView("DropDownCidades");
        }

        public int Remover(int IDProduto = 0)
        {
            if (User.IsInRole("Cliente") || User.IsInRole("Funcionario"))
            {
                return (c.RemoveFromCart(U.GetUser(HttpContext.User.Identity.Name).IDUsuario, IDProduto));
            }
            else
            {
                return -1;
            }
        }

        [HttpPost]
        public int Adicionar(int IDProduto, int quantidade = 1)
        {
            if (User.IsInRole("Cliente") || User.IsInRole("Funcionario"))
            {
                return (C.AddToCart(IDProduto, U.GetUser(HttpContext.User.Identity.Name).IDUsuario, quantidade));
            }
            else
            {
                return -1;
            }
        }

        [Authorize(Roles = "Administrador, Funcionario, Cliente")]
        public int QuantidadeCarrinho()
        {
            if (User.IsInRole("Cliente") || User.IsInRole("Funcionario"))
            {
                return (C.GetCount(U.GetUser(HttpContext.User.Identity.Name).IDUsuario));
            }
            else
            {
                return -1;
            }
        }










    }


}
