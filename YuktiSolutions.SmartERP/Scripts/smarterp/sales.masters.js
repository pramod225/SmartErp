function SaveRating() {
    var ratingName = $("#footerRatingName").val();
    $("#footerRatingName").val('');
    var ratingID = kendo.guid();
    if(ratingName == ''){
        alert('Rating Required');
    }
    else {
        $.post("/Masters/MasterEntries_Save", {
            Name: ratingName,
            ID: ratingID,
            MasterEntryType: 4
        }, function (data) {
            if (data.Success == true) {
                $("#RatingList").data("kendoGrid").dataSource.read().trim(' ');
            }
            else {
                alert(data.Message);
            }
        });
    }
}


function SaveCompanySize() {
    var companysize = $("#footerCompanySize").val();
    $("#footerCompanySize").val('');
    var companysizeID = kendo.guid();
    if (companysize == '') {
        alert('Company Size is Required');
    }
    else {
        $.post("/Masters/MasterEntries_Save", {
            Name: companysize,
            ID: companysizeID,
            MasterEntryType: 1
        }, function (data) {
            if (data.Success == true) {
                $("#CompanySizeList").data("kendoGrid").dataSource.read().trim(' ');
            }
            else {
                alert(data.Message);
            }
        });
    }
}
function SaveLossReason() {
    var LossReason = $("#footerLossReason").val();
    $("#footerLossReason").val(' ');
    var LoassReasonId = kendo.guid();
    if (LossReason == '') {
        alert('Loss Reason is Required');
    }
    else {
        $.post("/Masters/MasterEntries_Save", {
            Name: LossReason,
            ID: LoassReasonId,
            MasterEntryType: 6
        }, function (data) {
            if (data.Success == true) {
                $("#LossReasonList").data("kendoGrid").dataSource.read().trim(' ');
            }
            else {
                alert(data.Message);
            }
        });
    }
}

function SaveCampaignSource() {
    var CampaignSourceName = $("#footerCampaignSource").val();
    $("#footerCampaignSource").val(' ');
    var CampaignSourceID = kendo.guid();
    if (CampaignSourceName == '') {
        alert('Campaign Source is Required');
    }
    else {
        $.post("/Masters/MasterEntries_Save", {
            Name: CampaignSourceName,
            ID: CampaignSourceID,
            MasterEntryType: 7
        }, function (data) {
            if (data.Success == true) {
                $("#CampaignSourceList").data("kendoGrid").dataSource.read().trim(' ');
            }
            else {
                alert(data.Message);
            }
        });
    }
}
function SaveCountry() {
    var countryCode = $("#footerCountryCode").val();
    var countryName = $("#footerCountryName").val();
    $("#footerCountryCode").val('');
    $("#footerCountryName").val('');
    var countryID = kendo.guid();
    if (countryCode == '') {
        alert('Country Code is Required');
    }
    if (countryName == '') {
        alert('Country Name is Required')
    }
    else {
        $.post("/Masters/Countries_Save", {
            CountryCode: countryCode,
            CountryName: countryName,
            ID: countryID
        }, function (data) {
            if (data.Success == true) {
                $("#CountriesList").data("kendoGrid").dataSource.read().trim(' ');
            }
            else {
                alert(data.Message);
            }
        });
    }
}
function IndustrySave() {
    var industryName = $("#footerIndustryName").val();
    $("#footerIndustryName").val('');
    var industryID = kendo.guid();
    if (industryName == '') {
        alert(' Industry Name is Required');
    }
    else {
        $.post("/Masters/MasterEntries_Save", {
            Name: industryName,
            ID: industryID,
            MasterEntryType: 3
        }, function (data) {
            if (data.Success == true) {
                $("#IndustriesList").data("kendoGrid").dataSource.read().trim(' ');
            }
            else {
                alert(data.Message);
            }
        });
    }
}
function SaveStage() {
    var StageName = $("#footerStageName").val();
    $("#footerStageName").val('');
    var StageID = kendo.guid();
    if (StageName == '') {
           alert('Stage Name is Required');
         }
    else {
        $.post("/Masters/MasterEntries_Save", {
            Name: StageName,
            MasterEntryType: 5,
            ID: StageID
        }, function (data) {
            if (data.Success == true) {
                $("#StageList").data("kendoGrid").dataSource.read().trim(' ');
            }
            else {
                alert(data.Message);
            }
        });
    }
}
function SaveOpportunityType() {
    var opportunityName = $("#footerOpportunityType").val();
    $("#footerOpportunityType").val('');
    var opportunityId = kendo.guid();
    if (opportunityName == '') {
        alert('Opportunity Type  is Required');
    }
    else {
        $.post("/Masters/MasterEntries_Save", {
            Name: opportunityName,
            ID: opportunityId,
            MasterEntryType: 8
        }, function (data) {
            if (data.Success == true) {
                $("#OpportunityTypeList").data("kendoGrid").dataSource.read().trim(' ');
            }
            else {
                alert(data.Message);
            }
        });
    }
}
function SaveTitle() {
    var titleName = $("#FooterListTitle").val();
    $("#FooterListTitle").val('');
    var titleID = kendo.guid();
    if(titleName == '') {
        alert('Title Name is Required');
    }
    else {
        $.post("/Masters/MasterEntries_Save", {
            Name: titleName,
            ID: titleID,
            MasterEntryType: 2
        }, function (data) {
            if (data.Success == true) {
                $("#TitleList").data("kendoGrid").dataSource.read();
            }
            else {
                alert(data.Message);
            }
        });
    }
}

function SaveLeadSource() {
    var LeadName = $("#footerLeadSource").val();
    var ApikeyName = $("#ApiKey").val();
    $("#footerLeadSource").val("");
    $("#ApiKey").val("");
    var LeadID = kendo.guid();
    if (LeadName == '') {
        alert('Lead Source Name is Required');
    }
    else {
        $.post("/Masters/MasterEntries_Save", {
            Name: LeadName,
            ID: LeadID,
            ApiKey: ApikeyName,
            MasterEntryType: 0
        }, function (data) {
            if (data.Success == true) {
                $("#LeadSourceList").data("kendoGrid").dataSource.read().trim(' ');
            }
            else {
                alert(data.Message);
            }
        });
    }
}
