$("#ddlDepartmentID").change(function () {
    if ($("#ddlDepartmentID").val() != "") {

        if ($("#ddlDepartmentID").val() == 5) {
            $("#nonICConditionGrid").hide();
            $(".ICGridHIde").show();
            fillFormForICDept();
        }
        else {
            $(".ICGridHIde").hide();
            $("#nonICConditionGrid").show();
            fillFormForNONICDept();
        }
    }
    else {
        $("#nonICConditionGrid").show();
        $(".ICGridHIde").hide();
        if ($("#hidDepartmentId").val() == 5) {
            $("#nonICConditionGrid").hide();
            $(".ICGridHIde").show();
            fillFormForICDept();
        }
    }
});

function fillFormForICDept() {
    $("#hidClaimNumber").val(" ");
    $("#AdjusterName").val(" ");
    $("#InsurerName").val(" ");
    $("#InsurerBranchName").val(" ");
    $("#EmployerName").val(" ");
    //$("#ddlStateID").attr("required", true);
    //$("#ICRecordDetailAddress").attr("required", true);
    //$("#ICRecordDetailCity").attr("required", true);
    //$("#ICRecordDetailZip").attr("required", true);
    //$("#ICRecordDetailTaxID").attr("required", true);
   
}


function fillFormForNONICDept() {
    $("#ICRecordDetailAddress").val(" ");
    $("#ICRecordDetailCity").val(" ");
    $("#StateID").val(0);
    $("#ICRecordDetailZip").val(" ");
    $("#ICRecordDetailTaxID").val(" ");
    $('#ddlStateID').prop('selectedIndex', 0);
    $("#ddlStateID").attr("required", false);
    $("#ICRecordDetailAddress").attr("required", false);
    $("#ICRecordDetailCity").attr("required", false);
    $("#ICRecordDetailZip").attr("required", false);
    $("#ICRecordDetailTaxID").attr("required", false);
   


   
}

