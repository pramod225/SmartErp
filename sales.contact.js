function OnResetData() {
    $("#contactForm")[0].reset();
}
function OnContactSaved(data) {
    if (data.Success == true) {
        /*Reset the form values or close the dialog.*/
        $("#myModal").modal("hide");
        $("#ContactsList").data("kendoGrid").dataSource.read();
        $("#contactForm")[0].reset();
        $("#Id").val(kendo.guid());
    }
    else {
        alert(data.Message);
    }
}

function EditContact(link) {
    var grid = $("#ContactsList").data("kendoGrid");
    var tr = $(link).closest("tr");
    var dataItem = grid.dataItem(tr);


    $("#Id").val(dataItem.id);
    $("#Title").val(dataItem.Title);
    $("#FirstName").val(dataItem.FirstName);
    $("#LastName").val(dataItem.LastName);
    $("#EmailId").val(dataItem.EmailId);
    $("#PhoneNo").val(dataItem.PhoneNo);
    $("#MobileNo").val(dataItem.MobileNo);
    $("#Extension").val(dataItem.Extension);
    $("#SkypeId").val(dataItem.SkypeId);
    $("#WhatsApp").val(dataItem.WhatsApp);
    $("#AccountName").val(dataItem.AccountName);
    $("#Designation").val(dataItem.Designation);
    $("#LinkdinProfile").val(dataItem.LinkdinProfile);
    $("#myModal").modal("show");


}
