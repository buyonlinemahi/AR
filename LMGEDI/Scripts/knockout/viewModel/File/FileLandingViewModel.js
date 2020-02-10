function FilelandingViewModel(model) {
    var self = this;

    self.FileSearchResult = ko.observableArray([]);
    self.FileCount = ko.observable();
    self.FileSearchText = ko.observable();

    self.TotalItemCount = ko.computed(function () {
        return self.FileCount();
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
        var searchTextHidden = $('#FileSearchText').val();
        if (searchTextHidden == "") {
            $.ajax({
                url: "/File/Filelanding",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({ skip: $('#hidskip').val(), searchFile: $('#FileSearch').val() }),
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
                url: "/File/GetFileBySearch",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({ name: $('#FileSearchText').val(), skip: $('#hidskip').val() }),
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

    self.SearchFileButton = function () {
        var searchText = $('#FileSearch').val();
        self.FileSearchText(searchText);
        if (searchText == "") {
            alertify.alert("Please Enter Text");
        }
        else {
            $.ajax({
                url: "/File/GetFileBySearch",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({ searchFile: searchText, skip: 0 }),
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

    self.ResetFileButton = function () {
        $('#FileSearchText').val("");
        $('#FileSearch').val("");
        self.FileSearchResult([]);
        self.FileCount(0);

        //$.ajax({
        //    url: "/File/FileLanding",
        //    cache: false,
        //    type: "POST", dataType: 'json',
        //    contentType: 'application/json',
        //    data: ko.toJSON({ skip: 0 }),
        //    success: function (dataSearch) {
        //        ko.mapping.fromJS(dataSearch, {}, self);
        //        self.Pager().CurrentPage(1);
        //    },
        //    error: function (data) {
        //        alertify.alert("Data Not Found");
        //    }
        //});

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

$('#FileSearch').keypress(function (e) {
    if (e.keyCode == '13') {
        $('#btnSearchFile').click();
    }
});