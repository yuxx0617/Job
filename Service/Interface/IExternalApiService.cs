using Job.ImportModel;
using Job.Model;
using Job.ViewModel;

namespace Job.Service.Interface;

public interface IExternalApiService
{
    Task<ResultViewModel<List<CompanyModel>>> GetCompanyData();
    Task<ResultViewModel<List<CompanyModel>>> GetBranchCompanyData();
    Task<ResultViewModel<List<CompanyModel>>> GetForeignCompanyData();
    Task<ResultViewModel<List<CompanyModel>>> GetBusinessData();
    Task<ResultViewModel<List<CompanyModel>>> GetCompanyDisbandData();
    Task<ResultViewModel<List<CompanyModel>>> GetBranchCompanyAbolishData();
    Task<ResultViewModel<List<CompanyModel>>> GetStopBusinessData();
    Task<ResultViewModel<List<CompanyModel>>> GetLtdDisbandData();
}