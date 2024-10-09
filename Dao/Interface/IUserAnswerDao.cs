using Job.Model;

namespace Job.Dao.Interface;

public interface IUserAnswerDao
{
    void CreateAnswer(UserAnswerModel userAnswerModel);
    void UpdateGrade(UserAnswerModel userAnswerModel);
    void UpdateResult(UserAnswerModel userAnswerModel);
    void DeleteTest(int id);
    UserAnswerModel GetAnswer(int id);
    void CreateJob(JobModel jobModel);
    JobModel GetJob(int id);
    List<JobModel> GetJobResult(string mbti, string holland);


}