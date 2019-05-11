$("#btnDeleteFeedbacks").click(function () {
    var lstFeedbacks = "";

    $("input[class=ckFeedback]").each(function () {

        if (this.checked) {
            var thisVal = $(this).val();

            lstFeedbacks = lstFeedbacks += (lstFeedbacks === "" ? thisVal : "," + thisVal);

            console.debug(lstFeedbacks);
        }
    });

    $.ajax({

        url: "/Feedback/DeleteFeedbacks",
        data: { Ids: lstFeedbacks },
        method: "POST",
        success: function (data) {
            RefreshPage();
        }
    });

});

$("#checkAllFeedback").click(function () {
    console.log(1);
    $("input[class=ckFeedback]").each(function () {
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