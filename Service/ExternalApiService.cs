using System;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CsvHelper;
using Job.Dao.Interface;
using Job.Model;
using Job.Service.Interface;
using Job.util;
using Job.ViewModel;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ExternalApiService : IExternalApiService
{
    private readonly IExternalApiDao _dao;
    private readonly HttpClient _httpClient;
    private readonly appSetting _appSetting;


    public ExternalApiService(HttpClient httpClient, IOptions<appSetting> appSetting, IExternalApiDao dao)
    {
        _httpClient = httpClient;
        _appSetting = appSetting.Value;
        _dao = dao;
    }
    #region 公司資料每日解散異動查詢
    // public async Task<ResultViewModel<List<CompanyDisbandViewModel>>> GetCompanyDisbandData()
    // {
    //     try
    //     {
    //         var today = DateTime.Now;
    //         var rocYear = today.Year - 1911;
    //         var formattedDate = $"{rocYear}{today:MMdd}";

    //         var url = $"https://data.gcis.nat.gov.tw/od/data/api/561D23B6-5EA7-4FF4-A78D-A617ED64BC64?$format=json&$filter=Change_Of_Approval_Data eq {formattedDate}&$skip=0&$top=500";

    //         HttpResponseMessage response = await _httpClient.GetAsync(url);

    //         if (response.IsSuccessStatusCode)
    //         {
    //             var jsonResponse = await response.Content.ReadAsStringAsync();
    //             if (jsonResponse == null || !jsonResponse.Any())
    //             {
    //                 return new ResultViewModel<List<CompanyDisbandViewModel>>() { result = new List<CompanyDisbandViewModel>() };
    //             }
    //             else
    //             {
    //                 var companyData = JsonConvert.DeserializeObject<List<CompanyDisbandViewModel>>(jsonResponse);
    //                 return new ResultViewModel<List<CompanyDisbandViewModel>>() { result = companyData };
    //             }
    //         }
    //         else
    //         {
    //             return new ResultViewModel<List<CompanyDisbandViewModel>>("API 請求失敗") { };
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         return new ResultViewModel<List<CompanyDisbandViewModel>>(ex.Message) { };
    //     }
    // }
    public async Task<ResultViewModel<List<CompanyDisbandViewModel>>> GetCompanyDisbandData(string formattedDate)
    {
        try
        {
            var url = $"https://data.gcis.nat.gov.tw/od/data/api/561D23B6-5EA7-4FF4-A78D-A617ED64BC64?$format=json&$filter=Change_Of_Approval_Data eq {formattedDate}&$skip=0&$top=500";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (jsonResponse == null || !jsonResponse.Any())
                {
                    return new ResultViewModel<List<CompanyDisbandViewModel>>() { result = new List<CompanyDisbandViewModel>() };
                }
                else
                {
                    var companyData = JsonConvert.DeserializeObject<List<CompanyDisbandViewModel>>(jsonResponse);
                    return new ResultViewModel<List<CompanyDisbandViewModel>>() { result = companyData };
                }
            }
            else
            {
                return new ResultViewModel<List<CompanyDisbandViewModel>>("API 請求失敗") { };
            }
        }
        catch (Exception ex)
        {
            return new ResultViewModel<List<CompanyDisbandViewModel>>(ex.Message) { };
        }
    }
    #endregion
    #region 公司類別查詢
    public async Task<ResultViewModel<List<CompanyViewModel>>> GetCompanyData(string BusinessAccount)
    {
        try
        {
            var url = $"https://data.gcis.nat.gov.tw/od/data/api/236EE382-4942-41A9-BD03-CA0709025E7C?$format=json&$filter=Business_Accounting_NO eq {BusinessAccount}&$skip=0&$top=50";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                if (jsonResponse == null || !jsonResponse.Any())
                {
                    return new ResultViewModel<List<CompanyViewModel>>() { result = new List<CompanyViewModel>() };
                }
                else
                {
                    var jsonArray = JArray.Parse(jsonResponse);
                    var companyDataList = new List<CompanyViewModel>();

                    foreach (var item in jsonArray)
                    {
                        var companyViewModel = new CompanyViewModel
                        {
                            Business_Accounting_NO = item["Business_Accounting_NO"]?.ToString(),
                            Company_Name = item["Company_Name"]?.ToString(),
                            Company_Status = item["Company_Status"]?.ToString(),
                            Company_Status_Desc = item["Company_Status_Desc"]?.ToString(),
                            Company_Setup_Date = item["Company_Setup_Date"]?.ToString(),
                            Cmp_Business = item["Cmp_Business"]?.Select(business => new BusinessTypeViewModel
                            {
                                Business_Seq_NO = business["Business_Seq_NO"]?.ToString(),
                                Business_Item = business["Business_Item"]?.ToString(),
                                Business_Item_Desc = business["Business_Item_Desc"]?.ToString()
                            }).ToList()
                        };

                        companyDataList.Add(companyViewModel);
                    }

                    return new ResultViewModel<List<CompanyViewModel>>() { result = companyDataList };
                }
            }
            else
            {
                return new ResultViewModel<List<CompanyViewModel>>("API 請求失敗") { };
            }
        }
        catch (Exception ex)
        {
            return new ResultViewModel<List<CompanyViewModel>>(ex.Message) { };
        }
    }
    #endregion
    #region 分公司資料每日廢止異動查詢
    public async Task<ResultViewModel<List<CompanyAbolishViewModel>>> GetBranchCompanyAbolishData(string formattedDate)
    {
        try
        {
            var url = $"https://data.gcis.nat.gov.tw/od/data/api/50F22B9D-5A21-4E0B-835B-721459C1182A?$format=json&$filter=Chg_App_Date eq {formattedDate}&$skip=0&$top=500";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (jsonResponse == null || !jsonResponse.Any())
                {
                    return new ResultViewModel<List<CompanyAbolishViewModel>>() { result = new List<CompanyAbolishViewModel>() };
                }
                else
                {
                    var companyData = JsonConvert.DeserializeObject<List<CompanyAbolishViewModel>>(jsonResponse);
                    return new ResultViewModel<List<CompanyAbolishViewModel>>() { result = companyData };
                }
            }
            else
            {
                return new ResultViewModel<List<CompanyAbolishViewModel>>("API 請求失敗") { };
            }
        }
        catch (Exception ex)
        {
            return new ResultViewModel<List<CompanyAbolishViewModel>>(ex.Message) { };
        }
    }
    #endregion
    #region 商業資料每日歇業異動查詢
    public async Task<ResultViewModel<List<StopBusinessViewModel>>> GetStopBusinessData(string formattedDate)
    {
        try
        {
            var url = $"https://data.gcis.nat.gov.tw/od/data/api/632775D2-2FAA-4ECA-81DA-89C74349AB8B?$format=json&$filter=Business_Last_Change_Date eq {formattedDate}&$skip=0&$top=500";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (jsonResponse == null || !jsonResponse.Any())
                {
                    return new ResultViewModel<List<StopBusinessViewModel>>() { result = new List<StopBusinessViewModel>() };
                }
                else
                {
                    var companyData = JsonConvert.DeserializeObject<List<StopBusinessViewModel>>(jsonResponse);
                    return new ResultViewModel<List<StopBusinessViewModel>>() { result = companyData };
                }
            }
            else
            {
                return new ResultViewModel<List<StopBusinessViewModel>>("API 請求失敗") { };
            }
        }
        catch (Exception ex)
        {
            return new ResultViewModel<List<StopBusinessViewModel>>(ex.Message) { };
        }
    }
    #endregion
    #region 商業類別查詢
    public async Task<ResultViewModel<List<BusinessViewModel>>> GetBusinessData(string President)
    {
        try
        {
            var url = $"https://data.gcis.nat.gov.tw/od/data/api/426D5542-5F05-43EB-83F9-F1300F14E1F1?$format=json&$filter=President_No eq {President}&$skip=0&$top=50";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                if (jsonResponse == null || !jsonResponse.Any())
                {
                    return new ResultViewModel<List<BusinessViewModel>>() { result = new List<BusinessViewModel>() };
                }
                else
                {
                    var jsonArray = JArray.Parse(jsonResponse);
                    var businessDataList = new List<BusinessViewModel>();

                    foreach (var item in jsonArray)
                    {
                        if (item == null)
                        {
                            continue;
                        }

                        var businessViewModel = new BusinessViewModel
                        {
                            President_No = item["President_No"]?.ToString(),
                            Business_Name = item["Business_Name"]?.ToString(),
                            Business_Current_Status = item["Business_Current_Status"]?.ToString(),
                            Business_Current_Status_Desc = item["Business_Current_Status_Desc"]?.ToString(),
                            Business_Setup_Approve_Date = item["Business_Setup_Approve_Date"]?.ToString(),
                            Business_Organization_Type_Desc = item["Business_Organization_Type_Desc"]?.ToString(),
                            Agency = item["Agency"]?.ToString(),
                            Agency_Desc = item["Agency_Desc"]?.ToString(),
                            Business_Address = item["Business_Address"]?.ToString(),
                            Business_Item_Old = item["Business_Item_Old"] is JArray businessItems
                                ? businessItems.Select(business => new BusinessTypeViewModel
                                {
                                    Business_Seq_NO = business["Business_Seq_NO"]?.ToString(),
                                    Business_Item = business["Business_Item"]?.ToString(),
                                    Business_Item_Desc = business["Business_Item_Desc"]?.ToString()
                                }).ToList()
                                : new List<BusinessTypeViewModel>()
                        };

                        businessDataList.Add(businessViewModel);
                    }
                    return new ResultViewModel<List<BusinessViewModel>>() { result = businessDataList };
                }
            }
            else
            {
                return new ResultViewModel<List<BusinessViewModel>>("API 請求失敗") { };
            }
        }
        catch (Exception ex)
        {
            return new ResultViewModel<List<BusinessViewModel>>(ex.Message) { };
        }
    }
    #endregion
    #region 查公司解散類別
    // public async Task<ResultViewModel<List<TypeStatusViewModel>>> UpdateCompanyDisbandType()
    // {
    //     try
    //     {
    //                     var today = DateTime.Now;
    // var rocYear = today.Year - 1911;
    // var formattedDate = $"{rocYear}{today:MMdd}";
    //         var companyData = await GetCompanyDisbandData();
    //         foreach (var company in companyData.result)
    //         {
    //             var compamyType = await GetCompanyData(company.Business_Accounting_NO);
    //             if (compamyType != null)
    //             {
    //                 foreach (var status in compamyType.result)
    //                 {
    //                     foreach (var type in status.Cmp_Business)
    //                     {
    //                         var addModel = new TypeStatusModel()
    //                         {
    //                             businessNum = company.Business_Accounting_NO,
    //                             companyName = company.Company_Name,
    //                             companyStatus = company.Company_Status_Desc,
    //                             businessType = type.Business_Item_Desc,
    //                             date = today
    //                         };
    //                         if (addModel != null)
    //                         {
    //                             _dao.AddTypeStatusData(addModel);
    //                         }
    //                         else
    //                         {
    //                             continue;
    //                         }
    //                     }
    //                 }
    //             }
    //         }
    //         var typeStatuss = _dao.TypeStatusList();
    //         var result = typeStatuss.Select(com => new TypeStatusViewModel
    //         {
    //             c_id = com.c_id,
    //             businessNum = com.businessNum,
    //             companyName = com.companyName,
    //             companyStatus = com.companyStatus,
    //             businessType = com.businessType,
    //             date = com.date
    //         }).ToList();

    //         return new ResultViewModel<List<TypeStatusViewModel>>() { result = result };
    //     }
    //     catch (Exception ex)
    //     {
    //         return new ResultViewModel<List<TypeStatusViewModel>>(ex.Message) { };
    //     }
    // }
    public async Task<ResultViewModel<List<TypeStatusViewModel>>> UpdateCompanyDisbandType()
    {
        try
        {
            // 設定日期範圍的起始日期（112年1月1日 -> 2023年1月1日）
            var startDate = new DateTime(2023, 6, 30);
            var today = DateTime.Now;

            // 將民國年轉換為字串
            var startRocYear = startDate.Year - 1911;
            var todayRocYear = today.Year - 1911;

            // 迴圈從 startDate 到 today，每天執行一次
            for (var datetime = startDate; datetime <= today; datetime = datetime.AddDays(1))
            {
                // 轉換當前日期為民國年格式的字串，格式為 "yyyMMdd"
                var currentRocYear = datetime.Year - 1911;
                var formattedDate = $"{currentRocYear}{datetime:MMdd}";
                var forDate = datetime.ToString("yyyyMMdd");

                // 獲取公司解散資料，傳遞民國年格式的日期字串
                var companyData = await GetCompanyDisbandData(formattedDate);

                foreach (var company in companyData.result)
                {
                    var compamyType = await GetCompanyData(company.Business_Accounting_NO);
                    if (compamyType != null)
                    {
                        foreach (var status in compamyType.result)
                        {
                            foreach (var type in status.Cmp_Business)
                            {
                                var addModel = new TypeStatusModel()
                                {
                                    businessNum = company.Business_Accounting_NO,
                                    companyName = company.Company_Name,
                                    companyStatus = company.Company_Status_Desc,
                                    businessType = type.Business_Item_Desc,
                                    date = forDate // 直接使用民國年格式的日期字串
                                };
                                _dao.AddTypeStatusData(addModel);
                            }
                        }
                    }
                }
            }

            var typeStatuss = _dao.TypeStatusList();
            var result = typeStatuss.Select(com => new TypeStatusViewModel
            {
                cs_id = com.cs_id,
                businessNum = com.businessNum,
                companyName = com.companyName,
                companyStatus = com.companyStatus,
                businessType = com.businessType,
                date = com.date
            }).ToList();

            return new ResultViewModel<List<TypeStatusViewModel>>() { result = result };
        }
        catch (Exception ex)
        {
            return new ResultViewModel<List<TypeStatusViewModel>>(ex.Message) { };
        }
    }
    #endregion
    #region 查分公司廢止類別
    // public async Task<ResultViewModel<List<TypeStatusViewModel>>> UpdateBranchCompanyAbolishType()
    // {
    //     try
    //     {
    //         var today = DateTime.Now.ToString("yyyyMM");
    //         var branchcompanyData = await GetBranchCompanyAbolishData();

    //         foreach (var branchcompany in branchcompanyData.result)
    //         {
    //             var compamyType = await GetCompanyData(branchcompany.Business_Accounting_NO);
    //             if (compamyType != null)
    //             {
    //                 foreach (var status in compamyType.result)
    //                 {
    //                     if (status != null)
    //                     {
    //                         foreach (var type in status.Cmp_Business)
    //                         {
    //                             var addModel = new TypeStatusModel()
    //                             {
    //                                 businessNum = branchcompany.Business_Accounting_NO,
    //                                 companyName = branchcompany.Company_Name,
    //                                 companyStatus = branchcompany.Branch_Office_Status_Desc,
    //                                 businessType = type.Business_Item_Desc,
    //                                 date = today
    //                             };
    //                             _dao.AddTypeStatusData(addModel);
    //                         }
    //                     }
    //                 }
    //             }
    //         }
    //         var typeStatuss = _dao.TypeStatusList();
    //         var result = typeStatuss.Select(com => new TypeStatusViewModel
    //         {
    //             c_id = com.c_id,
    //             businessNum = com.businessNum,
    //             companyName = com.companyName,
    //             companyStatus = com.companyStatus,
    //             businessType = com.businessType,
    //             date = com.date
    //         }).ToList();
    //         return new ResultViewModel<List<TypeStatusViewModel>>() { result = result };
    //     }
    //     catch (Exception ex)
    //     {
    //         return new ResultViewModel<List<TypeStatusViewModel>>(ex.Message) { };

    //     }
    // }
    public async Task<ResultViewModel<List<TypeStatusViewModel>>> UpdateBranchCompanyAbolishType()
    {
        try
        {
            // 設定日期範圍的起始日期（112年1月1日 -> 2023年1月1日）
            var startDate = new DateTime(2024, 7, 21);
            var today = DateTime.Now;

            // 將民國年轉換為字串
            var startRocYear = startDate.Year - 1911;
            var todayRocYear = today.Year - 1911;

            // 迴圈從 startDate 到 today，每天執行一次
            for (var datetime = startDate; datetime <= today; datetime = datetime.AddDays(1))
            {
                // 轉換當前日期為民國年格式的字串，格式為 "yyyMMdd"
                var currentRocYear = datetime.Year - 1911;
                var formattedDate = $"{currentRocYear}{datetime:MMdd}";
                var forDate = datetime.ToString("yyyyMMdd");

                // 獲取公司解散資料，傳遞民國年格式的日期字串
                var branchcompanyData = await GetBranchCompanyAbolishData(formattedDate);

                foreach (var branchcompany in branchcompanyData.result)
                {
                    var compamyType = await GetCompanyData(branchcompany.Business_Accounting_NO);
                    if (compamyType != null)
                    {
                        foreach (var status in compamyType.result)
                        {
                            if (status != null)
                            {
                                foreach (var type in status.Cmp_Business)
                                {
                                    var addModel = new TypeStatusModel()
                                    {
                                        businessNum = branchcompany.Business_Accounting_NO,
                                        companyName = branchcompany.Company_Name,
                                        companyStatus = branchcompany.Branch_Office_Status_Desc,
                                        businessType = type.Business_Item_Desc,
                                        date = forDate
                                    };
                                    _dao.AddTypeStatusData(addModel);
                                }
                            }
                        }
                    }
                }
            }
            var typeStatuss = _dao.TypeStatusList();
            var result = typeStatuss.Select(com => new TypeStatusViewModel
            {
                cs_id = com.cs_id,
                businessNum = com.businessNum,
                companyName = com.companyName,
                companyStatus = com.companyStatus,
                businessType = com.businessType,
                date = com.date
            }).ToList();
            return new ResultViewModel<List<TypeStatusViewModel>>() { result = result };
        }
        catch (Exception ex)
        {
            return new ResultViewModel<List<TypeStatusViewModel>>(ex.Message) { };

        }
    }
    #endregion
    #region 查商業歇業類別
    // public async Task<ResultViewModel<List<TypeStatusViewModel>>> UpdateStopBusinessType()
    // {
    //     try
    //     {
    //         var today = DateTime.Now.ToString("yyyyMM");
    //         var businessData = await GetStopBusinessData();

    //         foreach (var business in businessData.result)
    //         {
    //             var businessType = await GetBusinessData(business.President_No);
    //             if (businessType.result.Count != 0)
    //             {
    //                 foreach (var status in businessType.result)
    //                 {
    //                     foreach (var type in status.Business_Item_Old)
    //                     {
    //                         var addModel = new TypeStatusModel()
    //                         {
    //                             businessNum = business.President_No,
    //                             companyName = business.Business_Name,
    //                             companyStatus = business.Business_Current_Status_Desc,
    //                             businessType = type.Business_Item_Desc,
    //                             date = today
    //                         };
    //                         _dao.AddTypeStatusData(addModel);
    //                     }
    //                 }
    //             }
    //         }
    //         var typeStatuss = _dao.TypeStatusList();
    //         var result = typeStatuss.Select(com => new TypeStatusViewModel
    //         {
    //             c_id = com.c_id,
    //             businessNum = com.businessNum,
    //             companyName = com.companyName,
    //             companyStatus = com.companyStatus,
    //             businessType = com.businessType,
    //             date = com.date
    //         }).ToList();

    //         return new ResultViewModel<List<TypeStatusViewModel>>() { result = result };
    //     }
    //     catch (Exception ex)
    //     {
    //         return new ResultViewModel<List<TypeStatusViewModel>>(ex.Message) { };
    //     }
    // }
    public async Task<ResultViewModel<List<TypeStatusViewModel>>> UpdateStopBusinessType()
    {
        try
        {
            // 設定日期範圍的起始日期（112年1月1日 -> 2023年1月1日）
            var startDate = new DateTime(2024, 9, 14);
            var today = DateTime.Now;

            // 將民國年轉換為字串
            var startRocYear = startDate.Year - 1911;
            var todayRocYear = today.Year - 1911;

            // 迴圈從 startDate 到 today，每天執行一次
            for (var datetime = startDate; datetime <= today; datetime = datetime.AddDays(1))
            {
                // 轉換當前日期為民國年格式的字串，格式為 "yyyMMdd"
                var currentRocYear = datetime.Year - 1911;
                var formattedDate = $"{currentRocYear}{datetime:MMdd}";
                var forDate = datetime.ToString("yyyyMMdd");

                // 獲取公司解散資料，傳遞民國年格式的日期字串
                var businessData = await GetStopBusinessData(formattedDate);
                if (businessData != null)
                {
                    foreach (var business in businessData.result)
                    {
                        var businessType = await GetBusinessData(business.President_No);
                        if (businessType.result.Count != 0)
                        {
                            foreach (var status in businessType.result)
                            {
                                foreach (var type in status.Business_Item_Old)
                                {
                                    var addModel = new TypeStatusModel()
                                    {
                                        businessNum = business.President_No,
                                        companyName = business.Business_Name,
                                        companyStatus = business.Business_Current_Status_Desc,
                                        businessType = type.Business_Item_Desc,
                                        date = forDate
                                    };
                                    _dao.AddTypeStatusData(addModel);
                                }
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    continue;
                }
            }
            var typeStatuss = _dao.TypeStatusList();
            var result = typeStatuss.Select(com => new TypeStatusViewModel
            {
                cs_id = com.cs_id,
                businessNum = com.businessNum,
                companyName = com.companyName,
                companyStatus = com.companyStatus,
                businessType = com.businessType,
                date = com.date
            }).ToList();

            return new ResultViewModel<List<TypeStatusViewModel>>() { result = result };
        }
        catch (Exception ex)
        {
            return new ResultViewModel<List<TypeStatusViewModel>>(ex.Message) { };
        }
    }
    #endregion
    #region 台灣就業通網站職缺清單
    public async Task<ResultViewModel<List<VacancieViewModel>>> UpdateVacanciesData()
    {
        try
        {
            var joblist = new List<string>
        {
            "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22"
        };
            var citylist = new List<string>
        {
            "01", "02", "11", "12", "13", "14", "15", "23", "24", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46"
        };

            var allVacancies = new List<VacancieModel>();

            foreach (var jobCode in joblist)
            {
                foreach (var cityCode in citylist)
                {
                    var url = $"https://free.taiwanjobs.gov.tw/webservice_taipei/webservice.ashx?jobno={jobCode}&city={cityCode}";

                    HttpResponseMessage response = await _httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var xmlStream = await response.Content.ReadAsStringAsync();

                        var sanitizedXml = new string(xmlStream.Where(c => c >= 32 || c == '\n' || c == '\r').ToArray());

                        var xmlDoc = XDocument.Parse(sanitizedXml);
                        var dataList = xmlDoc.Descendants("Data");

                        foreach (var data in dataList)
                        {
                            try
                            {
                                var jobVacancy = new VacancieModel
                                {
                                    name = data.Element("OCCU_DESC")?.Value.Trim(),
                                    type = int.TryParse(data.Element("CJOB1_COUNT")?.Value.Trim(), out var type) ? type : 0,
                                    amount = int.TryParse(data.Element("JOB_PERSON")?.Value, out var amount) ? amount : 0,
                                    address = data.Element("CITYNAME")?.Value.Trim(),
                                    companyName = data.Element("COMPNAME")?.Value.Trim(),
                                    date = data.Element("TRANDATE")?.Value.Trim()
                                };
                                var yesterday = DateTime.Now.AddDays(-1);
                                var dateFormat = "yyyyMMdd";
                                DateTime vacancyDate = DateTime.ParseExact(jobVacancy.date, dateFormat, null);
                                if (vacancyDate > yesterday)
                                {
                                    await _dao.AddVacancies(jobVacancy);
                                    allVacancies.Add(jobVacancy);
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            catch (Exception ex)
                            {
                                return new ResultViewModel<List<VacancieViewModel>>(ex.Message) { };
                            }
                        }
                    }
                }
            }

            var vacancie = _dao.VacancieList();
            var result = vacancie.Select(van => new VacancieViewModel
            {
                v_id = van.v_id,
                name = van.name,
                type = van.type,
                amount = van.amount,
                address = van.address,
                companyName = van.companyName,
                date = van.date
            }).ToList();

            return new ResultViewModel<List<VacancieViewModel>>() { result = result };
        }
        catch (Exception ex)
        {
            return new ResultViewModel<List<VacancieViewModel>>(ex.Message) { };
        }
    }
    #endregion

}
