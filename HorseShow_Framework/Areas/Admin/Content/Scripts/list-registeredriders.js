$(document).ready(function () {
    $('#regRider').DataTable({
        dom: '<"row"<"col-xl-3"f><"col-xl-6"><"col-xl-3"p>>t',    // search & pagination on top
        "info": false,
        "lengthChange": false,
        "pageLength": 10,
        "language": { search: '', searchPlaceholder: "Search..." },
        "columnDefs": [
            { "orderable": false, "targets": 1 },
            { "orderData": 0, "targets": 6 }
        ]
    });
    $("#regRider_filter label").addClass("float-left");
    $("#regRider_filter label").css("width", "100%");
});

function removeRider(id) {
    console.log("remove id: " + id);
    if (confirm("Are you sure you want to delete this Rider?")) {
        var data = {
            intHorseRiderID: id
        };
        console.log("data:", data);

        AjaxCall('/Admin/RunningShow/RemoveRider', JSON.stringify(data), 'POST').done(function (response) {
            console.log(response);
            location.reload();
        }).fail(function (error) {
            alert(error.StatusText);
        });
    }
}
function AjaxCall(url, data, type) {
    return $.ajax({
        url: url,
        type: type ? type : 'GET',
        data: data,
        contentType: 'application/json'
    });
}