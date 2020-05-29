/******************** GLOBAL VARIABLES *********************** */
var classesSelected = [];
var storeItemSelected = [];
var cartTotal = 0; var storeCost = 0;
var ranchSelected = [];
var nonRanchSelected = [];
var invalidNumber = false;

function loadDivisionDropDown(riderAge) {
    var arrDIVs = JSON.parse($('#Divs').val());
    $('#DivisionDDL').empty();
    $('#classListing').html('');
    //$('#storeListing').removeClass('show').addClass('hide');
    $('#storeListing').toggle();
    $('#DivisionDDL').append($('<option></option>').html("Select Division"));
    for (var index = 0; index < arrDIVs.length; index++) {
        if (riderAge <= arrDIVs[index].intMaxAge && riderAge >= arrDIVs[index].intMinAge) {
            $('#DivisionDDL').append($('<option></option>').val(arrDIVs[index].strDivisionName).html(arrDIVs[index].strDivisionName));
        }
    }
}

function checkAgainstRiderNumbers(chosenNumberT) {
    var chosenNumber = parseInt(chosenNumberT);
    console.log(chosenNumber);
    if (chosenNumber == 0 || chosenNumber == null || chosenNumber === '') {
        $('#horse_intRiderNumber').removeClass('alert-danger').removeClass('alert-success');
        $('#alertNormal').addClass('show').removeClass('hide');
        $('#alertError').addClass('hide').removeClass('show');
        invalidNumber = false;
        return;
    }
    var arrExistingNumbers = JSON.parse($('#RiderNums').val());
    console.log(arrExistingNumbers);
    var index = arrExistingNumbers.indexOf(chosenNumber);
    console.log(index);
    if (index == -1) {
        $('#horse_intRiderNumber').removeClass('alert-danger').addClass('alert-success');
        invalidNumber = false;
        $('#alertNormal').addClass('show').removeClass('hide');
        $('#alertError').addClass('hide').removeClass('show');
        return;
    }
    else {
        $('#horse_intRiderNumber').select().addClass('alert-danger').removeClass('alert-success');
        invalidNumber = true;
        $('#alertNormal').addClass('hide').removeClass('show');
        $('#alertError').addClass('show').removeClass('hide').addClass('alert-warning');
        return;
    }
}

function populateDDL(ddl) {
    var obj = {id: ddl.options[ddl.selectedIndex].value }
    console.log(obj);
    
    AjaxCall('/KCrusade/Home/GetClasses', JSON.stringify(obj), 'POST').done(function (response) {
        //console.log(response);
        ranchSelected.length = 0;
        nonRanchSelected.length = 0;
        classesSelected.length = 0;
        console.log(classesSelected);
        //storeItemSelected.length = 0;
        //console.log(storeItemSelected);
        cartTotal = storeCost;
        $('#totalDue').prop('value', cartTotal);
        $('#strSelectedClasses').prop('value', '');
        //$('#strSelectedStoreItems').prop('value', '');
        $('#classListing').html(response);
        $('#ClassHeader').removeClass('hide');
        $('#ClassHeader').addClass('show');
        $('#storeListing').removeClass('hide');
        $('#storeListing').addClass('show');
        return;

    }).fail(function (error) {
        alert(error.statusText);
    });
}

