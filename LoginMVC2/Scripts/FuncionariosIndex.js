var linhaAtual;
function callAjax(flag, url, title) {
    var modalcar = $("#myModal2");
    $(modalcar).find(".modal-dialog").addClass("modal-sm");
    $(modalcar).find(".modal-header h4").text(title);
    if (flag) {
        $.ajax({
            type: "POST",
            url: url
        }).done(function (excluiu) {
            if (excluiu) {
                linhaAtual.remove();
            } else {
                $(modalcar).find(".modal-body p").text("Ocorreu um erro na exclusão do produto.");
                $(modalcar).modal("show");
            }
        }).fail(function () {
            $(modalcar).find(".modal-body p").text("Erro ao excluir produto");
            $(modalcar).modal("show");
        });
    }
    return false;
}

$("table .btn.btn-danger.btn-xs").off().on('click', function (event) {
    event.preventDefault();
    linhaAtual = $(this).closest("tr");
    customConfirm("Deseja remover o produto selecionado?", callAjax, "Remover produto", $(this).attr("href"));
});

var modalaux = $(".modal");
$(modalaux).find(".modal-header button.close").on("click", function () {
    $(modalaux).modal("hide");
});

$(modalaux).find(".modal-footer button").on("click", function () {
    $(modalaux).modal("hide");
});

$("table .btn.btn-info.btn-xs").off().on('click', function (event) {
    event.preventDefault();
    var modalcar = $("#myModal2");
    $(modalcar).find(".modal-header h4").text("Detalhes");
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
});