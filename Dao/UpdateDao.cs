using Job.AppDBContext;
using Job.Dao.Interface;
using Job.Model;
using Microsoft.EntityFrameworkCore;

namespace Job.Dao;

public class UpdateDao : IUpdateDao
{
    private readonly AppDbContext _context;

    public UpdateDao(AppDbContext context)
    {
        _context = context;
    }
    #region 新增工作
    public void CreateJob(JobModel jobModel)
    {
        _context.Job.Add(jobModel);
        _context.SaveChanges();
    }
    #endregion
    #region 取得單一工作
    public JobModel GetJob(int id)
    {
        return _context.Job.FirstOrDefault(a => a.j_id == id);
    }
    #endregion
    #region 取得指定工作列表
    public List<JobModel> GetJobResult(string mbti, string holland)
    {
        return _context.Job.Where(a => a.MBTI == mbti || a.HOL == holland).ToList();
    }
    #endregion
}