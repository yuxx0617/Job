using Job.ImportModel;
using Job.ViewModel;

namespace Job.Service.Interface;

public interface ITestService
{
    ResultViewModel CreateTest(CreateTestImportModel createTest);
    ResultViewModel EditTest(EditTestImportModel editTest);
    ResultViewModel DeleteTest(int t_id);
    ResultViewModel<List<TestViewModel>> TestList();
    ResultViewModel<TestViewModel> GetTest(int t_id);
    ResultViewModel CreateSeletion(CreateSeletionImportModel createSeletion);
    ResultViewModel EditSeletion(EditSeletionImportModel editSeletion);
    ResultViewModel DeleteSeletion(int ts_id);
    ResultViewModel<List<SeletionViewModel>> GetTestSeletion(int t_id);

}