function AssignCheckSearchFileViewModel() {
    var self = this;

    self.SearchFileResult = ko.observableArray();
    self.FileAssignedGridResult = ko.observableArray();
    self.OCRpaymentDetails = ko.observableArray();
    self.iTotalItemCount = ko.observable();
    self.hidsearch = ko.observable();
    self.ClaimNumber = ko.observable();
    self.InvoiceNumber = ko.observable();
    self.FilesName = ko.observable();
    //-----------OCR Save --------------//
    self.FileID = ko.observable();
    self.OcrId = ko.observable();
    self.CreatedBy = ko.observable();
    self.InvoiceID = ko.observable();
    self.PaymentId = ko.observable();    
    self.PaymentAmount = ko.observable().extend({ numeric: 2 });
    self.PaymentReceived = ko.observable();
    self.CheckNumber = ko.observable();
    self.CheckDate = ko.observable();
    self.OcrFilePath = ko.observable();
    self.OcrFileName = ko.observable();
    self.CheckUploadName = ko.observable();
    self.OutstandingBalance = ko.observable();
    self.InvoiceDate = ko.observable();
    self.TotalPaymentAmount = ko.observable(0);

    self.Init = function (model) {
        
        if (model != null) {            
            ko.mapping.fromJS(model, {}, self);            
            $('#divPDFViewer').html('<object data="' + self.OcrFilePath() + '" id="album" type="text/html" width="100%" height="400"><param name="wmode" value="transparent" /></object>');            
        };
    };

    self.closeSearchPopUp = function () {
        self.SearchFileResult.removeAll();
        $('#searchTextFilePopUpOCR').val('');
        self.iTotalItemCount(0);
    };


    $(document).ready(function () {        
        $("#aPaymentAmount").on("keyup", function () {
            var valid = /^\d{0,10}(\.\d{0,2})?$/.test(this.value),
                val = this.value;

            if (!valid) {
                this.value = val.substring(val.length - 1, val.length - 1);
            }
        });

        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0!

        var yyyy = today.getFullYear();
        if (dd < 10) {
            dd = '0' + dd;
        }
        if (mm < 10) {
            mm = '0' + mm;
        }
        var today = mm + '/' + dd + '/' + yyyy;
     
           
            

        $('#aPaymentReceived').val(today);
    
        $('#aPaymentReceived').keyup(function () {           
            var valid1 = /^[0,1]?\d{1}\/(([0-2]?\d{1})|([3][0,1]{1}))\/(([1]{1}[9]{1}[9]{1}\d{1})|([2-9]{1}\d{3}))$/.test(this.value),
                 val = this.value;

            if (!valid1) {
                this.value = val.substring(0, val.length - 1);
            }
        });
       
        $('#aCheckNumber').keyup(function () {
            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
            }
        });
    });

    var mappingOptions = {
        'InvoiceDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM/DD/YYYY");
            }
        }
    }

    self.SearchFilePopUp = function () {
        self.SearchFileResult.removeAll();
        self.iTotalItemCount(0);
        var _inputData = $('#searchTextFilePopUpOCR').val();
        if (_inputData != '') {
            self.hidsearch(_inputData);
            $.ajax({
                url : "/CheckAssignment/SearchFileAssignCheck",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({ searchFileName: $('#hidsearch').val() }),
                success: function (dataSearch) {
                    if (dataSearch != null) {
                        ko.mapping.fromJS(dataSearch.OCRFileSearchResult, mappingOptions, self.SearchFileResult);
                        var datamodel = self.SearchFileResult;
                        if (datamodel().length > 0) {
                            self.iTotalItemCount(dataSearch.OCRFileCount);
                            self.iPager().iCurrentPage(1);
                        }
                        else {
                            self.iTotalItemCount(0);
                        }
                    }
                    else if (dataSearch == "Error occur") {
                        alertify.alert("Error Occur");
                    }                  
                },
                error: function (data) {
                    alertify.alert("Data Not Found");
                }
            });

        }
        else {
            alertify.alert("File name required");
        }
    };

    self.AddAssignedNewFile = function (assignData) {  
        var validate = 0;
        var _claim = $('#aClaimNumber').val();
        var _fileid = $('#aFileID').val();
        var _fileName = $('#aFilesName').val();
        var _invoiceid = $('#aInvoiceID').val();
        var _invoiceNum = $('#aInvoiceNumber').val();
        var _paymentAmt = $('#aPaymentAmount').val();
        var _paymentRecivd = $('#aPaymentReceived').val();
        var _checkNumb = $('#aCheckNumber').val();
        var _checkDate = $('#aCheckDate').val();
        var _OutstandingBalance = $('#aOutstandingBalance').val();


        _OutstandingBalance = _OutstandingBalance.replace("$", "");

        if (_claim == '')
        {
            validate = 1;
            alertifyAlertOverPDF("Claim number required go to search and select invoice");
        }
        else if (_fileid == '' && validate == 0) {
            validate = 1;
            alertifyAlertOverPDF("File  required go to search and select invoice");
        }
        else if (_fileName == '' && validate == 0) {
            validate = 1;
            alertifyAlertOverPDF("File name required go to search and select invoice");
        }
        else if (_invoiceid == '' && validate == 0) {
            validate = 1;
            alertifyAlertOverPDF("Invoice required go to search and select invoice");
        }
        else if (_invoiceNum == '' && validate == 0) {
            validate = 1;
            alertifyAlertOverPDF("Invoice number required go to search and select invoice");
        }
        else if (_paymentAmt == ''  && validate == 0) {
            validate = 1;
            alertifyAlertOverPDF("Payment amount required");
        }
        else if (_paymentAmt == '' && validate == 0) {
            alertifyAlertOverPDF("Payment amount required");
        }
        else if (parseFloat(_paymentAmt) == 0 && validate == 0) {
            alertifyAlertOverPDF("Payment amount can't be zero.");
            validate = 1;
        }
        else if (_paymentRecivd == '' && validate == 0) {
            validate = 1;
            alertifyAlertOverPDF("Date required");
        }
        else if (_checkNumb == '' && validate == 0) {
            validate = 1;
            alertifyAlertOverPDF("Check no. required");
            
        }
        else if (_checkDate == '' && validate == 0) {
            validate = 1;
            alertifyAlertOverPDF("Check date required");
            
        }

        var _TotalPaymentAmount = 0;
        var checkamt = 0;
        var amt = 0;
        var validCheckNo = 0;
        var viewModel = self.FileAssignedGridResult;
        for (var i = 0; i <= viewModel().length - 1; i++) {
            if (viewModel()[i].ClaimNumber() == _claim && viewModel()[i].InvoiceID() == _invoiceid) {
                amt += parseFloat(viewModel()[i].PaymentAmount());
               
            }
            if (viewModel()[i].CheckNumber() != _checkNumb) {
                validCheckNo = 1;

            }
           
        }         
    
        checkamt = parseFloat(_paymentAmt) + parseFloat(amt);

        if (validate == 0) {
            if (validCheckNo == 0) {
                $.ajax({
                    url: "/CheckAssignment/CheckOCRPaymentDetails",
                    cache: false,
                    type: "POST", dataType: 'json',
                    contentType: 'application/json',
                    data: ko.toJSON({ InvoiceID: _invoiceid, PaymentAmount: checkamt }),
                    success: function (result) {
                        if (result == 0) {
                            alertifyAlertOverPDF("Full payment recevied against Invoice");
                        }
                        else if (result == -1) {
                            alertifyAlertOverPDF("Amount can't be greater then invoice amount");
                        }
                        else if (result == 1) {
                            self.FileAssignedGridResult.push(new InsertFileAssignedGrid(_fileid, _fileName, _claim, self.OcrId(), self.CreatedBy(), _invoiceid, 0, _paymentAmt, _paymentRecivd, _checkNumb, _checkDate, _invoiceNum, _OutstandingBalance, self.OcrFileName()));
                            ResetAssignCheck();                            
                          
                            for (var i = 0; i < viewModel().length ; i++) {
                                _TotalPaymentAmount += parseFloat(viewModel()[i].PaymentAmount());
                            }
                            self.TotalPaymentAmount(_TotalPaymentAmount);
                        }
                        else if (result == "Error occur") {
                            alertifyAlertOverPDF("Error Occur");
                        }

                    },
                    error: function (data) {
                        alertifyAlertOverPDF("Data Not Found");
                    }
                });
            }
            else {
                $("#album").addClass('hidden_searchfilediv');
                alertify.error("Check no should be same", function () {
                    $("#album").removeClass('hidden_searchfilediv')
                });
            }

          
        }
        
    };

   
    self.SaveFileAssignedGrid = function () {
        var viewModel = self.FileAssignedGridResult;
        if (viewModel != null) {
            $.ajax({
                url: "/CheckAssignment/AddOCRPaymentRecords",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({ objOCRPaymentSave: self.FileAssignedGridResult }),
                success: function (result) {
                    if (result == "Error occur") {
                        alertifyAlertOverPDF("Error Occur");
                    }
                    else if (result == 1) {
                        $("#album").addClass('hidden_searchfilediv'); 
                        alertify.confirm("Save successfully.", function (e) {
                            if (e) {                                
                                window.location = "/CheckAssignment";
                            }
                        });                       
                    }
                    else if (result == 2)
                    {   
                        alertifyAlertOverPDF("No File to Save");
                    }
                },
                error: function (data) {
                    alertifyAlertOverPDF("Data Not Found");
                }
            });
        };
    };

    function ResetAssignCheck() {
        self.FileID(0);
        self.OcrId(0);
        self.CreatedBy(0);
        self.InvoiceID(0);
        self.PaymentId(0);
        self.PaymentAmount('');
        self.PaymentReceived('');
        self.CheckNumber('');
        self.CheckUploadName('');
        $('#aClaimNumber').val('');
        $('#aFileID').val(0);
        $('#aFilesName').val('');
        $('#aInvoiceID').val(0);
        $('#aInvoiceNumber').val('');
        $('#aPaymentAmount').val('');
        $('#aOutstandingBalance').val('');
    };
    
    self.SelectInvoiceNumberInSearchFile = function (data) {
      
        $('#aClaimNumber').val(data.ClaimNumber());
        $('#aFileID').val(data.FileID());
        $('#aFilesName').val(data.FilesName());
        $('#aInvoiceID').val(data.InvoiceID());
        $('#aInvoiceNumber').val(data.InvoiceNumber());
        var _balanceOutsanding = '$' + data.OutstandingBalance();
        $('#aOutstandingBalance').val(_balanceOutsanding);
        $('#CloseSearchFileModelPopUp').click();
        
    };
    //========= paging code for Search File grid only ===========//
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

        $.ajax({
            url: "/CheckAssignment/SearchFileAssignCheckSearch",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: ko.toJSON({ SearchName: $('#hidsearch').val(), skip: $('#hidSearchskip').val() }),
            success: function (dataSearch) {

                if (dataSearch != null) {
                    ko.mapping.fromJS(dataSearch.OCRFileSearchResult, mappingOptions, self.SearchFileResult);
                    self.iTotalItemCount(dataSearch.OCRFileCount);
                }
                else if (dataSearch == "Error occur") {                   
                    alertifyAlertOverPDF("Error Occur");
                }
            },
            error: function (data) {
                alertifyAlertOverPDF("Data Not Found");
            }
        });

    };
    // ============= paging code for Search File ends =============== //

    //---------------------------- Search File ends -----------------------------------//
};

