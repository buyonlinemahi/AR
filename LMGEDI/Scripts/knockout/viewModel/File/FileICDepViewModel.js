function FileICDepViewModel() {
    var self = this;
    var _invICdelAmt = "";
    var index = 0;

    self.FileID = ko.observable();
    self.InvoiceID = ko.observable();
    self.FileFullName = ko.observable();
    self.ClaimNumber = ko.observable();
    self.InvoiceNumber = ko.observable();
    self.InvoiceAmt = ko.observable();
    self.InvoiceICAmt = ko.observable();
    self.DepartmentId = ko.observable();
    self.IsChecked = ko.observable();

    self.Departments = ko.observableArray();
    self.selectedDepartment = ko.observable();
    self.selectedDepartmentFile = ko.observable();

    self.InvoiceWithICDetails = ko.observableArray();
    self.InvoiceWithoutICDetails = ko.observableArray();
    self.InvAddICDetails = ko.observableArray();


    this.Init = function (model) {
        if (model != null) {
            $.each(model.InvoiceICDetails, function (index, value) {
                if (value.InvoiceICAmt == null) {
                    self.InvoiceWithoutICDetails.push(new WithoutICAmtGrid(value));
                }
                else {
                    self.InvoiceWithICDetails.push(new WithICAmtGrid(value));
                }

            });
        }
    }
    $(document).ready(function () {
        $.getJSON("/File/getDepartment?isIC=0", function (data) {
            self.Departments(data.slice());
            self.selectedDepartment(5);
        });

    });



    function isNumberKeydecimal(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
            return false;
        else {
            var len = $(evt).val().length;
            var index = $(evt).val().indexOf('.');
            if (index > 0 && charCode == 46) {
                return false;
            }
            if (index > 0) {
                var CharAfterdot = (len + 1) - index;
                if (CharAfterdot > 3) {
                    return false;
                }
            }
        }
        return true;
    }

    function WithICAmtGrid(value) {
        var self = this;
        self.FileID = ko.observable(value.FileID);
        self.InvoiceID = ko.observable(value.InvoiceID);
        self.FileFullName = ko.observable(value.FileFullName);
        self.ClaimNumber = ko.observable(value.ClaimNumber);
        self.InvoiceNumber = ko.observable(value.InvoiceNumber);
        self.InvoiceAmt = ko.observable(value.InvoiceAmt);
        self.InvoiceICAmt = ko.observable(value.InvoiceICAmt);
        self.DepartmentId = ko.observable(value.DepartmentId);
    }
    function WithoutICAmtGrid(value) {
        var self = this;
        self.FileID = ko.observable(value.FileID);
        self.InvoiceID = ko.observable(value.InvoiceID);
        self.FileFullName = ko.observable(value.FileFullName);
        self.ClaimNumber = ko.observable(value.ClaimNumber);
        self.InvoiceNumber = ko.observable(value.InvoiceNumber);
        self.InvoiceAmt = ko.observable(value.InvoiceAmt);
        self.InvoiceICAmt = ko.observable();
        self.DepartmentId = ko.observable(value.DepartmentId);
        self.IsChecked = ko.observable(value.IsChecked);
    }


    self.AddInvoiceSuccess = function (modelinvo) {
        var modelinvo = $.parseJSON(modelinvo);
        if (modelinvo != "0") {
            if (self.InvoiceWithICDetails().length > 0) {
                var invoiceIDs = ko.utils.arrayMap(self.InvoiceWithICDetails(), function (item) { return item.InvoiceID })
                var subInvoiceIDs = ko.utils.arrayGetDistinctValues(invoiceIDs).sort();

                $.ajax({
                    url: "/File/UpdateAssignedToInvoiceID/",
                    cache: false,
                    type: "POST", dataType: 'json',
                    contentType: 'application/json',
                    data: ko.toJSON({ InvoiceID: modelinvo, SubInvoiceID: subInvoiceIDs }),
                    success: function (model) {
                        self.InvoiceWithICDetails.removeAll();
                        alertify.success("Saved Successfully");
                        $("#InvoiceNumber").val("");
                        $("#InvoiceAmount").val("");
                        $("#InvoiceDate").val("");
                        $("#InvoiceSent").val("");
                        $("#BillingWeek").val("");
                        $("#AdjustmentAmt").val("");
                        $("#InvoiceBalanceAmt").val("");
                        $("#AdjustmentNotes").val("");
                        var control = $('#UploadFile');
                        control.replaceWith(control.val('').clone(true));

                        control.on({
                            change: function () { console.log("Changed") },
                            focus: function () { console.log("Focus") }
                        });
                    },
                    error: function () {
                        alertify.alert("Error");
                    }

                });                
            }
            else {
                alertify.success("Saved Successfully");
                $("#InvoiceNumber").val("");
                $("#InvoiceAmount").val("");
                $("#InvoiceDate").val("");
                $("#InvoiceSent").val("");
                $("#BillingWeek").val("");
                $("#AdjustmentAmt").val("");
                $("#InvoiceBalanceAmt").val("");
                $("#AdjustmentNotes").val("");
                var control = $('#UploadFile');
                control.replaceWith(control.val('').clone(true));

                control.on({
                    change: function () { console.log("Changed") },
                    focus: function () { console.log("Focus") }
                });
            }

        }
        else {
            alertify.alert("Invoice Number Already Exist");
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
    }


    self.UpdateICAmountInvoice = function (model) {
        var _invICAddAmount = "";
        var invoiceLength = self.InvoiceWithoutICDetails().length;
        for (var i = 0; i < invoiceLength; i++) {

            var _FileID = self.InvoiceWithoutICDetails()[i].FileID._latestValue;
            var _InvoiceID = self.InvoiceWithoutICDetails()[i].InvoiceID._latestValue;
            var _InvoiceICAmt = self.InvoiceWithoutICDetails()[i].InvoiceICAmt._latestValue;
            var isChecked = self.InvoiceWithoutICDetails()[i].IsChecked._latestValue;
            var regexp = /^\d+(?:\.\d\d?)?$/;
            var _invoiceamountCheckTest = regexp.test(_InvoiceICAmt);

            if (isChecked == true && _invoiceamountCheckTest == true) {
                _invICAddAmount += _FileID + "-" + _InvoiceID + "-" + _InvoiceICAmt + ",";
            }
        }
        var _fileID = parseInt($("#hidInvoiceFileID").val());
        if (_invICAddAmount != "") {
            $.ajax({
                url: "/File/UpdateInvoiceICByFileID",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({
                    FileInvoiceList: _invICAddAmount, fileID: _fileID
                }),
                success: function (model) {
                    if (model != null) {
                        self.InvoiceWithoutICDetails.removeAll();
                        self.InvoiceWithICDetails.removeAll();
                        $.each(model.InvoiceICDetails, function (index, value) {
                            if (value.InvoiceICAmt == null) {
                                self.InvoiceWithoutICDetails.push(new WithoutICAmtGrid(value));
                            }
                            else {
                                self.InvoiceWithICDetails.push(new WithICAmtGrid(value));
                            }

                        });
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
            alertify.alert("Please fill the IC Amount and checked the checkbox");
        }
    }


};