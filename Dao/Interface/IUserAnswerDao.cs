using Job.Model;

namespace Job.Dao.Interface;

public interface IUserAnswerDao
{
    UserAnswerModel CreateAnswer(UserAnswerModel userAnswerModel);
    void UpdateGrade(UserAnswerModel userAnswerModel);
    void UpdateResult(UserAnswerModel userAnswerModel);
    public List<UserAnswerModel> AnswerList();
    UserAnswerModel GetAnswer(int id);
    List<JobModel> GetJobResult(string mbti, string holland);
}