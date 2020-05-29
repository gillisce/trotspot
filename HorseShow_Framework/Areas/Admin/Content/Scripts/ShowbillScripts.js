// Contents:
// 1. script for helper js
//      - editRow(id)
//      - cancelEditRow(id)
//      - confirmUpdate(id)
// 2. script for updating rows
//      - updateRequiredSupport(id)
//      - updateFileType(id)
//      - updateStatus(id)
//      - updateKPI(id)
//      - updateCategory(id)
// 3. script for inserting rows
// 4. script for removing rows
//      - removeRequiredSupportType(id)
//      - removeFileType(id)
//      - removeStatus(id)
//      - removeImpactKPI(id)
//      - removeCategory(id)
// 5. script for ajax
//      - AjaxCall(url, data, type)


// ** to organize ** //

$(document).ready(function () {
    initShowbillAdminDatatable('divisionTable');
    initShowbillAdminDatatable('classTable');
    initShowbillAdminDatatable('pointPlaceTable');
    initShowbillAdminDatatable('storeTable');

    //initDivisionTable();
    //initClassTable();
    //initPointPlaceTable();
    //initStoreTable();
    $(".jp-multiselect").jQueryMultiSelection();

});

function addNewRow() {
    console.log("add");
    $("div .datatableFooter").each(function () {
        $(this).find('.divNoNewRow').toggle();
        $(this).find('.divYesNewRow').toggle();
    });
}

function cancelInsertRow() {
    $("div .datatableFooter").each(function () {
        $(this).find('.divNoNewRow').toggle();
        $(this).find('.divYesNewRow').toggle();
    });
}

function initShowbillAdminDatatable(idOfTable) {
    $('#' + idOfTable).DataTable({
        response: true,
        dom: '<"row my-4"Bf>t',
        buttons: [
            {
                extend: 'colvis',
                columns: ':not(.hide)'
            }
        ],
        "paging": false,
        "info": false,
        "language": { search: '', searchPlaceholder: "Search..." }
    });
    $("#" + idOfTable + "_filter").addClass("col-xl-6 m-auto");
    $("#" + idOfTable + "_filter input").addClass("form-control py-0");
    $("#" + idOfTable + "_wrapper .dt-buttons").addClass("col-xl-6 mx-auto mb-2 d-inline");
    $("#" + idOfTable + "_wrapper .buttons-colvis").html("<span>Show / Hide Columns </span>");
    $("#" + idOfTable + "_wrapper .dt-button").addClass("my-1");

}

// ** end of to organize ** //


//**** start script for helper js ****//

//** Edit Event Handler **//
function editRow(id) {

    var row = $("[id='" + id + "_row']");
    $("td", row).each(function () {
        $(this).find(".divYesEditing").toggle();
        $(this).find(".divNoEditing").toggle();
    });

}

//** Cancel event handler **//
function cancelEditRow(id) {
    console.log("cancel edit");
    if (confirm("Are you sure you would like to cancel editing this row?")) {
        var row = $("[id='" + id + "_row']");
        console.log("row: " + row);
        console.log("#" + id + "_row");
        $("td", row).each(function () {
            $(this).find(".divYesEditing").toggle();
            $(this).find(".divNoEditing").toggle();
        });
    }
}

//** confirm row update **//
function confirmUpdate() {
    console.log("confirm update");
    $('#confirmUpdateModal').modal({
    });
    //alert("The row has been successfully updated.");
}

//****  end script for helper js ****//


//** submit changes for Division_Index
function updateDivision(id) {

    var data = {
        id: id,
        strDivisionName: $("[id='" + id + "_name']").val(),
        strDivisionDescr: $("[id='" + id + "_descr']").val(),
        intMinRiderAge: $("[id='" + id + "_minAge']").val(),
        intMaxRiderAge: $("[id='" + id + "_maxAge']").val(),
    };
    console.log("data:");
    console.log(data);
    console.log(JSON.stringify(data));
    AjaxCall('/Admin/Showbill/UpdateDivision', JSON.stringify(data), 'POST').done(function (response) {
        console.log("success");
        console.log(response);
        refresh('Division');
        confirmUpdate();

    }).fail(function (error) {
        console.log(error);
        alert(error.StatusText);
    });
    var row = $("[id='" + id + "_row']");
    $("td", row).each(function () {
        $(this).find('.divYesEditing').toggle();
        $(this).find('.divNoEditing').toggle();
    });
}

