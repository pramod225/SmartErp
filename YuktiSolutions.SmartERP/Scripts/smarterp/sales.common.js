function GetAdditionData() {
    var searchText = $("#txtSearch").val();
    return {
        SearchText: searchText
    };
}
function BulkAction(e, Link, Grid, Alert, Confirm) {
    e.preventDefault();
    var deleteURL = $(Link).attr("href");
    var selectedCheckBoxes = $("#" + Grid + " .checked-item:checked,#" + Grid + " .sel-item:checked");
    if (selectedCheckBoxes.length > 0) {
        if (confirm(Confirm) == true) {
            var items = "";
            $(selectedCheckBoxes).each(function () {
                if (items != "") items += ";";
                items += $(this).data("id");
            });
            $.post(deleteURL, { IDs: items }, function (data) {
                if (data.Success == true) {
                    $("#" + Grid).data("kendoGrid").dataSource.read();
                }
                else {
                    alert(data.Message);
                }
            });
        }
    }
    else {
        alert(Alert);
    }
}

function GridCheckUncheck(headerCheckBox, Grid) {
    $("#" + Grid + " .checked-item").prop("checked", $(headerCheckBox).is(":checked"));
}

/**Common method to handle data source exceptions and 
 * show alert message that is shown from the server.
 * /
 * @param {any} e object received from the server.
 * @param {any} control control that fired this event.
 */
function DataSourceError(e, control) {
    if (e.errors) {
        var message = "";
        // Create a message containing all errors.
        $.each(e.errors, function (key, value) {
            if ('errors' in value) {
                $.each(value.errors, function () {
                    message += this + "\n";
                });
            }
        });
        // Display the message.
        alert(message);
        // Cancel the changes.
        var grid = $("#" + control).data("kendoGrid");
        grid.cancelChanges();
    }
}