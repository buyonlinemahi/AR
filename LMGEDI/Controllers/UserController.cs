using LMGEDIApp.Domain.Models.User;
using System.Linq;
using System.Web.Mvc;
using LMGEDI.Core.BL;
using LMGEDI.Core.Data;
using LMGEDIApp.Infrastructure.Base;
using LMGEDIApp.Infrastructure.Global;
using Omu.ValueInjecter;
using AutoMapper;
using LMGEDIApp.Infrastructure.ApplicationServices.Contracts;
using System.Web;
using LMGEDIApp.Infrastructure.ApplicationFilters;
using System;

namespace LMGEDI.Controllers
{
    public class UserController : BaseController
    {
        private IUser _userBL;
        private IUserRepository _userRepository;
        public readonly IEncryption _encryptionService;
        public readonly IARCommonServices _arCommonService;
        public UserController(IUser userBL, IUserRepository userRepository, IEncryption encryptionService, IARCommonServices arCommonService)
        {
            _userRepository = userRepository;
            _userBL = userBL;
            _encryptionService = encryptionService;
            _arCommonService = arCommonService;
        }
        // GET: User
       
        public ActionResult Index()
        {
            User user = new User();
            if (Request.Cookies[GlobalConst.SessionKeys.userDetailMgmt] != null)
            {
               
                HttpCookie myCookie = new HttpCookie(GlobalConst.SessionKeys.userDetailMgmt);
                Request.Cookies[GlobalConst.SessionKeys.userDetailMgmt].Expires = DateTime.Now;
                myCookie.Expires = DateTime.Now;
                Response.Cookies.Add(myCookie);

                System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                System.Web.HttpContext.Current.Response.Cache.SetExpires(DateTime.Now);
                System.Web.HttpContext.Current.Response.Cache.SetNoServerCaching();
                System.Web.HttpContext.Current.Response.Cache.SetNoStore();       
            }
            return View(user);
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            try
            {
                var data = _userBL.GetUserByUserName(user.Username);
                if (data != null)
                {
                    if (data.Username == user.Username && _encryptionService.VerifyHashedPassword(user.Password, data.Password) && data.IsActive)
                    {
                        HttpCookie ck = new HttpCookie(GlobalConst.SessionKeys.userDetailMgmt);
                        ck.Values[GlobalConst.SessionKeys.userID] = data.UserID.ToString();
                        Session[GlobalConst.SessionKeys.userID] = data.UserID.ToString();
                        ck.Values[GlobalConst.SessionKeys.userName] = data.Username.ToString();
                        ck.Values[GlobalConst.SessionKeys.UserFullName] = data.Username.ToString();// +' ' + data.LastName.ToString();
                        Response.Cookies.Add(ck);
                        return RedirectToAction(GlobalConst.Actions.FileController.FileLanding, GlobalConst.Controllers.File, new { area = GlobalConst.ConstantChar.Blank });
                    }
                    else if (!data.IsActive)
                        user.InvalidMsg = GlobalConst.alertMessages.UserNameInactive;
                    else
                        user.InvalidMsg = GlobalConst.alertMessages.UserNamePasswordIncorrect;
                }
                else
                    user.InvalidMsg = GlobalConst.alertMessages.UserNamePasswordIncorrect;

                return View(GlobalConst.Views.User.Index, user);
            }
            catch (Exception ex)
            {
                user.InvalidMsg = GlobalConst.alertMessages.ExceptionOccured;
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View(GlobalConst.Views.User.Index, user);
            }
        }
        [HttpGet]
        [AuthorizedUserCheckAttribute]
        public ActionResult Userlanding()
        {
            try
            {
                var getAllUsers = _userBL.GetAllUsers(GlobalConst.Records.Skip, GlobalConst.Records.LandingTake);
                UserViewModel userviewmodel = new UserViewModel();
                userviewmodel.UserSearchResult = getAllUsers.UserDetails.Select(userSearchResult => new UserSearchResult().InjectFrom(userSearchResult)).Cast<UserSearchResult>().ToList();
                userviewmodel.UserCount = getAllUsers.TotalCount;
                return View(userviewmodel);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View();
            }
        }
        [HttpPost]
        [AuthorizedUserCheckAttribute]
        public ActionResult Userlanding(int Skip)
        {
            try
            {
                var getAllUsers = _userBL.GetAllUsers(Skip, GlobalConst.Records.LandingTake);
                UserViewModel userviewmodel = new UserViewModel();
                userviewmodel.UserSearchResult = getAllUsers.UserDetails.Select(userSearchResult => new UserSearchResult().InjectFrom(userSearchResult)).Cast<UserSearchResult>().ToList();
                userviewmodel.UserCount = getAllUsers.TotalCount;
                return Json(userviewmodel, GlobalConst.ContentTypes.TextHtml);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View();
            }
        }
        [AuthorizedUserCheckAttribute]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [AuthorizedUserCheckAttribute]
        public ActionResult Add(User user)
        {
            try
            {
                user.Password = _encryptionService.HashPassword(user.Password);
                return Json(_userBL.AddUser(Mapper.Map<LMGEDI.Core.Data.Model.User>(user)), GlobalConst.ContentTypes.TextHtml);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View();
            }
        }
        [AuthorizedUserCheckAttribute]
        public ActionResult Detail(int id)
        {
            try
            {
                User user = Mapper.Map<User>(_userBL.GetUserByUserId(id));
                return View(user);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View();
            }
        }
        [HttpPost]
        [AuthorizedUserCheckAttribute]
        public ActionResult Update(User user)
        {
            try
            {
                if (user.IsPasswordDirty)
                    user.Password = _encryptionService.HashPassword(user.Password);
                return Json(_userBL.UpdateUser(Mapper.Map<LMGEDI.Core.Data.Model.User>(user)), GlobalConst.ContentTypes.TextHtml);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View();
            }
        }
        [HttpPost]
        [AuthorizedUserCheckAttribute]
        public ActionResult GetUserBySearch(string name, int Skip)
        {
            try
            {
                var getAllUsers = _userBL.GetUsersBySearch(name, Skip, GlobalConst.Records.LandingTake);
                UserViewModel userviewmodel = new UserViewModel();
                userviewmodel.UserSearchResult = getAllUsers.UserDetails.Select(userSearchResult => new UserSearchResult().InjectFrom(userSearchResult)).Cast<UserSearchResult>().ToList();
                userviewmodel.UserCount = getAllUsers.TotalCount;
                return Json(userviewmodel, GlobalConst.ContentTypes.TextHtml);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View();
            }
        }

    }
}