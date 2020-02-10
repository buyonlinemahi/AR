function getCommissionViewModel(model)
{
    var self = this;
    self.TotalItemCount = ko.observable(0);
    self.CommissionDetailsResult = ko.observable();
    self.LienCClientID = ko.observable();

    var mappingOptions = {
        'CDueDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM/DD/YYYY");
            }
        }
    }

    $(document).ready(function () {
        $.post("/Commission/getCommissionDashboardRecord", {
            skip: $("#hidskip").val()
        },
         function (_dataModel) {
             var model = $.parseJSON(_dataModel);
             ko.mapping.fromJS(model.CommissionDetails, mappingOptions, self.CommissionDetailsResult);
             self.TotalItemCount(model.TotalCount);
         });
    });
  

    self.getCommissionDetails = function (model)
    {
        self.LienCClientID(model.LienCClientID());
        window.location.href = "/commission/ClientCommission?clientID=" + self.LienCClientID();
    }

    self.GetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.Skip(0);
            self.Take(pagingSettings.pageSize);
        }
        else {
            self.Skip(skip);
            self.Take(take);
        }
        $.post("/Commission/getCommissionDashboardRecord", {
            skip: skip
        },
     function (_dataModel) {
         var model = $.parseJSON(_dataModel);
         ko.mapping.fromJS(model.CommissionDetails, mappingOptions, self.CommissionDetailsResult);
         self.TotalItemCount(model.TotalCount);
     });
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