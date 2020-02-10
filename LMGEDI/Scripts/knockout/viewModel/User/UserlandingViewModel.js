function UserlandingViewModel(model) {
    var self = this;
    
    self.UserSearchResult = ko.observableArray([]);
    self.UserCount = ko.observable();
    self.UserSearchText = ko.observable();
    
    self.TotalItemCount = ko.computed(function () {
        return self.UserCount();
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
        var searchTextHidden = $('#UserSearchText').val();
        if (searchTextHidden == "") {
            $.ajax({
                url: "/User/Userlanding",
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
                url: "/User/GetUserBySearch",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({ name: $('#UserSearchText').val(), skip: $('#hidskip').val() }),
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

    self.SearchUserButton = function () {
        var searchText = $('#UserSearch').val();
        self.UserSearchText(searchText);
        if (searchText == "") {
            alertify.alert("Please Enter Text");
        }
        else {
            $.ajax({
                url: "/User/GetUserBySearch",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({ name: searchText, skip: 0 }),
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

    self.ResetUserButton = function () {
        $('#UserSearchText').val("");
        $('#UserSearch').val("");
        $.ajax({
            url: "/User/Userlanding",
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