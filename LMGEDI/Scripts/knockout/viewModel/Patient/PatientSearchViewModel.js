function PatientSearchViewModel(model) {
    var self = this;
    self.PatientSearchResult = ko.observableArray([]);
    self.PatientCount = ko.observable();
    self.PatientSearchText = ko.observable();
    self.PatientFullName = ko.observable();
    self.TotalItemCount = ko.computed(function () {
        return self.PatientCount();
    });

  //  self.PatientSearchType = ko.observableArray([{ SearchCriteria: 1, SearchCriteriaName: "Patient Name" }, { SearchCriteria: 2, SearchCriteriaName: "Patient Tin" }]);

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
        var searchTextHidden = $('#PatientSearchText').val();
        if (searchTextHidden == "") {
            $.ajax({
                url: "/Patient/",
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
        }
        else {
            $.ajax({
                url: "/Patient/GetAllPatientByName",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({ PatientName: $('#PatientSearchText').val(), skip: $('#hidskip').val() }),
                success: function (dataSearch) {
                    if (dataSearch != null) {
                        ko.mapping.fromJS(dataSearch, {}, self);
                    }
                },
                error: function (data) {
                    alertify.alert("Data Not Found");
                }
            });
        }

    };

    self.SearchPatientButton = function () {
        var searchText = $('#PatientSearch').val();
        self.PatientSearchText(searchText);
        if (searchText == "")
        {
            alertify.alert("Please Enter Text");
        }
        else {
            $.ajax({
                url: "/Patient/GetAllPatientByName",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({ PatientName: searchText ,skip: 0}),
                success: function (dataSearch) {
                    if (dataSearch != null) {
                        ko.mapping.fromJS(dataSearch, {}, self);

                        self.Pager().CurrentPage(1);

                    }
                },
                error: function (data) {
                    alertify.alert("Data Not Found");
                }
            });
        }
    };

    self.ResetPatientButton = function () {
        $('#PatientSearchText').val("");
        $('#PatientSearch').val("");
            $.ajax({
                url: "/Patient/",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({ skip: 0 }),
                success: function (dataSearch) {
                    ko.mapping.fromJS(dataSearch, {}, self);
                    self.Pager().CurrentPage(1);
                },
                error: function (data) {
                    alertify.alert("Data Not Found");
                }
            });
        
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

}