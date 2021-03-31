function OnResetData() {
    $("#opportunityForm")[0].reset();
}
function BindGrid() {
    $("#OpportunityList").data("kendoGrid").dataSource.read();
}
function GetAdditionalData() {
    var searchText = $("#txtSearch").val();
    return {
        SearchText: searchText
    };
}

function OnOpportunitySaved(data) {
    if (data.Success == true) {
        /*Reset the form values or close the dialog.*/
        $("#myModal").modal("hide");
        $("#OpportunityList").data("kendoGrid").dataSource.read();
        $("#opportunityForm")[0].reset();
        $("#ID").val(kendo.guid());
    }
    else {
        alert(data.Message);
    }
}

function EditOpportunity(link) {
    var grid = $("#OpportunityList").data("kendoGrid");
    var tr = $(link).closest("tr");
    var dataItem = grid.dataItem(tr);

    $("#ID").val(dataItem.ID);
    $("#OpportunityName").val(dataItem.OpportunityName);
    $("#AccountName").val(dataItem.AccountName);
    $("#Stage").val(dataItem.Stage);
    $("#CloseDate").val(dataItem.CloseDate);
    $("#LossReason").val(dataItem.LossReason);
    $("#NextStep").val(dataItem.NextStep);
    $("#Owner").val(dataItem.Owner);
    $("#CreatedOn").val(dataItem.CreatedOn);

    console.log(dataItem);

    $("#OpportunityType").val(dataItem.OpportunityType);

    $("#myModal").modal("show");

}