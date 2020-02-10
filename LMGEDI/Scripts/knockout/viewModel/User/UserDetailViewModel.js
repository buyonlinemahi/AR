function UserDetailViewModel(model) {
    var self = this;
    self.UserStatusSelected = ko.observable();
    self.IsPasswordDirty = ko.observable(false);
    self.UserStatus = ko.observableArray([{ StatusName: "Active", Status: 'true' }, { StatusName: "InActive", Status: 'false' }]);
    this.Init = function (model) {
        if (model != null) {            
            ko.mapping.fromJS(model, {}, self);
            self.UserStatusSelected(model.IsActive);          
        }
    };

    self.UserInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmPatientDetail").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }
    self.UpdateUserSuccess = function (model) {
        if (model != "0") {
            alertify.confirm("Updated Successfully", function (e) {
                if (e) {
                    window.location = '/User/Userlanding';
                    self.IsPasswordDirty(false);
                }
            });
        }
        else {
            alertify.alert("Username Already Exists");
        }
    }
    self.PasswordValueChanged = function () {

        self.IsPasswordDirty(true);

    };
    //self.cancel = function () {
    //    clearTextFields();
    //}
}