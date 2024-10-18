using Job.AppDBContext;
using Job.Dao.Interface;
using Job.Model;
using Job.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Job.Dao;

public class UserAnswerDao : IUserAnswerDao
{
    private readonly AppDbContext _context;

    public UserAnswerDao(AppDbContext context)
    {
        _context = context;
    }
    #region 新增回答
    public UserAnswerModel CreateAnswer(UserAnswerModel userAnswerModel)
    {
        _context.UserAnswer.Add(userAnswerModel);
        _context.SaveChanges();
        return userAnswerModel;
    }

    #endregion
    #region 更新分數
    public void UpdateGrade(UserAnswerModel userAnswerModel)
    {
        var answer = _context.UserAnswer.FirstOrDefault(a => a.ua_id == userAnswerModel.ua_id);

        if (answer != null)
        {
            answer.count_MBTI_E = userAnswerModel.count_MBTI_E;
            _context.Entry(answer).Property(a => a.count_MBTI_E).IsModified = true;
            answer.count_MBTI_I = userAnswerModel.count_MBTI_I;
            _context.Entry(answer).Property(a => a.count_MBTI_I).IsModified = true;
            answer.count_MBTI_N = userAnswerModel.count_MBTI_N;
            _context.Entry(answer).Property(a => a.count_MBTI_N).IsModified = true;
            answer.count_MBTI_S = userAnswerModel.count_MBTI_S;
            _context.Entry(answer).Property(a => a.count_MBTI_S).IsModified = true;
            answer.count_MBTI_T = userAnswerModel.count_MBTI_T;
            _context.Entry(answer).Property(a => a.count_MBTI_T).IsModified = true;
            answer.count_MBTI_F = userAnswerModel.count_MBTI_F;
            _context.Entry(answer).Property(a => a.count_MBTI_F).IsModified = true;
            answer.count_MBTI_J = userAnswerModel.count_MBTI_J;
            _context.Entry(answer).Property(a => a.count_MBTI_J).IsModified = true;
            answer.count_MBTI_P = userAnswerModel.count_MBTI_P;
            _context.Entry(answer).Property(a => a.count_MBTI_P).IsModified = true;
            answer.count_HOL_E = userAnswerModel.count_HOL_E;
            _context.Entry(answer).Property(a => a.count_HOL_E).IsModified = true;
            answer.count_HOL_C = userAnswerModel.count_HOL_C;
            _context.Entry(answer).Property(a => a.count_HOL_C).IsModified = true;
            answer.count_HOL_I = userAnswerModel.count_HOL_I;
            _context.Entry(answer).Property(a => a.count_HOL_I).IsModified = true;
            answer.count_HOL_A = userAnswerModel.count_HOL_A;
            _context.Entry(answer).Property(a => a.count_HOL_A).IsModified = true;
            answer.count_HOL_R = userAnswerModel.count_HOL_R;
            _context.Entry(answer).Property(a => a.count_HOL_R).IsModified = true;
            answer.count_HOL_S = userAnswerModel.count_HOL_S;
            _context.Entry(answer).Property(a => a.count_HOL_S).IsModified = true;
            _context.SaveChanges();
        }
    }
    #endregion
    #region 更新結果
    public void UpdateResult(UserAnswerModel userAnswerModel)
    {
        var answer = _context.UserAnswer.FirstOrDefault(a => a.ua_id == userAnswerModel.ua_id);

        if (answer != null)
        {
            answer.MBTI_Result = userAnswerModel.MBTI_Result;
            _context.Entry(answer).Property(a => a.MBTI_Result).IsModified = true;
            answer.HOL_Result = userAnswerModel.HOL_Result;
            _context.Entry(answer).Property(a => a.HOL_Result).IsModified = true;
            answer.test_Result = userAnswerModel.test_Result;
            _context.Entry(answer).Property(a => a.test_Result).IsModified = true;
            _context.SaveChanges();
        }
    }
    #endregion
    #region 取得測驗回答
    public UserAnswerModel GetAnswer(int id)
    {
        return _context.UserAnswer.FirstOrDefault(a => a.ua_id == id);
    }
    #endregion
    #region 取得測驗回答列表
    public List<UserAnswerModel> AnswerList()
    {
        return _context.UserAnswer.OrderByDescending(u => u.testTime).ToList();
    }
    #endregion
    #region 更新題目選項
    public void UpdateTest(TestModel testModel)
    {
        var test = _context.Test.FirstOrDefault(a => a.t_id == testModel.t_id);

        if (test != null)
        {
            test.ts_idList = testModel.ts_idList;
            _context.SaveChanges();
        }
    }
    #endregion
    #region 取得指定工作列表
    public List<JobModel> GetJobResult(string mbti, string holland)
    {
        var job = _context.Job.Where(a => a.MBTI == mbti && a.HOL == holland).ToList();
        if (job.Count < 5)
        {
            job = _context.Job.Where(a => a.MBTI == mbti || a.HOL == holland).ToList();
        }
        return job;
    }
    #endregion
}