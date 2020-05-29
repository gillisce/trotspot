function loadPartialView(str) {
    var id = str.substring(0, str.indexOf(":"));
    var obj = { intClassNumber: id };
    $('.divYesEditing').removeClass('hide');
    $('.divYesEditing').addClass('divShow');
    AjaxCall('/Admin/RunningShow/getPossiblePlacing', JSON.stringify(obj), 'POST').done(function (response) {
        //console.log(response);
        $('#viewToLoad').html(response);
        return;
    });

}

function AjaxCall(url, data, type) {
    return $.ajax({
        url: url,
        type: type ? type : 'GET',
        data: data,
        contentType: 'application/json',
        dataType: 'html',
        async: true
    });
}