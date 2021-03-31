function BindUserGrid() {
    $("#AspNetUsers").data("kendoGrid").dataSource.read();
}
function EditUser(e, link) {
    e.preventDefault();
    var grid = $("#AspNetUsers").data("kendoGrid");
    var tr = $(link).closest("tr");
    var dataItem = grid.dataItem(tr);
    var id = dataItem.Id;
    BulkAssign("AccountList", id)
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
function EditUserOpportunity(e, link) {
    e.preventDefault();
    var grid = $("#AspNetUsers").data("kendoGrid");
    var tr = $(link).closest("tr");
    var dataItem = grid.dataItem(tr);
    var id = dataItem.Id;
    BulkAssignOpportunity("OpportunityList", id)
    $('#AssignUserModal').modal("hide");
}

function BulkAssignOpportunity(Grid, id) {

    var selectedCheckBoxes = $("#" + Grid + " .checked-item:checked,#" + Grid + " .sel-item:checked");
    if (selectedCheckBoxes.length > 0) {

        var items = "";
        $(selectedCheckBoxes).each(function () {
            if (items != "") items += ";";
            items += $(this).data("id");
        });
        $.post("/Opportunity/Opportunity_Assign", { IDs: items, Id: id }, function (data) {
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
function EditUserLead(e, link) {
    e.preventDefault();
    var grid = $("#AspNetUsers").data("kendoGrid");
    var tr = $(link).closest("tr");
    var dataItem = grid.dataItem(tr);
    var id = dataItem.Id;
    BulkAssignLeads("LeadsList", id);
    $('#AssignUserModal').modal("hide");
}
function BulkAssignLeads(Grid, id) {

    var selectedCheckBoxes = $("#" + Grid + " .checked-item:checked,#" + Grid + " .sel-item:checked");
    if (selectedCheckBoxes.length > 0) {

        var items = "";
        $(selectedCheckBoxes).each(function () {
            if (items != "") items += ";";
            items += $(this).data("id");
        });
        $.post("/Leads/Leads_Assign", { IDs: items, Id: id }, function (data) {
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
function ResetAssign() {
    BulkAssignReset("AccountList");
    $('#AssignUserModal').modal("hide");
}
function BulkAssignReset(Grid) {

    var selectedCheckBoxes = $("#" + Grid + " .checked-item:checked,#" + Grid + " .sel-item:checked");
    if (selectedCheckBoxes.length > 0) {

        var items = "";
        $(selectedCheckBoxes).each(function () {
            if (items != "") items += ";";
            items += $(this).data("id");
        });
        $.post("/Accounts/Accounts_ResetAssign", { IDs: items }, function (data) {
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
function EditUserContact(e, link) {
    e.preventDefault();
    var grid = $("#AspNetUsers").data("kendoGrid");
    var tr = $(link).closest("tr");
    var dataItem = grid.dataItem(tr);
    var id = dataItem.Id;
    BulkAssignContacts("ContactsList", id);
    $('#AssignUserModal').modal("hide");
}

function BulkAssignContacts(Grid, id) {

    var selectedCheckBoxes = $("#" + Grid + " .checked-item:checked,#" + Grid + " .sel-item:checked");
    if (selectedCheckBoxes.length > 0) {

        var items = "";
        $(selectedCheckBoxes).each(function () {
            if (items != "") items += ";";
            items += $(this).data("id");
        });
        $.post("/Contacts/Contacts_Assign", { IDs: items, Id: id }, function (data) {
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
function EditCurrentUser(e, link) {

    e.preventDefault();
    var grid = $("#AspNetUsers").data("kendoGrid");
    var tr = $(link).closest("tr");
    var dataItem = grid.dataItem(tr);
    $("#UserName").val(dataItem.UserName);
    $("#Email").val(dataItem.Email);
    $("#Number").val(dataItem.PhoneNumber);
    $("#Id").val(dataItem.Id);
    $('#myModal').modal("show");

}

function EditUserActivity(e, link) {
    e.preventDefault();
    var grid = $("#AspNetUsers").data("kendoGrid");
    var tr = $(link).closest("tr");
    var dataItem = grid.dataItem(tr);
    var id = dataItem.Id;
    $("#Name").val(dataItem.Name);
    $("#EmailId").val(dataItem.EmailId);
    $("#MobileNo").val(dataItem.MobileNo);
    $("#Id").val(dataItem.Id);
    BulkAssignActivities("EventList", id)
     $('#AssignUserModal').modal("hide");

}

function BulkAssignActivities(Grid, id) {

    var selectedCheckBoxes = $("#" + Grid + " .checked-item:checked,#" + Grid + " .sel-item:checked");
    if (selectedCheckBoxes.length > 0) {

        var items = "";
        $(selectedCheckBoxes).each(function () {
            if (items != "") items += ";";
            items += $(this).data("id");
        });
        $.post("/Activities/Activity_assign", { IDs: items, Id: id }, function (data) {
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


function CheckPass(e) {
 
    var pass = $("#Password").val();
    var cnfpass = $("#ConfirmPassword").val();
    if ($("#SetPass").is(":checked")) {
        if (pass == "" || cnfpass == "") {
            e.preventDefault();
            alert("Password and Confirm Password must be filled");
        }
        else {
            $("#UserForm").submit();
            $('#myModal').modal("hide");
        }
    }
}

function OnUserSaved(data) {
    if (data.Success == true) {
        /*Reset the form values or close the dialog.*/
        $("#myModal").modal("hide");
        $("#AspNetUsers").data("kendoGrid").dataSource.read();
        $("#userForm")[0].reset();
        $("#ID").val(kendo.guid());
    }
    else {
        alert(data.Message);
    }
}

function Resetform() {
    $("#userForm")[0].reset();
}