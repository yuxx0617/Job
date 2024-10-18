using Job.Dao.Interface;
using Job.ImportModel;
using Job.Model;
using Job.Service.Interface;
using Job.util;
using Job.ViewModel;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace Job.Service;

public class JobService : IJobService
{
    private readonly IJobDao _dao;
    private readonly appSetting _appSetting;
    private readonly HttpClient _httpClient;
    public JobService(IJobDao dao, IOptions<appSetting> appSetting, HttpClient httpClient)
    {
        _dao = dao;
        _appSetting = appSetting.Value;
        _httpClient = httpClient;
    }
    #region 新增工作
    public ResultViewModel CreateJob(CreateFileImportModel createJob)
    {
        try
        {
            if (createJob.file != null && createJob.file.Length > 0)
            {
                string fileExtension = Path.GetExtension(createJob.file.FileName).ToLower();
                if (fileExtension == ".xlsx" || fileExtension == ".xls")
                {
                    var filePath = Path.GetTempFileName();

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        createJob.file.CopyTo(stream);
                    }

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    List<JobModel> jobs = new List<JobModel>();

                    using (var package = new ExcelPackage(new FileInfo(filePath)))
                    {
                        var worksheet = package.Workbook.Worksheets[0];

                        if (worksheet == null)
                        {
                            throw new Exception("Excel檔案中無工作表");
                        }

                        for (int row = 2; row <= worksheet.Dimension.Rows; row++)
                        {
                            var job = new JobModel
                            {
                                name = worksheet.Cells[row, 1].Value?.ToString(),
                                MBTI = worksheet.Cells[row, 2].Value?.ToString(),
                                HOL = worksheet.Cells[row, 3].Value?.ToString(),
                            };

                            if (!string.IsNullOrEmpty(job.name))
                            {
                                jobs.Add(job);
                                _dao.CreateJob(job);
                            }
                        }
                    }

                    File.Delete(filePath);

                    return new ResultViewModel() { };
                }
                else
                {
                    return new ResultViewModel("文件不是有效的 Excel 檔案") { };
                }
            }
            else
            {
                return new ResultViewModel("未提供檔案") { };
            }
        }
        catch (Exception ex)
        {
            return new ResultViewModel(ex.Message) { };
        }
    }
    #endregion
    #region 取得單一工作
    public ResultViewModel<JobViewModel> GetJob(int j_id)
    {
        try
        {
            var job = _dao.GetJob(j_id);

            var result = new JobViewModel
            {
                j_id = job.j_id,
                name = job.name,
                MBTI = job.MBTI,
                HOL = job.HOL,
            };
            return new ResultViewModel<JobViewModel>() { result = result };
        }
        catch (Exception ex)
        {
            return new ResultViewModel<JobViewModel>(ex.Message) { };
        }
    }
    #endregion
    #region 更新工作薪水技能
    public async Task<ResultViewModel> UpdateJobContent()
    {
        try
        {
            var joblist = _dao.JobList();
            foreach (var job in joblist)
            {
                var url = $"http://127.0.0.1:5000/scrape?job={job.name}";

                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var jobContentData = JsonConvert.DeserializeObject<JobContentViewModel>(jsonResponse);
                    if (jobContentData != null)
                    {
                        var updatejob = new JobModel
                        {
                            j_id = job.j_id,
                            name = job.name,
                            MBTI = job.MBTI,
                            HOL = job.HOL,
                            oneDown = jobContentData.wageSet.oneDown ?? job.oneDown,
                            oneTothree = jobContentData.wageSet.oneTothree ?? job.oneTothree,
                            threeTofive = jobContentData.wageSet.threeTofive ?? job.threeTofive,
                            fiveToten = jobContentData.wageSet.fiveToten ?? job.fiveToten,
                            tenTofifteen = jobContentData.wageSet.tenTofifteen ?? job.tenTofifteen,
                            fifteenUp = jobContentData.wageSet.fifteenUp ?? job.fifteenUp,
                            skill = jobContentData.toolSet.skills ?? job.skill,
                            certificate = jobContentData.toolSet.certificates ?? job.certificate,
                            tool = jobContentData.toolSet.tools ?? job.tool
                        };
                        _dao.UpdateJobContent(updatejob);
                    }
                }
                else
                {
                    return new ResultViewModel("API 請求失敗") { };
                }
            }
            return new ResultViewModel() { };
        }
        catch (Exception ex)
        {
            return new ResultViewModel(ex.Message) { };
        }
    }
    #endregion
    #region 新增課程
    public ResultViewModel CreateLesson(CreateFileImportModel createLesson)
    {
        try
        {
            if (createLesson.file != null && createLesson.file.Length > 0)
            {
                string fileExtension = Path.GetExtension(createLesson.file.FileName).ToLower();
                if (fileExtension == ".xlsx" || fileExtension == ".xls")
                {
                    var filePath = Path.GetTempFileName();

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        createLesson.file.CopyTo(stream);
                    }

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    List<LessonModel> lessons = new List<LessonModel>();

                    using (var package = new ExcelPackage(new FileInfo(filePath)))
                    {
                        var worksheet = package.Workbook.Worksheets[0];

                        if (worksheet == null)
                        {
                            throw new Exception("Excel檔案中無工作表");
                        }

                        for (int row = 2; row <= worksheet.Dimension.Rows; row++)
                        {
                            var lesson = new LessonModel
                            {
                                name = worksheet.Cells[row, 1].Value?.ToString(),
                                time = worksheet.Cells[row, 2].Value?.ToString(),
                                content = worksheet.Cells[row, 3].Value?.ToString(),
                                http = worksheet.Cells[row, 4].Value?.ToString(),
                                address = worksheet.Cells[row, 5].Value?.ToString(),
                            };

                            if (!string.IsNullOrEmpty(lesson.name))
                            {
                                lessons.Add(lesson);
                                _dao.CreateLesson(lesson);
                            }
                        }
                    }

                    File.Delete(filePath);

                    return new ResultViewModel() { };
                }
                else
                {
                    return new ResultViewModel("文件不是有效的 Excel 檔案") { };
                }
            }
            else
            {
                return new ResultViewModel("未提供檔案") { };
            }
        }
        catch (Exception ex)
        {
            return new ResultViewModel(ex.Message) { };
        }
    }
    #endregion
    #region 新增證照
    public ResultViewModel CreateCertificate(CreateFileImportModel createCertificate)
    {
        try
        {
            if (createCertificate.file != null && createCertificate.file.Length > 0)
            {
                string fileExtension = Path.GetExtension(createCertificate.file.FileName).ToLower();
                if (fileExtension == ".xlsx" || fileExtension == ".xls")
                {
                    var filePath = Path.GetTempFileName();

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        createCertificate.file.CopyTo(stream);
                    }

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    List<CertificateModel> certificates = new List<CertificateModel>();

                    using (var package = new ExcelPackage(new FileInfo(filePath)))
                    {
                        var worksheet = package.Workbook.Worksheets[0];

                        if (worksheet == null)
                        {
                            throw new Exception("Excel檔案中無工作表");
                        }

                        for (int row = 2; row <= worksheet.Dimension.Rows; row++)
                        {
                            var certificate = new CertificateModel
                            {
                                name = worksheet.Cells[row, 1].Value?.ToString(),
                                rank = worksheet.Cells[row, 2].Value?.ToString(),
                                type = worksheet.Cells[row, 3].Value?.ToString(),
                                address = worksheet.Cells[row, 4].Value?.ToString(),
                                http = worksheet.Cells[row, 5].Value?.ToString(),
                                applyTime = worksheet.Cells[row, 6].Value?.ToString(),
                                testTIme = worksheet.Cells[row, 7].Value?.ToString(),
                            };

                            if (!string.IsNullOrEmpty(certificate.name))
                            {
                                certificates.Add(certificate);
                                _dao.CreateCertificate(certificate);
                            }
                        }
                    }

                    File.Delete(filePath);

                    return new ResultViewModel() { };
                }
                else
                {
                    return new ResultViewModel("文件不是有效的 Excel 檔案") { };
                }
            }
            else
            {
                return new ResultViewModel("未提供檔案") { };
            }
        }
        catch (Exception ex)
        {
            return new ResultViewModel(ex.Message) { };
        }
    }
    #endregion
    #region 新增補助
    public ResultViewModel CreateSubsidy(CreateFileImportModel createSubsidy)
    {
        try
        {
            if (createSubsidy.file != null && createSubsidy.file.Length > 0)
            {
                string fileExtension = Path.GetExtension(createSubsidy.file.FileName).ToLower();
                if (fileExtension == ".xlsx" || fileExtension == ".xls")
                {
                    var filePath = Path.GetTempFileName();

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        createSubsidy.file.CopyTo(stream);
                    }

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    List<SubsidyModel> subsidys = new List<SubsidyModel>();

                    using (var package = new ExcelPackage(new FileInfo(filePath)))
                    {
                        var worksheet = package.Workbook.Worksheets[0];

                        if (worksheet == null)
                        {
                            throw new Exception("Excel檔案中無工作表");
                        }

                        for (int row = 2; row <= worksheet.Dimension.Rows; row++)
                        {
                            var subsidy = new SubsidyModel
                            {
                                name = worksheet.Cells[row, 1].Value?.ToString(),
                                money = Convert.ToInt32(worksheet.Cells[row, 2].Value),
                            };

                            if (!string.IsNullOrEmpty(subsidy.name))
                            {
                                subsidys.Add(subsidy);
                                _dao.CreateSubsidy(subsidy);
                            }
                        }
                    }

                    File.Delete(filePath);

                    return new ResultViewModel() { };
                }
                else
                {
                    return new ResultViewModel("文件不是有效的 Excel 檔案") { };
                }
            }
            else
            {
                return new ResultViewModel("未提供檔案") { };
            }
        }
        catch (Exception ex)
        {
            return new ResultViewModel(ex.Message) { };
        }
    }
    #endregion
    #region 取得課程列表
    public ResultViewModel<List<LessonViewModel>> LessonList()
    {
        try
        {
            var lessonlist = _dao.LessonList();

            var result = lessonlist.Select(lesson => new LessonViewModel
            {
                l_id = lesson.l_id,
                name = lesson.name,
                time = lesson.time,
                content = lesson.content,
                http = lesson.http,
                address = lesson.address,
            }).ToList();
            return new ResultViewModel<List<LessonViewModel>>() { result = result };
        }
        catch (Exception ex)
        {
            return new ResultViewModel<List<LessonViewModel>>(ex.Message) { };
        }
    }
    #endregion
    #region 取得證照列表
    public ResultViewModel<List<CertificateViewModel>> CertificateList()
    {
        try
        {
            var certificatelist = _dao.CertificateList();

            var result = certificatelist.Select(certificate => new CertificateViewModel
            {
                c_id = certificate.c_id,
                name = certificate.name,
                rank = certificate.rank,
                type = certificate.type,
                applyTime = certificate.applyTime,
                testTIme = certificate.testTIme,
                http = certificate.http,
                address = certificate.address,
            }).ToList();
            return new ResultViewModel<List<CertificateViewModel>>() { result = result };
        }
        catch (Exception ex)
        {
            return new ResultViewModel<List<CertificateViewModel>>(ex.Message) { };
        }
    }
    #endregion
    #region 取得補助列表
    public ResultViewModel<List<SubsidyViewModel>> SubsidyList()
    {
        try
        {
            var subsidylist = _dao.SubsidyList();

            var result = subsidylist.Select(subsidy => new SubsidyViewModel
            {
                s_id = subsidy.s_id,
                name = subsidy.name,
                money = subsidy.money,
            }).ToList();
            return new ResultViewModel<List<SubsidyViewModel>>() { result = result };
        }
        catch (Exception ex)
        {
            return new ResultViewModel<List<SubsidyViewModel>>(ex.Message) { };
        }
    }
    #endregion
}