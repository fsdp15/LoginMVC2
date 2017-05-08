using System.Web.Mvc;
using LoginMVC2.Models.Persistence;
using System.Linq;
using LoginMVC2.Models;
using System.Collections.Generic;

namespace LoginMVC2.Controllers
{
    public class HistoricoController : Controller
    {
        private ProdutoDAL p = new ProdutoDAL();
        private CarrinhoDAL c = new CarrinhoDAL();
        private UsuariosDAL u = new UsuariosDAL();
        private ComprasDetalhesDAL cd = new ComprasDetalhesDAL();

        public UsuariosDAL U { get => u; set => u = value; }
        public CarrinhoDAL C { get => c; set => c = value; }
        public ProdutoDAL P { get => p; set => p = value; }
        public ComprasDetalhesDAL Cd { get => cd; set => cd = value; }

        // GET: Historico
        public ActionResult Index()
        {
            int ID = U.GetUser(HttpContext.User.Identity.Name).IDUsuario;
            var idsDetalhes = Cd.Db.Compras_Detalhes.Where(c_d => c_d.Cliente == ID).Select(c_d => c_d.IDCompra_Detalhes).ToArray();
            var datas = Cd.Db.Compras_Detalhes.Where(c_d => c_d.Cliente == ID).Select(c_d => c_d.DataCompra).ToArray();
            List<ListaCompras> comprasUsuario = new List<ListaCompras>();
            for (int i = 0; i < idsDetalhes.Length; i++)
            {
                int aux = idsDetalhes[i];
                ListaCompras listaaux = new ListaCompras();
                listaaux.Items = Cd.Db.Compras.Where(cmp => cmp.Detalhes == aux).ToList();
                listaaux.DataCompra = datas[i];
                comprasUsuario.Add(listaaux);
            }
            return View(comprasUsuario);
        }


    }
}