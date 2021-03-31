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

function EditLead(link) {
    var grid = $("#LeadsList").data("kendoGrid");
    var tr = $(link).closest("tr");
    var dataItem = grid.dataItem(tr);


    $("#ID").val(dataItem.ID);
    $("#FirstName").val(dataItem.FirstName);

    $("#myModal").modal("show");

}