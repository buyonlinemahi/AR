function PendingUploadViewModel(model)
{
    var self = this;
    self.TotalItemCount = ko.observable(model.TotalCount);

    ko.mapping.fromJS(model, {}, self);

    self.updatePandingUploadByID = function (model) {
        var plainJs = ko.toJS(model);
        plainJs.PendingUploadDate = moment(plainJs.PendingUploadDate).format("MM/DD/YYYY");
        alertify.confirm("Are you sure to delete this record?", function (e) {
            if (e) {
                showloderDiv();
                $.ajax({
                    url: "/PendingUpload/UpdatePendingUploadRecord/",
                    cache: false,
                    type: "POST", dataType: 'json',
                    contentType: 'application/json',
                    data: JSON.stringify(plainJs),
                    success: function (data) {
                        if (data != null) {
                            hideloderDiv();
                            pagingAndRebinding();
                            alertify.success("Deleted Successfully");
                        }
                    },
                    error: function (data) {
                        hideloderDiv();
                        alertify.alert("An error has occurred. Please contact your system administrator for futher assistance.");
                    }
                });
            }
        });
    }

    function showloderDiv()
    {
        $("#loaderDiv").addClass('loaderbg');
        $("#ImgSourceLoading").show();
    }
    function hideloderDiv() {
        $("#loaderDiv").removeClass('loaderbg');
        $("#ImgSourceLoading").hide();
    }

     
    self.MoveToDataBase = function (dataResult) {
        var _deptID = dataResult.DepartmentId();
        var _pendinguid = dataResult.PendingUploadId();

        if (_deptID != "" && _pendinguid != "") {
            alertify.confirm("Are you sure to move this record?", function (e) {
                if (e) {
                    showloderDiv();
                    $.ajax({
                        url: "/ExportExcelToDatabase/ValidateExcelTempDataImport",
                        cache: false,
                        type: "POST", dataType: 'json',
                        contentType: 'application/json',
                        data: ko.toJSON({ Department: _deptID, pendingUploadId: _pendinguid }),
                        success: function (dataSearch) {
                            if (dataSearch == "Error occur") {
                                hideloderDiv();
                                alertify.alert("An error has occurred. Please contact your system administrator for futher assistance.");

                            }
                            else if (dataSearch == 1) {
                                hideloderDiv();
                                alertify.success("Processed Successfully");
                                pagingAndRebinding();
                            }
                        },
                        error: function (data) {
                            hideloderDiv();
                            alertify.alert("Data Not Found");
                        }
                    });

                }
            });

        }
        else {
            alertify.alert("Please select record.");
        }
    };

    self.GetRecordsWithSkipTake = function (skip, take) {

        if (skip == undefined || take == undefined) {
            self.Skip(0);
            self.Take(pagingSettings.pageSize);
        }
        else {
            self.Skip(skip);
            self.Take(take);
        }
        pagingAndRebinding();
    }

    function pagingAndRebinding()
    {
        $.ajax({
            url: "/PendingUpload/",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: ko.toJSON({ skip: $('#hidskip').val() }),
            success: function (data) {
                if (data != null) {
                    self.TotalItemCount(data.TotalCount);
                    ko.mapping.fromJS(data, {}, self);
                }
            },
            error: function (data) {
                alertify.alert("Data Not Found");
            }
        });
    }

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

}