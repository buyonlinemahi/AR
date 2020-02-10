function ClientCommissionViewModel(model)
{
    var self = this;
    self.ClientType = ko.observable();
    self.ClientTypes = ko.observableArray();
    self.selectedClient = ko.observable();
    self.CommissionID   = ko.observable();
    self.LienCommissionID  = ko.observable();
    self.LienCName  = ko.observable();
    self.LienCClientID  = ko.observable();
    self.LienCStartDate  = ko.observable();
    self.LienCEndDate  = ko.observable();
    self.LienCPrecentage  = ko.observable();
    self.IsPaymentRecevied  = ko.observable();
    self.CAmountDue = ko.observable().extend({ numeric: 2 });
    self.CDueDate  = ko.observable();
    self.CPPaymentPaidAmount  = ko.observable();
    self.CPCheckNumber  = ko.observable();
    self.CPCheckSentOn = ko.observable();
    self.CommissionDetailsResult = ko.observable();
    self.TotalItemCount = ko.observable(0);
    self.CommissionPaymentID=ko.observable();
    self.PTotalItemCount = ko.observable(0);
    self.CTotalInvoicedAmount = ko.observable().extend({ numeric: 2 });
    self.CommissionPaymentDetailsResult = ko.observable();
    self.InvoiceDateQuater = ko.observable();
   
    /*get client list*/
    var mappingOptions = {
        'CDueDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM/DD/YYYY");
            }
        },
        'LienCStartDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM/DD/YYYY");
            }
        },
        'LienCEndDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM/DD/YYYY");
            }
        }
    }
 
    var url = window.location.href;
    if (url.substring(url.lastIndexOf('=') + 1)) {
        if (url.substring(url.lastIndexOf('/') + 1) == 'ClientCommission') {
            $("#hfdLientID").val("");
        }
        else {
            var id = url.substring(url.lastIndexOf('=') + 1);
            $("#hfdLientID").val(id);
        }
    }
    
    $.getJSON("/Commission/getAllLienClientBilling", function (_data) {
        self.ClientTypes(_data.slice());
        var clientIdd = $("#hfdLientID").val();
        if (clientIdd != "") {
            self.ClientType(clientIdd);
            $('#ClientID').prop('disabled', true);
        }
        else {
            $('#ClientID').prop('disabled', false);
        }
    });
    self.TypeChanged = function (item) {
        var r = item.ClientType();
        $("#hdfClientid").val(r);      
        self.getClientDetails(0);    
    }
    self.getClientDetails = function (skip) {
        $.post("/Commission/getCommissionRecordByLienClientID", {
            _lienClientID: $("#hdfClientid").val(), _skip: $("#hidskip").val()
        },
     function (_dataModel) {
         var model = $.parseJSON(_dataModel);
         ko.mapping.fromJS(model.CommissionDetails, mappingOptions, self.CommissionDetailsResult);
         self.TotalItemCount(model.TotalCount);
        
         var q = new Date();
         var year = q.getFullYear();       
         var quaterList = [1, 2, 3, 4];
         var quaterNumber = quaterList[Math.floor(q.getMonth() / 3)];       
         var _commissionDetails = self.CommissionDetailsResult().length;
         for (var i = 0; i < _commissionDetails; i++) {         
             if (self.CommissionDetailsResult()[i].InvoiceDateQuater() >= quaterNumber || self.CommissionDetailsResult()[i].CDueYear() > year) {
                $("#ClienPaymentPopUp"+[i]).addClass('disable-link');                
             }            
         }
     });
        
    }   

    /**commission Payment popup***/
    self.SaveClientPaymentDetailsByID = function (model)
    {   
        $("#myModalCommissionPayment").modal('show');
        $("#amountDue").val("$" + model.CAmountDue().toFixed(2));
        $("#hdfcommissionId").val(model.CommissionID());
        self.IsPaymentRecevied(model.IsPaymentRecevied());
        self.InvoiceDateQuater(model.InvoiceDateQuater());
            $.post("/Commission/getAllCommissionPaymentByCommissionID", {
                _commissionID: $("#hdfcommissionId").val(), _quaterID: self.InvoiceDateQuater(), _skip: $("#hidskip").val()
            },
         function (_dataModel) {
             var _model = $.parseJSON(_dataModel);
             ko.mapping.fromJS(_model.CommissionPaymentDetails, mappingOptions, self.CommissionPaymentDetailsResult);
             self.PTotalItemCount(_model.TotalCount);             
             if (self.PTotalItemCount() > 0) {
                 $("#CommissionPaymentID").val(_model.CommissionPaymentDetails[0].CommissionPaymentID);
                 var hh = model.IsPaymentRecevied();
                 if (hh == true) {
                     $("#paymentAmount").val("$" + _model.CommissionPaymentDetails[0].CPPaymentPaidAmount);
                     $('#paymentAmount').prop('disabled', true);
                     $("#checknumber").val(_model.CommissionPaymentDetails[0].CPCheckNumber);
                     $('#checknumber').prop('disabled', true);
                     $("#checksent").val(moment(_model.CommissionPaymentDetails[0].CPCheckSentOn).format("MM/DD/YYYY"));
                     $('#checksent').prop('disabled', true);
                     $('#btnsave').prop('disabled', true);
                 }
             }
             else {
                 $("#CommissionPaymentID").val(0);
                 $('#paymentAmount').prop('disabled', false);
                 $('#checknumber').prop('disabled', false);
                 $('#checksent').prop('disabled', false);
                 $('#btnsave').prop('disabled', false);
             }
         });
           
    }
    self.SavePaymentDetail = function () {

        if ($("#paymentAmount").val() == "")
        {
            alertify.alert("please enter payment amount");
        }
        else if ($("#checknumber").val()=="")
        {
            alertify.alert("please enter check number");
        }
        else if ($("#checksent").val() == "") {
            alertify.alert("please enter check sent");
        }
        else {
            var _paymentObj = {
                CommissionPaymentID: $('#CommissionPaymentID').val(),//self.CommissionPaymentID(),//$('#CommissionPaymentID').val(),
                CommissionID: $("#hdfcommissionId").val(),
                CPPaymentPaidAmount: $("#paymentAmount").val(),
                CPCheckNumber: $("#checknumber").val(),
                CPCheckSentOn: $("#checksent").val(),
                InvoiceDateQuarter: self.InvoiceDateQuater(),//$("#InvoiceDateQuarter").val()
                IsPaymentRecevied: 1
            }
            
            var matchPrice = $("#amountDue").val();
            var price = matchPrice.replace('$', '');
            var correctAmount = parseFloat(price);            
            var payment = $("#paymentAmount").val();            
            if (payment == correctAmount) {
                $.ajax({
                    url: "/Commission/SaveEmployerRecord",
                    type: 'post',
                    dataType: 'json',
                    contentType: 'application/json',
                    data: JSON.stringify({ _commissionPayment: _paymentObj }),
                    cache: false,
                    success: function (_model) {
                        if (_model != "Error") {
                            alertify.alert("Save Successfully");
                            self.getClientDetails();
                        }
                        else {
                            alertify.alert(_model);
                        }
                    }
                });
                $("#paymentAmount").val("");
                $("#checknumber").val("");
                $("#checksent").val("");
                $("#myModalCommissionPayment").modal('hide');
            }
            else {
                alertify.alert("Payment amount should be same as Due amount");
            }
           
        }
    }
    self.CommissionCancel = function ()
    {
        $("#paymentAmount").val("");
        $("#checknumber").val("");
        $("#checksent").val("");
        $("#closeAddCommissionModel").modal('hide');
    }
    var dates = $("#checksent").datepicker({
      //  minDate: "0",
        maxDate: "+2Y",       
        dateFormat: 'mm/dd/yy',
        numberOfMonths: 1,
        onSelect: function (date) {
            for (var i = 0; i < dates.length; ++i) {
                if (dates[i].id < this.id)
                    $(dates[i]).datepicker('option', 'minDate', date);
                else if (dates[i].id > this.id)
                    $(dates[i]).datepicker('option', 'maxDate', date);
            }
        }

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
        self.getClientDetails(skip);
    };
    
    var pagingSettings = {
        pageSize: 25,
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
}
function isNumberKeydecimal(el, evt) {  
    var charCode = (evt.which) ? evt.which : event.keyCode;
    var number = el.value.split('.');
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    //just one dot
    if (number.length > 1 && charCode == 46) {
        return false;
    }
    //get the carat position
    var caratPos = getSelectionStart(el);
    var dotPos = el.value.indexOf(".");
    if (caratPos > dotPos && dotPos > -1 && (number[1].length > 1)) {
        return false;
    }
    return true;
}
function getSelectionStart(o) {
    if (o.createTextRange) {
        var r = document.selection.createRange().duplicate()
        r.moveEnd('character', o.value.length)
        if (r.text == '') return o.value.length
        return o.value.lastIndexOf(r.text)
    } else return o.selectionStart
}