function addToCart(id, cost, ranchFlag, crossFlag) {
    console.log(ranchFlag);
    console.log(crossFlag);
    if (!crossFlag) {
        if (ranchFlag == true && nonRanchSelected.length == 0) {
            $('.nonRanch').css('opacity', '.5');
            $('.ranch').css('opacity', '1');
            $('.all').css('opacity', '1');
        }
        else if (ranchFlag == true && nonRanchSelected.length > 0) {
            alert("You have selected to be in a Non-Ranch Class. If you with to be in Ranch Classes, deselect classes 1- 35 so you can enter classes 36,37,38");
            return;
        }
        else if (!ranchFlag && ranchSelected.length == 0) {
            $('.nonRanch').css('opacity', '1');
            $('.ranch').css('opacity', '.5');
            $('.all').css('opacity', '1');
        }
        else if (!ranchFlag && ranchSelected.length > 0) {
            alert("You have selected to be in a Ranch Class. If you do not wish to be in Ranch, deselect 36,37 and/or 38 so you can enter classes 1-35.");
            return;
        }
    }


    if ($('#' + id + '_checked').is(":checked")) {
        //Already Selected and they deselected;
        var index = classesSelected.indexOf(id);
        classesSelected.splice(index, 1);
        $('#' + id + '_card').removeClass('alert-success');
        $('#' + id + '_checked').prop('checked', false);
        cartTotal = cartTotal - cost;
        if (classesSelected.length === 0) {
            $('.nonRanch').css('pointer-events', 'auto').css('opacity', '1');
            $('.ranch').css('pointer-events', 'auto').css('opacity', '1');
            $('.all').css('pointer-events', 'auto').css('opacity', '1');
        }
        var ranchIndex = ranchSelected.indexOf(id);
        var nonRanchIndex = nonRanchSelected.indexOf(id);
        if (ranchIndex != -1) { ranchSelected.splice(ranchIndex, 1); }
        if (nonRanchIndex != -1) { nonRanchSelected.splice(ranchIndex, 1); }
        console.log('Ranch Array', ranchSelected);
        console.log('Non Ranch', nonRanchSelected);
    }
    else {
        console.log("I clicked: ", id);
        $('#' + id + '_card').addClass('alert-success');
        $('#' + id + '_checked').prop('checked', true);
        cartTotal = cost + cartTotal;
        if (ranchFlag) { ranchSelected.push(id); }
        else if (!ranchFlag && !crossFlag) { nonRanchSelected.push(id);}
        classesSelected.push(id);
        console.log('Full Select List', classesSelected);
        console.log('Ranch Array', ranchSelected);
        console.log('Non Ranch', nonRanchSelected);
    }

    $('#totCost').html(cartTotal);
    $('#totalDue').prop('value', cartTotal);
    $('#strSelectedClasses').prop('value', classesSelected);
    if (cartTotal >= 0) {
        //$('#totalCostLabel').addClass('show');
        //$('#totalCostLabel').removeClass('hide');
        $('#totalCostLabel').show();
    }
}

function addItemToCart(id, cost) {
    var arrayID = id.substring(6);
    if ($('#' + id + '_checked').is(":checked")) {
        //Already Selected and they deselected;
        var index = storeItemSelected.indexOf(arrayID);
        storeItemSelected.splice(index, 1);
        $('#' + id + '_card').removeClass('alert-success');
        $('#' + id + '_checked').prop('checked', false);
        cartTotal = cartTotal - cost;
        storeCost = storeCost - cost;
    }
    else {
        console.log("I clicked: ", id);
        $('#' + id + '_card').addClass('alert-success');
        $('#' + id + '_checked').prop('checked', true);
        cartTotal = cost + cartTotal;
        storeCost = storeCost + cost;
        storeItemSelected.push(arrayID);
        console.log('Store',storeItemSelected);
    }

    $('#totCost').html(cartTotal);
    $('#totalDue').prop('value', cartTotal);
    $('#strSelectedStoreItems').prop('value', storeItemSelected);

    if (cartTotal >= 0) {
        //$('#totalCostLabel').addClass('show');
        //$('#totalCostLabel').removeClass('hide');
        $('#totalCostLabel').show();
    }
}

function AjaxCall(url, data, type) {
    return $.ajax({
        url: url,
        type: type ? type : 'GET',
        data: data,
        contentType: 'application/json',
        dataType: 'html',
        async: false
    });
}


$(function () {

    $("#submitRider").submit(function (e) {
        e.preventDefault();
        $('#form-submit').prop('disabled', true);

        if (invalidNumber) {
            alert('Another rider has already chosen this number, please select a different number or leave it blank and we will assign a number when you check in for the show.');
            $('#horse_intRiderNumber').focus();
            $('#form-submit').prop('disabled', false);

            invalidNumber = false;
            return;
        } else {
            var formData = new FormData($('#submitRider').get(0));
            var xhr = new XMLHttpRequest();
            xhr.open('POST', '/Home/Registration', false);
            xhr.onload = function () {
                if (xhr.status === 200) {
                    var json = JSON.parse(xhr.responseText);
                    console.log(JSON.parse(xhr.responseText));
                    if (json.message === "success") {
                        alert('You Have succesfully registered for the show');
                        window.location.href = reviewURL;
                    }
                    else if (json.message === "MissingFields") {
                        $('#form-submit').prop('disabled', false);
                        return;
                    }
                    else {
                        console.log('Hopeful Error List', json.message);
                        $('#form-submit').prop('disabled', false);
                        console.log(json);
                    }
                }
            };
            xhr.send(formData);
        }
    });
});