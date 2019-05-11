$(function () {

    //$('#Price').change(function () {

    //    console.log(1);
    //    var price = parseInt($("#Price").val());
    //    if (isNaN(price)) {
    //        price = 0;
    //    }
    //    var discount = parseInt($("#Discount").val());
    //    if (isNaN(discount)) {

    //        discount = 0;
    //    }
    //    var changedPrice = price * (100 - discount) / 100;
    //    console.log(changedPrice);

    //    $("#ChangedPrice").val(changedPrice);
    //});

    $("input[name ='Price'], input[name='Discount']").change(function () {

        console.log(2);
        var price = parseInt($("#Price").val());
        if (isNaN(price))
        {
            price = 0;
        }

        var discount = parseInt($("#Discount").val());
        if (isNaN(discount))
        {
            discount = 0;
        }

        var changedPrice = price * (100 - discount) / 100;
        $("#ChangedPrice").val(changedPrice);

    });
});