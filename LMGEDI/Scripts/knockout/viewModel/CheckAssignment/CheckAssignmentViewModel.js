function checkAssignmentViewModel(model) {
    var self = this;    
    self.OCRCount = ko.observable();
    ko.mapping.fromJS(model.OCRCount, {}, self.OCRCount);
    self.TotalItemCount = ko.computed(function () {
        return self.OCRCount();
    });
    self.OCRSearchResult = ko.observableArray();
    ko.mapping.fromJS(model.OCRSearchResult, {}, self.OCRSearchResult);

    self.OCRFileFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        else {
            $("#loaderDiv").addClass('loaderbg');
            $("#ImgSourceLoading").removeClass('display-none');
        }
        return true;
    }
   
    self.AddOCRSuccess = function (result) {        
        document.getElementById("CheckAssignmentOCR").reset();
        if (result == "true") {            
            alertify.success("File Uploaded Successfully");
            $.ajax({
                url: "/CheckAssignment/Index",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({ skip: $('#hidskip').val() }),
                success: function (data) {
                    if (data != null) {
                        ko.mapping.fromJS(data, {}, self);
                        $("#loaderDiv").removeClass('loaderbg');
                        $("#ImgSourceLoading").addClass('display-none');
                    }
                },
                error: function (data) {
                    $("#loaderDiv").removeClass('loaderbg');
                    $("#ImgSourceLoading").addClass('display-none');
                    alertify.alert("Data Not Found");
                }
            });
        }
        else {
            $("#loaderDiv").removeClass('loaderbg');
            $("#ImgSourceLoading").addClass('display-none');
            alertify.alert("File Not Uploaded it doesn't contain barcode");
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
                url: "/CheckAssignment/Index",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({ skip: $('#hidskip').val() }),
                success: function (data) {
                    if (data != null) {                        
                        ko.mapping.fromJS(data.OCRSearchResult, {}, self.OCRSearchResult);                        
                    }
                },
                error: function (data) {
                    alertify.alert("Data Not Found");
                }
            });        
    };
    this.deleteOCRFile = function (itemToDelete) {
        var check = itemToDelete.OcrId();
        var name = itemToDelete.OcrFileName();
        alertify.confirm("Are you sure you want to delete the file?", function (e) {
            if (e) {
                $.ajax({
                    url: "/CheckAssignment/DeleteOCRFileDetailsById",
                    cache: false,
                    type: "POST", dataType: 'json',
                    contentType: 'application/json',
                    data: ko.toJSON({ OCRID: itemToDelete.OcrId(), Filename: itemToDelete.OcrFileName() }),
                    success: function (data) {
                        self.OCRSearchResult.remove(itemToDelete);                       
                        alert("File Deleted successfully");
                        $.ajax({
                            url: "/CheckAssignment/Index",
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
                            }
                        });
                        
                    },
                    error: function (data) {
                        alert("Error while deleting a file - " + data);
                    }
                });
                //self.Pager().CurrentPage(1);
            }


        })
      
    }
}  