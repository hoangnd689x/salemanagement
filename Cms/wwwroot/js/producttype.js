$("#btnDeleteProductTypes").click(function () {
    var lstProductTypes = "";

    $("input[class=ckProductType]").each(function () {

        if (this.checked) {
            var thisVal = $(this).val();
            console.log(thisVal);
            lstProductTypes = lstProductTypes += (lstProductTypes === "" ? thisVal : "," + thisVal);

            console.debug(lstProductTypes);
        }
    });

    $.ajax({

        url: "/ProductType/DeleteProductTypes",
        data: { Ids: lstProductTypes },
        method: "POST",
        success: function (data) {
            RefreshPage();
        }
    });

});


$("#checkAllProductType").click(function () {
    console.log(1);
    $("input[class=ckProductType]").each(function () {
        if (this.checked) {
            $(this).prop("checked", false);
        }
        else {
            $(this).prop("checked", true);
        }
    });
});

function RefreshPage() {
    location.reload();
}