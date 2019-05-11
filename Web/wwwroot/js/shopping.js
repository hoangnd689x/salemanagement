
//    var dialog = document.getElementById('myDialog');
//    document.getElementById('show').onclick = function () {

//        var urlGetProductSelected = "/Home/OpenShoppingCard/";
//        var a = $.ajax({
//            url: urlGetProductSelected,
//            async: false,
//            data: {},
//            type: "POST",
//            success: function (data) {
//                console.log(data);

//                var markup = "";
//                markup += "<div id='dialogcart' style='display: block;'>";
//                markup += "<div id='dialogcart_content'>";
//                markup += "<div class='close'></div><table width='100%' id='mainTable' cellpadding='5' style= 'border-collapse: collapse; border-width: 1px; float: left;' cellspacing= '0' > ";
//                markup += "<tbody>";
//                markup += "<tr style='background: #dddddd'>";
//                markup += "<th>Sản phẩm</th>";
//                markup += "<th style='width: 60%'>";
//                markup += "Tên sản phẩm";
//                markup += "</th>";
//                markup += "<th style='width: 10%'>";
//                markup += "Mã sản phẩm";
//                markup += "</th>";
//                markup += "<th style='width: 10%'>";
//                markup += "Giá";
//                markup += "</th>";
//                markup += "<th style='width: 5%'>";
//                markup += "Số lượng";
//                markup += "</th>";
//                markup += "<th style='width: 15%'>";
//                markup += "Tổng cộng";
//                markup += "</th>";
//                markup += "</tr>";

//                for (var x = 0; x < data.length; x++) {
//                    markup += "<tr>";
//                    markup += "<td>";
//                    markup += "<a href=''>";
//                    markup += "<img alt='' src='" + data[x].images[0].imageUrl + "' width='100' height='60'>";
//                    markup += "</a>";
//                    markup += "</td>";
//                    markup += "<td>";
//                    markup += "<p>";
//                    markup += "<a href='/am-chen-men-ran-co/bo-am-chen-dang-nhat-men-ran-co-boc-dong-23179c18720.aspx'>";
//                    markup += "<b>";
//                    markup += data[x].name;
//                    markup += "</b>";
//                    markup += "</a>";
//                    markup += "</p>";
//                    markup += "<a style='color: #ff6600' class='remove-item' href='' onclick='DeleteProduct(" + data[x].id + ")'>Bỏ sản phẩm</a>";
//                    markup += "</td>";
//                    markup += "<td align='center'>";
//                    markup += data[x].code;
//                    markup += "</td>";
//                    markup += "<td align='right'>";
//                    markup += data[x].price;
//                    markup += "</td>";
//                    markup += "<td align='center'>";
//                    markup += "<span style='background: rgba(0, 0, 0, 0) url('/Images/button-card.png') repeat scroll 0 0; float: left; height: 17px; margin-left: 17px; width: 20px; cursor: pointer' onclick='changeFromCartAdd('23179',true)'></span>";
//                    markup += "<input type='text' class='number' maxlength='3' id='input23179' pid='23179' onchange= 'changeFromCart('23179',this)' style='width: 50px; text-align: center' value= '1' > ";
//                    markup += "<span style='background: rgba(0, 0, 0, 0) url('/Images/button-card.png') repeat scroll 0 bottom; float: left; height: 17px; margin-left: 17px; width: 20px; cursor: pointer' onclick='changeFromCartAdd('23179', false)'></span>";
//                    markup += "</td>";
//                    markup += "<td align='right'>";
//                    markup += data[x].price;
//                    markup += "</td>";
//                    markup += "</tr>";

//                }

//                markup += "<tr>";
//                markup += "<td colspan='6' align='right'>Giá trên chưa bao gồm: phí vận chuyển, VAT</td>";
//                markup += "</tr>";
//                markup += "</tbody>";
//                markup += "</table>";
//                markup += "</div>";
//                markup += "<div class='naviagtion-order'>";
//                markup += "<a class='process' style='float: left' href='/ ' id='hide'>Tiếp tục mua hàng</a>";
//                markup += "<a class='process' href='/ shopping - cart.aspx#info'>Thanh toán</a>";
//                markup += "</div>";
//                markup += "</div>";


//                $("#ShoppingCart").html(markup).show();


