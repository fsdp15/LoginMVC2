function dropDownEstado(cidade) {
    $.ajax({
        type: "GET",
        datatype: "html",
        url: "/Clientes/DropDownCidades" + '?val=' + $("form #Estado").find(":selected").val()
    }).done(function (data) {
        $("#divCidades").html(data);
        var y = document.getElementById("Cidade");
        var i = 0;
        while (y.options[i].textContent.localeCompare(cidade) != 0) {
            i++;
        }
        y.options[i].selected = true;
        }).fail(function () {
            var modalcar = $("#myModal2");
            $(modalcar).find(".modal-dialog").addClass("modal-sm");
            $(modalcar).find(".modal-body p").text("Erro ao adicionar produto");
            $(modalcar).modal("show");
    });
}

var numero = $("#Numero");
numero.on("change", function () {
    if (numero.val() < 1) {
        numero.val(1);
    }
    else if (numero.val() > 99999) {
        numero.val(99999);
    }
});

$('#Telefone').mask("(99) 99999-9999");
$('#CEP').mask("99999999");

numero.attr('maxlength', 5);
$('#Primeiro_Nome').attr('maxlength', 50);
$('#Sobrenome').attr('maxlength', 100);
$('#Endereco').attr('maxlength', 100);
$('#Complemento').attr('maxlength', 20);
$('form').validate({
    rules: {
        Primeiro_Nome: {
            required: true,
            minlength: 2
        },
        Sobrenome: {
            required: true,
            minlength: 2
        },
        CEP: {
            required: true,
        },
        Endereco: {
            required: true
        },
        Numero: {
            required: true
        },
        Complemento: {
            required: true
        },
        Estado: {
            required: true
        },
        Cidade: {
            required: true
        },
        Telefone: {
            required: true
        }
    }
});

function acha_sigla(estado) {
    switch (estado) {
        case 'AC': return 'Acre';
        case 'AL': return 'Alagoas';
        case 'PR': return 'Paraná';
    }
}

var modalaux = $(".modal");
$(modalaux).find(".modal-header button.close").on("click", function () {
    $(modalaux).modal("hide");
});

$(modalaux).find(".modal-footer button").on("click", function () {
    $(modalaux).modal("hide");
});

$("#CEP").blur(function () {
    var that = $(this);
    var cep = that.val();
    var regex = /^[0-9]{8}$/;
    if (regex.test(cep)) {
        $.getJSON("//viacep.com.br/ws/" + cep + "/json/?callback=?", function (dados) {
            if (!dados.erro) {
                $('#Endereco').val(dados.logradouro + ' - ' + dados.bairro);
                var x = document.getElementById("Estado");
                var estado = acha_sigla(dados.uf);
                var i = 0;
                while (x.options[i].textContent.localeCompare(estado) != 0) {
                    i++;
                }
                x.options[i].selected = true;
                dropDownEstado(dados.localidade);
            }
            else {
                var modalcar = $("#myModal2");
                $(modalcar).find(".modal-dialog").addClass("modal-sm");
                $(modalcar).find(".modal-body p").text("CEP inválido!");
                $(modalcar).modal("show");
            }
        });
    }
});