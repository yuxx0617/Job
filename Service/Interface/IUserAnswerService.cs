using Job.ImportModel;
using Job.ViewModel;

namespace Job.Service.Interface;

public interface IUserAnswerService
{
    ResultViewModel CreateAnswer(CreateAnswerImportModel createAnswer);
    ResultViewModel<AnswerViewModel> CountGrade(int ua_id);
    ResultViewModel CreateJob(CreateJobImportModel createJob);
}