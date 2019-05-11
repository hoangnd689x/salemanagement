$("#btnDeleteProducts").click(function () {
    var lstProducts = "";

    $("input[class=ckProduct]").each(function () {

        if (this.checked) {
            var thisVal = $(this).val();

            lstProducts = lstProducts += (lstProducts === "" ? thisVal : "," + thisVal);

            console.debug(lstProducts);
        }
    });

    $.ajax({

        url: "/Product/DeleteProducts",
        data: { Ids: lstProducts },
        method: "POST",
        success: function (data) {
            RefreshPage();
        }
    });

});


$("#checkAllProduct").click(function () {

    $("input[class=ckProduct]").each(function () {
        if (this.checked) {
            $(this).prop("checked", false);
        }
        else
        {
            $(this).prop("checked", true);
        }
    });
});

function RefreshPage() {
    location.reload();
}