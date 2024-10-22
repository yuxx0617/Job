using Job.Model;

namespace Job.Dao.Interface;

public interface IJobDao
{
    void CreateJob(JobModel jobModel);
    JobModel GetJob(int id);
    List<JobModel> GetJobResult(string mbti, string holland);
    List<JobModel> JobList();
    void UpdateJobContent(JobModel jobModel);
    void CreateLesson(LessonModel lessonModel);
    void CreateCertificate(CertificateModel certificateModel);
    void CreateSubsidy(SubsidyModel subsidyModel);
    List<LessonModel> LessonList();
    List<CertificateModel> CertificateList();
    List<SubsidyModel> SubsidyList();
    List<int> GetHotRecord();
}