using Job.Model;

namespace Job.Dao.Interface;

public interface IPredictDao
{
    void AddTypeStatusData(TypeStatusModel typeStatusModel);
    List<TypeStatusModel> TypeStatusList();
    Task AddVacancies(VacancieModel vacancieModel);
    List<VacancieModel> VacancieList();
    List<VacancieModel> VacancieList01();
    List<VacancieModel> VacancieList02();
    List<VacancieModel> VacancieList03();
    List<VacancieModel> VacancieList04();
    List<VacancieModel> VacancieList05();
    List<VacancieModel> VacancieList06();
    List<VacancieModel> VacancieList07();
    List<VacancieModel> VacancieList08();
    List<VacancieModel> VacancieList09();
    List<VacancieModel> VacancieList10();
    List<VacancieModel> VacancieList11();
    List<VacancieModel> VacancieList12();
    List<VacancieModel> VacancieList13();
    List<VacancieModel> VacancieList14();
    List<VacancieModel> VacancieList15();
    List<VacancieModel> VacancieList16();
    List<VacancieModel> VacancieList17();
    List<VacancieModel> VacancieList18();
    List<VacancieModel> VacancieList19();
    List<VacancieModel> VacancieList20();
    List<VacancieModel> VacancieList21();
    List<VacancieModel> VacancieList22();
    List<TypeStatusModel> TypeStatusList01();
    List<TypeStatusModel> TypeStatusList02();
    List<TypeStatusModel> TypeStatusList03();
    List<TypeStatusModel> TypeStatusList04();
    List<TypeStatusModel> TypeStatusList05();
    List<TypeStatusModel> TypeStatusList06();
    List<TypeStatusModel> TypeStatusList07();
    List<TypeStatusModel> TypeStatusList08();
    List<TypeStatusModel> TypeStatusList09();
    List<TypeStatusModel> TypeStatusList10();
    List<TypeStatusModel> TypeStatusList11();
    List<TypeStatusModel> TypeStatusList12();
    List<TypeStatusModel> TypeStatusList13();
    List<TypeStatusModel> TypeStatusList14();
    List<TypeStatusModel> TypeStatusList15();
    List<TypeStatusModel> TypeStatusList16();
    List<TypeStatusModel> TypeStatusList17();
    List<TypeStatusModel> TypeStatusList18();
    List<TypeStatusModel> TypeStatusList19();
    List<TypeStatusModel> TypeStatusList20();
    List<TypeStatusModel> TypeStatusList21();
    List<TypeStatusModel> TypeStatusList22();
    void CreatePredict(PredictModel predictModel);
    List<PredictModel> Predictlist();
}