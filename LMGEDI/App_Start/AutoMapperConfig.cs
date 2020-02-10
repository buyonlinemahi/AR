using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
namespace LMGEDIApp
{
    public class AutoMapperConfig
    {
        public static void RegisterMapping()
        {      
                  
            #region Users
            Mapper.CreateMap<LMGEDI.Core.Data.Model.User, LMGEDIApp.Domain.Models.User.User>().ReverseMap();
            #endregion Users

            #region Files
            Mapper.CreateMap<LMGEDI.Core.Data.Model.Adjuster, LMGEDIApp.Domain.Models.AdjusterModel.Adjuster>().ReverseMap();
            Mapper.CreateMap<LMGEDI.Core.Data.Model.Employer, LMGEDIApp.Domain.Models.EmployerModel.Employer>().ReverseMap();
            Mapper.CreateMap<LMGEDI.Core.Data.Model.File, LMGEDIApp.Domain.Models.FileModel.File>().ReverseMap();
            Mapper.CreateMap<LMGEDI.Core.Data.Model.Insurer, LMGEDIApp.Domain.Models.InsurerModel.Insurer>().ReverseMap();
            Mapper.CreateMap<LMGEDI.Core.Data.Model.InsurerBranch, LMGEDIApp.Domain.Models.InsurerBranchModel.InsurerBranch>().ReverseMap();
            Mapper.CreateMap<LMGEDI.Core.Data.Model.Invoice, LMGEDIApp.Domain.Models.InvoiceModel.Invoice>().ReverseMap();
            Mapper.CreateMap<LMGEDI.Core.BL.Model.AdjusterSearch, LMGEDIApp.Domain.Models.AdjusterModel.AdjusterSearch>().ReverseMap();
            Mapper.CreateMap<LMGEDI.Core.BL.Model.EmployerSearch, LMGEDIApp.Domain.Models.EmployerModel.EmployerSearch>().ReverseMap();
            Mapper.CreateMap<LMGEDI.Core.BL.Model.File, LMGEDIApp.Domain.Models.FileModel.File>().ReverseMap();
            Mapper.CreateMap<LMGEDI.Core.Data.Model.File, LMGEDIApp.Domain.Models.FileModel.File>().ReverseMap();
            Mapper.CreateMap<LMGEDI.Core.BL.Model.InsurerSearch, LMGEDIApp.Domain.Models.InsurerModel.InsurerSearch>().ReverseMap();
            Mapper.CreateMap<LMGEDI.Core.BL.Model.InsurerBranchSearch, LMGEDIApp.Domain.Models.InsurerBranchModel.InsurerBranchSearch>().ReverseMap();
            Mapper.CreateMap<LMGEDI.Core.BL.Model.TempExcelDataColumns, LMGEDIApp.Domain.Models.ExcelUploadDomain.TempExcelDataColumns>().ReverseMap();
            #endregion Files

            #region Inovice
            Mapper.CreateMap<LMGEDI.Core.Data.Model.Invoice, LMGEDIApp.Domain.Models.InvoiceModel.Invoice>().ReverseMap();

            #endregion Invoice

            #region InoviceIC
            Mapper.CreateMap<LMGEDI.Core.Data.Model.InvoiceICDetail, LMGEDIApp.Domain.Models.InvoiceModel.InvoiceICDetail>().ReverseMap();

            #endregion Invoice

            #region Inovice Adjustment
            Mapper.CreateMap<LMGEDI.Core.Data.Model.InvoiceAdjustment, LMGEDIApp.Domain.Models.InvoiceAdjustmentModel.InvoiceAdjustment>().ReverseMap();

            #endregion Invoice

            #region FileDetail
            Mapper.CreateMap<LMGEDI.Core.Data.Model.FileDetail, LMGEDIApp.Domain.Models.FileModel.FileDetail>().ReverseMap();
            #endregion FileDetail

            #region Adjuster
            Mapper.CreateMap<LMGEDI.Core.Data.Model.AdjusterSearch, LMGEDIApp.Domain.Models.AdjusterModel.AdjusterSearch>().ReverseMap();
            #endregion Adjuster

            #region Department
            Mapper.CreateMap<LMGEDI.Core.BL.Model.Department, LMGEDIApp.Domain.Models.DepartmentModel.Department>().ReverseMap();
            #endregion Department

            #region State
            Mapper.CreateMap<LMGEDI.Core.BL.Model.State, LMGEDIApp.Domain.Models.StateModel.State>().ReverseMap(); 
            #endregion State


            #region Payment
            Mapper.CreateMap<LMGEDI.Core.Data.Model.Payment, LMGEDIApp.Domain.Models.PaymentModel.Payment>().ReverseMap();
            Mapper.CreateMap<LMGEDI.Core.Data.Model.PaymentRefund, LMGEDIApp.Domain.Models.PaymentModel.PaymentRefund>().ReverseMap();
            #endregion Payment

            #region PandingUpload
            Mapper.CreateMap<LMGEDI.Core.Data.Model.PendingUpload, LMGEDIApp.Domain.Models.PendingUploadModel.PendingUpload>().ReverseMap();
            #endregion PandingUpload


            #region LienTempTable
            Mapper.CreateMap<LMGEDI.Core.Data.Model.LienTempTable, LMGEDIApp.Domain.Models.LienTempTableModel.LienTempTable>().ReverseMap();
            Mapper.CreateMap<LMGEDI.Core.Data.Model.LienTempTableRecords, LMGEDIApp.Domain.Models.LienTempTableModel.LienTempTable>().ReverseMap();
            #endregion LienTempTable

            #region OCR
            Mapper.CreateMap<LMGEDI.Core.Data.Model.OCRFileInvoices, LMGEDIApp.Domain.Models.OCRModel.OCRFileInvoices>().ReverseMap();
            Mapper.CreateMap<LMGEDI.Core.Data.Model.OCR, LMGEDIApp.Domain.Models.OCRModel.OCRModel>().ReverseMap();
            Mapper.CreateMap<LMGEDI.Core.Data.Model.OCRPaymentDetails, LMGEDIApp.Domain.Models.OCRModel.OCRPaymentDetails>().ReverseMap();
            Mapper.CreateMap<LMGEDI.Core.Data.Model.OCRPaymentSave, LMGEDIApp.Domain.Models.OCRModel.OCRPaymentSave>().ReverseMap();
            Mapper.CreateMap<LMGEDI.Core.Data.Model.OCRPayment, LMGEDIApp.Domain.Models.OCRModel.OCRPayment>().ReverseMap();

            #endregion OCR

            #region InoviceNote
            Mapper.CreateMap<LMGEDI.Core.Data.Model.InvoiceNote, LMGEDIApp.Domain.Models.InvoiceNoteModel.InvoiceNote>().ReverseMap();
            Mapper.CreateMap<LMGEDI.Core.Data.Model.InvoiceNoteDetail, LMGEDIApp.Domain.Models.InvoiceNoteModel.InvoiceNoteDetail>().ReverseMap();
            #endregion

            #region Commission
            Mapper.CreateMap<LMGEDI.Core.Data.Model.LienClientBilling, LMGEDIApp.Domain.Models.ClientCommissionModel.LienClientBilling>().ReverseMap();
            Mapper.CreateMap<LMGEDI.Core.BL.Model.CommissionSearch, LMGEDIApp.Domain.Models.ClientCommissionModel.CommissionSearch>().ReverseMap();
            Mapper.CreateMap<LMGEDI.Core.Data.Model.CommissionPayment, LMGEDIApp.Domain.Models.ClientCommissionModel.CommissionPayment>().ReverseMap();
            Mapper.CreateMap<LMGEDI.Core.Data.Model.Commission, LMGEDIApp.Domain.Models.ClientCommissionModel.Commission>().ReverseMap();
            #endregion
        }
    }
}