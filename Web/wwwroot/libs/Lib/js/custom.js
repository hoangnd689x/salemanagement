function equalHeight(group) {
    var tallest = 0;
    group.each(function () {
        var thisHeight = $(this).height();
        if (thisHeight > tallest) {
            tallest = thisHeight;
        }
    });
    group.height(tallest);
}

function addToCart(id) {
    var cart = readCookie("cart");
    var items = new Array();
    if (cart != null && cart.length > 0) {
        items = cart.split('|');
    }

    var found = false;
    for (var i = 0; i < items.length; i++) {
        var item = items[i].split(',');
        if (item.length == 2) {
            if (item[0] == id) {
                found = true;
                item[1] = parseInt(item[1]) + 1;
            }
            items[i] = item.join(',');
        }

    }
    if (!found) {
        items.push(id + ',' + 1);
    }
    createCookie("cart", removeEmptyItem(items.join('|')), 7);
}

function removeFromCart(id) {
    var cart = readCookie("cart");
    var items = new Array();
    if (cart != null && cart.length > 0) {
        items = cart.split('|');
    }

    var found = false;
    for (var i = 0; i < items.length; i++) {
        var item = items[i].split(',');
        if (item.length == 2) {
            if (item[0] == id) {
                found = true;
                items[i] = "";
            }
        }
    }

    createCookie("cart", removeEmptyItem(items.join('|')), 7);

}

function changeFromCart(id, obj) {
    location.href = "/shopping-cart.aspx?p=" + id + '&q=' + obj.value + '&a=update';
    return;
}

function changeFromCartAdd(id, inc) {
    var val = $("#input" + id).val();
    if (inc) {
        val = eval(val) + 1;
    }
    else {
        val = eval(val) - 1;
        if (val < 1)
            val = 1;

    }
    if (!$("#dialogcart").is(':visible')) {
        location.href = "/shopping-cart.aspx?p=" + id + '&q=' + val + '&a=update'
        return;
    }
    else
        addToCard("/shopping-cart.aspx?p=" + id + '&q=' + val + '&a=update');
}

function addToCard(url) {
    $("#mask").show();
    $.ajax({
        url: url,
        cache: false,
        success: function (response) {
            var parsed = $.parseHTML(response);
            result = $(parsed).find("#mainTable");

            setTimeout(function () {
                var trCount = $(result).find('tr').length;
                var itemCount = eval(trCount) - 3;
                if (itemCount <= 0)
                    itemCount = 0;
                $(".counter").html(itemCount);

            }, 100);

            $("#dialogcart_content").html('<div class="close" onclick="continueShopping();"></div>');
            $("#dialogcart_content").append(result);

            $("#dialogcart").fadeIn();
            $(".remove-item").click(function (e) {
                e.preventDefault();
                addToCard($(this).attr('href'));
            });

            $(".number").on("change", function (e) {
                e.preventDefault();
                addToCard($(this).attr('href'));
            });

        }
    });
}

function continueShopping() {
    $("#mask").fadeOut();
    var dialog = document.getElementById('myDialog');
    dialog.close();
    var dialog2 = document.getElementById('myDialog2');
    dialog2.close();

    //location.reload();
}


function removeEmptyItem(cc) {
    while(cc.indexOf("||") != -1) {
        cc = cc.replace("||", "|");
    }
    return cc;
}
function createCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 30 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else var expires = "";
    document.cookie = name + "=" + value + expires + "; path=/";
}

function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) {
            return c.substring(nameEQ.length, c.length);
        }
    }
    return null;
}

function parseUrl(val) {
    var result = null,
        tmp = [];

    location.search.substr(1)
        .split("&")
        .forEach(function (item) {
            tmp = item.split("=");
            if (tmp[0] === val) result = decodeURIComponent(tmp[1]);
        });
    return result;
}
/*kiem tra User xem thuoc domain nao mua*/
var local = parseUrl("utm_source") != null ? parseUrl("utm_source") : (document.referrer != null ? document.referrer.split('/')[2] : document.hostname);
if (readCookie("bto_user") == null || parseUrl("utm_source") != null) {
    if (local == undefined)
        local = 'battrangonline.vn';
    createCookie("bto_user", local, 1);
}
/*kiem tra User xem thuoc domain nao mua*/