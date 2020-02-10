using LMGEDI.Core.Data;
using LMGEDI.Core.Data.Model;
using DLModel = LMGEDI.Core.Data.Model;
using System.Linq;
using BLModel = LMGEDI.BL.Model;
using Omu.ValueInjecter;

namespace LMGEDI.Core.BL.Implementation
{
    public class UserImpl : IUser
    {
         private readonly IUserRepository _usersRepository;

         public UserImpl(IUserRepository userRepository)
         {
             _usersRepository = userRepository;
         }
         public User  GetUserByID(int userID)
         {
             return (_usersRepository.GetById(userID));
         }
         public User GetUserByUserName(string userName)
         {
             var User = _usersRepository.GetAll(user => user.Username == userName).FirstOrDefault();
             return User != null ? User : null;
         }
         public BLModel.Paged.UserDetail GetAllUsers(int skip, int take)
         {
             return new BLModel.Paged.UserDetail
             {
                 UserDetails = _usersRepository.GetAll().Skip(skip).Take(take).Select(userSearchResult => new User().InjectFrom(userSearchResult)).Cast<User>().ToList(),
                 TotalCount = _usersRepository.GetAll().Count()
             };
         }
         public int AddUser(User user)
         {
             user.FirstName = "Super";
             user.LastName = user.Username;
             if (_usersRepository.GetAll().Where(users => users.Username.Equals(user.Username)).Count() == 0)
                 return _usersRepository.Add((DLModel.User)new DLModel.User().InjectFrom(user)).UserID;
             else
                 return 0;
         }
         public int UpdateUser(User user)
         {
             if (_usersRepository.GetAll(users => (users.Username == user.Username) && (users.UserID != user.UserID)).Count() == 0)
                 return _usersRepository.Update((DLModel.User)new DLModel.User().InjectFrom(user), (users => users.Password), (users => users.Username), (users => users.IsActive), (users => users.UserID));
             else
                 return 0;
         }
        public User GetUserByUserId(int id)
        {
            return _usersRepository.GetById(id);
        }
        public BLModel.Paged.UserDetail GetUsersBySearch(string search, int skip, int take)
        {
            if (search.Equals("%"))
            {
                return new BLModel.Paged.UserDetail
                {
                    UserDetails = _usersRepository.GetAll().Skip(skip).Take(take).Select(user => new DLModel.User().InjectFrom(user)).Cast<DLModel.User>().ToList(),
                    TotalCount = _usersRepository.GetAll().Count()
                };
            }
            else
            {
                return new BLModel.Paged.UserDetail
                {

                    UserDetails = _usersRepository.GetAll(user => user.Username.Contains(search.ToString())).Skip(skip).Take(take).Select(user => new DLModel.User().InjectFrom(user)).Cast<DLModel.User>().ToList(),
                    TotalCount = _usersRepository.GetAll(user => user.Username.Contains(search)).Count()
                };
            }
           
        }
    }
}