function updateClass(id) {

    var data = {
        id: id,
        intClassNumber: $("[id='" + id + "_Number']").val(),
        strClassName: $("[id='" + id + "_Name']").val(),
        strNotes: $("[id='" + id + "_Notes']").val(),
        ysnFlag: $("[id='" + id + "_West']").prop('checked'),
        ysnCross: $("[id='" + id + "_Cross']").prop('checked'),
        fltCost: $("[id='" + id + "_Cost']").val(),
    };
    console.log("data:");
    console.log(data);
    console.log(JSON.stringify(data));
    AjaxCall('/Admin/Showbill/UpdateClass', JSON.stringify(data), 'POST').done(function (response) {
        console.log("success");
        console.log(response);
        refresh('Class');
        confirmUpdate();

    }).fail(function (error) {
        console.log(error);
        alert(error.StatusText);
    });
    var row = $("[id='" + id + "_row']");
    $("td", row).each(function () {
        $(this).find('.divYesEditing').toggle();
        $(this).find('.divNoEditing').toggle();
    });
}

function updatePointPlace(id) {

    var data = {
        intPlacePointValues: id,
        intPlace: $("[id='" + id + "_Place']").val(),
        intPointValue: $("[id='" + id + "_Points']").val(),
    };
    console.log("data:");
    console.log(data);
    console.log(JSON.stringify(data));
    AjaxCall('/Admin/Showbill/UpdatePointsPerPlace', JSON.stringify(data), 'POST').done(function (response) {
        console.log("success");
        console.log(response);
        refresh('PointPlace');
        confirmUpdate();

    }).fail(function (error) {
        console.log(error);
        alert(error.StatusText);
    });
    var row = $("[id='" + id + "_row']");
    $("td", row).each(function () {
        $(this).find('.divYesEditing').toggle();
        $(this).find('.divNoEditing').toggle();
    });
}

function updateStoreItem(id) {

    var data = {
        intStoreItemID: id,
        strItemName: $("[id='" + id + "_Name']").val(),
        strItemDescr: $("[id='" + id + "_Descr']").val(),
        fltCost: $("[id='" + id + "_Cost']").val(),
    };
    console.log("data:");
    console.log(data);
    console.log(JSON.stringify(data));
    AjaxCall('/Admin/Showbill/UpdateStoreItem', JSON.stringify(data), 'POST').done(function (response) {
        console.log("success");
        console.log(response);
        refresh('Store');
        confirmUpdate();

    }).fail(function (error) {
        console.log(error);
        alert(error.StatusText);
    });
    var row = $("[id='" + id + "_row']");
    $("td", row).each(function () {
        $(this).find('.divYesEditing').toggle();
        $(this).find('.divNoEditing').toggle();
    });
}
//**** end script for updating rows  ****//






//**** start script for inserting rows ****//

function insertDivision() {
    console.log("add: ");
    var data = {
        strDivisionName: $("#divisionName").val(),
        strDivisionDescr: $("#divisionDescr").val(),
        intMinRiderAge: $("#divisionMinAge").val(),
        intMaxRiderAge: $("#divisionMaxAge").val()
    };
    console.log(data);
    console.log(JSON.stringify(data));

    AjaxCall('/Admin/Showbill/CreateDivision', JSON.stringify(data), 'POST').done(function (response) {
        console.log("success");
        console.log(response);

        refresh('Division');

    }).fail(function (error) {
        console.log("fail");
        console.log(error);
    });

    var row = $("#footer");
    $("td", row).each(function () {
        $(this).find('.divYesEditing').toggle();
        $(this).find('.divNoEditing').toggle();
    });
}

function insertClass() {
    console.log("add: ");
    var data = {
        intClassNumber: $('#classNumber').val(),
        strClassName: $("#className").val(),
        ysnFlag: $("#ysnWest").prop('checked'),
        ysnCross: $("#ysnCross").prop('checked'),
        strNotes: $("#strNotes").val(),
        fltCost: $("#fltCost").val(),
    };
    console.log(data);
    console.log(JSON.stringify(data));

    AjaxCall('/Admin/Showbill/CreateClass', JSON.stringify(data), 'POST').done(function (response) {
        console.log("success");
        console.log(response);

        refresh('Class');

    }).fail(function (error) {
        console.log("fail");
        console.log(error);
    });

    var row = $("#footer");
    $("td", row).each(function () {
        $(this).find('.divYesEditing').toggle();
        $(this).find('.divNoEditing').toggle();
    });
}

function insertPointPlace() {
    console.log("add: ");
    var data = {
        intPlace: $("#Place").val(),
        intPointValue: $("#Points").val(),
    };
    console.log(data);
    console.log(JSON.stringify(data));

    AjaxCall('/Admin/Showbill/CreatePointsPerPlace', JSON.stringify(data), 'POST').done(function (response) {
        console.log("success");
        console.log(response);

        refresh('PointPlace');

    }).fail(function (error) {
        console.log("fail");
        console.log(error);
    });

    var row = $("#footer");
    $("td", row).each(function () {
        $(this).find('.divYesEditing').toggle();
        $(this).find('.divNoEditing').toggle();
    });
}