//========== pager js for Search File grid only ========//
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
//============= pager js for Search File ends  ============//

// To hide object tag so that popup appears above it and then show it after alert goes . . . TG
function alertifyAlertOverPDF(msg)
{
    $("#album").addClass('hidden_searchfilediv');
    alertify.alert(msg, function () {
        $("#album").removeClass('hidden_searchfilediv');
    });
}
function InsertFileAssignedGrid(_fileID, _fileName, _claim, _ocrID, _createdBY, _invoiceID, _paymentID, _paymentAmount, _paymentReceived, _checkNumber, _checkDate, _invoiceNumb, _OutstandingBalance, _checkUploadName) {
    var self = this;
    self.FileID = ko.observable(_fileID);
    self.FilesName = ko.observable(_fileName);
    self.ClaimNumber = ko.observable(_claim);
    self.OcrId = ko.observable(_ocrID);
    self.CreatedBy = ko.observable(_createdBY);
    self.InvoiceID = ko.observable(_invoiceID);
    self.PaymentId = ko.observable(_paymentID);
    self.PaymentAmount = ko.observable(_paymentAmount);
    self.PaymentReceived = ko.observable(moment(_paymentReceived).format("MM/DD/YYYY"));
    self.CheckNumber = ko.observable(_checkNumber);
    self.CheckDate = ko.observable(moment(_checkDate).format("MM/DD/YYYY"));
    self.InvoiceNumber = ko.observable(_invoiceNumb);
    self.OutstandingBalance = ko.observable(_OutstandingBalance);
    self.CheckUploadName = ko.observable(_checkUploadName);
};

$("#searchFile").click(function(){
//    $("#album").addClass('hidden_searchfilediv');    
    //$(".modal-backdrop").css('opacity', '0.1 !important');
} );

$("#CloseSearchFileModelPopUp").click(function () {
    $("#album").removeClass('hidden_searchfilediv');
});
 