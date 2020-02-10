function ExcelUploadViewModel() {
    var self = this;

    self.Departments = ko.observableArray();
    self.selectedDepartment = ko.observable();
    self.rdoValue = ko.observable();
    self.UploadFilePath = ko.observable();
    self.ExcelHeaderName = ko.observable();
    self.ColumnName = ko.observable();
    self.ExcelHeaderDrop = ko.observableArray([]);
    self.selectedItemHeader = ko.observable(0);
    self.TempExcelDataDrop = ko.observableArray([]);
    self.selectedItemTempExcel = ko.observable(0);
    self.ExcelDatabaseHeaders = ko.observableArray([]);
    self.DuplicateValueCheck = ko.observable();
    self.ImportExcelDone = ko.observable();
    self.ChangingDropOption = ko.observable();

    self.UploadExcelButton = function (model) {       
        var response = $.parseJSON(model);
        if (response.TempExcelDataColumnsDetails != null && response.ExcelHeaderDetails != null && response.rdoValue!=null) {
            $.each(response.ExcelHeaderDetails, function (index, value) {
                self.ExcelHeaderDrop.push(new ExcelHeaderDropData(value.ExcelHeaderName));
            });
            $.each(response.TempExcelDataColumnsDetails, function (index, value) {
                self.TempExcelDataDrop.push(new TempExcelDataDropData(value.ColumnName));
            });
            ko.mapping.fromJS(response.ExcelHeaderDetails, {}, self.UploadFilePath);
            ko.mapping.fromJS(response.rdoValue, {}, self.rdoValue);
            $("#DivDropDownListShow").show();
            $("#MergeSubmit").show();
            $("#btnUpload").hide();
            $("#UploadLinkCompleted").addClass('active');
            $("#UploadLinkCompleted").removeClass('previous visited');
            $("#ImgSourceLoading").hide();
            $("#loaderDiv").removeClass('loaderbg');

        }
        else {
            alert("Please upload correct format");
        }
    };

    self.MergeExcelDataheader = function () {
        self.DuplicateValueCheck = ko.observable("False");
        $.each(self.ExcelDatabaseHeaders(), function (index, item) {
            var excelCurrent = $("#excelDropDownExcelHeaderName").val();
            var columnCurrent = $("#excelDropDownColumnName").val();
            var excelLoopItem = item.ExcelHeaderName();
            var columnLoopItem = item.ColumnName();

            if (excelCurrent == excelLoopItem || columnCurrent == columnLoopItem) {
                self.DuplicateValueCheck = ko.observable("True");               
            }
           
        });

        if (self.DuplicateValueCheck() == "False") {
            self.ExcelDatabaseHeaders.push(new BindMegeExcelDataGrid($("#excelDropDownExcelHeaderName").val(), $("#excelDropDownColumnName").val()));
            $("#DivMappingGrid").show();
            $("#MappingLinkCompleted").addClass('active');
            $("#MappingLinkCompleted").removeClass('previous visited');
        }
        else {
            alert("Duplicate Value Entered");
        }
        
    };
   
    //Import to database
    self.VerifyExcelDataheader = function (model) {
        if (model.ExcelDatabaseHeaders() != null && model.ExcelDatabaseHeaders() != "") {
            if (self.ImportExcelDone() == "False") {
                var verify = confirm("Are you sure you want to proceed")
                if (verify == true) {
                    $("#loaderDiv").addClass('loaderbg');
                    $("#ImgSourceLoading").show();
                    var filePath = $("#uploadFile").val();
                    $.ajax({
                        url: "/ExportExcelToDatabase/MappingExcelDatabaseField",
                        cache: false,
                        type: "POST", dataType: 'json',
                        contentType: 'application/json',
                        data: ko.toJSON({ modelExcelHeaderData: model.ExcelDatabaseHeaders, modelTempExcelData: model.ExcelDatabaseHeaders, uploadFi: filePath, rdoValue: self.rdoValue() }),
                        success: function (data) {
                            $("#ImgSourceLoading").hide();
                            $("#loaderDiv").removeClass('loaderbg');
                            $("#uploadFile").val(null);
                            $("#DivUploadExcel").hide();
                            $("#DivDropDownListShow").hide();
                            $("#MergeSubmit").hide();
                            $("#rdoPatientInfo").hide();
                            $("#rdoPatientBilling").hide();
                            $("#btnUpload").hide();
                            $("#loaderDiv").removeClass('loaderbg');
                            alert("Inserted Successfully");                           
                            $("#VerifySubmit").show();
                            self.ImportExcelDone("True");
                            $("#ImportLinkCompleted").addClass('active');
                            $("#ImportLinkCompleted").removeClass('previous visited');

                        },
                        error: function (data) {
                            alert("Error Occur");
                        }
                    });
                }
            }
            else if (self.ImportExcelDone() == "True") {
                alert("You already done to import data now you need to verify imported data");
            }
        }
    };

    self.DeleteDataFromGrid = function (RowDelete) {
        self.ExcelDatabaseHeaders.remove(RowDelete);
    }

    //------------------Replace Duplicate ,Validate data------------------------------------//
    self.ReplaceDuplicateValueFromTemp = function () {
        $("#loaderDiv").addClass('loaderbg');
        $("#ImgSourceLoading").show();
        $.ajax({
            url: "/ExportExcelToDatabase/ReplaceDuplicateValueFromTemp",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: ko.toJSON({ rdoValue: self.rdoValue() }),
            success: function (data) {
                if (data == "Error occur") {
                    $("#loaderDiv").removeClass('loaderbg');
                    $("#ImgSourceLoading").hide();
                    alert("Error Occur");                    
                }
                else if (data == "success")
                {
                    $("#loaderDiv").removeClass('loaderbg');
                    $("#ImgSourceLoading").hide();
                    alert("Verification Completed");
                    $("#VerifyLinkCompleted").addClass('active');
                    $("#VerifyLinkCompleted").removeClass('previous visited');
                    // window.location = document.URL;
                    $("#DivUploadExcel").hide();
                    $("#DivDropDownListShow").hide();
                    $("#MergeSubmit").hide();
                    $("#DivMappingGrid").hide();
                    $("#btnUpload").hide();
                    $("#VerifySubmit").hide();
                    $("#rdoPatientInfo").hide();
                    $("#rdoPatientBilling").hide();
            }
              
            },
            error: function (data) {
                alert("Error Occur");
            }
        });
    }
    
    $(document).ready(function () {        
        var _radioValue = "";
        $.getJSON("/ExportExcelToDatabase/getDepartment", self.Departments);
        $("#btnUpload").click(function () {
            $("#ImgSourceLoading").show();
            $("#loaderDiv").addClass('loaderbg');
        });
     
        $("#divRadioButtonList input[name=rdoValue]:radio").on('change',function () {
            _radioValue = $('input[name=rdoValue]:checked', '#divRadioButtonList').val();
            if (_radioValue != "") {
                if (self.ChangingDropOption() == "False") {
                    $("#DivUploadExcel").show();
                    $("#btnUpload").show();
                    self.ChangingDropOption("True")
                }
                else if (self.ChangingDropOption() == "True")
                {
                    var verify = confirm("Option change will clear all previous data")
                    if (verify == true) {
                        window.location = document.URL;
                        $("#DivUploadExcel").show();
                        $("#btnUpload").show();
                    }
                }
            }

        });     
          
            $("#DivUploadExcel").hide();
            $("#DivDropDownListShow").hide();
            $("#MergeSubmit").hide();
            $("#DivMappingGrid").hide();
            $("#btnUpload").hide();
            $("#VerifySubmit").hide();
            $("#ImgSourceLoading").hide();
            self.ImportExcelDone("False");
            self.ChangingDropOption("False");
    
    });


   
};

function ExcelHeaderDropData(_value) {
    var self = this;
    self.ExcelHeaderName = ko.observable(_value);
    
};

function TempExcelDataDropData(_value) {
    var self = this;
    self.ColumnName = ko.observable(_value);
};

function BindMegeExcelDataGrid(_ExcelHeaderName,_ColumnName) {
    var self = this;
    self.ExcelHeaderName = ko.observable(_ExcelHeaderName);
    self.ColumnName = ko.observable(_ColumnName);
};

