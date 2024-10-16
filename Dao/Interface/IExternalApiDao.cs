using Job.Model;

namespace Job.Dao.Interface;

public interface IExternalApiDao
{
    void AddTypeStatusData(TypeStatusModel typeStatusModel);
    List<TypeStatusModel> TypeStatusList();
    Task AddVacancies(VacancieModel vacancieModel);
    List<VacancieModel> VacancieList();
}