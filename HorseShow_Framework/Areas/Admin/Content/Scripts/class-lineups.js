function loadPartialView(str) {
    var id = str.substring(0, str.indexOf(":"));
    var obj = { intClassNumber: id };
    AjaxCall('/Admin/RunningShow/getRidersInClass', JSON.stringify(obj), 'POST').done(function (response) {
        $('#viewToLoad').html(response);
    });
}


function AjaxCall(url, data, type) {
    return $.ajax({
        url: url,
        type: type ? type : 'GET',
        data: data,
        contentType: 'application/json',
        async: false
    });
}
function intitClassTable() {
    $('#classTable').DataTable({
        dom: '<"row my-4"Bf>t',
        buttons: [
            {
                extend: 'print',
                text: 'Print Line Up',
                customize: function (win) {
                    console.log('got called');
                    $(win.document.body).find('thead').prepend('<div class="header-print compact"><h1>' + $('#tableTitle')[0].innerHTML + '</h1></div><br/><div class="header-print compact"><h2>' + $('#tableCount')[0].innerHTML + '</h2></div><br/>');
                    $(win.document.body).find('table').addClass('compact')
                },
                exportOptions: {
                    modifier: {
                        search: 'none'
                    }
                }
            },
            {
                extend: 'colvis',
                columns: ':not(.hide)'
            },
        ],
        "paging": false,
        "info": false,
        "language": { search: '', searchPlaceholder: "Search..." }
    });
    $("#classTable_filter").addClass("col-xl-6 m-auto");
    $(".dt-buttons").addClass("col-xl-6 mx-auto mb-2 d-inline");
    $(".buttons-colvis").html("<span><span class='fontWeight600'>Show</span> / <span class='fontWeight600'>Hide</span> Columns </span>");

}