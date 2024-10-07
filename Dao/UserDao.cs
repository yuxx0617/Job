using Job.AppDBContext;
using Job.Dao.Interface;
using Job.Model;
using Job.ViewModel;

namespace Job.Dao;

public class UserDao : IUserDao
{
    private readonly AppDbContext _context;

    public UserDao(AppDbContext context)
    {
        _context = context;
    }
    #region 取得會員
    public UserModel GetUser(string account)
    {
        var user = _context.User.FirstOrDefault(a => a.account == account);
        if (user != null)
            return user;
        else
            return null;
    }
    #endregion
    #region 驗證
    public bool EmailValid(UserModel userModel)
    {
        var user = _context.User.FirstOrDefault(item => item.account == userModel.account);
        if (user == null) return false;

        _context.Attach(user);
        if (userModel.emailValid != null)
        {
            if (user.emailValid == userModel.emailValid)
            {
                user.emailValid = "";
                _context.Entry(user).Property(item => item.emailValid).IsModified = true;
            }
        }
        _context.SaveChanges();
        return true;
    }
    #endregion
    #region 註冊
    public void Register(UserModel userModel)
    {
        _context.User.Add(userModel);
        _context.SaveChanges();
    }
    #endregion
    #region 忘記密碼
    public bool ForgetPassword(UserModel userModel)
    {
        var user = _context.User.FirstOrDefault(item => item.account == userModel.account);
        if (user == null) return false;

        _context.Attach(user);
        if (userModel.password != null)
        {
            user.password = userModel.password;
            _context.Entry(user).Property(item => item.password).IsModified = true;
        }
        _context.SaveChanges();
        return true;
    }
    #endregion
    #region 編輯會員資料
    public bool EditUser(UserModel userModel)
    {
        var user = _context.User.FirstOrDefault(item => item.account == userModel.account);
        if (user == null) return false;

        _context.Attach(user);

        user.name = userModel.name;
        _context.Entry(user).Property(item => item.name).IsModified = true;

        user.password = userModel.password;
        _context.Entry(user).Property(item => item.password).IsModified = true;
        user.phone = userModel.phone;
        _context.Entry(user).Property(item => item.phone).IsModified = true;

        user.address = userModel.address;
        _context.Entry(user).Property(item => item.address).IsModified = true;

        user.edu = userModel.edu;
        _context.Entry(user).Property(item => item.edu).IsModified = true;

        user.sex = userModel.sex;
        _context.Entry(user).Property(item => item.sex).IsModified = true;

        user.birth = userModel.birth;
        _context.Entry(user).Property(item => item.birth).IsModified = true;

        _context.SaveChanges();
        return true;
    }
    #endregion
    #region 會員列表
    public List<UserModel> UserList()
    {
        return _context.User.Where(u => u.role == false).ToList();
    }
    #endregion
    #region 登入時間
    public bool LoginDay(UserModel userModel)
    {
        var user = _context.User.FirstOrDefault(item => item.account == userModel.account);
        if (user == null) return false;

        _context.Attach(user);
        var today = DateTime.Now.ToString("yyyy-MM-dd");
        user.loginDay = DateTime.Parse(today);
        _context.Entry(user).Property(item => item.loginDay).IsModified = true;

        _context.SaveChanges();
        return true;
    }
    #endregion
    #region 取得一年未上線使用者列表
    public List<UserModel> GetDeleteUsers()
    {
        var oneYearAgo = DateTime.Now.AddYears(-1);
        return _context.User
                       .Where(u => u.role == false && u.loginDay <= oneYearAgo).ToList();
    }
    #endregion
    #region 刪除會員
    public void DeleteUser(string account)
    {
        var user = _context.User.FirstOrDefault(u => u.account == account);
        if (user != null)
        {
            _context.User.Remove(user);
            _context.SaveChanges();
        }
    }
    #endregion
}