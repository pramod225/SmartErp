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

function SetCloseDate() {
    var closeDatePicker = $("#closeDatePicker").data("kendoDatePicker").value();
    $("#CloseDate").val(kendo.toString(closeDatePicker,"MM/dd/yyyy").trim(' '));
}

function OnOpportunitySaved(data) {
    if (data.Success == true) {
        /*Reset the form values or close the dialog.*/
        $("#myModal").modal("hide");
        //alert("Record saved successfully");
        $("#OpportunityList").data("kendoGrid").dataSource.read();
        $("#opportunityForm")[0].reset();
        $("#ID").val(kendo.guid());
    }
    else {
        alert(data.Message);
    }
}

function EditOpportunity(e, link) {
    e.preventDefault();
    var grid = $("#OpportunityList").data("kendoGrid");
    var tr = $(link).closest("tr");
    var dataItem = grid.dataItem(tr);
    $("#ID").val(dataItem.ID);
    $("#OpportunityName").val(dataItem.OpportunityName);
    $("#Account").val(dataItem.Account);
    $("#Stage").val(dataItem.Stage);
    $("#LossReason").val(dataItem.LossReason);
    $("#closeDatePicker").val(kendo.toString(dataItem.CloseDate, "MM/dd/yyyy").trim(' '));
    $("#NextStep").val(dataItem.NextStep);
    $("#PrimaryCampaignSource").val(dataItem.PrimaryCampaignSource);
    $("#LeadSource").val(dataItem.LeadSource);
    $("#Description").val(dataItem.Description);
    $("#OpportunityType").val(dataItem.OpportunityType);
    $("#Amount").val(dataItem.Amount);
    $("#Probability").val(dataItem.Probability);
    $("#Owner").val(dataItem.Owner);
    $("#BudgetConfirmed").prop('checked', dataItem.BudgetConfirmed);
    $("#ROIAnalysisCompleted").prop('checked', dataItem.ROIAnalysisCompleted);
    $("#DiscoveryCompleted").prop('checked', dataItem.DiscoveryCompleted);
   
    console.log(dataItem);
    $("#myModal").modal("show");

}