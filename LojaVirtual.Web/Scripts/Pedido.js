function SalvarPedido() {
    //Data
    var data = $("#Data").val();

    //Cliente
    var Cliente = {
        Id: $("#CodCli").val()
        , Nome: $("#Cliente").val()
    };

    //Valor
    var total = $("#Total").val().replace("R$", "");

    var token = $('input[name="__RequestVerificationToken"]').val();
    var tokenadr = $('form[action="/Vendas/Create"] input[name="__RequestVerificationToken"]').val();
    var headers = {};
    var headersadr = {};
    headers['__RequestVerificationToken'] = token;
    headersadr['__RequestVerificationToken'] = tokenadr;

    //Gravar
    var url = "/Vendas/Create";

    var id = $("#Id").val();


    //if (id !== "0") {
    //    url = "/Vendas/Edit";
    //}

    $.ajax({
        url: url
        , type: "POST"
        , datatype: "json"
        , headers: headersadr
        , data: { Id: id, CodCli: Cliente.Id, Cliente: Cliente.Nome, Data: data, Total: total, __RequestVerificationToken: token }
    }).done(function (data) {
        if (data.Resultado > 0) {
            ListarItens(data.Resultado);
        }
    });
}

function ListarPedido() {
    var url = "/Pedido/Listar";

    $.ajax({
        url: url
        , datatype: "html"
        , type: "GET"
        , data: { nome: nome }
    }).done(function (data) {
        $("#listarpedidos").html(data);
    })(jQuery);

}

function ListarItens(idPedido) {

    var url = "/Itens/ListarItens";

    $.ajax({
        url: url
        , type: "GET"
        , data: { id: idPedido }
        , dataType: "html"
        , success: function (data) {
            var divItens = $("#divItens");
            divItens.empty();
            divItens.show();
            divItens.html(data);
        }
    });
}

function SalvarItens() {

    var quantidade = $("#QuantProd").val();
    var produto = $("#Produto").val();
    var valUnit = $("#ValUnit").val();
    var idPedido = $("#Pedido").val();

    //var token = $('input[name="__RequestVerificationToken"]').val();
    //var tokenadr = $('form[action="/Vendas/Create"] input[name="__RequestVerificationToken"]').val();
    //var headers = {};
    //var headersadr = {};
    //headers['__RequestVerificationToken'] = token;
    //headersadr['__RequestVerificationToken'] = tokenadr;

    //Gravar
    var url = "/Itens/Create";

    var id = $("#Id").val();

    $.ajax({
        url: url
        , type: "GET"
        , datatype: "html"
        //, headers: headersadr
        , data: { Pedido: idPedido, Produto: produto, ValUnit: valUnit, QuantProd: quantidade, Id: id }
        , success: function (data) {
            ListarItens(idPedido);
        }
    });
}



