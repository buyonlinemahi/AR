function UserAddViewModel() {
    self.UserStatus = ko.observableArray([{ StatusName: "Active", Status: 'true' }, { StatusName: "InActive", Status: 'false' }]);
    this.UserInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    };
    this.AddUserSuccess = function (responseText) {
        if (responseText != "0") {
            alertify.confirm("Added Successfully", function (e) {
                if (e) {
                    window.location = '/User/Userlanding';
                }
            });
        }
        else
        {
            alertify.alert("Username Already Exists");
        }
    };
    self.cancel = function () {
        clearTextFields();
    }
}