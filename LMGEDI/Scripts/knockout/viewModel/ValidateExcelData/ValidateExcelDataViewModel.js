function ValidateExcelDataViewModel(model) {
    var self = this;

    self.PatientBillingValidationResult = ko.observableArray([]);
    self.PatientBillingCount = ko.observable();
    self.BillingID = ko.observable();
    self.UpdateOption = ko.observable();
    self.PatientFName = ko.observable();
    self.PatientLName = ko.observable();
    self.PatientSSN = ko.observable();
    self.BillingInsurance = ko.observable();
    self.TotalItemCount = ko.computed(function () {
        return self.PatientBillingCount();
    });
      

    ko.mapping.fromJS(model, {}, self);
  
    self.GetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.Skip(0);
            self.Take(pagingSettings.pageSize);
        }
        else {
            self.Skip(skip);
            self.Take(take);
        }
       
            $.ajax({
                url: "/ValidateExcelData/",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({ skip: $('#hidskip').val() }),
                success: function (data) {
                    if (data != null) {
                        ko.mapping.fromJS(data, {}, self);
                    }
                },
                error: function (data) {
                    alertify.alert("Data Not Found");
                }
            }); 
    };

    self.UpdateAndValidateData = function (data) {
        var IfValidate = "False";
        var _PatientFName = data.PatientFName();
        var _PatientSSN = data.PatientSSN();
        var _BillingInsurance =  data.BillingInsurance() ;
        if (_PatientFName == null)
        {
            IfValidate = "True";
        }
        else if (_PatientSSN == null)
        {
            IfValidate = "True";
        }
        else if (_BillingInsurance == null)
        {
            IfValidate = "True";
        }

        if (IfValidate == "False") {
            var verify = alertify.confirm("Are you sure you want to proceed");
            if (verify == true) {
                $.ajax({
                    url: "/ValidateExcelData/UpdateRowAndValidateData",
                    cache: false,
                    type: "POST", dataType: 'json',
                    contentType: 'application/json',
                    data: ko.toJSON({ patientBillingResultModel: data, UpdateOption: $("#_UpdateOption").val() }),
                    success: function (data) {
                        alertify.alert("Validate Successfully");
                     
                        window.location = document.URL;
                    },
                    error: function (data) {
                        alertify.alert("Error Occur");
                    }
                });
            }
            else {
                window.location = document.URL;
            }

        }
        else {
            alertify.alert("Please enter missing field then update");
        }
    };


    var pagingSettings = {
        pageSize: 20,
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

};

