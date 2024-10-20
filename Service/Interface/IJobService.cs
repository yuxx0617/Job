using Job.ImportModel;
using Job.ViewModel;

namespace Job.Service.Interface;

public interface IJobService
{
    ResultViewModel CreateJob(CreateFileImportModel createJob);
    ResultViewModel<JobViewModel> GetJob(int j_id);
    Task<ResultViewModel> UpdateJobContent();
    ResultViewModel CreateLesson(CreateFileImportModel createJob);
    ResultViewModel CreateCertificate(CreateFileImportModel createJob);
    ResultViewModel CreateSubsidy(CreateFileImportModel createJob);
    ResultViewModel<List<LessonViewModel>> LessonList();
    ResultViewModel<List<CertificateViewModel>> CertificateList();
    ResultViewModel<List<SubsidyViewModel>> SubsidyList();

}