using Job.Dao.Interface;
using Job.ImportModel;
using Job.Model;
using Job.Service.Interface;
using Job.util;
using Job.ViewModel;
using Microsoft.Extensions.Options;

namespace Job.Service;

public class TestService : ITestService
{
    private readonly ITestDao _dao;
    private readonly appSetting _appSetting;
    public TestService(ITestDao dao, IOptions<appSetting> appSetting)
    {
        _dao = dao;
        _appSetting = appSetting.Value;
    }
    #region 新增題目
    public ResultViewModel CreateTest(CreateTestImportModel createTest)
    {
        try
        {
            string formattedDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");

            var BgImgFileName = createTest.bgImg != null ?
              formattedDateTime + "_" + Guid.NewGuid().ToString() + Path.GetExtension(createTest.bgImg.FileName) :
              null;
            var BgImgFolderPath = Path.Combine(this._appSetting.bg_IMG);
            if (!Directory.Exists(BgImgFolderPath))
            {
                Directory.CreateDirectory(BgImgFolderPath);
            }

            var AnimateImgFileName = formattedDateTime + "_" + Guid.NewGuid().ToString() + Path.GetExtension(createTest.animateImg.FileName);
            var AnimateImgFolderPath = Path.Combine(this._appSetting.animate_IMG);
            if (!Directory.Exists(AnimateImgFolderPath))
            {
                Directory.CreateDirectory(AnimateImgFolderPath);
            }

            var createtest = new TestModel
            {
                question = createTest.question,
                bgImg = BgImgFileName,
                bgColor = createTest.bgColor,
                animateImg = AnimateImgFileName
            };
            _dao.CreateTest(createtest);

            if (BgImgFileName != null)
            {
                var path = Path.Combine(BgImgFolderPath, BgImgFileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    createTest.bgImg.CopyTo(stream);
                }
            }
            if (AnimateImgFileName != null)
            {
                var path = Path.Combine(AnimateImgFolderPath, AnimateImgFileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    createTest.animateImg.CopyTo(stream);
                }
            }

            return new ResultViewModel() { };

        }
        catch (Exception ex)
        {
            return new ResultViewModel(ex.Message);
        }
    }
    #endregion
    #region 編輯題目
    public ResultViewModel EditTest(EditTestImportModel editTest)
    {
        try
        {
            string formattedDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");

            var BgImgFileName = editTest.bgImg != null ?
              formattedDateTime + "_" + Guid.NewGuid().ToString() + Path.GetExtension(editTest.bgImg.FileName) :
              null;
            var BgImgFolderPath = Path.Combine(this._appSetting.bg_IMG);
            if (!Directory.Exists(BgImgFolderPath))
            {
                Directory.CreateDirectory(BgImgFolderPath);
            }

            var AnimateImgFileName = editTest.animateImg != null ?
              formattedDateTime + "_" + Guid.NewGuid().ToString() + Path.GetExtension(editTest.animateImg.FileName) :
              null;
            var AnimateImgFolderPath = Path.Combine(this._appSetting.animate_IMG);
            if (!Directory.Exists(AnimateImgFolderPath))
            {
                Directory.CreateDirectory(AnimateImgFolderPath);
            }

            UpdateTest(editTest.t_id);
            var seletions = _dao.GetTestSeletionID(editTest.t_id);
            var test = _dao.GetTest(editTest.t_id);
            var edittest = new TestModel
            {
                t_id = editTest.t_id,
                question = editTest.question ?? test.question,
                bgImg = BgImgFileName ?? test.bgImg,
                bgColor = editTest.bgColor ?? test.bgColor,
                animateImg = AnimateImgFileName ?? test.animateImg,
                ts_idList = string.Join(",", seletions)
            };
            _dao.EditTest(edittest);

            if (BgImgFileName != null)
            {
                var path = Path.Combine(BgImgFolderPath, BgImgFileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    editTest.bgImg.CopyTo(stream);
                }
            }
            if (AnimateImgFileName != null)
            {
                var path = Path.Combine(AnimateImgFolderPath, AnimateImgFileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    editTest.animateImg.CopyTo(stream);
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
    #region 刪除題目
    public ResultViewModel DeleteTest(DeleteTestImportModel deleteTest)
    {
        try
        {
            var test = _dao.GetTest(deleteTest.t_id);
            if (test != null)
            {
                var ts_ids = test.ts_idList.Split(',').Select(ts_id => ts_id.Trim()).Where(ts_id => !string.IsNullOrEmpty(test.ts_idList)).ToList();
                if (ts_ids != null)
                {
                    foreach (var ts_id in ts_ids)
                    {
                        DeleteSeletion(int.Parse(ts_id));
                    }
                }

                _dao.DeleteTest(deleteTest.t_id);
                return new ResultViewModel() { };
            }
            else
            {
                return new ResultViewModel("無此題目") { };
            }
        }
        catch (Exception ex)
        {
            return new ResultViewModel(ex.Message) { };
        }
    }
    #endregion
    #region 取得所有題目
    public ResultViewModel<List<TestViewModel>> TestList()
    {
        try
        {
            var testlist = _dao.TestList();

            var result = testlist.Select(test => new TestViewModel
            {
                t_id = test.t_id,
                question = test.question,
                bgImg = test.bgImg,
                bgColor = test.bgColor,
                animateImg = test.animateImg,
                ts_idList = test.ts_idList,
            }).ToList();
            return new ResultViewModel<List<TestViewModel>>() { result = result };
        }
        catch (Exception ex)
        {
            return new ResultViewModel<List<TestViewModel>>(ex.Message);
        }
    }
    #endregion
    #region 取得單一題目
    public ResultViewModel<TestViewModel> GetTest(GetTestImportModel getTest)
    {
        try
        {
            var test = _dao.GetTest(getTest.t_id);

            var result = new TestViewModel
            {
                t_id = test.t_id,
                question = test.question,
                bgImg = test.bgImg,
                bgColor = test.bgColor,
                animateImg = test.animateImg,
                ts_idList = test.ts_idList,
            };
            return new ResultViewModel<TestViewModel>() { result = result };
        }
        catch (Exception ex)
        {
            return new ResultViewModel<TestViewModel>(ex.Message) { };
        }
    }
    #endregion
    #region 更新題目的選項
    public ResultViewModel UpdateTest(int t_id)
    {
        try
        {
            var seletions = _dao.GetTestSeletionID(t_id);
            var updatetest = new TestModel
            {
                t_id = t_id,
                ts_idList = string.Join(",", seletions),
            };
            _dao.UpdateTest(updatetest);
            return new ResultViewModel() { };
        }
        catch (Exception ex)
        {
            return new ResultViewModel(ex.Message) { };
        }
    }
    #endregion
    #region 新增選項
    public ResultViewModel CreateSeletion(CreateSeletionImportModel createSeletion)
    {
        try
        {
            var createseletion = new SeletionModel
            {
                seletion = createSeletion.seletion,
                MBTI_I = createSeletion.MBTI_I,
                MBTI_E = createSeletion.MBTI_E,
                MBTI_F = createSeletion.MBTI_F,
                MBTI_T = createSeletion.MBTI_T,
                MBTI_N = createSeletion.MBTI_N,
                MBTI_S = createSeletion.MBTI_S,
                MBTI_P = createSeletion.MBTI_P,
                MBTI_J = createSeletion.MBTI_J,
                HOL_A = createSeletion.HOL_A,
                HOL_C = createSeletion.HOL_C,
                HOL_E = createSeletion.HOL_E,
                HOL_I = createSeletion.HOL_I,
                HOL_R = createSeletion.HOL_R,
                HOL_S = createSeletion.HOL_S,
                t_id = createSeletion.t_id,
            };
            _dao.CreateSeletion(createseletion);
            UpdateTest(createseletion.t_id);
            return new ResultViewModel() { };
        }
        catch (Exception ex)
        {
            return new ResultViewModel(ex.Message) { };
        }
    }
    #endregion
    #region 編輯選項
    public ResultViewModel EditSeletion(EditSeletionImportModel editSeletion)
    {
        try
        {
            var seletion = _dao.GetSeletion(editSeletion.ts_id);
            var editseletion = new SeletionModel
            {
                ts_id = editSeletion.ts_id,
                seletion = editSeletion.seletion ?? seletion.seletion,
                MBTI_I = editSeletion.MBTI_I ?? seletion.MBTI_I,
                MBTI_E = editSeletion.MBTI_E ?? seletion.MBTI_E,
                MBTI_F = editSeletion.MBTI_F ?? seletion.MBTI_F,
                MBTI_T = editSeletion.MBTI_T ?? seletion.MBTI_T,
                MBTI_N = editSeletion.MBTI_N ?? seletion.MBTI_N,
                MBTI_S = editSeletion.MBTI_S ?? seletion.MBTI_S,
                MBTI_P = editSeletion.MBTI_P ?? seletion.MBTI_P,
                MBTI_J = editSeletion.MBTI_J ?? seletion.MBTI_J,
                HOL_A = editSeletion.HOL_A ?? seletion.HOL_A,
                HOL_C = editSeletion.HOL_C ?? seletion.HOL_C,
                HOL_E = editSeletion.HOL_E ?? seletion.HOL_E,
                HOL_I = editSeletion.HOL_I ?? seletion.HOL_I,
                HOL_R = editSeletion.HOL_R ?? seletion.HOL_R,
                HOL_S = editSeletion.HOL_S ?? seletion.HOL_S,
            };
            _dao.EditSeletion(editseletion);
            return new ResultViewModel() { };
        }
        catch (Exception ex)
        {
            return new ResultViewModel(ex.Message) { };
        }
    }
    #endregion
    #region 刪除選項
    public ResultViewModel DeleteSeletion(int ts_id)
    {
        try
        {
            var seletion = _dao.GetSeletion(ts_id);
            _dao.DeleteSeletion(ts_id);
            UpdateTest(seletion.t_id);
            return new ResultViewModel() { };
        }
        catch (Exception ex)
        {
            return new ResultViewModel(ex.Message) { };
        }
    }
    #endregion
    #region 取得題目的所有選項內容
    public ResultViewModel<List<TestSeletionViewModel>> GetTestSeletion(GetTestSeletionImportModel getTestSeletion)
    {
        try
        {
            var testseletion = _dao.GetTestSeletion(getTestSeletion.t_id);

            var result = testseletion.Select(seletion => new TestSeletionViewModel
            {
                seletion = seletion.seletion,
                MBTI_I = seletion.MBTI_I,
                MBTI_E = seletion.MBTI_E,
                MBTI_F = seletion.MBTI_F,
                MBTI_T = seletion.MBTI_T,
                MBTI_N = seletion.MBTI_N,
                MBTI_S = seletion.MBTI_S,
                MBTI_P = seletion.MBTI_P,
                MBTI_J = seletion.MBTI_J,
                HOL_A = seletion.HOL_A,
                HOL_C = seletion.HOL_C,
                HOL_E = seletion.HOL_E,
                HOL_I = seletion.HOL_I,
                HOL_R = seletion.HOL_R,
                HOL_S = seletion.HOL_S,
                t_id = seletion.t_id,
            }).ToList();
            return new ResultViewModel<List<TestSeletionViewModel>>() { result = result };
        }
        catch (Exception ex)
        {
            return new ResultViewModel<List<TestSeletionViewModel>>(ex.Message);
        }
    }

    #endregion
}