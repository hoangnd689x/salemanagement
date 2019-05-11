

function AddProductToCard(productID) {
    var dialog = document.getElementById('myDialog');


    // neu mo gio hang tu header productId ==0, tu click san pham thi productId == productId
        var urlGetProductSelected = "/Home/AddProductToShoppingCard/";
        var a = $.ajax({
            url: urlGetProductSelected,
            async: false,
            data: { productId: productID },
            type: "POST",
            success: function (data) {
                $("#ShoppingCart").html(data.toString()).show();
                var c = $("#NumberOfShoppingCard").val();
                $("#NumberProductSelected12").html(c).show();
            }
        });

        dialog.showModal();
    }

function ThanhToan(sizearr) {
    var arrid = [];
    var arramount = [];
    for (var i = 0; i < sizearr; i++) {
        arrid.push($("#prId"+i).val());
        arramount.push($("#prAmount"+i).val());
    }
    post('/Home/Order', { arrId: arrid, arrAmount: arramount });
}

function post(path, params) {
    // The rest of this code assumes you are not using a library.
    // It can be made less wordy if you use one.
    var form = document.createElement("form");
    form.setAttribute("method", "post");
    form.setAttribute("action", path);

    for (var key in params) {
        if (params.hasOwnProperty(key)) {
            var hiddenField = document.createElement("input");
            hiddenField.setAttribute("type", "hidden");
            hiddenField.setAttribute("name", key);
            hiddenField.setAttribute("value", params[key]);

            form.appendChild(hiddenField);
        }
    }

    document.body.appendChild(form);
    form.submit();
}

function postOrder(arrsize) {

    // first validate
    if ($("#txtname").val().length == 0) {
        $("#txtNameValidate").html("Vui lòng nhập họ tên").show();
        $("#txtname").focus();
        return false;
    }
    if ($("#txtadd").val().length == 0) {
        $("#txtaddValidate").html("Vui lòng nhập địa chỉ").show();
        $("#txtadd").focus();
        return false;
    }
    if ($("#txtphone").val().length == 0) {
        $("#txtphoneValidate").html("Vui lòng nhập số điện thoại").show();
        $("#txtphone").focus();
        return false;
    }
    if ($("#txtemail").val().length == 0) {
        $("#txtEmailValidate").html("Vui lòng nhập email theo định dạng xxx@abc.zxc").show();
        $("#txtemail").focus();
        return false;
    }



    var arrid = [];
    var arramount = [];
    for (var i = 0; i < arrsize; i++) {
        arrid.push($("#arrid" + i).val());
        arramount.push($("#amount" + i).val());
    }
    console.log(arrid);
    console.log(arramount);
    var hoten = $("#txtname").val();
    var thanhpho = $("#ctl00_ContentPlaceHolder1_ShopCart1_ddlCity").val();
    var address = $("#txtadd").val();
    var phone = $("#txtphone").val();
    var email = $("#txtemail").val();
    var magiamgia = $("#txtPromotion").val();
    var ghichu = $("#ctl00_ContentPlaceHolder1_ShopCart1_txtDesc").val();
    post('/Home/SubmitOrder', {
        arrId: arrid, arrAmount: arramount, Name: hoten, City: thanhpho,
        Address: address, Phone: phone, Email: email, DiscountCode: magiamgia, Notes: ghichu
    });
}



function postComment(productID) {
    $("#txtsuccess").html("").show();
    // first validate
    if ($("#txtnameCM").val().length == 0) {
        $("#txtNameValidate").html("Vui lòng nhập họ tên").show();
        $("#txtnameCM").focus();
        return false;
    }
    if ($("#txtphoneCM").val().length == 0) {
        $("#txtphoneValidate").html("Vui lòng nhập số điện thoại").show();
        $("#txtphoneCM").focus();
        return false;
    }
    if ($("#txtemailCM").val().length == 0) {
        $("#txtEmailValidate").html("Vui lòng nhập email theo định dạng xxx@abc.zxc").show();
        $("#txtemailCM").focus();
        return false;
    }
    
    var hoten = $("#txtnameCM").val();
    var phone = $("#txtphoneCM").val();
    var email = $("#txtemailCM").val();
    var ghichu = $("#ctl00_ContentPlaceHolder1_ShopCart1_txtDescCM").val();

    var url = "/Home/SubmitComment/";
    var a = $.ajax({
        url: url,
        async: false,
        data: { productId: productID, Name: hoten, Phone: phone, Email: email, Notes: ghichu },
        type: "POST",
        success: function (data) {
            if (data == 1) {
                $("#txtnameCM").html("").val("");
                $("#txtphoneCM").html("").val("");
                $("#txtemailCM").html("").val("");
                $("#ctl00_ContentPlaceHolder1_ShopCart1_txtDescCM").val("");

                $("#txtsuccess").html("Gửi phản hồi thành công !").show();
            }
            console.log("==================");
            console.log(data);
        }
    });


    //post('/Home/SubmitComment', {
    //    ProductId:ProductID, Name: hoten, Phone: phone, Email: email, Notes: ghichu
    //});
}


