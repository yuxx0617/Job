using Job.util;
using Job.Dao.Interface;
using Job.ImportModel;
using Job.Model;
using Job.Service.Interface;
using Job.util;
using Job.ViewModel;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http.Abstractions;
using OfficeOpenXml;
using Job.util.Job.util;

namespace Job.Service;

public class UserAnswerService : IUserAnswerService
{
    private readonly TestService _testService;
    private readonly IUserAnswerDao _dao;
    private readonly appSetting _appSetting;
    private string account;
    private string role;
    private IHttpContextAccessor _HttpContextAccessor;
    public UserAnswerService(IUserAnswerDao dao, IOptions<appSetting> appSetting, IHttpContextAccessor HttpContextAccessor, TestService testService)
    {
        _testService = testService;
        _dao = dao;
        _appSetting = appSetting.Value;
        this._HttpContextAccessor = HttpContextAccessor ??
                        throw new ArgumentNullException(nameof(HttpContextAccessor));

        tokenEnCode TokenEnCode = new tokenEnCode(HttpContextAccessor.HttpContext);
        var PayLoad = TokenEnCode.GetPayLoad();
        this.account = PayLoad["account"].ToString();
        this.role = PayLoad["role"].ToString();
    }
    #region 新增回答並計算
    public ResultViewModel<AnswerIdViewModel> CreateAnswerAndCount(CreateAnswerImportModel createAnswer)
    {
        try
        {
            var today = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var createanswer = new UserAnswerModel
            {
                ua_goodList1 = createAnswer.ua_goodList1,
                ua_goodList2 = createAnswer.ua_goodList2,
                ua_goodList3 = createAnswer.ua_goodList3,
                ua_badList1 = createAnswer.ua_badList1,
                ua_badList2 = createAnswer.ua_badList2,
                ua_badList3 = createAnswer.ua_badList3,
                testTime = DateTime.Parse(today),
                account = this.account,
            };
            var answer = _dao.CreateAnswer(createanswer);

            CountGood1(answer.ua_id);
            CountGood2(answer.ua_id);
            CountGood3(answer.ua_id);
            CountBad1(answer.ua_id);
            CountBad2(answer.ua_id);
            CountBad3(answer.ua_id);
            CountResult(answer.ua_id);
            var result = new AnswerIdViewModel
            {
                ua_id = answer.ua_id,
            };
            return new ResultViewModel<AnswerIdViewModel>() { result = result };
        }
        catch (Exception ex)
        {
            return new ResultViewModel<AnswerIdViewModel>(ex.Message);
        }
    }
    #endregion
    #region 取得測驗結果
    public ResultViewModel<AnswerViewModel> GetAnswerResult(int ua_id)
    {
        try
        {


            var answer = _dao.GetAnswer(ua_id);

            var result = new AnswerViewModel
            {
                ua_id = answer.ua_id,
                ua_goodList1 = answer.ua_goodList1,
                ua_goodList2 = answer.ua_goodList2,
                ua_goodList3 = answer.ua_goodList3,
                ua_badList1 = answer.ua_badList1,
                ua_badList2 = answer.ua_badList2,
                ua_badList3 = answer.ua_badList3,
                testTime = answer.testTime,
                account = answer.account,
                MBTI_Result = answer.MBTI_Result,
                HOL_Result = answer.HOL_Result,
                test_Result = answer.test_Result,
                count_MBTI_E = answer.count_MBTI_E,
                count_MBTI_F = answer.count_MBTI_F,
                count_MBTI_I = answer.count_MBTI_I,
                count_MBTI_J = answer.count_MBTI_J,
                count_MBTI_N = answer.count_MBTI_N,
                count_MBTI_P = answer.count_MBTI_P,
                count_MBTI_S = answer.count_MBTI_S,
                count_MBTI_T = answer.count_MBTI_T,
                count_HOL_A = answer.count_HOL_A,
                count_HOL_C = answer.count_HOL_C,
                count_HOL_E = answer.count_HOL_E,
                count_HOL_I = answer.count_HOL_I,
                count_HOL_R = answer.count_HOL_R,
                count_HOL_S = answer.count_HOL_S,
            };
            return new ResultViewModel<AnswerViewModel>() { result = result };
        }
        catch (Exception ex)
        {
            return new ResultViewModel<AnswerViewModel>(ex.Message) { };
        }
    }
    #endregion
    #region 測驗結果列表
    public ResultViewModel<List<AnswerViewModel>> AnswerResultList()
    {
        try
        {


            var answerlist = _dao.AnswerList();

            var result = answerlist.Select(answer => new AnswerViewModel
            {
                ua_id = answer.ua_id,
                ua_goodList1 = answer.ua_goodList1,
                ua_goodList2 = answer.ua_goodList2,
                ua_goodList3 = answer.ua_goodList3,
                ua_badList1 = answer.ua_badList1,
                ua_badList2 = answer.ua_badList2,
                ua_badList3 = answer.ua_badList3,
                testTime = answer.testTime,
                account = answer.account,
                MBTI_Result = answer.MBTI_Result,
                HOL_Result = answer.HOL_Result,
                test_Result = answer.test_Result,
                count_MBTI_E = answer.count_MBTI_E,
                count_MBTI_F = answer.count_MBTI_F,
                count_MBTI_I = answer.count_MBTI_I,
                count_MBTI_J = answer.count_MBTI_J,
                count_MBTI_N = answer.count_MBTI_N,
                count_MBTI_P = answer.count_MBTI_P,
                count_MBTI_S = answer.count_MBTI_S,
                count_MBTI_T = answer.count_MBTI_T,
                count_HOL_A = answer.count_HOL_A,
                count_HOL_C = answer.count_HOL_C,
                count_HOL_E = answer.count_HOL_E,
                count_HOL_I = answer.count_HOL_I,
                count_HOL_R = answer.count_HOL_R,
                count_HOL_S = answer.count_HOL_S,
            }).ToList();
            return new ResultViewModel<List<AnswerViewModel>>() { result = result };
        }
        catch (Exception ex)
        {
            return new ResultViewModel<List<AnswerViewModel>>(ex.Message) { };
        }
    }
    #endregion
    #region 計算並更新選項分數
    public ResultViewModel CountGood1(int ua_id)
    {
        try
        {
            var answer = _dao.GetAnswer(ua_id);
            var good1 = answer.ua_goodList1.Split(',')
                            .Select(ts_id => ts_id.Trim())
                            .ToList();
            int num_MBTI_E = 0;
            int num_MBTI_I = 0;
            int num_MBTI_N = 0;
            int num_MBTI_S = 0;
            int num_MBTI_F = 0;
            int num_MBTI_T = 0;
            int num_MBTI_P = 0;
            int num_MBTI_J = 0;
            int num_HOL_A = 0;
            int num_HOL_C = 0;
            int num_HOL_E = 0;
            int num_HOL_I = 0;
            int num_HOL_R = 0;
            int num_HOL_S = 0;

            if (good1 != null && good1.Count > 0)
            {
                foreach (var ts_id in good1)
                {
                    var gooditem1 = _testService.GetSeletion(int.Parse(ts_id));
                    if (gooditem1.result.MBTI_E) num_MBTI_E += 3;
                    if (gooditem1.result.MBTI_I) num_MBTI_I += 3;
                    if (gooditem1.result.MBTI_N) num_MBTI_N += 3;
                    if (gooditem1.result.MBTI_S) num_MBTI_S += 3;
                    if (gooditem1.result.MBTI_F) num_MBTI_F += 3;
                    if (gooditem1.result.MBTI_T) num_MBTI_T += 3;
                    if (gooditem1.result.MBTI_P) num_MBTI_P += 3;
                    if (gooditem1.result.MBTI_J) num_MBTI_J += 3;
                    if (gooditem1.result.HOL_A) num_HOL_A += 3;
                    if (gooditem1.result.HOL_C) num_HOL_C += 3;
                    if (gooditem1.result.HOL_E) num_HOL_E += 3;
                    if (gooditem1.result.HOL_I) num_HOL_I += 3;
                    if (gooditem1.result.HOL_R) num_HOL_R += 3;
                    if (gooditem1.result.HOL_S) num_HOL_S += 3;
                }
                var updategrade = new UserAnswerModel()
                {
                    ua_id = ua_id,
                    count_MBTI_E = answer.count_MBTI_E + num_MBTI_E,
                    count_MBTI_F = answer.count_MBTI_F + num_MBTI_F,
                    count_MBTI_I = answer.count_MBTI_I + num_MBTI_I,
                    count_MBTI_J = answer.count_MBTI_J + num_MBTI_J,
                    count_MBTI_N = answer.count_MBTI_N + num_MBTI_N,
                    count_MBTI_P = answer.count_MBTI_P + num_MBTI_P,
                    count_MBTI_S = answer.count_MBTI_S + num_MBTI_S,
                    count_MBTI_T = answer.count_MBTI_T + num_MBTI_T,
                    count_HOL_A = answer.count_HOL_A + num_HOL_A,
                    count_HOL_C = answer.count_HOL_C + num_HOL_C,
                    count_HOL_E = answer.count_HOL_E + num_HOL_E,
                    count_HOL_I = answer.count_HOL_I + num_HOL_I,
                    count_HOL_R = answer.count_HOL_R + num_HOL_R,
                    count_HOL_S = answer.count_HOL_S + num_HOL_S,
                };
                _dao.UpdateGrade(updategrade);

                return new ResultViewModel { };
            }
            return new ResultViewModel("沒有答案");
        }
        catch (Exception ex)
        {
            return new ResultViewModel(ex.Message);
        }
    }
    public ResultViewModel CountGood2(int ua_id)
    {
        try
        {
            var answer = _dao.GetAnswer(ua_id);
            var good2 = answer.ua_goodList2.Split(',')
                            .Select(ts_id => ts_id.Trim())
                            .ToList();
            int num_MBTI_E = 0;
            int num_MBTI_I = 0;
            int num_MBTI_N = 0;
            int num_MBTI_S = 0;
            int num_MBTI_F = 0;
            int num_MBTI_T = 0;
            int num_MBTI_P = 0;
            int num_MBTI_J = 0;
            int num_HOL_A = 0;
            int num_HOL_C = 0;
            int num_HOL_E = 0;
            int num_HOL_I = 0;
            int num_HOL_R = 0;
            int num_HOL_S = 0;

            if (good2 != null && good2.Count > 0)
            {
                foreach (var ts_id in good2)
                {
                    var gooditem2 = _testService.GetSeletion(int.Parse(ts_id));
                    if (gooditem2.result.MBTI_E) num_MBTI_E += 2;
                    if (gooditem2.result.MBTI_I) num_MBTI_I += 2;
                    if (gooditem2.result.MBTI_N) num_MBTI_N += 2;
                    if (gooditem2.result.MBTI_S) num_MBTI_S += 2;
                    if (gooditem2.result.MBTI_F) num_MBTI_F += 2;
                    if (gooditem2.result.MBTI_T) num_MBTI_T += 2;
                    if (gooditem2.result.MBTI_P) num_MBTI_P += 2;
                    if (gooditem2.result.MBTI_J) num_MBTI_J += 2;
                    if (gooditem2.result.HOL_A) num_HOL_A += 2;
                    if (gooditem2.result.HOL_C) num_HOL_C += 2;
                    if (gooditem2.result.HOL_E) num_HOL_E += 2;
                    if (gooditem2.result.HOL_I) num_HOL_I += 2;
                    if (gooditem2.result.HOL_R) num_HOL_R += 2;
                    if (gooditem2.result.HOL_S) num_HOL_S += 2;
                }
                var updategrade = new UserAnswerModel()
                {
                    ua_id = ua_id,
                    count_MBTI_E = answer.count_MBTI_E + num_MBTI_E,
                    count_MBTI_F = answer.count_MBTI_F + num_MBTI_F,
                    count_MBTI_I = answer.count_MBTI_I + num_MBTI_I,
                    count_MBTI_J = answer.count_MBTI_J + num_MBTI_J,
                    count_MBTI_N = answer.count_MBTI_N + num_MBTI_N,
                    count_MBTI_P = answer.count_MBTI_P + num_MBTI_P,
                    count_MBTI_S = answer.count_MBTI_S + num_MBTI_S,
                    count_MBTI_T = answer.count_MBTI_T + num_MBTI_T,
                    count_HOL_A = answer.count_HOL_A + num_HOL_A,
                    count_HOL_C = answer.count_HOL_C + num_HOL_C,
                    count_HOL_E = answer.count_HOL_E + num_HOL_E,
                    count_HOL_I = answer.count_HOL_I + num_HOL_I,
                    count_HOL_R = answer.count_HOL_R + num_HOL_R,
                    count_HOL_S = answer.count_HOL_S + num_HOL_S,
                };

                _dao.UpdateGrade(updategrade);

                return new ResultViewModel { };
            }
            return new ResultViewModel("沒有答案");
        }
        catch (Exception ex)
        {
            return new ResultViewModel(ex.Message);
        }
    }
    public ResultViewModel CountGood3(int ua_id)
    {
        try
        {
            var answer = _dao.GetAnswer(ua_id);
            var good3 = answer.ua_goodList3.Split(',')
                            .Select(ts_id => ts_id.Trim())
                            .ToList();
            int num_MBTI_E = 0;
            int num_MBTI_I = 0;
            int num_MBTI_N = 0;
            int num_MBTI_S = 0;
            int num_MBTI_F = 0;
            int num_MBTI_T = 0;
            int num_MBTI_P = 0;
            int num_MBTI_J = 0;
            int num_HOL_A = 0;
            int num_HOL_C = 0;
            int num_HOL_E = 0;
            int num_HOL_I = 0;
            int num_HOL_R = 0;
            int num_HOL_S = 0;

            if (good3 != null && good3.Count > 0)
            {
                foreach (var ts_id in good3)
                {
                    var gooditem3 = _testService.GetSeletion(int.Parse(ts_id));
                    if (gooditem3.result.MBTI_E) num_MBTI_E += 1;
                    if (gooditem3.result.MBTI_I) num_MBTI_I += 1;
                    if (gooditem3.result.MBTI_N) num_MBTI_N += 1;
                    if (gooditem3.result.MBTI_S) num_MBTI_S += 1;
                    if (gooditem3.result.MBTI_F) num_MBTI_F += 1;
                    if (gooditem3.result.MBTI_T) num_MBTI_T += 1;
                    if (gooditem3.result.MBTI_P) num_MBTI_P += 1;
                    if (gooditem3.result.MBTI_J) num_MBTI_J += 1;
                    if (gooditem3.result.HOL_A) num_HOL_A += 1;
                    if (gooditem3.result.HOL_C) num_HOL_C += 1;
                    if (gooditem3.result.HOL_E) num_HOL_E += 1;
                    if (gooditem3.result.HOL_I) num_HOL_I += 1;
                    if (gooditem3.result.HOL_R) num_HOL_R += 1;
                    if (gooditem3.result.HOL_S) num_HOL_S += 1;
                }
                var updategrade = new UserAnswerModel()
                {
                    ua_id = ua_id,
                    count_MBTI_E = answer.count_MBTI_E + num_MBTI_E,
                    count_MBTI_F = answer.count_MBTI_F + num_MBTI_F,
                    count_MBTI_I = answer.count_MBTI_I + num_MBTI_I,
                    count_MBTI_J = answer.count_MBTI_J + num_MBTI_J,
                    count_MBTI_N = answer.count_MBTI_N + num_MBTI_N,
                    count_MBTI_P = answer.count_MBTI_P + num_MBTI_P,
                    count_MBTI_S = answer.count_MBTI_S + num_MBTI_S,
                    count_MBTI_T = answer.count_MBTI_T + num_MBTI_T,
                    count_HOL_A = answer.count_HOL_A + num_HOL_A,
                    count_HOL_C = answer.count_HOL_C + num_HOL_C,
                    count_HOL_E = answer.count_HOL_E + num_HOL_E,
                    count_HOL_I = answer.count_HOL_I + num_HOL_I,
                    count_HOL_R = answer.count_HOL_R + num_HOL_R,
                    count_HOL_S = answer.count_HOL_S + num_HOL_S,
                };
                _dao.UpdateGrade(updategrade);


                return new ResultViewModel { };
            }
            return new ResultViewModel("沒有答案");
        }
        catch (Exception ex)
        {
            return new ResultViewModel(ex.Message);
        }
    }
    public ResultViewModel CountBad1(int ua_id)
    {
        try
        {
            var answer = _dao.GetAnswer(ua_id);
            var bad1 = answer.ua_badList1.Split(',')
                            .Select(ts_id => ts_id.Trim())
                            .ToList();
            int num_MBTI_E = 0;
            int num_MBTI_I = 0;
            int num_MBTI_N = 0;
            int num_MBTI_S = 0;
            int num_MBTI_F = 0;
            int num_MBTI_T = 0;
            int num_MBTI_P = 0;
            int num_MBTI_J = 0;
            int num_HOL_A = 0;
            int num_HOL_C = 0;
            int num_HOL_E = 0;
            int num_HOL_I = 0;
            int num_HOL_R = 0;
            int num_HOL_S = 0;

            if (bad1 != null && bad1.Count > 0)
            {
                foreach (var ts_id in bad1)
                {
                    var baditem1 = _testService.GetSeletion(int.Parse(ts_id));
                    if (baditem1.result.MBTI_E) num_MBTI_E += 3;
                    if (baditem1.result.MBTI_I) num_MBTI_I += 3;
                    if (baditem1.result.MBTI_N) num_MBTI_N += 3;
                    if (baditem1.result.MBTI_S) num_MBTI_S += 3;
                    if (baditem1.result.MBTI_F) num_MBTI_F += 3;
                    if (baditem1.result.MBTI_T) num_MBTI_T += 3;
                    if (baditem1.result.MBTI_P) num_MBTI_P += 3;
                    if (baditem1.result.MBTI_J) num_MBTI_J += 3;
                    if (baditem1.result.HOL_A) num_HOL_A += 3;
                    if (baditem1.result.HOL_C) num_HOL_C += 3;
                    if (baditem1.result.HOL_E) num_HOL_E += 3;
                    if (baditem1.result.HOL_I) num_HOL_I += 3;
                    if (baditem1.result.HOL_R) num_HOL_R += 3;
                    if (baditem1.result.HOL_S) num_HOL_S += 3;
                }
                var updategrade = new UserAnswerModel()
                {
                    ua_id = ua_id,
                    count_MBTI_E = answer.count_MBTI_E - num_MBTI_E,
                    count_MBTI_F = answer.count_MBTI_F - num_MBTI_F,
                    count_MBTI_I = answer.count_MBTI_I - num_MBTI_I,
                    count_MBTI_J = answer.count_MBTI_J - num_MBTI_J,
                    count_MBTI_N = answer.count_MBTI_N - num_MBTI_N,
                    count_MBTI_P = answer.count_MBTI_P - num_MBTI_P,
                    count_MBTI_S = answer.count_MBTI_S - num_MBTI_S,
                    count_MBTI_T = answer.count_MBTI_T - num_MBTI_T,
                    count_HOL_A = answer.count_HOL_A - num_HOL_A,
                    count_HOL_C = answer.count_HOL_C - num_HOL_C,
                    count_HOL_E = answer.count_HOL_E - num_HOL_E,
                    count_HOL_I = answer.count_HOL_I - num_HOL_I,
                    count_HOL_R = answer.count_HOL_R - num_HOL_R,
                    count_HOL_S = answer.count_HOL_S - num_HOL_S,
                };
                _dao.UpdateGrade(updategrade);


                return new ResultViewModel { };
            }
            return new ResultViewModel("沒有答案");
        }
        catch (Exception ex)
        {
            return new ResultViewModel(ex.Message);
        }
    }
    public ResultViewModel CountBad2(int ua_id)
    {
        try
        {
            var answer = _dao.GetAnswer(ua_id);
            var bad2 = answer.ua_badList2.Split(',')
                            .Select(ts_id => ts_id.Trim())
                            .ToList();
            int num_MBTI_E = 0;
            int num_MBTI_I = 0;
            int num_MBTI_N = 0;
            int num_MBTI_S = 0;
            int num_MBTI_F = 0;
            int num_MBTI_T = 0;
            int num_MBTI_P = 0;
            int num_MBTI_J = 0;
            int num_HOL_A = 0;
            int num_HOL_C = 0;
            int num_HOL_E = 0;
            int num_HOL_I = 0;
            int num_HOL_R = 0;
            int num_HOL_S = 0;

            if (bad2 != null && bad2.Count > 0)
            {
                foreach (var ts_id in bad2)
                {
                    var baditem2 = _testService.GetSeletion(int.Parse(ts_id));
                    if (baditem2.result.MBTI_E) num_MBTI_E += 2;
                    if (baditem2.result.MBTI_I) num_MBTI_I += 2;
                    if (baditem2.result.MBTI_N) num_MBTI_N += 2;
                    if (baditem2.result.MBTI_S) num_MBTI_S += 2;
                    if (baditem2.result.MBTI_F) num_MBTI_F += 2;
                    if (baditem2.result.MBTI_T) num_MBTI_T += 2;
                    if (baditem2.result.MBTI_P) num_MBTI_P += 2;
                    if (baditem2.result.MBTI_J) num_MBTI_J += 2;
                    if (baditem2.result.HOL_A) num_HOL_A += 2;
                    if (baditem2.result.HOL_C) num_HOL_C += 2;
                    if (baditem2.result.HOL_E) num_HOL_E += 2;
                    if (baditem2.result.HOL_I) num_HOL_I += 2;
                    if (baditem2.result.HOL_R) num_HOL_R += 2;
                    if (baditem2.result.HOL_S) num_HOL_S += 2;
                }
                var updategrade = new UserAnswerModel()
                {
                    ua_id = ua_id,
                    count_MBTI_E = answer.count_MBTI_E - num_MBTI_E,
                    count_MBTI_F = answer.count_MBTI_F - num_MBTI_F,
                    count_MBTI_I = answer.count_MBTI_I - num_MBTI_I,
                    count_MBTI_J = answer.count_MBTI_J - num_MBTI_J,
                    count_MBTI_N = answer.count_MBTI_N - num_MBTI_N,
                    count_MBTI_P = answer.count_MBTI_P - num_MBTI_P,
                    count_MBTI_S = answer.count_MBTI_S - num_MBTI_S,
                    count_MBTI_T = answer.count_MBTI_T - num_MBTI_T,
                    count_HOL_A = answer.count_HOL_A - num_HOL_A,
                    count_HOL_C = answer.count_HOL_C - num_HOL_C,
                    count_HOL_E = answer.count_HOL_E - num_HOL_E,
                    count_HOL_I = answer.count_HOL_I - num_HOL_I,
                    count_HOL_R = answer.count_HOL_R - num_HOL_R,
                    count_HOL_S = answer.count_HOL_S - num_HOL_S,
                };

                _dao.UpdateGrade(updategrade);

                return new ResultViewModel { };
            }
            return new ResultViewModel("沒有答案");
        }
        catch (Exception ex)
        {
            return new ResultViewModel(ex.Message);
        }
    }
    public ResultViewModel CountBad3(int ua_id)
    {
        try
        {
            var answer = _dao.GetAnswer(ua_id);
            var bad3 = answer.ua_badList3.Split(',')
                            .Select(ts_id => ts_id.Trim())
                            .ToList();
            int num_MBTI_E = 0;
            int num_MBTI_I = 0;
            int num_MBTI_N = 0;
            int num_MBTI_S = 0;
            int num_MBTI_F = 0;
            int num_MBTI_T = 0;
            int num_MBTI_P = 0;
            int num_MBTI_J = 0;
            int num_HOL_A = 0;
            int num_HOL_C = 0;
            int num_HOL_E = 0;
            int num_HOL_I = 0;
            int num_HOL_R = 0;
            int num_HOL_S = 0;

            if (bad3 != null && bad3.Count > 0)
            {
                foreach (var ts_id in bad3)
                {
                    var baditem3 = _testService.GetSeletion(int.Parse(ts_id));
                    if (baditem3.result.MBTI_E) num_MBTI_E += 1;
                    if (baditem3.result.MBTI_I) num_MBTI_I += 1;
                    if (baditem3.result.MBTI_N) num_MBTI_N += 1;
                    if (baditem3.result.MBTI_S) num_MBTI_S += 1;
                    if (baditem3.result.MBTI_F) num_MBTI_F += 1;
                    if (baditem3.result.MBTI_T) num_MBTI_T += 1;
                    if (baditem3.result.MBTI_P) num_MBTI_P += 1;
                    if (baditem3.result.MBTI_J) num_MBTI_J += 1;
                    if (baditem3.result.HOL_A) num_HOL_A += 1;
                    if (baditem3.result.HOL_C) num_HOL_C += 1;
                    if (baditem3.result.HOL_E) num_HOL_E += 1;
                    if (baditem3.result.HOL_I) num_HOL_I += 1;
                    if (baditem3.result.HOL_R) num_HOL_R += 1;
                    if (baditem3.result.HOL_S) num_HOL_S += 1;
                }
                var updategrade = new UserAnswerModel()
                {
                    ua_id = ua_id,
                    count_MBTI_E = answer.count_MBTI_E - num_MBTI_E,
                    count_MBTI_F = answer.count_MBTI_F - num_MBTI_F,
                    count_MBTI_I = answer.count_MBTI_I - num_MBTI_I,
                    count_MBTI_J = answer.count_MBTI_J - num_MBTI_J,
                    count_MBTI_N = answer.count_MBTI_N - num_MBTI_N,
                    count_MBTI_P = answer.count_MBTI_P - num_MBTI_P,
                    count_MBTI_S = answer.count_MBTI_S - num_MBTI_S,
                    count_MBTI_T = answer.count_MBTI_T - num_MBTI_T,
                    count_HOL_A = answer.count_HOL_A - num_HOL_A,
                    count_HOL_C = answer.count_HOL_C - num_HOL_C,
                    count_HOL_E = answer.count_HOL_E - num_HOL_E,
                    count_HOL_I = answer.count_HOL_I - num_HOL_I,
                    count_HOL_R = answer.count_HOL_R - num_HOL_R,
                    count_HOL_S = answer.count_HOL_S - num_HOL_S,
                };

                _dao.UpdateGrade(updategrade);

                return new ResultViewModel { };
            }
            return new ResultViewModel("沒有答案");
        }
        catch (Exception ex)
        {
            return new ResultViewModel(ex.Message);
        }
    }

