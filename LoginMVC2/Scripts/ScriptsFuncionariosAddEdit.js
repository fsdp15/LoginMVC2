var $quantidade = $('#Quantidade');
$quantidade.attr('value', 1);
$quantidade.change(function () {
    var max = 100;
    var min = 1;
    var valor = $quantidade.val();
    if (valor > max) {
        $quantidade.val(max);
    }
    else if (valor < min) {
        $quantidade.val(min);
    }
});

$preco = $('#Preco');
$preco.maskMoney({ showSymbol: true, symbol: "R$", decimal: ",", thousands: "." })
$preco.attr("maxlength", 9);
$('#Nome').attr('maxlength', 50);
$('form').validate({
    rules: {
        Nome: {
            required: true,
            minlength: 2
        },
        Preco: {
            required: true
        },
        Quantidade: {
            required: true,
            range: [1, 100]
        }
    }
});