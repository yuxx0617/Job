using Job.AppDBContext;
using Job.Dao.Interface;
using Job.Model;
using Microsoft.EntityFrameworkCore;

namespace Job.Dao;

public class JobDao : IJobDao
{
    private readonly AppDbContext _context;

    public JobDao(AppDbContext context)
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
    #region 取得工作列表
    public List<JobModel> JobList()
    {
        return _context.Job.ToList();
    }
    #endregion
    #region 更新工作
    public void UpdateJobContent(JobModel jobModel)
    {
        var job = _context.Job.FirstOrDefault(a => a.j_id == jobModel.j_id);

        if (job != null)
        {
            job.contentImg = jobModel.contentImg;
            _context.Entry(job).Property(a => a.contentImg).IsModified = true;
            job.experienceImg = jobModel.experienceImg;
            _context.Entry(job).Property(a => a.experienceImg).IsModified = true;
            _context.SaveChanges();
        }
    }
    #endregion
    #region 新增課程
    public void CreateLesson(LessonModel lessonModel)
    {
        _context.Lesson.Add(lessonModel);
        _context.SaveChanges();
    }
    #endregion
    #region 新增證照
    public void CreateCertificate(CertificateModel certificateModel)
    {
        _context.Certificate.Add(certificateModel);
        _context.SaveChanges();
    }
    #endregion
    #region 新增補助
    public void CreateSubsidy(SubsidyModel subsidyModel)
    {
        _context.Subsidy.Add(subsidyModel);
        _context.SaveChanges();
    }
    #endregion
    #region 取得課程列表
    public List<LessonModel> LessonList()
    {
        return _context.Lesson.ToList();
    }
    #endregion
    #region 取得證照列表
    public List<CertificateModel> CertificateList()
    {
        return _context.Certificate.ToList();
    }
    #endregion
    #region 取得補助列表
    public List<SubsidyModel> SubsidyList()
    {
        return _context.Subsidy.ToList();
    }
    #endregion


}