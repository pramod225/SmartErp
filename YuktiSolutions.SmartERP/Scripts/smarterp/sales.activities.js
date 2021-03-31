
function OnResetEmail() {
    $("#emailForm")[0].reset();
}
function OnResetEvent() {
    $("#eventForm")[0].reset();
}

function OnResetCall() {
    $("#callForm")[0].reset();
}

function OnResetTask() {
    $("#taskForm")[0].reset();
}

function BindContactGrid() {
    $("#Contactdetails").data("kendoGrid").dataSource.read();
}

function BulkAssignContact(Grid, id) {

    var selectedCheckBoxes = $("#" + Grid + " .checked-item:checked,#" + Grid + " .sel-item:checked");
    if (selectedCheckBoxes.length > 0) {

        var items = "";
        $(selectedCheckBoxes).each(function () {
            if (items != "") items += ";";
            items += $(this).data("id");
        });
        $.post("/Activity/AssignActivity", { IDs: items, Id: id }, function (data) {
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

function DateCheck() {
    var startDatePicker = $("#startDatePicker").data("kendoDatePicker").value();
    $("#StartDateTime").val(kendo.toString(startDatePicker, "MM/dd/yyyy"));
    var endDatePicker = $("#endDatePicker").data("kendoDatePicker").value();
    $("#EndDateTime").val(kendo.toString(endDatePicker, "MM/dd/yyyy"));
    var sDate = new Date(startDatePicker);
    var eDate = new Date(endDatePicker);
    if (startDatePicker != '' && endDatePicker != '' && sDate > eDate) {
        alert("Please ensure, Start Date should be less than or equal to End Date.");
        return false;
    }
}

function SetStartDate() {
    var startDatePicker = $("#startDatePicker").data("kendoDatePicker").value();
    $("#StartDateTime").val(kendo.toString(startDatePicker, "MM/dd/yyyy").trim(' '));
}
function SetEndDate() {
    var endDatePicker = $("#endDatePicker").data("kendoDatePicker").value();
    $("#EndDateTime").val(kendo.toString(endDatePicker, "MM/dd/yyyy").trim(' '));
}

function SetDueDate() {
    var dueDatePicker = $("#dueDatePicker").data("kendoDatePicker").value();
    $("#DueDate").val(kendo.toString(dueDatePicker, "MM/dd/yyyy").trim(' '));
}


function OnEmailSave(data) {
    $("#emailModal").modal("show");
    if (data.Success == true) {
    /*Reset the form values or close the dialog.*/
        ActivityType:1
        $("#emailForm")[0].reset();
        $("#Id","#emailForm").val(kendo.guid());
    }  
}

function OnTaskSave(data) {
    $("#taskModal").modal("show");
    if (data.Success == true) {
        ActivityType: 2
        /*Reset the form values or close the dialog.*/
        $("#taskForm")[0].reset();
        $("#Id","#taskForm").val(kendo.guid());
       }
   
}
function OnEventSave(data) {
    $("#eventModal").modal("show");
    if (data.Success == true) {
        ActivityType: 3
    /*Reset the form values or close the dialog.*/

        $("#eventForm")[0].reset();
        $("#Id","#eventForm").val(kendo.guid());       
    }
}


function OnCallSave(data) {
    $("#callModal").modal("show");
    if (data.Success == true) {
        ActivityType: 0
        /*Reset the form values or close the dialog.*/
        $("#callForm")[0].reset();
        $("#Id","#callForm").val(kendo.guid());
     } 
}


$(".dropdown-menu li a").click(function () {
    var selectedText = $(this).attr('data-value');
    $("#Event").hide();
    $("#Email").hide();
    $("#Call").hide();
    $("#Task").hide();
    $("#" + selectedText).show();
});
