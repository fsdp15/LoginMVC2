using System.Web;
using System.Web.Optimization;

namespace LoginMVC2
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.1.1.min.js",
                        "~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/ValidationMessages.js"));

            bundles.Add(new ScriptBundle("~/bundles/crudfunc").Include(
                        "~/Scripts/jquery.maskMoney.min.js",
                        "~/Scripts/ScriptsFuncionariosAddEdit.js"));

            bundles.Add(new ScriptBundle("~/bundles/enderecos").Include(
            "~/Scripts/jquery.maskedinput.min.js",
            "~/Scripts/EnderecoScript.js"));

            bundles.Add(new ScriptBundle("~/bundles/carrinho").Include(
            "~/Scripts/customConfirm.js",
            "~/Scripts/ClientesCarrinho.js"));

            bundles.Add(new ScriptBundle("~/bundles/clienteindex").Include(
            "~/Scripts/customConfirm.js",
            "~/Scripts/ClientesIndex.js"));

            bundles.Add(new ScriptBundle("~/bundles/funcionarios").Include(
            "~/Scripts/customConfirm.js",
            "~/Scripts/FuncionariosIndex.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.min.css",
                      "~/Content/StyleSheet.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}