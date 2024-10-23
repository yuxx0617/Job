using Job.AppDBContext;
using Job.Dao.Interface;
using Job.Model;
using Job.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Job.Dao;

public class PredictDao : IPredictDao
{
    private readonly AppDbContext _context;

    public PredictDao(AppDbContext context)
    {
        _context = context;
    }
    #region 新增公司類型
    public void AddTypeStatusData(TypeStatusModel typeStatusModel)
    {
        _context.TypeStatus.Add(typeStatusModel);
        _context.SaveChanges();
    }
    #endregion
    #region 取得所有公司類型
    public List<TypeStatusModel> TypeStatusList()
    {
        var today = DateTime.Now.ToString("yyyyMM");
        return _context.TypeStatus.Where(c => c.date == today).ToList();
    }
    #endregion
    #region 新增職缺
    public async Task AddVacancies(VacancieModel vacancieModel)
    {
        await _context.Vacancie.AddAsync(vacancieModel);
        await _context.SaveChangesAsync();
    }
    #endregion
    #region 取得所有職缺類型
    public List<VacancieModel> VacancieList()
    {
        var today = DateTime.Now.ToString("yyyyMM");
        return _context.Vacancie.Where(c => c.date.Contains(today)).ToList();
    }
    #endregion
    #region 編輯選項
    public void EditSeletion(SeletionModel seletionModel)
    {
        var seletion = _context.Seletion.FirstOrDefault(t => t.ts_id == seletionModel.ts_id);
        if (seletion != null)
        {
            _context.Entry(seletionModel).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
    #endregion
    #region 刪除選項
    public void DeleteSeletion(int ts_id)
    {
        var seletionModel = _context.Seletion.FirstOrDefault(t => t.ts_id == ts_id);

        if (seletionModel != null)
        {
            _context.Seletion.Remove(seletionModel);
            _context.SaveChanges();
        }
    }
    #endregion
    #region 取得單一題目的所有選項
    public List<int> GetTestSeletionID(int t_id)
    {

        var seletion = _context.Seletion.Where(t => t.t_id == t_id).ToList();
        List<int> seletionIds = seletion.Select(s => s.ts_id).ToList();
        return seletionIds;
    }
    #endregion
    #region 取得選項
    public SeletionModel GetSeletion(int ts_id)
    {
        return _context.Seletion.FirstOrDefault(a => a.ts_id == ts_id);
    }
    #endregion
    public List<SeletionModel> GetTestSeletion(int t_id)
    {

        var seletion = _context.Seletion.Where(t => t.t_id == t_id).ToList();
        return seletion;
    }
    #region 取得經營/行政/總務資料
    public List<VacancieModel> VacancieList01()
    {
        return _context.Vacancie.Where(v => v.type == 1).ToList();
    }
    #endregion
    #region 取得業務/貿易/銷售資料
    public List<VacancieModel> VacancieList02()
    {
        return _context.Vacancie.Where(v => v.type == 2).ToList();
    }
    #endregion
    #region 取得人資/法務/智財資料
    public List<VacancieModel> VacancieList03()
    {
        return _context.Vacancie.Where(v => v.type == 3).ToList();
    }
    #endregion
    #region 取得財務/金融/保險資料
    public List<VacancieModel> VacancieList04()
    {
        return _context.Vacancie.Where(v => v.type == 4).ToList();
    }
    #endregion
    #region 取得廣告/公關/設計資料
    public List<VacancieModel> VacancieList05()
    {
        return _context.Vacancie.Where(v => v.type == 5).ToList();
    }
    #endregion
    #region 取得客服/門市資料
    public List<VacancieModel> VacancieList06()
    {
        return _context.Vacancie.Where(v => v.type == 6).ToList();
    }
    #endregion
    #region 取得工程/研發/生技資料
    public List<VacancieModel> VacancieList07()
    {
        return _context.Vacancie.Where(v => v.type == 7).ToList();
    }
    #endregion
    #region 取得資訊/軟體/系統資料
    public List<VacancieModel> VacancieList08()
    {
        return _context.Vacancie.Where(v => v.type == 8).ToList();
    }
    #endregion
    #region 取得品管/製造/環衛資料
    public List<VacancieModel> VacancieList09()
    {
        return _context.Vacancie.Where(v => v.type == 9).ToList();
    }
    #endregion
    #region 取得技術/維修/操作資料
    public List<VacancieModel> VacancieList10()
    {
        return _context.Vacancie.Where(v => v.type == 10).ToList();
    }
    #endregion
    #region 取得營建/製圖/施作資料
    public List<VacancieModel> VacancieList11()
    {
        return _context.Vacancie.Where(v => v.type == 11).ToList();
    }
    #endregion
    #region 取得新聞/出版/印刷資料
    public List<VacancieModel> VacancieList12()
    {
        return _context.Vacancie.Where(v => v.type == 12).ToList();
    }
    #endregion
    #region 取得傳播/娛樂/藝術資料
    public List<VacancieModel> VacancieList13()
    {
        return _context.Vacancie.Where(v => v.type == 13).ToList();
    }
    #endregion
    #region 取得教育/學術/研究資料
    public List<VacancieModel> VacancieList14()
    {
        return _context.Vacancie.Where(v => v.type == 14).ToList();
    }
    #endregion
    #region 取得物流/運輸/資材資料
    public List<VacancieModel> VacancieList15()
    {
        return _context.Vacancie.Where(v => v.type == 15).ToList();
    }
    #endregion
    #region 取得旅遊/餐飲/休閒資料
    public List<VacancieModel> VacancieList16()
    {
        return _context.Vacancie.Where(v => v.type == 16).ToList();
    }
    #endregion
    #region 取得醫療/美容/保健資料
    public List<VacancieModel> VacancieList17()
    {
        return _context.Vacancie.Where(v => v.type == 17).ToList();
    }
    #endregion
    #region 取得保全/軍警消資料
    public List<VacancieModel> VacancieList18()
    {
        return _context.Vacancie.Where(v => v.type == 18).ToList();
    }
    #endregion
    #region 取得清潔/家事/托育資料
    public List<VacancieModel> VacancieList19()
    {
        return _context.Vacancie.Where(v => v.type == 19).ToList();
    }
    #endregion
    #region 取得農林漁牧相關資料
    public List<VacancieModel> VacancieList20()
    {
        return _context.Vacancie.Where(v => v.type == 20).ToList();
    }
    #endregion
    #region 取得行銷/企劃/專案資料
    public List<VacancieModel> VacancieList21()
    {
        return _context.Vacancie.Where(v => v.type == 21).ToList();
    }
    #endregion
    #region 取得其他職類資料
    public List<VacancieModel> VacancieList22()
    {
        return _context.Vacancie.Where(v => v.type == 22).ToList();
    }
    #endregion
    #region 取得經營/行政/總務資料
    public List<TypeStatusModel> TypeStatusList01()
    {
        return _context.TypeStatus.Where(v => v.businessType.Contains("顧問")
        || v.businessType.Contains("管理")
        || v.businessType.Contains("買賣")
        || v.businessType.Contains("貿易")
        || v.businessType.Contains("經理")).ToList();
    }
    #endregion
    #region 取得業務/貿易/銷售資料
    public List<TypeStatusModel> TypeStatusList02()
    {
        return _context.TypeStatus.Where(v => v.businessType.Contains("業務")
        || v.businessType.Contains("零售")
        || v.businessType.Contains("貿易")
        || v.businessType.Contains("批發")).ToList();
    }
    #endregion
    #region 取得人資/法務/智財資料
    public List<TypeStatusModel> TypeStatusList03()
    {
        return _context.TypeStatus.Where(v => v.businessType.Contains("法務")
        || v.businessType.Contains("人力")
        || v.businessType.Contains("仲介")
        || v.businessType.Contains("智慧財產")
        || v.businessType.Contains("法律")
        || v.businessType.Contains("律師")).ToList();
    }
    #endregion
    #region 取得財務/金融/保險資料
    public List<TypeStatusModel> TypeStatusList04()
    {
        return _context.TypeStatus.Where(v => v.businessType.Contains("投資")
        || v.businessType.Contains("金融")
        || v.businessType.Contains("保險")
        || v.businessType.Contains("財務")
        || v.businessType.Contains("支付")).ToList();
    }
    #endregion
    #region 取得廣告/公關/設計資料
    public List<TypeStatusModel> TypeStatusList05()
    {
        return _context.TypeStatus.Where(v => v.businessType.Contains("廣告")
        || v.businessType.Contains("設計")
        || v.businessType.Contains("公關")
        || v.businessType.Contains("行銷")
        || v.businessType.Contains("媒體")).ToList();
    }
    #endregion
    #region 取得客服/門市資料
    public List<TypeStatusModel> TypeStatusList06()
    {
        return _context.TypeStatus.Where(v => v.businessType.Contains("零售")
        || v.businessType.Contains("客服")).ToList();
    }
    #endregion
    #region 取得工程/研發/生技資料
    public List<TypeStatusModel> TypeStatusList07()
    {
        return _context.TypeStatus.Where(v => v.businessType.Contains("工程")
        || v.businessType.Contains("研究")
        || v.businessType.Contains("能源")
        || v.businessType.Contains("發展")
        || v.businessType.Contains("生物")
        || v.businessType.Contains("科技")).ToList();
    }
    #endregion
    #region 取得資訊/軟體/系統資料
    public List<TypeStatusModel> TypeStatusList08()
    {
        return _context.TypeStatus.Where(v => v.businessType.Contains("資訊")
        || v.businessType.Contains("軟體")
        || v.businessType.Contains("系統")
        || v.businessType.Contains("AI")
        || v.businessType.Contains("資料")
        || v.businessType.Contains("電子")).ToList();
    }
    #endregion
    #region 取得品管/製造/環衛資料
    public List<TypeStatusModel> TypeStatusList09()
    {
        return _context.TypeStatus.Where(v => v.businessType.Contains("製造")
        || v.businessType.Contains("回收")
        || v.businessType.Contains("廢棄")
        || v.businessType.Contains("環境")
        || v.businessType.Contains("品管")
        || v.businessType.Contains("品質")).ToList();
    }
    #endregion
    #region 取得技術/維修/操作資料
    public List<TypeStatusModel> TypeStatusList10()
    {
        return _context.TypeStatus.Where(v => v.businessType.Contains("修理")
        || v.businessType.Contains("維修")
        || v.businessType.Contains("操作")
        || v.businessType.Contains("技術")).ToList();
    }
    #endregion
    #region 取得營建/製圖/施作資料
    public List<TypeStatusModel> TypeStatusList11()
    {
        return _context.TypeStatus.Where(v => v.businessType.Contains("裝潢")
        || v.businessType.Contains("建材")
        || v.businessType.Contains("不動產")
        || v.businessType.Contains("衛浴")
        || v.businessType.Contains("營建")
        || v.businessType.Contains("建設")
        || v.businessType.Contains("建築")
        || v.businessType.Contains("開發")).ToList();
    }
    #endregion
    #region 取得新聞/出版/印刷資料
    public List<TypeStatusModel> TypeStatusList12()
    {
        return _context.TypeStatus.Where(v => v.businessType.Contains("媒體")
        || v.businessType.Contains("新聞")
        || v.businessType.Contains("印刷")
        || v.businessType.Contains("出版")
        || v.businessType.Contains("書")).ToList();
    }
    #endregion
    #region 取得傳播/娛樂/藝術資料
    public List<TypeStatusModel> TypeStatusList13()
    {
        return _context.TypeStatus.Where(v => v.businessType.Contains("攝影")
        || v.businessType.Contains("演藝")
        || v.businessType.Contains("娛樂")
        || v.businessType.Contains("藝文")
        || v.businessType.Contains("音樂")
        || v.businessType.Contains("影音")).ToList();
    }
    #endregion
    #region 取得教育/學術/研究資料
    public List<TypeStatusModel> TypeStatusList14()
    {
        return _context.TypeStatus.Where(v => v.businessType.Contains("教育")
        || v.businessType.Contains("學術")
        || v.businessType.Contains("研究")
        || v.businessType.Contains("研製")
        || v.businessType.Contains("研發")
        || v.businessType.Contains("學習")).ToList();
    }
    #endregion
    #region 取得物流/運輸/資材資料
    public List<TypeStatusModel> TypeStatusList15()
    {
        return _context.TypeStatus.Where(v => v.businessType.Contains("倉儲")
        || v.businessType.Contains("物流")
        || v.businessType.Contains("運輸")
        || v.businessType.Contains("資材")
        || v.businessType.Contains("貨櫃")
        || v.businessType.Contains("倉庫")).ToList();
    }
    #endregion
    #region 取得旅遊/餐飲/休閒資料
    public List<TypeStatusModel> TypeStatusList16()
    {
        return _context.TypeStatus.Where(v => v.businessType.Contains("飲料")
        || v.businessType.Contains("餐飲")
        || v.businessType.Contains("餐館")
        || v.businessType.Contains("旅遊")
        || v.businessType.Contains("休閒")
        || v.businessType.Contains("旅館")
        || v.businessType.Contains("遊樂")).ToList();
    }
    #endregion
    #region 取得醫療/美容/保健資料
    public List<TypeStatusModel> TypeStatusList17()
    {
        return _context.TypeStatus.Where(v => v.businessType.Contains("醫療")
        || v.businessType.Contains("美容")
        || v.businessType.Contains("保健")
        || v.businessType.Contains("藥")
        || v.businessType.Contains("按摩")).ToList();
    }
    #endregion
    #region 取得保全/軍警消資料
    public List<TypeStatusModel> TypeStatusList18()
    {
        return _context.TypeStatus.Where(v => v.businessType.Contains("保全")
        || v.businessType.Contains("安全")
        || v.businessType.Contains("軍")
        || v.businessType.Contains("警")
        || v.businessType.Contains("消防")).ToList();
    }
    #endregion
    #region 取得清潔/家事/托育資料
    public List<TypeStatusModel> TypeStatusList19()
    {
        return _context.TypeStatus.Where(v => v.businessType.Contains("保母")
        || v.businessType.Contains("托育")
        || v.businessType.Contains("長照")
        || v.businessType.Contains("家事")
        || v.businessType.Contains("清潔")).ToList();
    }
    #endregion
    #region 取得農林漁牧相關資料
    public List<TypeStatusModel> TypeStatusList20()
    {
        return _context.TypeStatus.Where(v => v.businessType.Contains("果")
        || v.businessType.Contains("農")
        || v.businessType.Contains("漁")
        || v.businessType.Contains("洋")
        || v.businessType.Contains("林")
        || v.businessType.Contains("牧")
        || v.businessType.Contains("禽")
        || v.businessType.Contains("牲")
        || v.businessType.Contains("海")).ToList();
    }
    #endregion
    #region 取得行銷/企劃/專案資料
    public List<TypeStatusModel> TypeStatusList21()
    {
        return _context.TypeStatus.Where(v => v.businessType.Contains("經理")
        || v.businessType.Contains("行銷")
        || v.businessType.Contains("顧問")
        || v.businessType.Contains("企劃")
        || v.businessType.Contains("專案")
        || v.businessType.Contains("市場")
        || v.businessType.Contains("展覽")).ToList();
    }
    #endregion
    #region 取得其他職類資料
    public List<TypeStatusModel> TypeStatusList22()
    {
        return _context.TypeStatus.Where(v => v.businessType.Contains("其他")
        || v.businessType.Contains("除許可業務外")).ToList();
    }
    #endregion

    #region 新增預測資料
    public void CreatePredict(PredictModel predictModel)
    {
        _context.Predict.Add(predictModel);
        _context.SaveChanges();
    }
    #endregion
    #region 取得預測資料列表
    public List<PredictModel> Predictlist()
    {
        return _context.Predict.ToList();
    }
    #endregion

}