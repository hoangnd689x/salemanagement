Ladda.bind('input[type=submit]');


$('#appmodal').on('hidden.bs.modal', function () {
    // do something…
    $('#appmodal .modal-content').html('')
})


$('#btnAddContentPackage').click(function () {
    var sList = "";
    var type = $(this).data("type");

    $('input[class=chkSelected]').each(function () {

        if (this.checked) {
            var sThisVal = $(this).val();
            sList += (sList == "" ? sThisVal : "," + sThisVal);

            console.log(sList);
        }
    });


    $.get("/Package/AddContentPackage", { ids: sList, type: type }).done(function (data) {

        $('#appmodal .modal-content').html(data);
        $('#appmodal').modal('show');
    });

});


jQuery(document).ready(function () {
    $('.date-picker').datepicker();

    var $inputcontnet = $('.typeahead_content');

 

    $('.li-chosen-jq .label-danger').click(function () {
        var id = $(this).closest('.col-md-2');
        id.remove();
    })

    $('.typeahead_content').each(function () {

        var $this = $(this);
        var url = $this.data('url') + '?query=%QUERY';
        var target = $this.data('target');

        var sources = new Bloodhound({
            datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
            queryTokenizer: Bloodhound.tokenizers.whitespace,
            remote: {
                url: url,
                wildcard: '%QUERY'
            }
        });

        $this.typeahead({
            minLength: 1,
            highlight: true
        },
            {
                displayKey: 'name',
                name: 'my-dataset',
                source: sources,
                templates: {
                    suggestion: function (data) {
                        return '<div class="media"><div class="media-left">  <img src="' + data.image + '" class="media-object" style="width:60px;height:60px">  </div> <div class="media-body">  <h4 class="media-heading">' + data.name + '</h4>  </div></div>';
                    }
                }
            }).on('typeahead:selected', function (obj, datum) {
                console.log(obj);
                console.log(datum);
                var html = '<div data-id=' + datum.id + ' class="col-md-2">';
                html += '<div class="li-chosen-jq">';
                html += '<img class="chosen-edit-image img-responsive" src="' + datum.image + '"/>';
                html += '' + '<h5>' + datum.name + '</h5>';
                html += '' + '<a data-id=' + datum.id + ' class="label label-danger" href="javascript:;">Xóa</a>';
                html += '<input name=ContentId' + ' value=' + datum.id + ' type="hidden"' + '/>'
                $(target).append(html);

                $('.li-chosen-jq .label-danger').unbind('click');
                $('.li-chosen-jq .label-danger').click(function () {
                    var id = $(this).closest('.col-md-2');
                    id.remove();
                })
                $this.typeahead('val', '');
            });



    })
});
