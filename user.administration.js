function BindUserGrid() {
    $("#AspNetUsers").data("kendoGrid").dataSource.read();
}


function EditUser(link) {

    var grid = $("#AspNetUsers").data("kendoGrid");
    var tr = $(link).closest("tr");
    var dataItem = grid.dataItem(tr);
    var id = dataItem.Id;
    BulkAssign("AccountList", id);
    $('#AssignUserModal').modal("hide");
}

function BulkAssign(Grid, id) {

    var selectedCheckBoxes = $("#" + Grid + " .checked-item:checked,#" + Grid + " .sel-item:checked");
    if (selectedCheckBoxes.length > 0) {

        var items = "";
        $(selectedCheckBoxes).each(function () {
            if (items != "") items += ";";
            items += $(this).data("id");
        });
        $.post("/Accounts/Accounts_Assign", { IDs: items, Id: id }, function (data) {
            if (data.Success == true) {
                $("#" + Grid).data("kendoGrid").dataSource.read();
            }
            else {
                alert(data.Message);
            }
        });

    }
    else {
        alert(Alert);
    }
}

function GetAdditionalUserData() {
    var searchText = $("#txtuserSearch").val();
    return {
        SearchText: searchText
    };
}
