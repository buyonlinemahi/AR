using LMGEDI.BL.Model.Paged;
using LMGEDI.Core.Data.Model;

namespace LMGEDI.Core.BL
{
    public interface IUser
    {
        User GetUserByID(int userID);
        User GetUserByUserName(string userName);
        UserDetail GetAllUsers(int skip, int take);
        int AddUser(User user);
        int UpdateUser(User user);
        User GetUserByUserId(int id);
        UserDetail GetUsersBySearch(string search, int skip, int take);
    }
}