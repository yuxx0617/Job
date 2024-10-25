using Job.util;
using Job.Dao.Interface;
using Job.ImportModel;
using Job.Model;
using Job.Service.Interface;
using Job.ViewModel;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http.Abstractions;
using OfficeOpenXml;
using Job.util.Job.util;

namespace Job.Service;

public class ActRecordService : IActRecordService
{
    private readonly IActRecordDao _dao;
    private readonly appSetting _appSetting;
    private string account;
    private string role;
    private IHttpContextAccessor _HttpContextAccessor;
    public ActRecordService(IActRecordDao dao, IOptions<appSetting> appSetting, IHttpContextAccessor HttpContextAccessor)
    {
        _dao = dao;
        _appSetting = appSetting.Value;
        this._HttpContextAccessor = HttpContextAccessor ??
                        throw new ArgumentNullException(nameof(HttpContextAccessor));

        // tokenEnCode TokenEnCode = new tokenEnCode(HttpContextAccessor.HttpContext);
        // var PayLoad = TokenEnCode.GetPayLoad();
        // this.account = PayLoad["account"].ToString();
        // this.role = PayLoad["role"].ToString();
    }
    #region 新增活動紀錄
    public ResultViewModel CreateActRecord(string activity, string content)
    {
        try
        {
            var today = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var createact = new ActRecordModel
            {
                account = this.account,
                activity = activity,
                content = content,
                date = DateTime.Parse(today),
            };
            _dao.CreateActRecord(createact);
            return new ResultViewModel() { };
        }
        catch (Exception ex)
        {
            return new ResultViewModel(ex.Message);
        }
    }
    #endregion
    #region 取得活動紀錄列表
    public ResultViewModel<List<ActRecordViewModel>> ActRecordList()
    {
        try
        {
            var actlist = _dao.ActRecordList();

            var result = actlist.Select(act => new ActRecordViewModel
            {
                r_id = act.r_id,
                account = act.account,
                activity = act.activity,
                content = act.content,
                date = act.date,
            }).ToList();

            return new ResultViewModel<List<ActRecordViewModel>>() { result = result };
        }
        catch (Exception ex)
        {
            return new ResultViewModel<List<ActRecordViewModel>>(ex.Message) { };
        }
    }
    #endregion
    #region 取得單一紀錄
    public ResultViewModel<ActRecordViewModel> GetActRecord(int r_id)
    {
        try
        {
            var actRecord = _dao.GetActRecord(r_id);

            var result = new ActRecordViewModel
            {
                r_id = actRecord.r_id,
                account = actRecord.account,
                activity = actRecord.activity,
                content = actRecord.content,
                date = actRecord.date,
            };
            return new ResultViewModel<ActRecordViewModel>() { result = result };
        }
        catch (Exception ex)
        {
            return new ResultViewModel<ActRecordViewModel>(ex.Message) { };
        }
    }
    #endregion
}