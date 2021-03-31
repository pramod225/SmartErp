function OnResetData() {
    $("#leadForm")[0].reset();
}
function BindGrid() {
    $("#LeadsList").data("kendoGrid").dataSource.read();
}
function OnLeadSaved(data) {
    if (data.Success == true) {
        /*Reset the form values or close the dialog.*/
        $("#myModal").modal("hide");
        $("#LeadsList").data("kendoGrid").dataSource.read();
        $("#leadForm")[0].reset();
        $("#ID").val(kendo.guid());
    }
    else {
        alert(data.Message);
    }
}

function EditLead(e,link) {
    e.preventDefault();
    var grid = $("#LeadsList").data("kendoGrid");
    var tr = $(link).closest("tr");
    var dataItem = grid.dataItem(tr);


    $("#ID").val(dataItem.ID);
    $("#Title").val(dataItem.Title);
    $("#FirstName").val(dataItem.FirstName);
    $("#LastName").val(dataItem.LastName);
    $("#CompanyName").val(dataItem.CompanyName);
    $("#Skype").val(dataItem.Skype);
    $("#Designation").val(dataItem.Designation);
    $("#PhoneNumber").val(dataItem.PhoneNumber);
    $("#Email").val(dataItem.Email);
    $("#MobileNumber").val(dataItem.MobileNumber);
    $("#StateProvince").val(dataItem.StateProvince);
    $("#Country").val(dataItem.Country);
    $("#LeadSource").val(dataItem.LeadSource);
    $("#NoOfEmployees").val(dataItem.NoOfEmployees);
    $("#Industry").val(dataItem.Industry);
    $("#City").val(dataItem.City);
    $("#Extension").val(dataItem.Extension);
    $("#Rating").val(dataItem.Rating);
    $("#Owner").val(dataItem.Owner);
    $("#Street").val(dataItem.Street);
    $("#ZipCode").val(dataItem.ZipCode);

    $("#myModal").modal("show");

}
function ResetData() {
    $("#leadForm")[0].reset();
}