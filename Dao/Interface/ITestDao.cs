using Job.Model;

namespace Job.Dao.Interface;

public interface ITestDao
{
    void CreateTest(TestModel testModel);
    void EditTest(TestModel testModel);
    TestModel GetTest(int t_id);
    void DeleteTest(int id);
    List<TestModel> TestList();
    void UpdateTest(TestModel testModel);
    void CreateSeletion(SeletionModel seletionModel);
    void EditSeletion(SeletionModel seletionModel);
    void DeleteSeletion(int ts_id);
    List<int> GetTestSeletionID(int t_id);
    SeletionModel GetSeletion(int ts_id);
    List<SeletionModel> GetTestSeletion(int t_id);

}