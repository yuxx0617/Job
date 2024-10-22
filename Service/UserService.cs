using System.Net.Mail;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using Job.Dao.Interface;
using Job.ImportModel;
using Job.Model;
using Job.Service.Interface;
using Job.ViewModel;

namespace Job.Service;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly Token _token;
    private readonly IUserDao _dao;
    private string smtp_account = "jobwaitforyou@gmail.com"; // 帳號
    private string smtp_password = "kavencurulmnmpot"; // 密碼
    private string smtp_mail = "jobwaitforyou@gmail.com"; // 信箱
    public UserService(IUserDao dao, IHttpContextAccessor httpContextAccessor, Token token)
    {
        _dao = dao;
        _httpContextAccessor = httpContextAccessor;
        _token = token;
    }

    #region 寄驗證信
    public ResultViewModel EmailValid(EmailValidImportModel emailValid)
    {
        try
        {
            var user = _dao.GetUser(emailValid.account);
            if (user != null)
            {
                if (user.password == HashPassword(emailValid.password, user.salt))
                {
                    if (String.IsNullOrWhiteSpace(user.emailValid))
                    {
                        return new ResultViewModel("已完成驗證") { };
                    }
                    else
                    {
                        var userModel = new UserModel
                        {
                            account = user.account,
                            password = user.password,
                        };
                        var httpContext = _httpContextAccessor.HttpContext;
                        var expirationTime = DateTime.UtcNow.AddHours(48);
                        var expirationTimestamp = expirationTime.ToString("o"); // ISO 8601 格式
                        if (httpContext == null)
                        {
                            return new ResultViewModel("httpContext不存在");
                        }
                        string verifyUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/api/User/Validate?account={emailValid.account}&emailValid={user.emailValid}&expires={expirationTimestamp}";

                        string templPath = Path.Combine("EmailValid.html");
                        if (!System.IO.File.Exists(templPath))
                        {
                            return new ResultViewModel("郵件模板不存在");
                        }
                        string mailTemplate = System.IO.File.ReadAllText(templPath);
                        string mailBody = GetMailBody(mailTemplate, user.name, verifyUrl);
                        SendMail(mailBody, emailValid.account);

                        return new ResultViewModel() { };
                    }
                }
            }
            return new ResultViewModel("帳號錯誤") { };
        }
        catch (Exception ex)
        {
            return new ResultViewModel(ex.Message);
        }
    }
    public string GetMailBody(string TempString, string UserName, string ValidateUrl)
    {
        TempString = TempString.Replace("{{UserName}}", UserName);
        TempString = TempString.Replace("{{ValidateUrl}}", ValidateUrl);
        return TempString;
    }
    public void SendMail(string MailBody, string ToMail)
    {
        try
        {
            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
            smtpServer.Port = 587;
            smtpServer.Credentials = new System.Net.NetworkCredential(smtp_account, smtp_password);
            smtpServer.EnableSsl = true;
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(smtp_mail);
            mail.To.Add(ToMail);
            mail.Subject = "會員驗證信";
            mail.Body = MailBody;
            mail.IsBodyHtml = true;
            smtpServer.Send(mail);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"發送郵件時發生錯誤: {ex.Message}");
        }
    }
    #endregion
    #region 驗證
    public ResultViewModel Validate(string account, string emailValid, string expires)
    {
        try
        {
            if (!DateTime.TryParse(expires, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime expirationTime))
            {
                return new ResultViewModel("無效的過期時間");
            }

            if (DateTime.Now > expirationTime)
            {
                return new ResultViewModel("驗證連結已過期");
            }

            var validate = new UserModel
            {
                account = account,
                emailValid = emailValid,
            };

            if (!_dao.EmailValid(validate))
            {
                return new ResultViewModel("驗證失敗");
            }
            return new ResultViewModel() { };
        }
        catch (Exception ex)
        {
            return new ResultViewModel(ex.Message);
        }
    }
    #endregion
    #region 註冊
    public ResultViewModel Register(RegisterImportModel register)
    {
        try
        {
            var user = _dao.GetUser(register.account);
            if (user == null)
            {
                var code = GetRandomCode(10);
                var salt = GetRandomCode(15);
                var today = DateTime.Now.ToString("yyyy-MM-dd");
                var registeruser = new UserModel
                {
                    account = register.account,
                    password = HashPassword(register.password, salt),
                    name = register.name,
                    address = register.address,
                    phone = register.phone,
                    edu = register.edu,
                    sex = register.sex,
                    birth = register.birth,
                    emailValid = code,
                    role = false,
                    salt = salt,
                    loginDay = DateTime.Parse(today)
                };
                _dao.Register(registeruser);

                var httpContext = _httpContextAccessor.HttpContext;
                var expirationTime = DateTime.UtcNow.AddHours(48);
                var expirationTimestamp = expirationTime.ToString("o"); // ISO 8601 格式
                if (httpContext == null)
                {
                    return new ResultViewModel("httpContext不存在");
                }
                string verifyUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/api/User/Validate?account={register.account}&emailValid={code}&expires={expirationTimestamp}";

                string templPath = Path.Combine("EmailValid.html");
                if (!System.IO.File.Exists(templPath))
                {
                    return new ResultViewModel("郵件模板不存在");
                }
                string mailTemplate = System.IO.File.ReadAllText(templPath);
                string mailBody = GetMailBody(mailTemplate, register.name, verifyUrl);
                SendMail(mailBody, register.account);

                return new ResultViewModel() { };
            }
            else
            {
                return new ResultViewModel("帳號重複");
            }
        }
        catch (Exception ex)
        {
            return new ResultViewModel(ex.Message);
        }
    }
    #endregion
    #region Hash密碼
    public string HashPassword(string password, string salt)
    {
        string saltandpassword = String.Concat(salt, password);
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] passwordData = Encoding.Default.GetBytes(saltandpassword);
            byte[] hashData = sha256.ComputeHash(passwordData);
            string result = Convert.ToBase64String(hashData);
            return result;
        }
    }
    #endregion
    #region 登入
    public ResultViewModel<string> Login(LoginImportModel login)
    {
        try
        {
            var user = _dao.GetUser(login.account);
            if (user != null)
            {
                if (HashPassword(login.password, user.salt) == user.password)
                {
                    if (String.IsNullOrWhiteSpace(user.emailValid))
                    {
                        if (!_dao.LoginDay(user))
                        {
                            return new ResultViewModel<string>("失敗");
                        }
                        var usertoken = _token.GenerateToken(user);
                        return new ResultViewModel<string>() { result = usertoken };
                    }
                    else
                    {
                        return new ResultViewModel<string>("信箱尚未驗證請先去驗證");
                    }
                }
                else
                {
                    return new ResultViewModel<string>("密碼錯誤");
                }
            }
            else
            {
                return new ResultViewModel<string>("無此帳號");
            }
        }
        catch (Exception ex)
        {
            return new ResultViewModel<string>(ex.Message);
        }
    }
    #endregion
    #region 寄忘記密碼信
    public ResultViewModel ForgetPassword(ForgetPasswordImportModel forgrtPassword)
    {
        try
        {
            var user = _dao.GetUser(forgrtPassword.account);
            if (user != null)
            {
                var rdCode = GetRandomCode(10);
                var newpassword = HashPassword(rdCode, user.salt);
                if (String.IsNullOrWhiteSpace(user.emailValid))
                {
                    var forgetpassworduser = new UserModel
                    {
                        account = forgrtPassword.account,
                        password = newpassword
                    };
                    if (!_dao.ForgetPassword(forgetpassworduser))
                    {
                        return new ResultViewModel("失敗");
                    }

                    string forgettemplPath = Path.Combine("ForgetPassword.html");
                    if (!System.IO.File.Exists(forgettemplPath))
                    {
                        return new ResultViewModel("郵件模板不存在");
                    }
                    string forgetmailTemplate = System.IO.File.ReadAllText(forgettemplPath);
                    string forgetmailBody = GetForgetMailBody(forgetmailTemplate, forgrtPassword.account, rdCode);
                    SendForgetMail(forgetmailBody, forgrtPassword.account);
                    return new ResultViewModel<string>() { };
                }
                else
                {
                    return new ResultViewModel<string>("信箱尚未驗證請先去驗證");
                }
            }
            else
            {
                return new ResultViewModel<string>("無此帳號");
            }
        }

        catch (Exception ex)
        {
            return new ResultViewModel<string>(ex.Message);
        }
    }
    public string GetForgetMailBody(string TempString, string UserName, string UserPassword)
    {
        TempString = TempString.Replace("{{UserName}}", UserName);
        TempString = TempString.Replace("{{UserPassword}}", UserPassword);
        return TempString;
    }
    public void SendForgetMail(string MailBody, string ToMail)
    {
        try
        {
            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
            smtpServer.Port = 587;
            smtpServer.Credentials = new System.Net.NetworkCredential(smtp_account, smtp_password);
            smtpServer.EnableSsl = true;
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(smtp_mail);
            mail.To.Add(ToMail);
            mail.Subject = "會員忘記密碼信";
            mail.Body = MailBody;
            mail.IsBodyHtml = true;
            smtpServer.Send(mail);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"發送郵件時發生錯誤: {ex.Message}");
        }
    }
    #endregion
    #region 隨機亂碼
    public string GetRandomCode(int num)
    {
        string[] Code ={ "A", "B", "C", "D", "E", "F", "G", "H", "I",
                "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U",
                "V", "W", "X", "Y", "Z", "1", "2", "3", "4", "5", "6",
                "7", "8", "9", "a", "b", "c", "d", "e", "f", "g", "h",
                "i", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t",
                "u", "v", "w", "x", "y", "z" };

        string RandomCode = string.Empty;
        Random rd = new Random();
        for (int i = 0; i < num; i++)
        {
            RandomCode += Code[rd.Next(Code.Count())];
        }
        return RandomCode;
    }
    #endregion
    #region 編輯會員資料
    public ResultViewModel EditUser(EditUserImportModel editUser)
    {
        try
        {
            var user = _dao.GetUser(editUser.account);
            var updateInfoModel = new UserModel();

            if (editUser.password != null)
            {
                updateInfoModel = new UserModel
                {
                    account = editUser.account,
                    password = HashPassword(editUser.password, user.salt),
                    name = editUser.name ?? user.name,
                    address = editUser.address ?? user.address,
                    phone = editUser.phone ?? user.phone,
                    edu = editUser.edu ?? user.edu,
                    sex = editUser.sex ?? user.sex,
                    birth = editUser.birth ?? user.birth,
                };
            }
            else
            {
                updateInfoModel = new UserModel
                {
                    account = editUser.account,
                    password = user.password,
                    name = editUser.name ?? user.name,
                    address = editUser.address ?? user.address,
                    phone = editUser.phone ?? user.phone,
                    edu = editUser.edu ?? user.edu,
                    sex = editUser.sex ?? user.sex,
                    birth = editUser.birth ?? user.birth,
                };
            }

            if (!_dao.EditUser(updateInfoModel))
            {
                return new ResultViewModel("失敗");
            }
            return new ResultViewModel() { };
        }
        catch (Exception ex)
        {
            return new ResultViewModel<string>(ex.Message);
        }
    }
    #endregion
    #region 會員列表
    public ResultViewModel<List<UserViewModel>> UserList()
    {
        try
        {
            var userlist = _dao.UserList();
            var result = userlist.Select(user => new UserViewModel
            {
                account = user.account,
                name = user.name,
                phone = user.phone,
                address = user.address,
                edu = user.edu,
                sex = user.sex,
                birth = user.birth,
                loginDay = user.loginDay,
            }).ToList();

            return new ResultViewModel<List<UserViewModel>>() { result = result };
        }
        catch (Exception ex)
        {
            return new ResultViewModel<List<UserViewModel>>(ex.Message);
        }
    }
    #endregion
    #region 刪除一年未上線使用者
    public ResultViewModel DeleteUserList()
    {
        try
        {
            var deleteUser = _dao.GetDeleteUsers();
            foreach (var user in deleteUser)
            {
                _dao.DeleteUser(user.account);
            }
            return new ResultViewModel();
        }
        catch (Exception ex)
        {
            return new ResultViewModel(ex.Message);
        }
    }
    #endregion
    #region 取得單一會員
    public ResultViewModel<UserViewModel> GetUser(string account)
    {
        try
        {
            var user = _dao.GetUser(account);
            var result = new UserViewModel
            {
                account = user.account,
                name = user.name,
                phone = user.phone,
                address = user.address,
                edu = user.edu,
                sex = user.sex,
                birth = user.birth,
                loginDay = user.loginDay,
            };

            return new ResultViewModel<UserViewModel>() { result = result };
        }
        catch (Exception ex)
        {
            return new ResultViewModel<UserViewModel>(ex.Message);
        }
    }
    #endregion

}