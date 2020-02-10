function PatientDetailViewModel() {
    var self = this;
    self.PatientDOB = ko.observable();
    self.PatientSSN = ko.observable();
    self.PatientGenderselectedItem = ko.observable();
    self.PatientGenders = ko.observableArray([{ GenderName: "Male", GenderValue: 'Male' }, { GenderName: "Female", GenderValue: 'Female' }, { GenderName: "Unknown", GenderValue: 'Unknown' }]);
     this.Init = function (model) {
         if (model != null) {
             ko.mapping.fromJS(model, {}, self);
             self.PatientDOB = ko.observable(moment(model.PatientDOB).format("MM/DD/YYYY"));
             if (self.PatientGender() != null) {
                 self.PatientGenderselectedItem(self.PatientGender().trim());
             }
         }
     };


     self.PatientInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmPatientDetail").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }
    self.PatientDetailSuccess = function (model) {
        alertify.confirm(model, function (e) {
            if (e) {
                window.location = '/Patient/';
            }
        });
    }
    self.btncancel = function () {
        window.location = '/Patient/';
    }
}