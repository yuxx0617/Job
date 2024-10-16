using Job.Model;

namespace Job.Dao.Interface;

public interface IUpdateDao
{
    void CreateJob(JobModel jobModel);
    JobModel GetJob(int id);
    List<JobModel> GetJobResult(string mbti, string holland);


}