$("#btnDeleteBills").click(function () {
    var lstBills = "";

    $("input[class=ckBill]").each(function () {

        if (this.checked) {
            var thisVal = $(this).val();

            lstBills = lstBills += (lstBills === "" ? thisVal : "," + thisVal);

            console.debug(lstBills);
        }
    });

    $.ajax({

        url: "/Bill/DeleteBills",
        data: { Ids: lstBills },
        method: "POST",
        success: function (data) {
            RefreshPage();
        }
    });

});


$("#checkAllBill").click(function () {


    $("input[class=ckBill]").each(function () {
        if (this.checked) {
            $(this).prop("checked", false);
        }
        else {

            $(this).prop("checked", true);
        }

    });

    //RefreshPage();

});

function RefreshPage() {
    location.reload();
}