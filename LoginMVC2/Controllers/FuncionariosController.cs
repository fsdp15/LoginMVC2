using System.Web.Mvc;
using LoginMVC2.Models;
using System;
using LoginMVC2.Models.Persistence;
using System.Linq;
using PagedList;

namespace LoginMVC2.Controllers
{
    public class FuncionariosController : Controller
    {
        private ProdutoDAL ProdutoDAL = new ProdutoDAL();
        [Authorize(Roles = "Administrador, Funcionario")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.NomeFuncionario = HttpContext.User.Identity.Name;
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

            var produtos = from p in ProdutoDAL.Db.Produtos
                           select p;         

            if (!String.IsNullOrEmpty(searchString))
            {
                produtos = produtos.Where(p => p.Nome.Contains(searchString));
            }

            if (String.IsNullOrEmpty(sortOrder))
            {
                ViewBag.NameSortParm = "name_desc";
                return View(produtos.OrderBy(p => p.Nome).ToPagedList(pageNumber, 10));
            }
            else
            {
                ViewBag.NameSortParm = "";
                return View(produtos.OrderByDescending(p => p.Nome).ToPagedList(pageNumber, 10));
            }
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        public ActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [Authorize(Roles = "Administrador, Funcionario")]
        public ActionResult Adicionar(Produtos produto)
        {

            if (ModelState.IsValid)
            {
                ProdutoDAL.Insert(produto);
                TempData["alerta"] = "Produto adicionado com sucesso!";
                return RedirectToAction("Index");
            }
            return View(produto);
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        public ActionResult Editar(long id)
        {
            return View(ProdutoDAL.FindById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [Authorize(Roles = "Administrador, Funcionario")]
        public ActionResult Editar(Produtos produto)
        {
            if (ModelState.IsValid)
            {
                ProdutoDAL.Edit(produto);
                TempData["alerta"] = "Produto editado com sucesso!";
                return RedirectToAction("Index");
            }
            return View(produto);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador, Funcionario")]
        public string Remover(long id)
        {
            return ProdutoDAL.Delete(id);
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        public ActionResult Detalhes(long id)
        {
            return PartialView("Detalhes", ProdutoDAL.FindById(id));
        }
    }
}
