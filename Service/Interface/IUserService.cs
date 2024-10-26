using Job.ImportModel;
using Job.ViewModel;

namespace Job.Service.Interface;

public interface IUserService
{
    ResultViewModel EmailValid(EmailValidImportModel emailValid);
    ResultViewModel Validate(string account, string emailValid, string expires);
    ResultViewModel Register(RegisterImportModel register);
    ResultViewModel<string> Login(LoginImportModel login);
    ResultViewModel ForgetPassword(ForgetPasswordImportModel forgrtPassword);
    ResultViewModel EditUser(EditUserImportModel editUser);
    ResultViewModel<List<UserViewModel>> UserList();
    ResultViewModel<UserViewModel> GetUser(string account);
}