function insertStoreItem() {
    console.log("add: ");
    var data = {
        strItemName: $('#itemName').val(),
        strItemDescr: $('#itemDescr').val(),
        fltCost: $('#fltCost1').val(),
    };
    console.log(data);
    console.log(JSON.stringify(data));

    AjaxCall('/Admin/Showbill/CreateStoreItem', JSON.stringify(data), 'POST').done(function (response) {
        console.log("success");
        console.log(response);

        refresh('Store');

    }).fail(function (error) {
        console.log("fail");
        console.log(error);
    });

    var row = $("#footer");
    $("td", row).each(function () {
        $(this).find('.divYesEditing').toggle();
        $(this).find('.divNoEditing').toggle();
    });
}
//**** end script for inserting rows  ****//






//**** start script for removing rows   ****//

function removeDivision(id) {
    console.log("remove id: " + id);
    if (confirm("Are you sure you want to delete this row?")) {
        var data = {
            intDivisionID: id
        };
        console.log("data:");
        console.log(data);

        AjaxCall('/Admin/Showbill/RemoveDivision', JSON.stringify(data), 'POST').done(function (response) {
            console.log(response);
            refresh('Division');

        }).fail(function (error) {
            alert(error.StatusText);
        });
    }
}

function removeClass(id) {
    console.log("remove id: " + id);
    if (confirm("Are you sure you want to delete this row?")) {
        var data = {
            intClassID: id
        };
        console.log("data:");
        console.log(data);

        AjaxCall('/Admin/Showbill/RemoveClass', JSON.stringify(data), 'POST').done(function (response) {
            console.log(response);
            refresh('Class');

        }).fail(function (error) {
            alert(error.StatusText);
        });
    }
}

function removePointPlace(id) {
    console.log("remove id: " + id);
    if (confirm("Are you sure you want to delete this row?")) {
        var data = {
            intPlacePointValues: id
        };
        console.log("data:");
        console.log(data);

        AjaxCall('/Admin/Showbill/RemovePointsPerPlace', JSON.stringify(data), 'POST').done(function (response) {
            console.log(response);
            refresh('PointPlace');

        }).fail(function (error) {
            alert(error.StatusText);
        });
    }
}

function removeStoreItem(id) {
    console.log("remove id: " + id);
    if (confirm("Are you sure you want to delete this row?")) {
        var data = {
            storeItemID: id
        };
        console.log("data:");
        console.log(data);

        AjaxCall('/Admin/Showbill/RemoveStoreItem', JSON.stringify(data), 'POST').done(function (response) {
            console.log(response);
            refresh('Store');

        }).fail(function (error) {
            alert(error.StatusText);
        });
    }
}

//**** end script for removing rows  ****//






//**** start script for ajax ****//

function AjaxCall(url, data, type) {
    return $.ajax({
        url: url,
        type: type ? type : 'GET',
        data: data,
        contentType: 'application/json'
    });
}

//**** end script for ajax ****//


function createDivMapping() {
    //e.preventDefault();
    var divisionCounter = $('#intDivCount').val();
    var divisions = JSON.parse($('#Divs').val());
    var divIDsToCheck = [];

    var divMapping = [];

    for (var divIDs = 0; divIDs < divisions.length; divIDs++) {
        console.log(divisions[divIDs].intDivisionID);
        divIDsToCheck.push(divisions[divIDs].intDivisionID);
    }
    console.log(divIDsToCheck);
    for (var div = 0; div < divIDsToCheck.length; div++) {
        var sl = $('#newMap_' + divIDsToCheck[div])[0].options;
        var tmpDivClassObj = [];
        var tmpClassArray = [];
        for (var i = 0; i < sl.length; i++) {
            tmpObj = {
                intClassID: sl[i].value,
                strClassName: sl[i].getAttribute("data-classname")
            };
            tmpClassArray.push(tmpObj);
        }
        tmpDivClassObj = {
            intDivisionID: divIDsToCheck[div],
            classList: tmpClassArray
        };
        divMapping.push(tmpDivClassObj);
    }
    $('#stringMapping').prop('value', JSON.stringify(divMapping));
    console.log($('#stringMapping').val());
    var formData = new FormData($('#divClass').get(0));

    var xhr = new XMLHttpRequest();
    xhr.open('POST', '/Showbill/DivClass_Index', false);
    xhr.onload = function () {
        if (xhr.status === 200) {
            var json = JSON.parse(xhr.responseText);

            console.log(JSON.parse(xhr.responseText));
            if (json.message === "success") {
                alert('Changes Saved Succesfully');
                location.reload();
            }
            else {
                console.log('Hopeful Error List', json.message);
                console.log(json);
            }
        }
    };
    xhr.send(formData);
}