function FileViewModel() {
    var self = this;
    var popupOpened;
    //$('#divsearch').hide();
    self.PaymentAmount = ko.observable().extend({numeric:2});
    self.PaymentId = ko.observable();
    self.PaymentReceived = ko.observable();
    self.CheckNumber = ko.observable();
    self.CheckUploadName = ko.observable();
    self.InvoiceDetails = ko.observableArray();
    self.Departments = ko.observableArray();
    self.States = ko.observableArray();
    self.States = ko.observableArray([self.States(0)]);
    self.selectedItem = ko.observable(0);
    self.selectedDepartment = ko.observable();
    self.selectedDepartmentFile = ko.observable();
    self.InvoiceDate = ko.observable();
    self.InvoiceSent = ko.observable();
    self.BillingWeek = ko.observable();
    self.InvoiceCount = ko.observable();
    self.InvoiceAmount = ko.observable(0).extend({ numeric: 2 });
    self.FileID = ko.observable();
    self.InvoiceNote = ko.observable();
    self.hidInvoiceAmount = ko.observable(0).extend({ numeric: 2 });
    self.AdjustmentAmt = ko.observable().extend({ numeric: 2 });
    self.AdjustmentNotes = ko.observable();
    self.InvoiceBalanceAmt = ko.observable().extend({ numeric: 2 });

    //ICRecordDetails
    self.ICRecordDetailID = ko.observable();
    self.ICRecordDetailAddress = ko.observable();
    self.ICRecordDetailCity = ko.observable();
    self.StateID = ko.observable();
    self.ICRecordDetailZip = ko.observable();
    self.ICRecordDetailTaxID = ko.observable();
    self.AssignedICInvoices = ko.observableArray([]);
    //
    self.InvoiceNoteDetails = ko.observableArray();
    self.TotalItemCountNotes = ko.observable();
    var mappingOptions = {
        'InvoiceDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM/DD/YYYY");
            }
        }
    }


    this.Init = function (model)
    {
        if (model != null)
        {          
            $('#InvoiceDivPartialFile').show();
            $('#DivInvoiceDetailPartial').show();
            ko.mapping.fromJS(model.FileDetail, {}, self);
            ko.mapping.fromJS(model.InvoiceViewModel.InvoiceDetails, {}, self.InvoiceDetails);
            ko.mapping.fromJS(model.FileDetail.Departments, {}, self.Departments);
            ko.mapping.fromJS(model.FileDetail.States, {}, self.States);
            $("#hidDepartmentId").val(model.FileDetail.DepartmentId);
            if (model.FileDetail.DepartmentId != 5) {
                self.AdjusterName(model.FileDetail.AdjusterDetails.AdjusterFirstName + ' ' + model.FileDetail.AdjusterDetails.AdjusterLastName);
                self.AdjusterFirstName(model.FileDetail.AdjusterDetails.AdjusterFirstName);
                self.AdjusterLastName(model.FileDetail.AdjusterDetails.AdjusterLastName);
                self.AdjusterPhone(model.FileDetail.AdjusterDetails.AdjusterPhone);
                self.AdjusterEmail(model.FileDetail.AdjusterDetails.AdjusterEmail);
                $('#openInvoicePopUp').show();
                $('#openInvoiceICTab').hide();
            }
            else {
                self.ICRecordDetailID(model.FileDetail.ICRecordDetailID);
                self.ICRecordDetailAddress(model.FileDetail.ICRecordDetailAddress);
                self.ICRecordDetailCity(model.FileDetail.ICRecordDetailCity);
                self.selectedItem(model.FileDetail.StateID);
                self.ICRecordDetailZip(model.FileDetail.ICRecordDetailZip);
                self.ICRecordDetailTaxID(model.FileDetail.ICRecordDetailTaxID);
                $('#openInvoiceICTab').show();
                $('#openInvoicePopUp').hide();
            }          
         
                    
            self.FileID(model.FileDetail.FileID);
            self.InvoiceCount(model.InvoiceViewModel.InvoiceCount);
            popupOpened = "Invoice";
            self.iTotalItemCount(model.InvoiceViewModel.InvoiceCount);
            self.iPager().iCurrentPage(1);

            if (model.FileDetail.DBconnection == "L") {
                $("form :input").attr("disabled", "disabled");
                alertify.alert("As this is Lien Record,so cannot be edited");
            }
            if (model.FileDetail.InsurerName != null)
                $('#SelectInsurerBranchModalPopUp').css('visibility', 'visible');
            $('#DBNameInsurer').val(model.FileDetail.IsLienInsurerID);
            $('#DBNameInsurerBranch').val(model.FileDetail.IsLienInsurerBranchID);
            $('#DBNameEmployer').val(model.FileDetail.IsLienEmployerID);
            $('#DBNameAdjuster').val(model.FileDetail.IsLienAdjusterID);
            self.selectedDepartment(model.FileDetail.DepartmentId);
                self.selectedDepartmentFile(model.FileDetail.DepartmentId);          
            if (model.FileDetail.InvoiceDate != null)
                self.InvoiceDate = ko.observable(moment(model.FileDetail.InvoiceDate).format("MM/DD/YYYY"));
            if (model.FileDetail.InvoiceSent != null)
                self.InvoiceSent = ko.observable(moment(model.FileDetail.InvoiceSent).format("MM/DD/YYYY"));
            if (model.FileDetail.BillingWeek != null)
                self.BillingWeek = ko.observable(moment(model.FileDetail.BillingWeek).format("MM/DD/YYYY"));

            $('#title').text("Update Record ");
            if (model.FileDetail.IsLienClaimNumber == true)
            {
                  $("#FirstName").attr("readonly", "readonly");
                  $("#LastName").attr("readonly", "readonly");
                 
            } 
            if ($('#DBNameInsurer').val() == "false") {
                $('#AddInsurerBranchModalPopUp').show();
            }
            $('#InsurerIdPop').val(model.FileDetail.InsurerId);
        }
        else
        {           
            $.getJSON("/File/getDepartment?isIC=0", self.Departments);

            $.getJSON("/File/getState", function (data)
            {
                self.States(data.slice());
                if (model != null)
                    self.selectedItem(model.StateID);
            });
            $('#InvoiceDivPartialFile').hide();
            $('#DivInvoiceDetailPartial').hide();
            $(".ICGridHIde").hide();
        }       
        
    };




    //------ File --------------- //
    self.DepartmentId = ko.observable();
    self.FirstName = ko.observable();
    self.LastName = ko.observable();
    self.ClaimNumber = ko.observable();
    self.InsurerId = ko.observable();
    self.InsurerBranchId = ko.observable();
    self.EmployerId = ko.observable();
    self.AdjusterId = ko.observable();
    self.IsLienInsurerID = ko.observable();
    self.IsLienInsurerBranchID = ko.observable();
    self.IsLienEmployerID = ko.observable();
    self.IsLienAdjusterID = ko.observable();
    self.IsLienClaimNumber = ko.observable();
    self.Notes = ko.observable();
    self.AddFileSuccess = function (modelFile) {
        var modelFile = $.parseJSON(modelFile);
        if (modelFile == "Duplicate invoice record exist.") {
            alertify.alert("Invoice record already exist.");
        }
        if (modelFile == "Duplicate Claim Number exist.") {
            alertify.alert("Claim Number already Exist.");
        }
        if (modelFile == "Updated Successfully.") {            
            $('#InvoiceDivPartialFile').show();
            alertify.confirm("Updated Successfully.", function (e) {
                if (e) {
                    window.location = "/File/FileLanding";
                }
            });
        }
        else {
            if (modelFile != "0" && modelFile != "Duplicate Claim Number exist." && modelFile != "Duplicate invoice record exist." && modelFile != "Updated Successfully.") {
                self.FileID(modelFile);
                if ($("#ddlDepartmentID").val() != 5) {
                    self.AdjusterFirstName(adjusterDetailModel.AdjusterFirstName());
                    self.AdjusterLastName(adjusterDetailModel.AdjusterLastName());
                    self.AdjusterPhone(adjusterDetailModel.AdjusterPhone());
                    self.AdjusterEmail(adjusterDetailModel.AdjusterEmail());
                    $.getJSON("/File/getDepartment?isIC=1", self.Departments);
                    $('#openInvoicePopUp').show();
                    $('#openInvoiceICTab').hide();
                }
                else {
                    $("#ddlDepartmentID").attr("disabled", true);
                    $('#openInvoicePopUp').hide();
                    $('#openInvoiceICTab').show();
            }

                alertify.success("Inserted Successfully");
                $('#hidDepartmentId').val(self.selectedDepartment());
                $('#InvoiceDivPartialFile').show();
                $('#SaveFileButton').show();
                $('#CancelFileButton').hide();
            }
        }
    };
    self.FileCancel = function () {
        $('#FileID').val("");
        $('#DepartmentId').val("");
        $('#FirstName').val("");
        $('#LastName').val("");
        $('#ClaimNumber').val("");
        $('#InsurerId').val("");
        $('#InsurerBranchId').val("");
        $('#EmployerId').val("");
        $('#AdjusterId').val("");
        $('#InsurerName').val("");
        $('#AdjusterName').val("");
        $('#EmployerName').val("");
        $('#InsurerBranchName').val("");
        //   $('#SelectInsurerBranchModalPopUp').css('visibility', 'hidden');
        // $('#AddInsurerBranchModalPopUp').hide();
        self.selectedDepartment("");
    };

    self.FilePageInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }

        var _deptID = $("#ddlDepartmentID").val();
        if (_deptID == "5") {
            var status = 0;
            if ($("#ICRecordDetailZip").val() == "") {
                $("#ICRecordDetailZip").attr("required", "required");
                status = 1;
            }
           
            if ($("#ICRecordDetailTaxID").val() == "") {
                $("#ICRecordDetailTaxID").attr("required", "required");
                status = 1;
            }
           
            if ($("#ddlStateID").val() == "") {
                $("#ddlStateID").attr("required", "required");
                status = 1;
            }
            
            if ($("#ICRecordDetailCity").val() == "") {
                $("#ICRecordDetailCity").attr("required", "required");
                status = 1;
            }
            
            if ($("#ICRecordDetailAddress").val() == "") {
                $("#ICRecordDetailAddress").attr("required", "required");
                status = 1;
            }
           


            if (status == 0) {
                return true;
            }
            else {
                return false;
            }

        }
        else {
            return true;
        }
    }
    //----------------------------- file ends ----------------------------------//

    //------------Adjuster--------------------//
    var adjusterDetailModel;
    self.AdjusterSearchText = ko.observable();
    self.AdjusterName = ko.observable();
    self.AdjusterFirstName = ko.observable();
    self.AdjusterLastName = ko.observable();
    self.AdjusterPhone = ko.observable();
    self.AdjusterFax = ko.observable();
    self.AdjusterEmail = ko.observable();
    self.AdjustersArrResult = ko.observableArray([]);
    self.DBNameAdjuster = ko.observable();
    self.AdjusterCount = ko.observable();
    totalItemCount = self.AdjusterCount();
    self.TotalItemCount = ko.observable();
    self.AdjusterCancel = function () {
        $('#AdjusterFirstName').val("");
        $('#AdjusterLastName').val("");
        $('#AdjusterPhone').val("");
        $('#AdjusterFax').val("");
        $('#AdjusterEmail').val("");
    };

    self.SelectAdjusterName = function (modelPartial) {
        adjusterDetailModel = modelPartial;        
        self.AdjusterId(modelPartial.AdjusterId());
        self.AdjusterName(modelPartial.AdjusterFirstName() + ' ' + modelPartial.AdjusterLastName());
        $('#AdjusterId').val(modelPartial.AdjusterId());
        $('#AdjusterName').val(self.AdjusterName());        
        if (modelPartial.DBNameAdjuster() == "Lien") {
            $('#DBNameAdjuster').val(true);
        }
        else {
            $('#DBNameAdjuster').val(false);
        }
        $('#sectAdjuster').click();
    };
    $('#sectAdjuster').click(function () {
        self.AdjustersArrResult.removeAll();
        $('#SearchAdjusterTitle').val("");
        self.TotalItemCount(0);
    });
    self.SearchAdjusterByName = function () {
        var searchText = $("#SearchAdjusterTitle").val();
        if (searchText == "") {
            alertify.alert("Adjuster Name required");
        }
        else {
            $.ajax({
                url: "/File/getAdjusterByAdjusterNameSearch",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({ AdjusterName: $("#SearchAdjusterTitle").val(), skip: 0 }),
                success: function (data) {
                    if (data != null) {
                        ko.mapping.fromJS(data.AdjusterSearch, {}, self.AdjustersArrResult);
                        popupOpened = "Adjuster";
                        self.AdjusterCount(data.AdjusterCount);
                        self.TotalItemCount(data.AdjusterCount);
                        self.Pager().CurrentPage(1);
                    }
                    else {
                        alertify.alert("No data found");
                    }
                },
                error: function (data) {
                    alertify.alert("Error Occur");
                }
            });
        }
    };
    self.AddAdjusterName = function (resultadjust) {
        var data = $.parseJSON(resultadjust);
        if (data.AdjusterId > 0) {
            self.AdjusterId(data.AdjusterId);
            self.AdjusterName(data.AdjusterFirstName + ' ' + data.AdjusterLastName);
            $('#DBNameAdjuster').val(false);
            alertify.success("Inserted Successfully");
            $('#closeAddAdjusterModel').click();
        }
        else if (data.AdjusterId == 0) {
            alertify.alert("Adjuster already exists");
        }
    };
    $('#closeAddAdjusterModel').click(function () {
        $('#AdjusterFirstName').val("");
        $('#AdjusterLastName').val("");
        $('#AdjusterPhone').val("");
        $('#AdjusterFax').val("");
        $('#AdjusterEmail').val("");
    });
    self.AdjusterInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }
    //------------------------------ adjuter ends ---------------------------------//

    //-------------Employer---------------------------//
    self.EmployerName = ko.observable();
    self.EmployerArrResult = ko.observableArray([]);
    self.DBNameEmployer = ko.observable();
    self.EmployerCount = ko.observable();
    self.PaymentCount = ko.observable();

    self.SelectEmployerName = function (modelPartialemp) {
        self.EmployerId(modelPartialemp.EmployerId());
        self.EmployerName(modelPartialemp.EmployerName());      
        if (modelPartialemp.DBNameEmployer() == "Lien") {
            $('#DBNameEmployer').val(true);
        }
        else {
            $('#DBNameEmployer').val(false);
        }
        $('#EmployerId').val(modelPartialemp.EmployerId());
        $('#EmployerName').val(modelPartialemp.EmployerName());
        $('#sectionCloseEmployer').click();
    };
    $('#sectionCloseEmployer').click(function () {
        self.EmployerArrResult.removeAll();
        $('#SearchEmployerTitle').val("");
        self.TotalItemCount(0);
    });
    self.SearchEmployerByName = function () {
        var searchText = $("#SearchEmployerTitle").val();
        if (searchText == "") {
            alertify.alert("Employer Name required")
        }
        else {
            $.ajax({
                url: "/File/getEmployerByEmployerNameSearch",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({
                    EmployerName: $("#SearchEmployerTitle").val(), skip: 0
                }),
                success: function (data) {
                    if (data != null) {
                        ko.mapping.fromJS(data.EmployerSearch, {}, self.EmployerArrResult);
                        popupOpened = "Employer";
                        self.EmployerCount(data.EmployerCount);
                        self.TotalItemCount(data.EmployerCount);
                        self.Pager().CurrentPage(1);
                    }
                    else {
                        alertify.alert("No data found");
                    }
                },
                error: function (data) {
                    alertify.alert("Error Occur");
                }
            });
        }
    }
    self.AddEmployerName = function (modelemp) {
        var modelemp = $.parseJSON(modelemp);
        if (modelemp.EmployerId > 0) {
            self.EmployerId(modelemp.EmployerId);
            self.EmployerName(modelemp.EmployerName);
            $('#DBNameEmployer').val(false);
            alertify.success("Inserted Successfully");
            $('#closeAddEmployerModel').click();
        }
        else if (modelemp.EmployerId == 0) {
            alertify.alert("Employer already exists");
        }
    };
    $('#closeAddEmployerModel').click(function () {
        $('#EmployerNameAdd').val("");
    });
    self.EmployerInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }
    //---------------------------------------------------------------//

    //----------Insurer------------------------------//    
    self.InsurerName = ko.observable();
    self.InsurerArrResult = ko.observableArray([]);
    self.DBNameInsurer = ko.observable();
    $("#sectionCloseInsurer").click(function () {
        self.InsurerArrResult.removeAll();
        $("#SearchInsurerTitle").val("");
        self.TotalItemCount(0);
    });
    self.SelectInsurerName = function (modelPartialInsure) {
        self.InsurerId(modelPartialInsure.InsurerId());
        self.InsurerName(modelPartialInsure.InsurerName());
        $('#InsurerId').val(modelPartialInsure.InsurerId());
        $('#InsurerName').val(modelPartialInsure.InsurerName());
        $('#InsurerIdPop').val(modelPartialInsure.InsurerId());
        // $('#InsurerBranchName').val('');
        if (modelPartialInsure.DBNameInsurer() == "Lien")
            $('#DBNameInsurer').val(true);
        else
            $('#DBNameInsurer').val(false);
        $('#sectionCloseInsurer').click();
        $('#SelectInsurerBranchModalPopUp').css('visibility', 'visible');
        if (modelPartialInsure.DBNameInsurer() == "Lien") {
            //    $('#AddInsurerBranchModalPopUp').hide();
        }
        else {
            $('#AddInsurerBranchModalPopUp').show();
        }
    };

    self.SearchInsurerByName = function () {
        var searchText = $("#SearchInsurerTitle").val();
        if (searchText == "") {
            alertify.alert("Insurer Name required");
        }
        else {
            $.ajax({
                url: "/File/getInsurerByInsurerNameSearch",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({
                    InsurerName: $("#SearchInsurerTitle").val(), skip: 0
                }),
                success: function (data) {
                    if (data != null) {
                        ko.mapping.fromJS(data.InsurerSearch, {}, self.InsurerArrResult);
                        popupOpened = "Insurer";
                        self.TotalItemCount(data.InsurerCount);
                        self.Pager().CurrentPage(1);
                    }
                    else {
                        alertify.alert("No data found");
                    }
                },
                error: function (data) {
                    alertify.alert("Error Occur");
                }
            });
        }
    };

    self.AddInsurerName = function (modelinsu) {
        var modelinsu = $.parseJSON(modelinsu);
        if (modelinsu.InsurerId > 0) {
            self.InsurerId(modelinsu.InsurerId);
            self.InsurerName(modelinsu.InsurerName);
            $('#InsurerIdPop').val(modelinsu.InsurerId);
            $('#DBNameInsurer').val(false);
            $('#AddInsurerBranchModalPopUp').show();
            $('#SelectInsurerBranchModalPopUp').css('visibility', 'visible');
            alertify.success("Inserted Successfully");
            $('#closeAddInsurerModel').click();
        }
        else if (modelinsu.InsurerId == 0) {
            alertify.alert("Insurer already exists");
        }
    };

    $('#closeAddInsurerModel').click(function () {
        $('#AddInsurerName').val("");
    });

    self.InsurerInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }
    //---------------------------------------------------------------//

    //-------------Insurer Branches--------------------//    
    var DBNAme;
    self.InsurerBranchName = ko.observable();
    self.InsurerBranchArrResult = ko.observableArray([]);
    self.DBNameInsurerBranch = ko.observable();

    self.SearchInsurerBranchNameByInsurerIDPOPUP = function () {
        var searchText = $("#DBNameInsurer").val();
        if (searchText == "true") {
            DBNAme = 'l';
        }
        else {
            DBNAme = 'a';
        }
        $.ajax({
            url: "/File/GetAllInsurerBranch",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: ko.toJSON({ InsurerBranchName: $('#SearchInsurerBranchTitle').val(), skip: 0 }),
            success: function (data) {
                if (data != null) {
                    popupOpened = "InsurerBranch";
                    //    ko.mapping.fromJS(data.InsurerBranchSearch, {}, self.InsurerBranchArrResult);
                    // self.TotalItemCount(data.InsurerBranchCount);
                    //   self.Pager().CurrentPage(1);

                    //$('#AddInsurerBranchModalPopUp').hide();
                }
                else {
                    alertify.alert("No data found");
                }
            },
            error: function (data) {
                alertify.alert("Error Occur");
            }
        });
    }
    self.SelectInsurerBranchesName = function (modelPartialInsB) {
        self.InsurerBranchId(modelPartialInsB.InsurerBranchId());
        self.InsurerBranchName(modelPartialInsB.InsurerBranchName());
        $('#InsurerBranchId').val(modelPartialInsB.InsurerBranchId());
        $('#InsurerBranchName').val(modelPartialInsB.InsurerBranchName());
        if (modelPartialInsB.DBNameInsurerBranch() == "Lien") {
            $('#DBNameInsurerBranch').val(true);
        }
        else {
            $('#DBNameInsurerBranch').val(false);
        }
        $('#sectionCloseInsurerBranch').click();
    };
    $('#sectionCloseInsurerBranch').click(function () {
        self.InsurerBranchArrResult.removeAll();
        $('#SearchInsurerBranchTitle').val("");
    });
    self.SearchInsurerBranchByName = function () {
        var searchText = $("#SearchInsurerBranchTitle").val();
        if (searchText == "") {
            alertify.alert("Insurer Branch Name required");
        }
        else {
            $.ajax({
                url: "/File/GetAllInsurerBranch",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({
                    InsurerBranchName: $('#SearchInsurerBranchTitle').val(),  skip: 0
                }),
                success: function (data) {
                    if (data != null) {
                        popupOpened = "InsurerBranch";
                        ko.mapping.fromJS(data.InsurerBranchSearch, {}, self.InsurerBranchArrResult);
                        self.TotalItemCount(data.InsurerBranchCount);
                        self.Pager().CurrentPage(1);
                    }
                    else {
                        alertify.alert("No data found");
                    }
                },
                error: function (data) {
                    alertify.alert("Error Occur");
                }
            });
        }
    };
    $("#sectionCloseInsurerBranch").click(function () {
        self.InsurerBranchArrResult.removeAll();
        $("#SearchInsurerBranchTitle").val("");
        self.TotalItemCount(0);
    });
    self.AddInsurerBranchName = function (modelinsuBranch) {
        var modelinsuBranch = $.parseJSON(modelinsuBranch);
        if (modelinsuBranch.InsurerBranchId > 0) {
            self.InsurerBranchId(modelinsuBranch.InsurerBranchId);
            self.InsurerBranchName(modelinsuBranch.InsurerBranchName);
            $('#DBNameInsurerBranch').val(false);
            alertify.success("Inserted Successfully");
            $('#closeAddInsurerBranchModel').click();
        }
        else if (modelinsuBranch.InsurerBranchId == 0) {
            alertify.alert("Insurer Branch already exists");
        }
    };
    $('#closeAddInsurerBranchModel').click(function () {
        $('#AddInsurerBranchName').val("");
    });
    self.InsurerBranchInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }

    //-------------------------- Insurer Branches ends-------------------------------------//

    //--------------------- invoice -------------------------//
    self.iTotalItemCount = ko.observable();
    self.InvoiceID = ko.observable();
    self.InvoiceNumber = ko.observable();
    self.InvoiceAmt = ko.observable().extend({numeric:2});
    self.InvoiceDate = ko.observable();
    self.InvoiceDueDate = ko.observable();
    self.InvoiceSent = ko.observable();
    self.BillingWeek = ko.observable();
    self.AddInvoiceSuccess = function (modelinvo) {
        $("#InvoicePanalBody").addClass("panalwithscroll");
        if ($('#hidAdjustmentAmt').val() != "" && $('#hidInvoiceBalanceAmt').val() != "")
            var valBal = parseFloat((parseFloat($('#hidAdjustmentAmt').val(), 0.0) + parseFloat($('#hidInvoiceBalanceAmt').val(), 0.0)).toFixed(2) - self.AdjustmentAmt()).toFixed(2);
        else
            var valBal = parseFloat(self.InvoiceAmt() - self.AdjustmentAmt()).toFixed(2);
        $('#InvoiceBalanceAmt').val(valBal);
        self.InvoiceID(modelinvo);
        var modelinvo = $.parseJSON(modelinvo);
        if (modelinvo != "0") {        
                alertify.success("Saved Successfully")
                $('#divInvoiceNotes').show();

            // document.getElementById("frmInvoiceDetail").reset();

            $("#HFInvoiceID").val(modelinvo)

            $.ajax({
                url: "/File/InvoiceDetail",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({
                    C: $("#hidClaimNumber").val(),
                    FileID: self.FileID(),
                    Skip: 0
                }),
                success: function (data) {
                    if (data != null) {
                        //$('#CloseInvoicePartial').click();
                        self.InvoiceDetails.removeAll();
                        ko.mapping.fromJS(data.InvoiceDetails, {}, self.InvoiceDetails);
                        self.iTotalItemCount(data.InvoiceCount);
                        $('#DivInvoiceDetailPartial').show();
                        $('#divInvoiceNotes').show();
                      //  clearInvoiceForm();
                    }
                    else {
                        alertify.alert("No data found");
                    }
                },
                error: function (data) {
                    alertify.alert("Error Occur");
                }
            });
        }
        else {
            alertify.alert("Invoice Number Already Exist")
        }
        //$('#SaveFileButton').show();
    }

    self.ViewInvoiceDetailsByID = function (model) {
        IcDdlSelectedDetail();
        $("#InvoicePanalBody").addClass("panalwithscroll");
        $('#hidAdjustmentAmt').val(model.AdjustmentAmt());
        //$('#hidInvoiceBalanceAmt').val(model.InvoiceBalanceAmt());       
        self.InvoiceID(model.InvoiceID());
        self.hidInvoiceAmount(model.InvoiceAmt());
        self.selectedDepartment(model.DepartmentId());
        ko.mapping.fromJS(model, {}, self);
        

        //$('#InvoiceBalanceAmt').val(parseFloat(model.InvoiceBalanceAmt()).toFixed(2));
        $('#divInvoiceNotes').show();
        if (model.InvoiceDate != null)
            self.InvoiceDate(moment(model.InvoiceDate()).format("MM/DD/YYYY"));
        if (model.InvoiceDueDate() != null)
            self.InvoiceDueDate(moment(model.InvoiceDueDate()).format("MM/DD/YYYY"));
        if (model.InvoiceSent() != null) {
            if (model.InvoiceSent() == '/Date(-62135596800000)/') {
                self.InvoiceSent("");
            }
            else {
                self.InvoiceSent(moment(model.InvoiceSent()).format("MM/DD/YYYY"));
            }
        }
        if (model.BillingWeek() != null)
        {
            if (model.BillingWeek() == '/Date(-62135596800000)/') {
                self.BillingWeek("");                
            }
            else {                
                self.BillingWeek(moment(model.BillingWeek()).format("MM/DD/YYYY"));
            }                
        }
        
        InvoiceNoteBinding(0);
        $.post("/File/GetPaymentRecordByInvoiceId", {
            InvoiceId: model.InvoiceID(), Skip: 0, FileID: self.FileID()
        }, function (_data) {
            var data = $.parseJSON(_data);
            if (data.PaymentCount > 0)
                $('#InvoiceAmount').attr("readonly", "readonly");
            else
                $('#InvoiceAmount').attr("readonly", false);
        });

        $.post("/File/GetAssignedInvoices", {
            InvoiceID: model.InvoiceID()
        }, function (_data) {                        
            ko.mapping.fromJS(_data, {}, self.AssignedICInvoices);            
        });
    }
    function clearInvoiceForm() {
        self.InvoiceNumber('');
        self.InvoiceAmt('');
        self.InvoiceDate('');

        IcDdlSelectedDetail();
        $(".icUploadFile").val('');
        self.TotalItemCountNotes(0);
        self.InvoiceNoteDetails('');
        $('#hidAdjustmentAmt').val('');
        $('#hidInvoiceBalanceAmt').val('');
        self.AdjustmentAmt('');
        self.InvoiceBalanceAmt('');
        $('#divInvoiceNotes').hide();
        self.AdjustmentNotes('');
    }
    $('#openInvoicePopUp').click(function () {
        self.InvoiceID('');
        $('#InvoiceAmount').attr("readonly", false);
        clearInvoiceForm();
        $("#InvoicePanalBody").removeClass("panalwithscroll");
    });

    function IcDdlSelectedDetail() {
        if ($("#ddlDepartmentID").val() == 5)// in case of IC department, pre select the department in the dropdown.
        {
            self.selectedDepartment(5);
            self.InvoiceSent(null);
            self.BillingWeek(null);
            $("#invoiceAdjusterGrid").hide();
            //$("#InvoiceDepartment").attr("disabled", true);
            $("#InvoiceDepartment").val(5);
            $(".deptToggle").hide();
            $("#InvoiceSent").attr('required', false);
            $(".icUploadFile").show();
            $("#UploadFile").replaceWith($("#UploadFile").clone());
        }
        else {
            self.selectedDepartment('');
            $("#InvoiceSent").attr('required', 'required');
            $(".icUploadFile").hide();
            $(".deptToggle").show();
            self.InvoiceSent('');
            self.BillingWeek('');
        }
    }
    self.InvoiceInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($("#ddlDepartmentID").val() == 5)// in case of IC department, pre select the department in the dropdown.
        {
            if ($('#UploadFile').val() != "") {
                var ext = $('#UploadFile').val().split('.').pop().toLowerCase();
                if ($.inArray(ext, ['pdf', 'PDF']) == -1) {
                    alertify.alert('Upload Pdf file');
                    return false;
                }
            }
        }
        //if (parseFloat($('#hidInvoiceAmt').val()) != parseFloat($('#InvoiceAmount').val())) {
        //    $('#hidInvoiceBalanceAmt').val(parseFloat($('#InvoiceAmount').val()) - parseFloat($('#hidAdjustmentAmt').val()));
        //    alert($('#hidInvoiceBalanceAmt').val());
        //}
        if (parseFloat($('#AdjustmentAmt').val()) > (parseFloat($('#hidInvoiceBalanceAmt').val()) + parseFloat($('#hidAdjustmentAmt').val()))) {
            alertify.alert("Adjustment amount should be less then Balance");
            $('#AdjustmentAmt').val(parsefloat($('#hidAdjustmentAmt').val()));
            return false;
        }
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        else if (self.InvoiceAmt() == 0) {
            alertify.alert("Invoice amount can't be zero.");
            return false;
        }
        return true;
    }


    //========= paging code for invoice grid only ===========//
    var ipagingSettings = {
        ipageSize: 10,
        ipageSlide: 2
    };
    self.iSkip = ko.observable(0);
    self.iTake = ko.observable(ipagingSettings.ipageSize);
    self.iPager = ko.ipager(self.iTotalItemCount);
    self.iPager().iPageSize(ipagingSettings.ipageSize);
    self.iPager().iPageSlide(ipagingSettings.ipageSlide);
    self.iPager().iCurrentPage(1);
    self.iPager().iCurrentPage.subscribe(function () {
        var skip = ipagingSettings.ipageSize * (self.iPager().iCurrentPage() - 1);
        var take = ipagingSettings.ipageSize;
        self.iGetRecordsWithSkipTake(skip, take);
    });
    self.iGetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.iSkip(0);
            self.iTake(ipagingSettings.ipageSize);
        }
        else {
            self.iSkip(skip);
            self.iTake(take);
        }

        InvoiceRebindandPaging();

    };
    // ============= paging code for invoice ends =============== //

    //---------------------------- invoice ends -----------------------------------//

    //------------------ payment ------------------------//
    self.PaymentDetails = ko.observableArray([]);
    $('#sectPayment').click(function () {
        self.TotalItemCount(0);
    });
    self.ViewPaymentDetails = function (model) {
        resetPaymentcontrol();
        self.InvoiceAmount(model.InvoiceAmt());
        self.InvoiceID(model.InvoiceID());
        $.post("/File/GetPaymentRecordByInvoiceId", {
            InvoiceId: model.InvoiceID(), Skip: 0, FileID: self.FileID()
        }, function (_data) {
            var data = $.parseJSON(_data);
            ko.mapping.fromJS(data.PaymentDetails, {}, self.PaymentDetails);
            self.PaymentCount(data.PaymentCount);
            popupOpened = "Payment";
            self.TotalItemCount(data.PaymentCount);
            self.Pager().CurrentPage(1);
        });
    }
    self.PaymentInfoFormBeforeSubmit = function (arr, $form, options) {
        $("#divPayment :text").each(function () {
            if ($(this).val() == "") {
                $(this).addClass('required_bdr');
                $(this).removeClass('remove_bdr');
            }
        });
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        else if (parseFloat(self.PaymentAmount()) == 0)
        {
            alertify.alert("Payment amount can't be zero.");
            return false;
        }
        //else if (parseFloat(self.PaymentAmount()) + self.PaymentTotal() - self.oldPaymentAmount() > self.InvoiceAmount()) {
        //    alertify.alert("Payment(s) amount can't be greater than Invoice amount.");
        //    return false;
        //}
        return true;
    }
    //self.PaymentTotal = ko.computed(function () {
    //    var total = 0;
    //    if (self.PaymentDetails().length > 0) {
    //        ko.utils.arrayForEach(self.PaymentDetails(), function (data) {
    //            total += data.PaymentAmount();
    //        });
    //    }
    //    return total;
    //});
    self.AddPaymentDetailSuccess = function (model) {
        if (model != null) {
            var newPayment = $.parseJSON(model);
            var viewModel = self.PaymentDetails;
            if (!newPayment.IsPaymentGreater) {
                if (!newPayment.flag) {
                    for (var i = 0; i <= viewModel().length - 1; i++) {
                        if (viewModel()[i].PaymentId() == newPayment.PaymentId) {
                            self.PaymentDetails.splice(i, 1);
                            self.PaymentDetails.splice(i, 0, new InsertPaymentLineItem(newPayment.PaymentId, newPayment.PaymentAmount, newPayment.PaymentReceived, newPayment.CheckNumber, newPayment.CheckUploadName, newPayment.CheckDownloadPath));
                            alertify.success("Updated Successfully");
                            break;
                        }
                    }
                }
                else {
                    $.post("/File/GetPaymentRecordByInvoiceId", {
                        InvoiceId: newPayment.InvoiceId, Skip: 0, FileID: self.FileID()
                    }, function (_data) {
                        var data = $.parseJSON(_data);
                        self.PaymentDetails.removeAll();
                        ko.mapping.fromJS(data.PaymentDetails, {
                        }, self.PaymentDetails);
                        self.PaymentCount(data.PaymentCount);
                        popupOpened = "Payment";
                        self.TotalItemCount(data.PaymentCount);
                        self.Pager().CurrentPage(1);
                    });
                    alertify.success("Inserted Successfully");
                }
                resetPaymentcontrol();
                InvoiceRebindandPaging();

            }
            else {
                alertify.alert("Payment(s) amount can't be greater than Invoice amount.");
            }
        }
    };
    function InvoiceRebindandPaging() {
        $.ajax({
            url: "/File/InvoiceDetail",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: ko.toJSON({
                FileID: self.FileID(), Skip: $('#hidInvoiceskip').val(), C: $('#hidClaimNumber').val(), 
            }),
            success: function (data) {
                if (data != null) {
                    ko.mapping.fromJS(data.InvoiceDetails, {
                    }, self.InvoiceDetails);
                    self.iTotalItemCount(data.InvoiceCount);
                }
                else {
                    alertify.alert("No data found");
                }
            },
            error: function (data) {
                alertify.alert("Error Occur");
            }
        });
    }
    function InsertPaymentLineItem(_PaymentId, _PaymentAmount, _PaymentReceived, _CheckNumber, _CheckUploadName, _CheckDownloadPath) {
        var self = this;
        self.PaymentId = ko.observable(_PaymentId);
        self.PaymentAmount = ko.observable(_PaymentAmount);
        self.PaymentReceived = ko.observable(moment(_PaymentReceived).format("MM/DD/YYYY"));
        self.CheckNumber = ko.observable(_CheckNumber);
        self.CheckUploadName = ko.observable(_CheckUploadName);
        self.CheckDownloadPath = ko.observable(_CheckDownloadPath);
    }
    function resetPaymentcontrol() {
        self.PaymentId("");
        self.PaymentAmount("");
        self.PaymentReceived("");
        self.CheckNumber("");
        self.CheckUploadName("");
        //self.oldPaymentAmount("");
        $('#PaymentDate').val("");
        $('input').addClass('remove_bdr');
        $('.form-group').removeClass('has-error has-feedback');
        $('.form-group').find('.help-block').hide();
        $('.form-group').find('i.form-control-feedback').hide();
        $("#CheckUploadName").val("");
        $("#CheckUploadName").replaceWith($("#CheckUploadName").clone(true));
    }
    self.ViewPaymentLineItems = function (model) {
        self.PaymentId(model.PaymentId());
        self.PaymentAmount(model.PaymentAmount());
        if (model.PaymentReceived()!=null)
            self.PaymentReceived(moment(model.PaymentReceived()).format("MM/DD/YYYY"));
        else
            self.PaymentReceived(model.PaymentReceived());

        self.CheckNumber(model.CheckNumber());
        self.CheckUploadName(model.CheckUploadName());
        self.CheckDownloadPath = (model.CheckDownloadPath);
        //self.oldPaymentAmount(model.PaymentAmount());

    }
    //----------------------------payment ends-----------------------------------//



    //------------------ Refund------------------------//
    self.PaymentRefundID = ko.observable();
    self.InvoiceId = ko.observable();
    self.RefundAmount = ko.observable();
    self.RefundReceived = ko.observable();
    self.CheckNumber = ko.observable();
    self.CheckUploadName = ko.observable();
    self.RfTotalItemCount = ko.observable();
    self.PaymentRefundCount = ko.observable();

    self.PaymentRefundDetails = ko.observableArray([]);
    $('#sectPaymentRefund').click(function () {
        self.RfTotalItemCount(0);
    });
    self.ViewPaymentRefundDetails = function (model) {
        resetPaymentRefundcontrol();
        self.InvoiceAmount(model.InvoiceAmt());
        self.InvoiceID(model.InvoiceID());
        $.post("/File/GetPaymentRefundRecordByInvoiceId", {
            InvoiceId: model.InvoiceID(), Skip: 0, FileID: self.FileID()
        }, function (_data) {
            var data = $.parseJSON(_data);
            ko.mapping.fromJS(data.PaymentRefundDetails, {}, self.PaymentRefundDetails);
            self.PaymentRefundCount(data.PaymentRefundCount);
            popupOpened = "PaymentRefund";
            self.RfTotalItemCount(data.PaymentRefundCount);
            self.RfPager().RfCurrentPage(1);
        });
    }
    self.PaymentRefundInfoFormBeforeSubmit = function (arr, $form, options) {
        $("#divPaymentRefund :text").each(function () {
            if ($(this).val() == "") {
                $(this).addClass('required_bdr');
                $(this).removeClass('remove_bdr');
            }
        });
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        else if (parseFloat(self.RefundAmount()) == 0) {
            alertify.alert("Refund amount can't be zero.");
            return false;
        }
      
        return true;
    }
   
    self.AddPaymentRefundDetailSuccess = function (model) {
        if (model != null) {
            var newPayment = $.parseJSON(model);
            var viewModel = self.PaymentRefundDetails;
            if (!newPayment.IsPaymentGreater) {
                if (!newPayment.flag) {
                    for (var i = 0; i <= viewModel().length - 1; i++) {
                        if (viewModel()[i].PaymentRefundID() == newPayment.PaymentRefundID) {
                            self.PaymentRefundDetails.splice(i, 1);
                            self.PaymentRefundDetails.splice(i, 0, new InsertPaymentRefundLineItem(newPayment.PaymentRefundID, newPayment.RefundAmount, newPayment.RefundReceived, newPayment.CheckNumber, newPayment.CheckUploadName, newPayment.CheckDownloadPath));
                            alertify.success("Updated Successfully");
                            break;
                        }
                    }
                }
                else {
                    $.post("/File/GetPaymentRefundRecordByInvoiceId", {
                        InvoiceId: newPayment.InvoiceId, Skip: 0, FileID: self.FileID()
                    }, function (_data) {
                        var data = $.parseJSON(_data);
                        if (self.PaymentRefundDetails()!= null)
                        self.PaymentRefundDetails.removeAll();
                        ko.mapping.fromJS(data.PaymentRefundDetails, {
                        }, self.PaymentRefundDetails);
                        self.PaymentRefundCount(data.PaymentRefundCount);
                        popupOpened = "PaymentRefund";
                        self.RfTotalItemCount(data.PaymentRefundCount);
                        self.Pager().CurrentPage(1);
                    });
                    alertify.success("Inserted Successfully");
                }
                resetPaymentRefundcontrol();
                InvoiceRebindandPaging();

            }
            else {
                alertify.alert("Refund(s) amount can't be greater than Invoice amount.");
            }
        }
    };    
    function InsertPaymentRefundLineItem(_PaymentRefundID, _RefundAmount, _RefundReceived, _CheckNumber, _CheckUploadName, _CheckDownloadPath) {
        var self = this;
        self.PaymentRefundID = ko.observable(_PaymentRefundID);
        self.RefundAmount = ko.observable(_RefundAmount);
        self.RefundReceived = ko.observable(moment(_RefundReceived).format("MM/DD/YYYY"));
        self.CheckNumber = ko.observable(_CheckNumber);
        self.CheckUploadName = ko.observable(_CheckUploadName);
        self.CheckDownloadPath = ko.observable(_CheckDownloadPath);
    }
    function resetPaymentRefundcontrol() {
        self.PaymentRefundID("");
        self.RefundAmount("");
        self.RefundReceived("");
        self.CheckNumber("");
        self.CheckUploadName("");
        $('#RefundReceived').val("");
        $('input').addClass('remove_bdr');
        $('.form-group').removeClass('has-error has-feedback');
        $('.form-group').find('.help-block').hide();
        $('.form-group').find('i.form-control-feedback').hide();
        $("#CheckUploadNameRefund").val("");
        $("#CheckUploadNameRefund").replaceWith($("#CheckUploadNameRefund").clone(true));
    }
    self.ViewPaymentRefundLineItems = function (model) {
        self.PaymentRefundID(model.PaymentRefundID());
        self.RefundAmount(model.RefundAmount());
        if (model.RefundReceived() != null)
            self.RefundReceived(moment(model.RefundReceived()).format("MM/DD/YYYY"));
        else
            self.RefundReceived(model.RefundReceived());

        self.CheckNumber(model.CheckNumber());
        self.CheckUploadName(model.CheckUploadName());
        self.CheckDownloadPath = (model.CheckDownloadPath);

    }


    var RfpagingSettings = {
        RfpageSize: 10,
        RfpageSlide: 2
    };
    self.RfSkip = ko.observable(0);
    self.RfTake = ko.observable(RfpagingSettings.RfpageSize);
    self.RfPager = ko.Rfpager(self.RfTotalItemCount);
    self.RfPager().RfPageSize(RfpagingSettings.RfpageSize);
    self.RfPager().RfPageSlide(RfpagingSettings.RfpageSlide);
    self.RfPager().RfCurrentPage(1);
    self.RfPager().RfCurrentPage.subscribe(function () {
        var skip = RfpagingSettings.RfpageSize * (self.RfPager().RfCurrentPage() - 1);
        var take = RfpagingSettings.RfpageSize;
        self.RfGetRecordsWithSkipTake(skip, take);
    });
    self.RfGetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.RfSkip(0);
            self.RfTake(RfpagingSettings.RfpageSize);
        }
        else {
            self.RfSkip(skip);
            self.RfTake(take);
        }

        InvoiceRebindandPaging();

    };
    //----------------------------Refund ends-----------------------------------//

    //-------- Paging for others ---------------------------//
    var pagingSettings = {
        pageSize: 10,
        pageSlide: 2
    };
    self.Skip = ko.observable(0);
    self.Take = ko.observable(pagingSettings.pageSize);
    self.Pager = ko.pager(self.TotalItemCount);
    self.Pager().PageSize(pagingSettings.pageSize);
    self.Pager().PageSlide(pagingSettings.pageSlide);
    self.Pager().CurrentPage(1);
    self.Pager().CurrentPage.subscribe(function () {
        var skip = pagingSettings.pageSize * (self.Pager().CurrentPage() - 1);
        var take = pagingSettings.pageSize;
        self.GetRecordsWithSkipTake(skip, take);
    });
    self.GetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.Skip(0);
            self.Take(pagingSettings.pageSize);
        }
        else {
            self.Skip(skip);
            self.Take(take);
        }
        if (popupOpened == "Adjuster") {
            $.ajax({
                url: "/File/getAdjusterByAdjusterNameSearch",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({
                    AdjusterName: $("#SearchAdjusterTitle").val(), skip: $('#hidskip').val()
                }),
                success: function (data) {
                    if (data != null) {
                        ko.mapping.fromJS(data.AdjusterSearch, {
                        }, self.AdjustersArrResult);
                        self.TotalItemCount(data.AdjusterCount);
                    }
                },
                error: function (data) {
                    alertify.alert("Data Not Found");
                }
            });
        }
        if (popupOpened == "Employer") {
            $.ajax({
                url: "/File/getEmployerByEmployerNameSearch",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({
                    EmployerName: $("#SearchEmployerTitle").val(), skip: $('#hidskipEmp').val()
                }),
                success: function (data) {
                    if (data != null) {
                        ko.mapping.fromJS(data.EmployerSearch, {
                        }, self.EmployerArrResult);
                        self.TotalItemCount(data.EmployerCount);
                    }
                    else {
                        alertify.alert("No data found");
                    }
                },
                error: function (data) {
                    alertify.alert("Error Occur");
                }
            });
        }
        if (popupOpened == "Insurer") {
            $.ajax({
                url: "/File/getInsurerByInsurerNameSearch",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({
                    InsurerName: $("#SearchInsurerTitle").val(), skip: $('#hidskipIns').val()
                }),
                success: function (data) {
                    if (data != null) {
                        ko.mapping.fromJS(data.InsurerSearch, {
                        }, self.InsurerArrResult);
                        self.TotalItemCount(data.InsurerCount);
                    }
                    else {
                        alertify.alert("No data found");
                    }
                },
                error: function (data) {
                    alertify.alert("Error Occur");
                }
            });
        }
        if (popupOpened == "Payment") {
            $.ajax({
                url: "/File/GetPaymentRecordByInvoiceId",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({
                    InvoiceId: self.InvoiceID(), Skip: $('#hidPaymentskip').val(), FileID: self.FileID()
                }),
                success: function (data) {
                    if (data != null) {
                        ko.mapping.fromJS(data.PaymentDetails, {}, self.PaymentDetails);
                        self.PaymentCount(data.PaymentCount);
                    }
                    else {
                        alertify.alert("No data found");
                    }
                },
                error: function (data) {
                    alertify.alert("Error Occur");
                }
            });
        }
        if (popupOpened == "Invoice") {
            $.ajax({
                url: "/File/InvoiceDetail",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({
                    FileID: self.FileID(), Skip: $('#hidInvoiceskip').val(), C: $('#hidClaimNumber').val(),
                }),
                success: function (data) {
                    if (data != null) {
                        ko.mapping.fromJS(data.InvoiceDetails, {}, self.InvoiceDetails);
                        self.InvoiceCount(data.InvoiceCount);
                    }
                    else {
                        alertify.alert("No data found");
                    }
                },
                error: function (data) {
                    alertify.alert("Error Occur");
                }
            });
        }
        if (popupOpened == "InsurerBranch") {
            $.ajax({
                url: "/File/GetAllInsurerBranch",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({
                     InsurerBranchName: $('#SearchInsurerBranchTitle').val(), skip: $('#hidskipInsBr').val()
                }),
                success: function (data) {
                    if (data != null) {
                        ko.mapping.fromJS(data.InsurerBranchSearch, {
                        }, self.InsurerBranchArrResult);
                        self.TotalItemCount(data.InsurerBranchCount);
                    }
                    else {
                        alertify.alert("No data found");
                    }
                },
                error: function (data) {
                    alertify.alert("Error Occur");
                }
            });
            $.ajax({
                url: "/File/getAdjusterByAdjusterNameSearch",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({
                    AdjusterName: $("#SearchAdjusterTitle").val(), skip: $('#hidskip').val()
                }),
                success: function (data) {
                    if (data != null) {
                        ko.mapping.fromJS(data.AdjusterSearch, {
                        }, self.AdjustersArrResult);
                        self.AdjusterCount(data.AdjusterCount);
                    }
                },
                error: function (data) {
                    alertify.alert("Data Not Found");
                }
            });
        }
    }; 
    //------------- Paging for others ends ---------------------------//

    $(document).ready(function () {
       // $.getJSON("/File/getDepartment", self.Departments);

        if ($('#DBNameInsurer').val() == "false") {
            $('#AddInsurerBranchModalPopUp').show();
        }
        else {
            //  $('#AddInsurerBranchModalPopUp').hide();
        }
        $('#SearchInsurerBranchTitle').show();
        $('#SearchInsurerBranch').show();

        if (self.DepartmentId() == 5) //in case of the IC department, hide some form details and disable department dropdown from edit
        {
            $("#ddlDepartmentID").attr("disabled", true);
            $("#nonICConditionGrid").hide();
            $("#invoiceAdjusterGrid").hide();
            $("#InvoiceDepartment").attr("disabled", true);

            fillFormForICDept();
        }
        else {
            $("#ICGrid").show();
        }

        $('#InvoiceAmount').blur(function () {
            if (parseFloat($('#hidInvoiceAmt').val()) != parseFloat($('#InvoiceAmount').val())) {
                $('#hidInvoiceBalanceAmt').val(parseFloat($('#InvoiceAmount').val()) - parseFloat($('#hidAdjustmentAmt').val()));
            }
        });
    });

    self.saveInvoiceNote = function (e) {
        var invoiceNote = {
            InvoiceNotesID: 0,
            InvoiceID: self.InvoiceID(),
            InvoiceNotes: self.InvoiceNote(),
            UserID: $('#UserID').val()
        }
        var plainJs = ko.toJS(invoiceNote);

        if (invoiceNote.InvoiceNotes != "") {
            $.ajax({
                url: "/File/saveInvoiceNote",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: JSON.stringify(plainJs),
                success: function (data) {
                    if (data != null) {
                        InvoiceNoteBinding(0);
                        self.PagerNotes().CurrentPageNotes(1);
                    }
                    else {
                        alertify.alert("No data found");
                    }
                },
                error: function (data) {
                    alertify.alert("Error Occur");
                }
            });
        }
        else {
            alertify.alert("Enter Invoice Notes");
        }
    }

    function InvoiceNoteBinding(skipValue) {
        $.ajax({
            url: "/File/GetInvoiceNotesByInvoiceID",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: ko.toJSON({
                InvoiceID: self.InvoiceID(), skip: skipValue
            }),
            success: function (data) {
                if (data != null) {
                    ko.mapping.fromJS(data.InvoiceNoteDetails, {}, self.InvoiceNoteDetails);
                    self.TotalItemCountNotes(data.TotalCount);
                    self.InvoiceNote('');
                }
                else {
                    alertify.alert("No data found");
                }
            },
            error: function (data) {
                alertify.alert("Error Occur");
            }
        });
    }


    //  --------------------------------------------------

    //-------- Paging for Notes ---------------------------//
    var pagingSettingsNotes = {
        pageSize: 5,
        pageSlide: 2
    };
    self.SkipNotes = ko.observable(0);
    self.TakeNotes = ko.observable(pagingSettings.pageSize);
    self.PagerNotes = ko.pagerNotes(self.TotalItemCountNotes);
    self.PagerNotes().PageSizeNotes(pagingSettingsNotes.pageSize);
    self.PagerNotes().PageSlideNotes(pagingSettingsNotes.pageSlide);
    self.PagerNotes().CurrentPageNotes(1);
    self.PagerNotes().CurrentPageNotes.subscribe(function () {
        var skipNotes = pagingSettingsNotes.pageSize * (self.PagerNotes().CurrentPageNotes() - 1);
        var takeNotes = pagingSettingsNotes.pageSize;
        self.GetRecordsWithSkipTakeNotes(skipNotes, takeNotes);
    });
    self.GetRecordsWithSkipTakeNotes = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.SkipNotes(0);
            self.TakeNotes(pagingSettingsNotes.pageSize);
        }
        else {
            self.SkipNotes(skip);
            self.TakeNotes(take);
        }
       
        InvoiceNoteBinding(self.SkipNotes());
    };
    //------------- Paging for Notes ends ---------------------------//

};
$('#CheckUploadName').change(function () {
    var fileExtension = ['pdf'];
    if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
        alertify.alert("Only PDF formats are allowed.");
        $("#CheckUploadName").val("");
        $("#CheckUploadName").replaceWith($("#CheckUploadName").clone(true));
    }
});
//========== pager js for invoice grid only ========//
(function (ko) {
    var inumericObservable = function (initialValue) {
        var _iactual = ko.observable(initialValue);
        var iresult = ko.dependentObservable({
            read: function () {
                return _iactual();
            },
            write: function (newValue) {
                var iparsedValue = parseFloat(newValue);
                _iactual(isNaN(iparsedValue) ? newValue : iparsedValue);
            }
        });
        return iresult;
    };

    function iPager(totalItemCount) {
        var self = this;
        self.iCurrentPage = inumericObservable(1);
        self.iTotalItemCount = ko.computed(totalItemCount);

        self.iPageSize = inumericObservable(10);
        self.iPageSlide = inumericObservable(2);

        self.iLastPage = ko.computed(function () {
            return Math.floor((self.iTotalItemCount() - 1) / self.iPageSize()) + 1;
        });

        self.iHasNextPage = ko.computed(function () {
            return self.iCurrentPage() < self.iLastPage();
        });

        self.iHasPrevPage = ko.computed(function () {
            return self.iCurrentPage() > 1;
        });

        self.iFirstItemIndex = ko.computed(function () {
            return self.iPageSize() * (self.iCurrentPage() - 1) + 1;
        });

        self.iLastItemIndex = ko.computed(function () {
            return Math.min(self.iFirstItemIndex() + self.iPageSize() - 1, self.iTotalItemCount());
        });

        self.iPages = ko.computed(function () {
            var ipageCount = self.iLastPage();
            var ipageFrom = Math.max(1, self.iCurrentPage() - self.iPageSlide());
            var ipageTo = Math.min(ipageCount, self.iCurrentPage() + self.iPageSlide());
            ipageFrom = Math.max(1, Math.min(ipageTo - 2 * self.iPageSlide(), ipageFrom));
            ipageTo = Math.min(ipageCount, Math.max(ipageFrom + 2 * self.iPageSlide(), ipageTo));

            var iresult = [];
            for (var i = ipageFrom; i <= ipageTo; i++) {
                iresult.push(i);
            }
            return iresult;
        });
    }

    ko.ipager = function (totalItemCount) {
        var ipager = new iPager(totalItemCount);
        return ko.observable(ipager);
    };
}(ko));
//============= pager js for invoice ends  ============//

