$(function () {
    var parsedOld = JSON.parse($('#OldPlaces').val());
    if (parsedOld.length > 0) {
        var arrAlreadyReload = [];
        for (var i = 0; i < parsedOld.length; i++) {
            var sl = $('#riderNumber_' + parsedOld[i].intPlace);
            var sl2 = $('#2riderNumber_' + parsedOld[i].intPlace);
            var index = arrAlreadyReload.indexOf(parsedOld[i].intPlace);
            if (index != -1) {
                sl2.prop('value', parsedOld[i].intHorseRiderID);
            }
            else {
                sl.prop('value', parsedOld[i].intHorseRiderID);
                arrAlreadyReload.push(parsedOld[i].intPlace);
            }

            
            
        }
    }

});

$("#placeClassForm").submit(function (e) {
    e.preventDefault();
    $('#saveButton').prop("disabled", true);
    console.log('Called the function');
    var places = JSON.parse($('#Place').val());

    var placeMapping = [];
    for (var place = 0; place < places.length; place++) {
        var sl = $('#riderNumber_' + places[place].intPlace);
        if (sl.val() != null) {
            var tmpPlacingObj = {
                intHorseRiderID: sl.val(),
                intPlace: sl.find(':selected').attr('data-place')
            };
            placeMapping.push(tmpPlacingObj);
        }
    }
    if ($('#ClassNum').val() == 33) {
        for (var place = 0; place < places.length; place++) {
            var sl = $('#2riderNumber_' + places[place].intPlace);
            if (sl.val() != null) {
                var tmpPlacingObj = {
                    intHorseRiderID: sl.val(),
                    intPlace: sl.find(':selected').attr('data-place')
                };
                placeMapping.push(tmpPlacingObj);
            }
        }
    }

    $('#strPlacedRiders').prop('value', JSON.stringify(placeMapping));
    console.log($('#strPlacedRiders').val());

    var formData = new FormData($('#placeClassForm').get(0));

    var xhr = new XMLHttpRequest();
    xhr.open('POST', '/RunningShow/SubmitPlacing', true);
    xhr.onload = function () {
        if (xhr.status === 200) {
            var json = JSON.parse(xhr.responseText);
            console.log(JSON.parse(xhr.responseText));
            if (json.message === "success") {
                alert('Placing Saved Succesfully');
                location.reload();
            }
            else {
                alert("There was an error placing the class");
                $('#saveButton').prop('disabled', false);
                console.log('Hopeful Error List', json.message);
                console.log(json);
            }
        }
    };
    xhr.send(formData);
});

