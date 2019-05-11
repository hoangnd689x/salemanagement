$("#btnDeleteNews").click(function () {
    var lstNews = "";

    $("input[class=ckNews]").each(function () {

        if (this.checked) {
            var thisVal = $(this).val();

            lstNews = lstNews += (lstNews === "" ? thisVal : "," + thisVal);

            console.log(lstNews);
        }
       
    });

    $.ajax({

        url: "/News/DeleteNews",
        data: { Ids: lstNews },
        method: "POST",
        success: function (data) {
            RefreshPage();
        }
    });

});


$("#checkAllNews").click(function () {

    console.log(1);
    $("input[class=ckNews]").each(function () {
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