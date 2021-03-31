function OnResetData() {
    $("#contactForm")[0].reset();
}

function BindGrid() {
    $("#ContactsList").data("kendoGrid").dataSource.read();
}

function OnContactSaved(data) {
    if (data.Success == true) {
        /*Reset the form values or close the dialog.*/
        $("#contactModal").modal("hide");
        $("#ContactsList").data("kendoGrid").dataSource.read();
        $("#contactForm")[0].reset();
        $("#Id","#contactForm").val(kendo.guid());
    }
   
}

function EditContact(e,link) {
    e.preventDefault();
    var grid = $("#ContactsList").data("kendoGrid");
    var tr = $(link).closest("tr");
    var dataItem = grid.dataItem(tr);

    $("#Id","#contactForm").val(dataItem.Id);
    $("#Title").val(dataItem.Title);
    $("#FirstName").val(dataItem.FirstName);
    $("#LastName").val(dataItem.LastName);
    $("#EmailId").val(dataItem.EmailId);
    $("#PhoneNo").val(dataItem.PhoneNo);
    $("#MobileNo").val(dataItem.MobileNo);
    $("#Extension").val(dataItem.Extension);
    $("#SkypeId").val(dataItem.SkypeId);
    $("#WhatsApp").val(dataItem.WhatsApp);
    $("#Account").val(dataItem.Account);
    $("#Designation").val(dataItem.Designation);
    $("#LinkdinProfile").val(dataItem.LinkdinProfile);
    $("#contactModal").modal("show");
}