//========== pager js for Plead body part grid only ========//
(function (ko) {
    var numericObservableNotes = function (initialValue) {
        var _actual = ko.observable(initialValue);

        var result = ko.dependentObservable({
            read: function () {
                return _actual();
            },
            write: function (newValue) {
                var parsedValue = parseFloat(newValue);
                _actual(isNaN(parsedValue) ? newValue : parsedValue);
            }
        });

        return result;
    };

    function PagerNotes(totalItemCountNotes) {
        var self = this;
        self.CurrentPageNotes = numericObservableNotes(1);

        self.TotalItemCountNotes = ko.computed(totalItemCountNotes);

        self.PageSizeNotes = numericObservableNotes(10);
        self.PageSlideNotes = numericObservableNotes(2);

        self.LastPageNotes = ko.computed(function () {
            return Math.floor((self.TotalItemCountNotes() - 1) / self.PageSizeNotes()) + 1;
        });

        self.HasNextPageNotes = ko.computed(function () {
            return self.CurrentPageNotes() < self.LastPageNotes();
        });

        self.HasPrevPageNotes = ko.computed(function () {
            return self.CurrentPageNotes() > 1;
        });

        self.FirstItemIndexNotes = ko.computed(function () {
            return self.PageSizeNotes() * (self.CurrentPageNotes() - 1) + 1;
        });

        self.LastItemIndexNotes = ko.computed(function () {
            return Math.min(self.FirstItemIndexNotes() + self.PageSizeNotes() - 1, self.TotalItemCountNotes());
        });

        self.PagesNotes = ko.computed(function () {
            var pageCountNotes = self.LastPageNotes();
            var pageFromNotes = Math.max(1, self.CurrentPageNotes() - self.PageSlideNotes());
            var pageToNotes = Math.min(pageCountNotes, self.CurrentPageNotes() + self.PageSlideNotes());
            pageFromNotes = Math.max(1, Math.min(pageToNotes - 2 * self.PageSlideNotes(), pageFromNotes));
            pageToNotes = Math.min(pageCountNotes, Math.max(pageFromNotes + 2 * self.PageSlideNotes(), pageToNotes));

            var result = [];
            for (var i = pageFromNotes; i <= pageToNotes; i++) {
                result.push(i);
            }
            return result;
        });
    }

    ko.pagerNotes = function (totalItemCountNotes) {
        var pagerNotes = new PagerNotes(totalItemCountNotes);
        return ko.observable(pagerNotes);
    };
}(ko));
//============= pager js for Plead body part ends  ============//