//            }

//        });
//        dialog.showModal();
//    };
//    document.getElementById('hide').onclick = function () {
//        dialog.close();
//    };



//function DeleteProduct(productID) {
//    var urlGetProductSelected = "/Home/DeleteProductInSelected/";
//    var a = $.ajax({
//        url: urlGetProductSelected,
//        async: false,
//        data: { productId: productID},
//        type: "POST",
//        success: function (data) {
//            console.log(data);

//            var markup = "";
//            markup += "<div id='dialogcart' style='display: block;'>";
//            markup += "<div id='dialogcart_content'>";
//            markup += "<div class='close'></div><table width='100%' id='mainTable' cellpadding='5' style= 'border-collapse: collapse; border-width: 1px; float: left;' cellspacing= '0' > ";
//            markup += "<tbody>";
//            markup += "<tr style='background: #dddddd'>";
//            markup += "<th>Sản phẩm</th>";
//            markup += "<th style='width: 60%'>";
//            markup += "Tên sản phẩm";
//            markup += "</th>";
//            markup += "<th style='width: 10%'>";
//            markup += "Mã sản phẩm";
//            markup += "</th>";
//            markup += "<th style='width: 10%'>";
//            markup += "Giá";
//            markup += "</th>";
//            markup += "<th style='width: 5%'>";
//            markup += "Số lượng";
//            markup += "</th>";
//            markup += "<th style='width: 15%'>";
//            markup += "Tổng cộng";
//            markup += "</th>";
//            markup += "</tr>";

//            for (var x = 0; x < data.length; x++) {
//                markup += "<tr>";
//                markup += "<td>";
//                markup += "<a href=''>";
//                markup += "<img alt='' src='" + data[x].images[0].imageUrl + "' width='100' height='60'>";
//                markup += "</a>";
//                markup += "</td>";
//                markup += "<td>";
//                markup += "<p>";
//                markup += "<a href='/am-chen-men-ran-co/bo-am-chen-dang-nhat-men-ran-co-boc-dong-23179c18720.aspx'>";
//                markup += "<b>";
//                markup += data[x].name;
//                markup += "</b>";
//                markup += "</a>";
//                markup += "</p>";
//                markup += "<a style='color: #ff6600' class='remove-item' href='' onclick='DeleteProduct(" + data[x].id + ")'>Bỏ sản phẩm</a>";
//                markup += "</td>";
//                markup += "<td align='center'>";
//                markup += data[x].code;
//                markup += "</td>";
//                markup += "<td align='right'>";
//                markup += data[x].price;
//                markup += "</td>";
//                markup += "<td align='center'>";
//                markup += "<span style='background: rgba(0, 0, 0, 0) url('/Images/button-card.png') repeat scroll 0 0; float: left; height: 17px; margin-left: 17px; width: 20px; cursor: pointer' onclick='changeFromCartAdd('23179',true)'></span>";
//                markup += "<input type='text' class='number' maxlength='3' id='input23179' pid='23179' onchange= 'changeFromCart('23179',this)' style='width: 50px; text-align: center' value= '1' > ";
//                markup += "<span style='background: rgba(0, 0, 0, 0) url('/Images/button-card.png') repeat scroll 0 bottom; float: left; height: 17px; margin-left: 17px; width: 20px; cursor: pointer' onclick='changeFromCartAdd('23179', false)'></span>";
//                markup += "</td>";
//                markup += "<td align='right'>";
//                markup += data[x].price;
//                markup += "</td>";
//                markup += "</tr>";

//            }

//            markup += "<tr>";
//            markup += "<td colspan='6' align='right'>Giá trên chưa bao gồm: phí vận chuyển, VAT</td>";
//            markup += "</tr>";
//            markup += "</tbody>";
//            markup += "</table>";
//            markup += "</div>";
//            markup += "<div class='naviagtion-order'>";
//            markup += "<a class='process' style='float: left' href='/ ' id='hide'>Tiếp tục mua hàng</a>";
//            markup += "<a class='process' href='/ shopping - cart.aspx#info'>Thanh toán</a>";
//            markup += "</div>";
//            markup += "</div>";


//            $("#ShoppingCart").html(markup).show();


//        }

//    });
//    dialog.showModal();
//}