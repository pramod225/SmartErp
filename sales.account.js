var selectedAccountID = null;

function BindGrid() {
    $("#AccountList").data("kendoGrid").dataSource.read();
}
function OnAccountSaved(data) {
    if (data.Success == true) {
        /*Reset the form values or close the dialog.*/
        $("#myModal").modal("hide");
        $("#AccountList").data("kendoGrid").dataSource.read();
        $("#ID").val(kendo.guid());
        $("#accountForm")[0].reset();

    }
    else {
        alert(data.Message);
    }
}

function OnAccountSelected(e) {
    var grid = $("#AccountList").data("kendoGrid");
    var selectedItems = grid.select();
    selectedAccountID = grid.dataItem($(selectedItems).first()).ID;
    
}

function GetAccountDetails() {
   
    if (selectedAccountID == null) selectedAccountID = kendo.guid();

    return {
        AccountID: selectedAccountID
    };
}

function EditAccount(link) {
    var grid = $("#AccountList").data("kendoGrid");
    var tr = $(link).closest("tr");
    var dataItem = grid.dataItem(tr);

    $("#ID").val(dataItem.ID);
    $("#AccountName").val(dataItem.AccountName);
    $("#PhoneNumber").val(dataItem.PhoneNumber);
    $("#Country").val(dataItem.Country);
    $("#Industry").val(dataItem.Industry);
    $("#Website").val(dataItem.Website);
    $("#FaxNumber").val(dataItem.FaxNumber);
    $("#CompanySize").val(dataItem.CompanySize);
    $("#GSTNumber").val(dataItem.GSTNumber);
    $("#BillingAddressLine1").val(dataItem.BillingAddressLine1);
    $("#BillingAddressLine2").val(dataItem.BillingAddressLine2);
    $("#BillingCityState").val(dataItem.BillingCityState);
    $("#ShippingAddressLine1").val(dataItem.ShippingAddressLine1);
    $("#ShippingAddressLine2").val(dataItem.ShippingAddressLine2);
    $("#BillingCityState").val(dataItem.BillingCityState);
    $("#ZipCode").val(dataItem.ZipCode);
    $("#ShippingZipCode").val(dataItem.ShippingZipCode);
    $("#ShippingCountry").val(dataItem.ShippingCountry);

    $("#myModal").modal("show");
}
function BillingAddress() {
    $("#select_all").on("click", function () {
        if ($(this).prop("checked")==true) {
            $("#ShippingAddress1").val($("#BillingAddress1").val());
            $("#ShippingAddress2").val($("#BillingAddress2").val());
            $("#ShippingCityState").val($("#CityState").val());
            $("#ShippingCountry").val($("#Country").val());
            $("#ShippingZipCode").val($("#ZipCode").val());
        }
        else {
           
            $("#ShippingAddress1").val('');
            $("#ShippingAddress2").val('');
           
            $("#ShippingCityState").val('');
           
            $("#ShippingCountry").val('');
            $("#ShippingZipCode").val('');
        }
    });
};
function ResetData() {
    $("#accountForm")[0].reset();
}