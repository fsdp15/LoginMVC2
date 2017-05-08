var linhaAtual;

function callAjax(flag, url, title) {
    var modalcar = $("#myModal2");
    $(modalcar).find(".modal-dialog").addClass("modal-sm");
    $(modalcar).find(".modal-header h4").text(title);
    if (flag) {
        $.ajax({
            type: "POST",
            url: url
        }).done(function (itemCount) {
            var quantidade = linhaAtual.find("table .tdQuantidade");
            var preco = linhaAtual.find("table #tdPreco");
            var precoFloat = parseFloat(preco.text());
            var precoUni = (precoFloat / quantidade.text()).toFixed(2);
            var precoTotal = $("table #tdPrecoTotal");
            var quantidadeCarrinho = $("table #quantidadeCarrinho");
            if (itemCount > 0) {
                preco.text((precoFloat - precoUni).toFixed(2).replace(".", ","));
                quantidade.text(parseInt(quantidade.text()) - 1);
                $(precoTotal).text((parseFloat($(precoTotal).text()) - precoUni).toFixed(2).replace(".", ","));
                $(quantidadeCarrinho).text('(' + ((parseInt($(quantidadeCarrinho).text().replace('(', '').replace(')', '')) - 1).toString()) + ')');
                $(modalcar).find(".modal-body p").text("Produto removido do carrinho!");
                $(modalcar).modal("show");
            }
            else if (itemCount == 0) {
                linhaAtual.remove();
                $(quantidadeCarrinho).text('(' + ((parseInt($(quantidadeCarrinho).text().replace('(', '').replace(')', '')) - 1).toString()) + ')');
                $(precoTotal).text((parseFloat($(precoTotal).text()) - precoUni).toFixed(2).replace(".", ","));
                $(modalcar).find(".modal-body p").text("Produto removido do carrinho!");
                $(modalcar).modal("show");
            }
            else {
                $(modalcar).find(".modal-body p").text("Erro ao remover produto.");
                $(modalcar).modal("show");
            }
        }).fail(function () {
            $(modalcar).find(".modal-body p").text("Erro ao tentar remover.");
            $(modalcar).modal("show");
        });
    }
    return false;
}

$("table .btn.btn-danger.btn-xs").off().on('click', function (event) {
    event.preventDefault();
    linhaAtual = $(this).closest("tr");
    customConfirm("Deseja remover produto do carrinho?", callAjax, "Remover produto", $(this).attr("href"));
});

var modalaux = $(".modal");
$(modalaux).find(".modal-header button.close").on("click", function () {
    $(modalaux).modal("hide");
});

$(modalaux).find(".modal-footer button").on("click", function () {
    $(modalaux).modal("hide");
});

$("table .btn.btn-success").off().on('click', function (event) {
    event.preventDefault();
    var modalcar = $("#myModal2");
    $(modalcar).find(".modal-header h4").text("Detalhes");
    if ($("#tableCarrinho >tbody >tr").length > 0) {
        $.ajax({
            dataType: "html",
            type: "GET",
            url: $(this).attr("href")
        }).done(function (data) {
            if ($(modalcar).find(".modal-dialog.modal-sm").length) { // verifica se existe a classe
                $(modalcar).find(".modal-dialog.modal-sm").removeClass("modal-sm");
            }
            $(modalcar).find(".modal-body p").html(data);
            $(modalcar).modal("show");
        }).fail(function () {
            $(modalcar).find(".modal-dialog").addClass("modal-sm");
            $(modalcar).find(".modal-body p").text("Ocorreu algum erro");
            $(modalcar).modal("show");
        });
    }
    else {
        $(modalcar).find(".modal-dialog").addClass("modal-sm");
        $(modalcar).find(".modal-body p").text("Você não possui produtos no carrinho!");
        $(modalcar).modal("show");
    }
});