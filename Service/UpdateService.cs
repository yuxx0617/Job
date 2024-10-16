using Job.Dao.Interface;
using Job.ImportModel;
using Job.Model;
using Job.Service.Interface;
using Job.util;
using Job.ViewModel;
using Microsoft.Extensions.Options;
using OfficeOpenXml;

namespace Job.Service;

public class UpdateService : IUpdateService
{
    private readonly IUpdateDao _dao;
    private readonly appSetting _appSetting;
    public UpdateService(IUpdateDao dao, IOptions<appSetting> appSetting)
    {
        _dao = dao;
        _appSetting = appSetting.Value;
    }
    #region 新增工作
    public ResultViewModel CreateJob(CreateJobImportModel createJob)
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
}