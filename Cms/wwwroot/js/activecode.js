function GetProductsByName()
{

    console.log("check fat lào !!!");
    var _name = $("[name='k']").val();
    var _code = $("[name='code']").val();
    var _type = $("[name='ProductTypeId']").val();

    var url = "/Product/GetProducts";
    $.ajax({

        url: url,
        data: { typeId: _type, name: _name, code: _code },
        
        success: function (data) {

            $("#lstProduct").empty();

            console.log(data);

            for (var x = 0; x < data.Products.length; x++)
            {
                
                var x = "<label class=\"mt-checkbox mt-checkbox-outline\">" + 
                data.Products[x].Name + 
                    "<input type=\"checkbox\" value=" + data.Products[x].Id + " name=\"RelatedProductIds\"/><span></span></label>"

                $("#lstProduct").append(x);

            }

            $("#pager").empty();
            $("#pager").html("<pager pager=" + data.Pager + "></pager>").show();
        },
        error: function (reponse) {
            console.log("error1");
        }
    });
}


function GoToModal() {

    var type = $("[name'ProductTypeId']").val();
    var url = "/Product/CreateRelatedProducts?typeId=" + type;



}

//function PrepareCreateSoftcard(_countryId) {
//    //$('#loadingDiv').show();
//    var urlGetAmountByCountry = "/ActiveCode/GetAmountByCountry/";

//    $.ajax({
//        url: urlGetAmountByCountry,
//        data: { CountryId: _countryId },
//        cache: false,
//        type: "POST",
//        success: function (data) {
//            var arrayInput = [];
//            var listAmount = [];
//            for (var x = 0; x < data.length; x++) {
//                var inputValue = $("#input" + x).val();
//                if (inputValue === "") {
//                    arrayInput[x] = "0";
//                } else {
//                    arrayInput[x] = inputValue;
//                }
//                listAmount[x] = data[x].id;
//            }
//            console.log(arrayInput);
//            console.log(listAmount);
//            CreateSoftcard(_countryId, arrayInput, listAmount);

//        },
//        error: function (reponse) {
//            console.log("error2 : " + reponse);
//        }
//    });
//}


//function CreateSoftcard(_countryId, arrayInput, listAmount) {

//    var urlCreateSoftCard = "/ActiveCode/CreateSoftcard/";
//    var providerId = $('#providerId').val();
//    var a = $.ajax({
//        url: urlCreateSoftCard,
//        async: false,
//        data: { countryId: _countryId, providerId: providerId, arrayInput: arrayInput, listAmountId: listAmount },
//        type: "POST",
//        success: function (data) {
//            console.log('data ' + data);
//            if (data == -1) {
//                $("#error").show();
//                $("#success").html("");
//                $("#error").html("Không đủ thẻ trắng !");
//            } else {
//                $("#success").show();
//                $("#success").html("Tạo thẻ mềm thành công !");
//                $("#error").html("");
//                $('#appmodal').modal('hide');
//                RefreshPage();
//            }
//        }

//    });


//}


//function RefreshPage() {
//    location.reload();
//}


//function ActiveSingleSoftcard(softcardId) {
//    var urlCreateSoftCard = "/ActiveCode/ActiveSingleSoftcard/";
//    $.ajax({
//        url: urlCreateSoftCard,
//        data: { countryId: softcardId},
//        type: "POST",
//        success: function (data) {
//            if (data === -1) {

//            } else {
//                RefreshPage();
//            }
            
//        }

//    });
//}

//function CancelSingleSoftcard(softcardId) {
//    var urlCreateSoftCard = "/ActiveCode/CancelSingleSoftcard/";
//    $.ajax({
//        url: urlCreateSoftCard,
//        data: { countryId: softcardId },
//        type: "POST",
//        success: function (data) {
//            if (data == -1) {

//            } else {
//                RefreshPage();
//            }

//        }

//    });
//}



//$('#btnCancelMultiCard').click(function () {
//    var listActivecardId = "";
//    var type = $(this).data("type");