    #endregion
    #region 計算並並更新結果
    public ResultViewModel CountResult(int ua_id)
    {
        try
        {
            var answer = _dao.GetAnswer(ua_id);
            var MBTIresult = String.Empty;
            var HOLresult = String.Empty;

            if (answer.count_MBTI_I > answer.count_MBTI_E) MBTIresult = "I";
            else MBTIresult = "E";
            if (answer.count_MBTI_S > answer.count_MBTI_N) MBTIresult += "S";
            else MBTIresult += "N";
            if (answer.count_MBTI_T > answer.count_MBTI_F) MBTIresult += "T";
            else MBTIresult += "F";
            if (answer.count_MBTI_P > answer.count_MBTI_J) MBTIresult += "P";
            else MBTIresult += "J";

            var holScores = new Dictionary<string, int>
{
    { "A", answer.count_HOL_A },
    { "C", answer.count_HOL_C },
    { "R", answer.count_HOL_R },
    { "I", answer.count_HOL_I },
    { "E", answer.count_HOL_E },
    { "S", answer.count_HOL_S }
};

            var top3HOL = holScores
                            .OrderByDescending(kvp => kvp.Value)
                            .Take(3)
                            .ToList();

            foreach (var hol in top3HOL)
            {
                HOLresult += hol.Key;
            }

            var job = _dao.GetJobResult(MBTIresult, HOLresult);
            List<int> joblist = job.Select(s => s.j_id).ToList();

            var updateresult = new UserAnswerModel()
            {
                ua_id = ua_id,
                MBTI_Result = MBTIresult,
                HOL_Result = HOLresult,
                test_Result = string.Join(",", joblist)
            };
            _dao.UpdateResult(updateresult);

            return new ResultViewModel { };
        }
        catch (Exception ex)
        {
            return new ResultViewModel(ex.Message);
        }
    }
    #endregion
}