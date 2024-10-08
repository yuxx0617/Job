using Job.ImportModel;
using Job.ViewModel;

namespace Job.Service.Interface;

public interface ITestService
{
    ResultViewModel CreateTest(CreateTestImportModel createTest);
    ResultViewModel EditTest(EditTestImportModel editTest);
    ResultViewModel DeleteTest(DeleteTestImportModel deleteTest);
    ResultViewModel<List<TestViewModel>> TestList();
    ResultViewModel<TestViewModel> GetTest(GetTestImportModel getTest);
    ResultViewModel CreateSeletion(CreateSeletionImportModel createSeletion);
    ResultViewModel EditSeletion(EditSeletionImportModel editSeletion);
    ResultViewModel DeleteSeletion(int ts_id);
    ResultViewModel<List<TestSeletionViewModel>> GetTestSeletion(GetTestSeletionImportModel getTestSeletion);

}