function changeAmountFromCart(sizearr) {
    var arrid = [];
    var arramount = [];
    for (var i = 0; i < sizearr; i++) {
        arrid.push($("#arrid" + i).val());
        arramount.push($("#amount" + i).val());
    }
    post('/Home/Order', { arrId: arrid, arrAmount: arramount });
}

function UpNumberAmount(sizearr, productId) {

    var arrid = [];
    var arramount = [];
    for (var i = 0; i < sizearr; i++) {
        var Ids = Number($("#arrid" + i).val());
        var Amounts = Number($("#amount" + i).val());
        if (Ids == productId) {
            Amounts += 1;
        }
        arrid.push($("#arrid" + i).val());
        arramount.push(Amounts);
    }
    post('/Home/Order', { arrId: arrid, arrAmount: arramount });
}

function DownNumberAmount(sizearr, productId) {
    var arrid = [];
    var arramount = [];
    for (var i = 0; i < sizearr; i++) {
        var Ids = Number($("#arrid" + i).val());
        var Amounts = Number($("#amount" + i).val());
        if (Ids == productId && Amounts > 0) {
            Amounts -= 1;
        }
        arrid.push($("#arrid" + i).val());
        arramount.push(Amounts);
    }
    post('/Home/Order', { arrId: arrid, arrAmount: arramount });
}


function UpNumberAmountFromPop(sizearr, productID) {
    var dialog = document.getElementById('myDialog');
    var arrid = [];
    var arramount = [];
    for (var i = 0; i < sizearr; i++) {
        var Ids = Number($("#prId" + i).val());
        var Amounts = Number($("#prAmount" + i).val());
        arrid.push(Ids);
        arramount.push(Amounts);
    }

    // neu mo gio hang tu header productId ==0, tu click san pham thi productId == productId
    var urlGetProductSelected = "/Home/AddProductToShoppingCardUp/";
    var a = $.ajax({
        url: urlGetProductSelected,
        async: false,
        data: { productId: productID, arrId: arrid.toString(), arrAmount: arramount.toString() },
        type: "POST",
        success: function (data) {
            console.log(data);
            $("#ShoppingCart").html(data.toString()).show();
            var c = $("#NumberOfShoppingCard").val();
            $("#NumberProductSelected12").html(c).show();
        }
    });

    dialog.showModal();
}


function DownNumberAmountFromPop(sizearr, productID) {
    var dialog2 = document.getElementById('myDialog2');
    var arrid = [];
    var arramount = [];
    for (var i = 0; i < sizearr; i++) {
        var Ids = Number($("#prId" + i).val());
        var Amounts = Number($("#prAmount" + i).val());
        arrid.push($("#prId" + i).val());
        arramount.push(Amounts);
    }

    // neu mo gio hang tu header productId ==0, tu click san pham thi productId == productId
    var urlGetProductSelected = "/Home/AddProductToShoppingCardDown/";
    var a = $.ajax({
        url: urlGetProductSelected,
        async: false,
        data: { productId: productID, arrId: arrid, arrAmount: arramount },
        type: "POST",
        success: function (data) {
            console.log(data);
            $("#ShoppingCart2").html(data.toString()).show();
            
            var c = $("#NumberOfShoppingCard").val();
            $("#NumberProductSelected12").html(c).show();
        }
    });

    dialog2.showModal();
}




function DeleteProductSelectedFromOrder(sizearr, productID) {
    var arrid = [];
    var arramount = [];
    for (var i = 0; i < sizearr; i++) {
        arrid.push($("#arrid" + i).val());
        arramount.push($("#amount" + i).val());
    }
    post('/Home/DeleteProductInSelectedOrder', { arrId: arrid, arrAmount: arramount, productId: productID });
}



document.getElementById('showProductSelected').onclick = function () {
    var dialog = document.getElementById('myDialog');
    var urlGetProductSelected = "/Home/OpenShoppingCard/";
    var a = $.ajax({
        url: urlGetProductSelected,
        async: false,
        data: {},
        type: "POST",
        success: function (data) {
            $("#ShoppingCart").html(data.toString()).show();
        }
    });
    dialog.showModal();
};

function DeleteProduct(sizearr, productID) {

    var arrid = [];
    var arramount = [];
    for (var i = 0; i < sizearr; i++) {
        arrid.push($("#prId" + i).val());
        arramount.push($("#prAmount" + i).val());
    }

    var dialog = document.getElementById('myDialog');
    var urlGetProductSelected = "/Home/DeleteProductInSelected/";
    var a = $.ajax({
        url: urlGetProductSelected,
        async: false,
        data: { productId: productID, arrId: arrid.toString(), arrAmount: arramount.toString() },
        type: "POST",
        success: function (data) {
            $("#ShoppingCart").html(data.toString()).show();

            var c = $("#NumberOfShoppingCard").val();
            $("#NumberProductSelected12").html(c).show();
        }

    });
    
    dialog.showModal();

};




function UnselectFromOrderPage(productID) {
    var urlUnSelectedProduct = "/Home/UnselectProduct/";
    var a = $.ajax({
        url: urlUnSelectedProduct,
        async: false,
        data: { productId: productID },
        type: "POST",
        success: function (data) {
        }

    });
}