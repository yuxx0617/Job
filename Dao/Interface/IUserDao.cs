using Job.Model;

namespace Job.Dao.Interface;

public interface IUserDao
{
    UserModel GetUser(string account);
    bool EmailValid(UserModel userModel);
    void Register(UserModel userModel);
    bool ForgetPassword(UserModel userModel);
    bool EditUser(UserModel userModel);
    List<UserModel> UserList();
    bool LoginDay(UserModel userModel);
    List<UserModel> GetDeleteUsers();
    void DeleteUser(string account);
    List<int> GetHotRecord();
}