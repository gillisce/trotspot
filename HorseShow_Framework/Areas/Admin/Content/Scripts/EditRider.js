//var ranchSelected = [];
//var nonRanchSelected = [];


$(function () {
    reloadClasses(divName);
    listClasses();
    listStoreItems();
    //loadDivisionDropDown(riderAge);
    $('#strSelectedClasses').prop('value', "NoEdit");
    $('#strSelectedStoreItems').prop('value', "NoEdit");

    $("#editVersion").submit(function (e) {
        e.preventDefault();
        if ($(".alert-danger")[0]) {
            console.log('IT works');
            alert("The Rider must be removed from the Invalid classes, before they can be updated");
            return;
        } else {
            console.log("Not so much");
            var formData = new FormData($('#editVersion').get(0));
            var xhr = new XMLHttpRequest();
            xhr.open('POST', '/RunningShow/EditRiderRegistration', false);
            xhr.onload = function () {
                if (xhr.status === 200) {
                    var json = JSON.parse(xhr.responseText);

                    console.log(JSON.parse(xhr.responseText));
                    if (json.message === "success") {
                        alert('Changes Saved Succesfully');
                        window.location.href = reviewURL;
                    }
                    else {
                        console.log('Hopeful Error List', json.message);
                        console.log(json);
                    }
                }
            };
            xhr.send(formData);
        }
        
    });


});

function loadDivisionDropDown(riderAge) {
    var arrDIVs = JSON.parse($('#Divs').val());
    //$('#DivisionDDL').empty();
    $('#DivisionDDL').append($('<option></option>').html("Select Division"));
    for (var index = 0; index < arrDIVs.length; index++) {
        if (riderAge <= arrDIVs[index].intMaxAge && riderAge >= arrDIVs[index].intMinAge) {
            $('#DivisionDDL').append($('<option></option>').val(arrDIVs[index].strDivisionName).html(arrDIVs[index].strDivisionName));
        }
    }
}

function unlockForm() {
    // On 'Update' button click > hide 'non editable' text + show 'editable' text - Radu 02.11.2019
    $('.divNoEditing').removeClass('divShow');
    $('.divNoEditing').addClass('hide');
    $('.divYesEditing').removeClass('hide');
    $('.divYesEditing').addClass('divShow');


    $('#saveCancelButtons').addClass('divShow');
    $('#saveCancelButtons').removeClass('hide');

    // On 'Update' button click > hide the 'Update' button - Radu 02.11.2019
    $('.buttonUpdateUseCase').addClass('hide');      // hiding 'Update' button
    $('#colChangeWidth4to6').removeClass('col-md-4');
    $('#colChangeWidth4to6').addClass('col-md-6');
    $('#colChangeWidth5to6').removeClass('col-md-3');
    $('#colChangeWidth5to6').addClass('col-md-6');
    $('#colChangeWidth6to6').removeClass('col-md-6');
    $('#colChangeWidth6to6').addClass('col-md-12');
}

function checkAgainstRiderNumbers(chosenNumberT) {
    var chosenNumber = parseInt(chosenNumberT);
    //console.log(chosenNumber);
    if (chosenNumber == 0 || chosenNumber == null || chosenNumber === '') {
        $('#horse_intRiderNumber').removeClass('invalidData').removeClass('alert-success');
        return;
    }
    var arrExistingNumbers = JSON.parse($('#RiderNums').val());
   // console.log(arrExistingNumbers);
    var index = arrExistingNumbers.indexOf(chosenNumber);
   // console.log(index);
    if (index == -1) {
        $('#horse_intRiderNumber').removeClass('invalidData').addClass('alert-success');
        $('#alertError').addClass('hide').removeClass('divShow');
        return;
    }
    else {
        $('#horse_intRiderNumber').select().addClass('invalidData').removeClass('alert-success');
        $('#alertError').addClass('divShow').removeClass('hide');
        return;
    }
}


// Firing when 'Cancel' form update link is clicked - Radu 02.12.2019
//    OK - reloads the page to take you to the state before 'Update' button was hit
//    Cancel - gets you out of this function to the state of the page being 'Updated'
function cancelFormUpade() {
    var txt;
    var r = confirm("Cancel Update of the Use Case");
    if (r == true) {
        $('#saveModal').modal({});
        window.location.reload();
    }
    else { return; }
}


