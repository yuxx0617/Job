using Job.ImportModel;
using Job.ViewModel;

namespace Job.Service.Interface;

public interface IUserAnswerService
{
    ResultViewModel<AnswerIdViewModel> CreateAnswerAndCount(CreateAnswerImportModel createAnswer);
    ResultViewModel<AnswerViewModel> GetAnswerResult(int ua_id);
    ResultViewModel<List<AnswerViewModel>> AnswerResultList();
}