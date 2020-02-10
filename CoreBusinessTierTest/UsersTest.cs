using LMGEDI.Core.BL.Implementation;
using LMGEDI.Core.BL;
using LMGEDI.Core.Data;
using LMGEDI.Core.Data.SqlServer.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LMGEDI.Core.Data.Model;

namespace CoreBusinessTierTest
{
    [TestClass]
   public  class UsersTest
    {
        private IUserRepository _usersRepository;
        private IUser _userBL;
        private LMGEDI.Core.Data.Model.User DLModel = new LMGEDI.Core.Data.Model.User();

        [TestInitialize]
        public void UsersInitialize()
        {
            _usersRepository = new UserRepository(new Core.Base.Data.SqlServer.Factory.BaseContextFactory<LMGEDI.Core.Data.SqlServer.LMGEDIDBContext>());
            _userBL = new UserImpl(_usersRepository);
        }

        [TestMethod]
        public void GetUserByID()
        {
            var user = _userBL.GetUserByID(1);
            Assert.IsTrue(user != null, "Unable to find");
        }

        [TestMethod]
        public void GetUserByUserName()
        {
            var user = _userBL.GetUserByUserName("s");
            Assert.IsTrue(user != null, "Unable to find");
        }
        [TestMethod]
        public void GetAllUsers()
        {
            int skip = 0;
            int take = 10;
            var user = _userBL.GetAllUsers(skip, take);
            Assert.IsTrue(user != null, "Unable to find");
        }
        [TestMethod]
        public void AddUser()
        {
            User user = new User();
            user.Username = "testuser2";
            user.Password = "testpass2";
            user.IsActive  = true;
            var userid = _userBL.AddUser(user);
            Assert.IsTrue(user != null, "Unable to find");
        }

        [TestMethod]
        public void UpdateUser()
        {
            User user = new User();
            user.Username = "sadfdd";
            user.Password = "testss";
            user.IsActive = false;
            user.UserID = 32;
            var userid = _userBL.UpdateUser(user);
            Assert.IsTrue(user != null, "Unable to find");
        }
        [TestMethod]
        public void GetUserByUserId()
        {
            int id = 13;
            User user = _userBL.GetUserByUserId(id);
            Assert.IsTrue(user != null, "Unable to find");
        }
        [TestMethod]
        public void GetUsersBySearch()
        {
            string search = "s";
            int skip = 0;
            int take = 10;
            var user = _userBL.GetUsersBySearch(search,skip, take);
            Assert.IsTrue(user != null, "Unable to find");
        }
    }
}
