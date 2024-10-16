using Job.ImportModel;
using Job.Model;
using Job.ViewModel;

namespace Job.Service.Interface;

public interface IExternalApiService
{
    Task<ResultViewModel<List<TypeStatusViewModel>>> UpdateCompanyDisbandType();
    Task<ResultViewModel<List<TypeStatusViewModel>>> UpdateBranchCompanyAbolishType();
    Task<ResultViewModel<List<TypeStatusViewModel>>> UpdateStopBusinessType();
    Task<ResultViewModel<List<VacancieViewModel>>> UpdateVacanciesData();
}