//    $('input[class=chkActiveCard]').each(function () {

//        if (this.checked) {
//            var sThisVal = $(this).val();
//            listActivecardId += (listActivecardId === "" ? sThisVal : "," + sThisVal);

//            console.log(listActivecardId);
//        }
//    });

//    var urlCancelSoftCard = "/ActiveCode/CancelSoftcard/";
//    $.ajax({
//        url: urlCancelSoftCard,
//        data: { activecardId: listActivecardId },
//        type: "POST",
//        success: function (data) {
//            RefreshPage();
//        }

//    });

//});


//$('.table').each(function () {
//    return $(".table #checkAll").click(function () {
//        if ($(".table #checkAll").is(":checked")) {
//            return $(".table input[type=checkbox]").each(function () {
//                return $(this).prop("checked", true);
//            });
//        } else {
//            return $(".table input[type=checkbox]").each(function () {
//                return $(this).prop("checked", false);
//            });
//        }
//    });
//});

//function SearchByKeyword() {
//    var urlIndexSoftcard = "/ActiveCode/SoftCard/";
//    var x = $("#searchBox").val();
//    alert(x);
//    $.ajax({
//        url: urlIndexSoftcard,
//        data: { q: x },
//        cache: false,
//        type: "POST",
//        success: function (data) {

//        },
//        error: function (reponse) {
//            console.log("error2 : " + reponse);
//        }
//    });
//}


//$('input').bind("enterKey", function (e) {
//    var urlIndexSoftcard = "/ActiveCode/SoftCard/";
//    var x = $("#searchBox").val();
//    alert(x);
//    $.ajax({
//        url: urlIndexSoftcard,
//        data: { q: x },
//        cache: false,
//        type: "POST",
//        success: function (data) {

//        },
//        error: function (reponse) {
//            console.log("error2 : " + reponse);
//        }
//    });
//});


//$('#btnActiveMultiCard').click(function () {
//    var listActivecardId = "";
//    var type = $(this).data("type");

//    $('input[class=chkActiveCard]').each(function () {

//        if (this.checked) {
//            var sThisVal = $(this).val();
//            listActivecardId += (listActivecardId === "" ? sThisVal : "," + sThisVal);

//            console.log(listActivecardId);
//        }
//    });
//    var urlActiveSoftCard = "/ActiveCode/ActiveSoftcard/";
//    $.ajax({
//        url: urlActiveSoftCard,
//        data: { activecardId: listActivecardId },
//        type: "POST",
//        success: function (data) {
//            RefreshPage();
//        }

//    });

//});


//$(document).ready(

//    // show checkbox check all when status != default
//    $(function () {
//        var valueSelected = $("#selectStatus option:selected").val();

//        if (valueSelected == -1) {
//            $('#checkAll').hide();
//            $('.chkActiveCard').hide();
//            $('#btnActiveMultiCard').hide();
//            $('#btnCancelMultiCard').hide();
//        }

//        //if (valueSelected == 3) {
//        //    $('#btnCancelMultiCard').hide();
//        //}

//        if (valueSelected == 2) {
//            $('#btnActiveMultiCard').hide();
//        }

//        if (valueSelected == 4 || valueSelected == 3) {
//            $('#btnActiveMultiCard').hide();
//            $('#btnCancelMultiCard').hide();
//            $('#checkAll').hide();
//            $('.chkActiveCard').hide();
//        }
//        $('#hideCode').hide();

//    })
//);

////$('#showCodeBtn').click(function () {
////    alert('here');
////    $('#hideCode').show();
////    $('#showCode').hide();
////})

////$('#hideCodeBtn').click(function () {
////    alert('there');
////    $('#hideCode').hide();
////    $('#showCode').show();
////})

//function clickShow() {
//    $('#hideCode').show();
//    $('#showCode').hide();
//}

//function clickHide() {
//    $('#hideCode').hide();
//    $('#showCode').show();
//}
