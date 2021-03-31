var selectedAccountID = null;

function GetAccountDetails() {

    if (selectedAccountID == null) selectedAccountID = kendo.guid();

    return {
        AccountID: selectedAccountID
    };
}

function BindGrid() {
    $("#AccountList").data("kendoGrid").dataSource.read();
}

function OnAccountSaved(data) {
    if (data.Success == true) {
        /*Reset the form values or close the dialog.*/
        $("#myModal").modal("hide");
        $("#AccountList").data("kendoGrid").dataSource.read();
        $("#accountForm")[0].reset();
        $("#ID").val(kendo.guid());
    }
    else {
        alert(data.Message);
    }
}


function OnAccountSelected(e) {
    var grid = $("#AccountList").data("kendoGrid");
    var selectedItems = grid.select();
    var account = grid.dataItem($(selectedItems).first());

    selectedAccountID = account.ID;
    $("#selectedAccountName").text("Account Name:" + account.AccountName);

    $("#ContactsList").data("kendoGrid").dataSource.read();
}
function GetAdditionalData() {
    var searchText = $("#txtSearch").val();
    return {
        SearchText: searchText
    };
}



function EditAccount(e, link) {
    e.preventDefault();
    var grid = $("#AccountList").data("kendoGrid");
    var tr = $(link).closest("tr");
    var dataItem = grid.dataItem(tr);
    $("#ID").val(dataItem.ID);
    $("#AccountName").val(dataItem.AccountName);
    $("#Website").val(dataItem.Website);
    $("#Industry").val(dataItem.Industry);
    $("#CompanySize").val(dataItem.CompanySize);
    $("#PhoneNumber").val(dataItem.PhoneNumber);
    $("#GSTNumber").val(dataItem.GSTNumber);
    $("#FaxNumber").val(dataItem.FaxNumber);
    $("#BillingAddressLine1").val(dataItem.BillingAddressLine1);
    $("#BillingAddressLine2").val(dataItem.BillingAddressLine2);
    $("#ShippingAddressLine1").val(dataItem.ShippingAddressLine1);
    $("#ShippingAddressLine2").val(dataItem.ShippingAddressLine2);
    $("#ShippingCityState").val(dataItem.ShippingCityState);
    $("#BillingCityState").val(dataItem.BillingCityState);
    $("#ZipCode").val(dataItem.ZipCode);
    $("#ShippingZipCode").val(dataItem.ShippingZipCode);
    $("#ShippingCountry").val(dataItem.ShippingCountry);
    $("#Country").val(dataItem.Country);
    $("#myModal").modal("show");
}

function BillingAddress(checkbox) {
    if ($(checkbox).is(":checked") == true) {
            $("#ShippingAddressLine1").val($("#BillingAddressLine1").val());
            $("#ShippingAddressLine2").val($("#BillingAddressLine2").val());
            $("#ShippingCityState").val($("#BillingCityState").val());
            $("#ShippingCountry").val($("#Country").val());
            $("#ShippingZipCode").val($("#ZipCode").val());
    }
    else {
            $("#ShippingAddressLine1").val('');
            $("#ShippingAddressLine2").val('');
            $("#ShippingCityState").val('');
            $("#ShippingCityState").val('');
            $("#ShippingZipCode").val('');
    }
}

function ResetData() {
    $("#accountForm")[0].reset();
}


