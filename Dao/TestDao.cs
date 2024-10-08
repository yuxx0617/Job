using Job.AppDBContext;
using Job.Dao.Interface;
using Job.Model;
using Microsoft.EntityFrameworkCore;

namespace Job.Dao;

public class TestDao : ITestDao
{
    private readonly AppDbContext _context;

    public TestDao(AppDbContext context)
    {
        _context = context;
    }
    #region 新增題目
    public void CreateTest(TestModel testModel)
    {
        _context.Test.Add(testModel);
        _context.SaveChanges();
    }
    #endregion
    #region 編輯題目
    public void EditTest(TestModel testModel)
    {
        var test = _context.Test.FirstOrDefault(a => a.t_id == testModel.t_id);

        if (test != null)
        {
            test.question = testModel.question;
            test.bgImg = testModel.bgImg;
            test.bgColor = testModel.bgColor;
            test.animateImg = testModel.animateImg;
            test.ts_idList = testModel.ts_idList;

            _context.Entry(test).State = EntityState.Modified;

            _context.SaveChanges();
        }
    }
    #endregion
    #region 取得指定題目
    public TestModel GetTest(int id)
    {
        return _context.Test.FirstOrDefault(a => a.t_id == id);
    }
    #endregion
    #region 刪除題目
    public void DeleteTest(int id)
    {
        var testModel = _context.Test.FirstOrDefault(a => a.t_id == id);

        if (testModel != null)
        {
            _context.Test.Remove(testModel);
            _context.SaveChanges();
        }
    }
    #endregion
    #region 取得所有題目
    public List<TestModel> TestList()
    {
        return _context.Test.ToList();
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
    #region 新增選項
    public void CreateSeletion(SeletionModel seletionModel)
    {
        _context.Seletion.Add(seletionModel);
        _context.SaveChanges();
    }
    #endregion
    #region 編輯選項
    public void EditSeletion(SeletionModel seletionModel)
    {
        var seletion = _context.Seletion.FirstOrDefault(t => t.ts_id == seletionModel.ts_id);

        if (seletion != null)
        {
            seletion.seletion = seletionModel.seletion;
            seletion.MBTI_I = seletionModel.MBTI_I;
            seletion.MBTI_E = seletionModel.MBTI_E;
            seletion.MBTI_F = seletionModel.MBTI_F;
            seletion.MBTI_T = seletionModel.MBTI_T;
            seletion.MBTI_N = seletionModel.MBTI_N;
            seletion.MBTI_S = seletionModel.MBTI_S;
            seletion.MBTI_P = seletionModel.MBTI_P;
            seletion.MBTI_J = seletionModel.MBTI_J;
            seletion.HOL_A = seletionModel.HOL_A;
            seletion.HOL_C = seletionModel.HOL_C;
            seletion.HOL_E = seletionModel.HOL_E;
            seletion.HOL_I = seletionModel.HOL_I;
            seletion.HOL_R = seletionModel.HOL_R;
            seletion.HOL_S = seletionModel.HOL_S;

            _context.Entry(seletion).State = EntityState.Modified;

            _context.SaveChanges();
        }
    }
    #endregion
    #region 刪除選項
    public void DeleteSeletion(int ts_id)
    {
        var seletionModel = _context.Seletion.FirstOrDefault(t => t.ts_id == ts_id);

        if (seletionModel != null)
        {
            _context.Seletion.Remove(seletionModel);
            _context.SaveChanges();
        }
    }
    #endregion
    #region 取得單一題目的所有選項
    public List<int> GetTestSeletionID(int t_id)
    {

        var seletion = _context.Seletion.Where(t => t.t_id == t_id).ToList();
        List<int> seletionIds = seletion.Select(s => s.ts_id).ToList();
        return seletionIds;
    }
    #endregion
    #region 取得選項
    public SeletionModel GetSeletion(int ts_id)
    {
        return _context.Seletion.FirstOrDefault(a => a.ts_id == ts_id);
    }
    #endregion
    public List<SeletionModel> GetTestSeletion(int t_id)
    {

        var seletion = _context.Seletion.Where(t => t.t_id == t_id).ToList();
        return seletion;
    }
}