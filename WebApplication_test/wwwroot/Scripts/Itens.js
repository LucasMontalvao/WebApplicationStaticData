function SaveOrder() {
    var orderId = $("#orderId").val();
    var clientId = $("#clientId").val();
    var totalOrderValue = $("#totalOrderValue").val();
    var totalOrderValueDescount = $("#totalOrderValueDescount").val();

    var token = $('input[name="__RequestVerificationToken"]').val();
    var tokenadr = $('form[action="/ProductOrder/Create"] input[name="__RequestVerificationToken"]').val();
    var headers = {};
    var headersadr = {};
    headers['__RequestVerificationToken'] = token;
    headersadr['__RequestVerificationToken'] = tokenadr;

    var url = "/ProductOrder/Create";
    $.ajax({
        url: url,
        type: "POST",
        dataType: "json",
        headers: headersadr,
        data: {
            orderId: orderId,
            clientId: clientId,
            totalOrderValue: totalOrderValue,
            totalOrderValueDescount: totalOrderValueDescount,
            __RequestVerificationToken: token
        },
        success: function (id) {
            if (id> 0) {
                showProductsForm(id);
            }
        }
    })
}

function showProductsForm(orderId) {
    var url = "/OrderItens/AddProducts";

    $.ajax({
        url: url,
        type: "GET",
        data: { orderId: orderId },
        datatype: "html",
        success: function (data) {
            var divOrderItens = $("#divOrderItens");
            divOrderItens.empty();
            divOrderItens.show();
            divOrderItens.html(data);
        }
    })
}

function SaveProducts() {
    var productId = $("#productId").val();
    var quantity = $("#Quantity").val();
    var clientDiscount = $("#clientDiscount").val();
    var orderId = $("#orderId").val();

    var url = "/OrderItens/SaveItens";

    $.ajax({
        url: url,
        data: {
            orderId: orderId,
            quantity: quantity,
            productId: productId,
            clientDiscount: clientDiscount
        },
        type: "GET",
        datatype: "json",
        success: function (id) {
            if (id > 0) {
                showProductsForm(orderId);
            }
        }
    });
}

function checkQuantity() {
    var productId = $("#productId").val();
    var quantity = $("#Quantity").val();

    var url = "/OrderItens/CheckQuantity";

    debugger;
    $.ajax({
        url: url,
        data: {
            productId: productId,
            quantity: quantity,
        },
        type: "GET",
        datatype: "json",
        success: function (outOfStock) {
            if (outOfStock === true) {
                alert("The quantity entered exceeds the current stock or you dont choose a product!");
                $("#Quantity").val(0);
            }
        }
    });
}

function checkDiscountValue() {
    var productId = $("#productId").val();
    var clientDiscount = $("#clientDiscount").val();

    var url = "/OrderItens/CheckDiscount";

    $.ajax({
        url: url,
        data: {
            productId: productId,
            clientDiscount: clientDiscount,
        },
        type: "GET",
        datatype: "json",
        success: function (response) {
            if (response.result === false && response.maxDiscount == "notfound") {
                alert("The discount you entered is out of range for this product or you dont choose a product!");
                $("#clientDiscount").val(0);
            } else if (response.result === false && response.maxDiscount != "notfound") {
                alert("The discount you entered is out of range for this product, the maximun discount you can obtain is " + response.maxDiscount + "%");
                $("#clientDiscount").val(0);
            }
        }
    });
}

function showProducts(id) {
    var url = "/OrderItens/GetItens";
    $.ajax({
        url: url,
        data: {
            orderId: id
        },
        type: "GET",
        datatype: "html",
        success: function (data) {
            var divOrderItensDetails = $("#divOrderItensDetails");
            divOrderItensDetails.empty();
            divOrderItensDetails.show();
            divOrderItensDetails.html(data);
        }
    });

} 