//========== pager js for   Refund  grid only ========//
(function (ko) {
    var RfnumericObservable = function (initialValue) {
        var _actual = ko.observable(initialValue);

        var result = ko.dependentObservable({
            read: function () {
                return _actual();
            },
            write: function (newValue) {
                var parsedValue = parseFloat(newValue);
                _actual(isNaN(parsedValue) ? newValue : parsedValue);
            }
        });

        return result;
    };

    function RfPager(totalItemCount) {
        var self = this;
        self.RfCurrentPage = RfnumericObservable(1);

        self.RfTotalItemCount = ko.computed(totalItemCount);

        self.RfPageSize = RfnumericObservable(10);
        self.RfPageSlide = RfnumericObservable(2);

        self.RfLastPage = ko.computed(function () {
            return Math.floor((self.RfTotalItemCount() - 1) / self.RfPageSize()) + 1;
        });

        self.RfHasNextPage = ko.computed(function () {
            return self.RfCurrentPage() < self.RfLastPage();
        });

        self.RfHasPrevPage = ko.computed(function () {
            return self.RfCurrentPage() > 1;
        });

        self.RfFirstItemIndex = ko.computed(function () {
            return self.RfPageSize() * (self.RfCurrentPage() - 1) + 1;
        });

        self.RfLastItemIndex = ko.computed(function () {
            return Math.min(self.RfFirstItemIndex() + self.RfPageSize() - 1, self.RfTotalItemCount());
        });

        self.RfPages = ko.computed(function () {
            var RfpageCount = self.RfLastPage();
            var RfpageFrom = Math.max(1, self.RfCurrentPage() - self.RfPageSlide());
            var RfpageTo = Math.min(RfpageCount, self.RfCurrentPage() + self.RfPageSlide());
            RfpageFrom = Math.max(1, Math.min(RfpageTo - 2 * self.RfPageSlide(), RfpageFrom));
            RfpageTo = Math.min(RfpageCount, Math.max(RfpageFrom + 2 * self.RfPageSlide(), RfpageTo));

            var result = [];
            for (var i = RfpageFrom; i <= RfpageTo; i++) {
                result.push(i);
            }
            return result;
        });
    }

    ko.Rfpager = function (totalItemCount) {
        var Rfpager = new RfPager(totalItemCount);
        return ko.observable(Rfpager);
    };
}(ko));
//============= pager js for Refund ends  ============//

$("#InvoiceAmount").change(function () {
    $("#InvoiceBalanceAmt").val(parseFloat($("#InvoiceAmount").val()).toFixed(2));
});
