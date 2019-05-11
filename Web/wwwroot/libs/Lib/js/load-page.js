

function LoadPage(productTypeID, Order) {
    var urlGetProductSelected = "/Product/ListProductByType/";
    var a = $.ajax({
        url: urlGetProductSelected,
        async: false,
        data: { productTypeId: productTypeID, order: Order },
        type: "POST",
        success: function (data) {
            console.log(data);
            $("#listContent").html(data).show();
        }
    });

}


function LoadPageByPrice(productTypeID, price) {
    var urlGetProductSelected = "/Product/ListProductByType/";
    var a = $.ajax({
        url: urlGetProductSelected,
        async: false,
        data: { productTypeId: productTypeID, amount: price },
        type: "POST",
        success: function (data) {
            console.log(data);
            $("#listContent").html(data).show();
        }
    });

}

//function LoadPageByProductType(productTypeID) {
//    var urlGetProductSelected = "/Product/ProductByProductTypeAjaxLoad/";
//    var a = $.ajax({
//        url: urlGetProductSelected,
//        async: false,
//        data: { productTypeId: productTypeID },
//        type: "POST",
//        success: function (data) {
//            console.log(data);
//            $("#listContent").html(data).show();
//        }
//    });

//}
