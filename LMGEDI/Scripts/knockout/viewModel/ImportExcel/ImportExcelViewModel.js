function ImportExcelViewModel(model) {
    var self = this;
    self.Departments = ko.observableArray();
    self.selectedDepartment = ko.observable();
    self.Uid = ko.observable();
    self.deptId = ko.observable();
    self.IsChanged = ko.observable(false);
    self.FileUploadViewModelResults = ko.observableArray();
    self.FileCount = ko.observable();
    self.IsDeleted  = ko.observable();
    self.TotalItemCount = ko.computed(function () {
        return self.FileCount();
    });
    self.curentPage = ko.observable(1);
        
    //  $(document).init(function () {
    $.getJSON("/ExportExcelToDatabase/getDepartment", self.Departments);
    $("#ImgSourceLoading").hide();
    $("#loaderDiv").removeClass('loaderbg');
    // });
    ko.mapping.fromJS(model, {}, self);

    $('#uploadButtonData').click(function () {
        var up = $('#uploadFile').val();
        var dep = $('#DepartmentID').val();
        if (up != '' && dep != '') {
            $("#loaderDiv").addClass('loaderbg');
            $("#ImgSourceLoading").show();
        }
    });

    self.UploadExcelButton = function (result) {       
        var modelinvo = $.parseJSON(result);
        if (modelinvo.IsDeleted == true) {

            $.ajax({
                url: "/ExportExcelToDatabase/UpdatePendingUploadDeleted",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({ pendingUploadID: modelinvo.PendingUploadId }),
                success: function (dataSearch) {
                    if (dataSearch != null) {
                        
                    }
                },
                error: function (data) {
                  
                    alertify.alert("Data Not Found");
                }
            });

            $("#ImgSourceLoading").hide();
            $("#loaderDiv").removeClass('loaderbg');
            alertify.alert("An error has occurred. Please contact your system administrator for futher assistance.");
        }
        else {           
            $('#Uid').val(modelinvo.PendingUploadId);
            $('#deptId').val(modelinvo.DepartmentId);

            $("#ImgSourceLoading").hide();
            $("#loaderDiv").removeClass('loaderbg');
            alertify.success("Uploaded Successfully");
        }
    }


    self.PullDataFromLien = function () {
        alertify.confirm("Are you sure you want to pull data from lien ?", function (e) {
            if (e) {
                $("#loaderDiv").addClass('loaderbg');
                $("#ImgSourceLoading").show();
                $.ajax({
                    url: "/ExportExcelToDatabase/PullDataFromLienToAR",
                    cache: false,
                    type: "POST", dataType: 'json',
                    contentType: 'application/json',
                    data: ko.toJSON({}),
                    success: function (dataSearch) {
                        if (dataSearch != null) {
                            $("#ImgSourceLoading").hide();
                            $("#loaderDiv").removeClass('loaderbg');
                            alertify.success("Record pulled Successfully");
                        }
                    },
                    error: function (data) {
                        $("#ImgSourceLoading").hide();
                        $("#loaderDiv").removeClass('loaderbg');
                        alertify.alert("Data Not Found");
                    }
                });
            }
        })
    }


    self.valueChanged = function(val) {
        self.IsChanged(true);
    }

    self.GetRecordsWithSkipTake = function (skip, take) {
            GetFileData(skip, take);    
    };

    function GetFileData(skip, take)
    {
        if (skip == undefined || take == undefined) {
            self.Skip(0);
            self.Take(pagingSettings.pageSize);
        }
        else {
            self.Skip(skip);
            self.Take(take);
        }
        $("#loaderDiv").addClass('loaderbg');
        $("#ImgSourceLoading").show();
        $.ajax({
            url: "/ExportExcelToDatabase/GetFileAllData",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: ko.toJSON({ skip: $('#hidskip').val() }),
            success: function (dataSearch) {
                if (dataSearch != null) {
                    ko.mapping.fromJS(dataSearch, {}, self);
                    self.curentPage(self.Pager().CurrentPage());
                    $("#ImgSourceLoading").hide();
                    $("#loaderDiv").removeClass('loaderbg');
                }
                else if (dataSearch == "Error occur") {
                    $("#ImgSourceLoading").hide();
                    $("#loaderDiv").removeClass('loaderbg');
                    alertify.alert("Error Occur");
                }
            },
            error: function (data) {
                $("#ImgSourceLoading").hide();
                $("#loaderDiv").removeClass('loaderbg');
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
        if (self.IsChanged()) {
            if (self.curentPage() != self.Pager().CurrentPage()) {
                alertify.confirm("Do you want to save your change(s)?", function (e) {
                    if (e) {
                        var skip = pagingSettings.pageSize * (self.Pager().CurrentPage() - 1);
                        var take = pagingSettings.pageSize;
                        self.GetRecordsWithSkipTake(skip, take);
                        self.IsChanged(false);
                    }
                    else {
                        self.Pager().CurrentPage(self.curentPage());
                    }
                });
            }
        }
        else
        {
            var skip = pagingSettings.pageSize * (self.Pager().CurrentPage() - 1);
            var take = pagingSettings.pageSize;
            self.GetRecordsWithSkipTake(skip, take);
        }
    });

    self.ImportDataButton = function (dataResult) {
        var control = $('#uploadFile');
        var _deptID = $('#deptId').val();
        var _pendinguid = $('#Uid').val()

        if (_deptID != "" && _pendinguid !="") {               
            $("#loaderDiv").addClass('loaderbg');
            $("#ImgSourceLoading").show();
            $.ajax({
                url: "/ExportExcelToDatabase/ValidateExcelTempDataImport",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({ Department: $('#deptId').val(), pendingUploadId: $('#Uid').val() }),
                success: function (dataSearch) {                   
                    if (dataSearch == "Error occur") {
                        $("#ImgSourceLoading").hide();
                        $("#loaderDiv").removeClass('loaderbg');
                        alertify.alert("An error has occurred. Please contact your system administrator for futher assistance.");
                    }
                    else if (dataSearch == 1) {
                       
                        $.post("/ExportExcelToDatabase/GetFileAllData", {
                            skip: $('#hidskip').val()
                        }, function (_data) {
                            var _data = $.parseJSON(_data);
                            ko.mapping.fromJS(_data.FileUploadViewModelResults, {}, self.FileUploadViewModelResults);
                            self.FileCount(_data.FileCount);
                            self.Pager().CurrentPage(1);
                           
                        });
                        self.Departments.removeAll();
                         $.getJSON("/ExportExcelToDatabase/getDepartment", self.Departments);  
                        control.replaceWith(control.val('').clone(true));

                        control.on({
                            change: function () { console.log("Changed") },
                            focus: function () { console.log("Focus") }
                        });


                        $("#ImgSourceLoading").hide();
                        $("#loaderDiv").removeClass('loaderbg');

                        alertify.success("Processed Successfully");
                    }

                },
                error: function (data) {
                    $("#ImgSourceLoading").hide();
                    $("#loaderDiv").removeClass('loaderbg');
                    alertify.alert("Data Not Found");
                }
            });
        }
        else {
            alertify.alert("Please go to pending upload");
        }
    };

    self.DeleteFilesByFileIDButton = function (modeldata) {
        var uploadID = modeldata.UploadId();
        alertify.confirm("Are you sure to delete this record?", function (e) {
            if (e) {
                $("#loaderDiv").addClass('loaderbg');
                $("#ImgSourceLoading").show();
                $.ajax({
                    url: "/ExportExcelToDatabase/DeleteFilesByUploadID",
                    cache: false,
                    type: "POST", dataType: 'json',
                    contentType: 'application/json',
                    data: ko.toJSON({ uploadID: uploadID }),
                    success: function (dataSearch) {
                        if (dataSearch == "Error occur") {
                            $("#ImgSourceLoading").hide();
                            $("#loaderDiv").removeClass('loaderbg');
                            alertify.alert("Error occur");
                        }
                        else {
                            
                            $.post("/ExportExcelToDatabase/GetFileAllData", {
                                skip: $('#hidskip').val()
                            }, function (_data) {
                                var _data = $.parseJSON(_data);
                                ko.mapping.fromJS(_data.FileUploadViewModelResults, {}, self.FileUploadViewModelResults);
                                self.FileCount(_data.FileCount);
                                self.Pager().CurrentPage(1);

                            });

                            $("#ImgSourceLoading").hide();
                            $("#loaderDiv").removeClass('loaderbg');
                            alertify.success("Deleted Successfully");
                        }
                    },
                    error: function (data) {
                        $("#ImgSourceLoading").hide();
                        $("#loaderDiv").removeClass('loaderbg');
                        alertify.alert("Data Not Found");
                    }
                });
            }
        });
    };

    self.UpdateResultSuccess = function (resultSuccess) {
        var _Claim = 0;
        var EmptyGrid = 0;
        var viewModel = self.FileUploadViewModelResults;
        for (var i = 0; i <= viewModel().length - 1;i++)
        {  
            EmptyGrid = 1;
            if (viewModel()[i].Claim() == '') {
                _Claim = 1;
            }
        }
        if (EmptyGrid == 1) {
            if (_Claim == 0) {
                $("#loaderDiv").addClass('loaderbg');
                $("#ImgSourceLoading").show();
                $.ajax({
                    url: "/ExportExcelToDatabase/UpdateLienItemTable",
                    cache: false,
                    type: "POST", dataType: 'json',
                    contentType: 'application/json',
                    data: ko.toJSON({ objLienTempTable: self.FileUploadViewModelResults }),
                    success: function (dataSearch) {
                       
                        if (dataSearch == "Error occur") {
                            $("#ImgSourceLoading").hide();
                            $("#loaderDiv").removeClass('loaderbg');
                            alertify.alert("An error has occurred. Please contact your system administrator for futher assistance.");
                        }
                        else if (dataSearch == 1) {                            
                            $.post("/ExportExcelToDatabase/GetFileAllData", {
                                skip: $('#hidskip').val()
                            }, function (_data) {
                                var _data = $.parseJSON(_data);
                                ko.mapping.fromJS(_data.FileUploadViewModelResults, {}, self.FileUploadViewModelResults);
                                self.FileCount(_data.FileCount);
                                self.Pager().CurrentPage(1);

                            });

                            $("#ImgSourceLoading").hide();
                            $("#loaderDiv").removeClass('loaderbg');
                            alertify.success("Record updated and Moved Successfully");

                        }
                        else if (dataSearch == 0) {   
                            $.post("/ExportExcelToDatabase/GetFileAllData", {
                                skip: $('#hidskip').val()
                            }, function (_data) {
                                var _data = $.parseJSON(_data);
                                ko.mapping.fromJS(_data.FileUploadViewModelResults, {}, self.FileUploadViewModelResults);
                                self.FileCount(_data.FileCount);
                                self.Pager().CurrentPage(1);

                            });

                            $("#ImgSourceLoading").hide();
                            $("#loaderDiv").removeClass('loaderbg');
                            alertify.success("Record updated");
                        }

                    },
                    error: function (data) {
                        $("#ImgSourceLoading").hide();
                        $("#loaderDiv").removeClass('loaderbg');
                        alertify.alert("Data Not Found");
                    }
                });
                self.IsChanged(false);
            }
            else {
                alertify.alert("Claim number required");
            }
        }
    };

};