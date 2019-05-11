

function loadTest(productTypeID) {
    var dialog = document.getElementById('myDialog');
    
    // neu mo gio hang tu header productId ==0, tu click san pham thi productId == productId
    var urlGetProductSelected = "/Product/ListProductByParent";
        var a = $.ajax({
            url: urlGetProductSelected,
            async: false,
            data: { productTypeId: productTypeID },
            type: "POST",
            success: function (data) {
                alert('here');
                console.log(data);
                $("#listContent").html(data).show();
            }
        });

        dialog.showModal();
    }