function listClasses() {
    for (var i = 0; i < arrClasses.length; i++) {
        addToEditCart(arrClasses[i].intClassID, arrClasses[i].fltCost, arrClasses[i].ysnFlag, arrClasses[i].ysnCross);
    }
    var cl = JSON.parse($('#CLASSES').val());
    for (var in1 = 0; in1 < cl.length; in1++) {
        var indexOf = arrClasses.indexOf(cl[in1].intClassID);
        console.log('CHECK INDEX',indexOf);
        if (!cl[in1].isActiveMapping && indexOf == -1) {
            console.log('MADE IT IN LOOP', cl[in1].intClassID)
            $('#' + cl[in1].intClassID + '_card').removeClass('alert-danger');
            $('#' + cl[in1].intClassID + '_wrapper').css('pointer-events', 'none').css('opacity', '.5')
        }
    }
}

function listStoreItems() {
    var textForHTML;
    var arrHtml = [];
    var tmpText;
    var fakeID;
    for (var i = 0; i < arrStoreItems.length; i++) {
        fakeID = 'store_' + arrStoreItems[i].intItemID;
        addItemToCart(fakeID, arrStoreItems[i].fltCost);
    }
}

function reloadClasses(divName) {
    var obj = { id: divName }
    //console.log(obj);
    AjaxCall('/Admin/RunningShow/GetClasses', JSON.stringify(obj), 'POST').done(function (response) {
       // console.log(response);
        $('#ClassHeader').removeClass('hide');
        $('#ClassHeader').addClass('divShow');
        $('#storeListing').removeClass('hide');
        $('#storeListing').addClass('divShow');
        $('#classListing').html(response);
        return;

    }).fail(function (error) {
        alert(error.statusText);
    });
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



function addToEditCart(id, cost, ranchFlag, crossFlag) {
    console.log(ranchFlag);
    console.log(crossFlag);
    if (!crossFlag) {
        if (ranchFlag == true && nonRanchSelected.length == 0) {
            $('.nonRanch').css('pointer-events', 'none').css('opacity', '.5');
            //$('.noGood').css('pointer-events', 'none').css('opacity', '.5');
            $('.ranch').css('pointer-events', 'auto').css('opacity', '1');
            $('.all').css('pointer-events', 'auto').css('opacity', '1');

        }
        else if (ranchFlag == true && nonRanchSelected.length > 0) {
            alert("You have selected to be in a Non-Ranch Class. If you with to be in Ranch Classes, deselect classes 1- 35 so you can enter classes 36,37,38");
            return;
        }
        else if (!ranchFlag && ranchSelected.length == 0) {
            $('.nonRanch').css('pointer-events', 'auto').css('opacity', '1');
            $('.ranch').css('pointer-events', 'none').css('opacity', '.5');
            $('.all').css('pointer-events', 'auto').css('opacity', '1');
           // $('.noGood').css('pointer-events', 'none').css('opacity', '.5');
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
        if ($('#' + id + '_card').hasClass('alert-danger')){
            $('#' + id + '_card').removeClass('alert-danger');
            $('#' + id + '_wrapper').css('pointer-events', 'none').css('opacity', '.5');
        }
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
       // console.log("I clicked: ", id);
        $('#' + id + '_card').addClass('alert-success');
        $('#' + id + '_checked').prop('checked', true);
        cartTotal = cost + cartTotal;
        if (ranchFlag === true) { ranchSelected.push(id); }
        else if (!ranchFlag && !crossFlag) { nonRanchSelected.push(id); }
        classesSelected.push(id);
        console.log('Full Select List', classesSelected);
        console.log('Ranch Array', ranchSelected);
        console.log('Non Ranch', nonRanchSelected);
    }
    $('.noGood').css('pointer-events', 'none').css('opacity', '.5');
    if ($('#' + id + '_card').hasClass('alert-danger')) {
        $('#' + id + '_wrapper').css('pointer-events', 'auto').css('opacity', '.5');
    }
    
    $('#totCost').html(cartTotal);
    $('#totalDue').prop('value', cartTotal);
    $('#strSelectedClasses').prop('value', classesSelected);
    if (cartTotal >= 0) {
        $('#totalCostLabel').addClass('divShow');
        $('#totalCostLabel').removeClass('hide');
    }

}

