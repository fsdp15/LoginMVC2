function customConfirm(message, callback, title, optional = "") {
    var modalConfirm = $("#modalConfirm");
    $(modalConfirm).find(".modal-body p").text(message);
    $(modalConfirm).find(".modal-header h4").text(title);
    var buttons = $(modalConfirm).find(".modal-body button");

    $(modalConfirm).modal("show");
    $(buttons[0]).off().on('click', function () { // off chama o evento apenas uma vez
        $(modalConfirm).modal("hide");
        callback(true, optional, title);
    });
    $(buttons[1]).off().on('click', function () {
        $(modalConfirm).modal("hide");
        callback(false, optional, title);
    });
}