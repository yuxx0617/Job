using System;
using System.Net.Http;
using System.Threading.Tasks;
using Job.Model;
using Job.Service.Interface;
using Job.ViewModel;
using Microsoft.AspNetCore.Builder.Extensions;
using Newtonsoft.Json;

public class ExternalApiService : IExternalApiService
{
    private readonly HttpClient _httpClient;

    public ExternalApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    #region 公司資料異動查詢
    public async Task<ResultViewModel<List<CompanyModel>>> GetCompanyData()
    {
        try
        {
            var today = DateTime.Now;
            var rocYear = today.Year - 1911;
            var formattedDate = $"{rocYear}{today:MMdd}";

            var url = $"https://data.gcis.nat.gov.tw/od/data/api/4347A009-6489-4F19-AC79-78F366BE7976?$format=json&$filter=Change_Of_Approval_Data eq {formattedDate}&$skip=0&$top=500";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                var companyData = JsonConvert.DeserializeObject<List<CompanyModel>>(jsonResponse);

                return new ResultViewModel<List<CompanyModel>>() { result = companyData };
            }
            else
            {
                return new ResultViewModel<List<CompanyModel>>("API 請求失敗") { };
            }
        }
        catch (Exception ex)
        {

            return new ResultViewModel<List<CompanyModel>>(ex.Message) { };

        }
    }
    #endregion
    #region 分公司資料異動查詢
    public async Task<ResultViewModel<List<CompanyModel>>> GetBranchCompanyData()
    {
        try
        {
            var today = DateTime.Now;
            var rocYear = today.Year - 1911;
            var formattedDate = $"{rocYear}{today:MMdd}";

            var url = $"https://data.gcis.nat.gov.tw/od/data/api/F0CAB3B6-7A39-46DD-B490-0EED944783BD?$format=json&$filter=Chg_App_Date eq {formattedDate}&$skip=0&$top=500";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                var companyData = JsonConvert.DeserializeObject<List<CompanyModel>>(jsonResponse);

                return new ResultViewModel<List<CompanyModel>>() { result = companyData };
            }
            else
            {
                return new ResultViewModel<List<CompanyModel>>("API 請求失敗") { };
            }
        }
        catch (Exception ex)
        {

            return new ResultViewModel<List<CompanyModel>>(ex.Message) { };

        }
    }
    #endregion
    #region 外國公司資料異動查詢
    public async Task<ResultViewModel<List<CompanyModel>>> GetForeignCompanyData()
    {
        try
        {
            var today = DateTime.Now;
            var rocYear = today.Year - 1911;
            var formattedDate = $"{rocYear}{today:MMdd}";

            var url = $"https://data.gcis.nat.gov.tw/od/data/api/4526A764-A491-49FA-8509-21DCF53BDF9D?$format=json&$filter=Chg_App_Date eq {formattedDate}&$skip=0&$top=500";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                var companyData = JsonConvert.DeserializeObject<List<CompanyModel>>(jsonResponse);

                return new ResultViewModel<List<CompanyModel>>() { result = companyData };
            }
            else
            {
                return new ResultViewModel<List<CompanyModel>>("API 請求失敗") { };
            }
        }
        catch (Exception ex)
        {

            return new ResultViewModel<List<CompanyModel>>(ex.Message) { };

        }
    }
    #endregion
    #region 商業資料異動查詢
    public async Task<ResultViewModel<List<CompanyModel>>> GetBusinessData()
    {
        try
        {
            var today = DateTime.Now;
            var rocYear = today.Year - 1911;
            var formattedDate = $"{rocYear}{today:MMdd}";

            var url = $"https://data.gcis.nat.gov.tw/od/data/api/9B84E7EF-7DEE-4426-A53D-059113F6B1E3?$format=json&$filter=Business_Last_Change_Date eq {formattedDate}&$skip=0&$top=500";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                var companyData = JsonConvert.DeserializeObject<List<CompanyModel>>(jsonResponse);

                return new ResultViewModel<List<CompanyModel>>() { result = companyData };
            }
            else
            {
                return new ResultViewModel<List<CompanyModel>>("API 請求失敗") { };
            }
        }
        catch (Exception ex)
        {

            return new ResultViewModel<List<CompanyModel>>(ex.Message) { };

        }
    }
    #endregion
    #region 公司資料每日解散異動查詢
    public async Task<ResultViewModel<List<CompanyModel>>> GetCompanyDisbandData()
    {
        try
        {
            var today = DateTime.Now;
            var rocYear = today.Year - 1911;
            var formattedDate = $"{rocYear}{today:MMdd}";

            var url = $"https://data.gcis.nat.gov.tw/od/data/api/561D23B6-5EA7-4FF4-A78D-A617ED64BC64?$format=json&$filter=Change_Of_Approval_Data eq {formattedDate}&$skip=0&$top=500";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                var companyData = JsonConvert.DeserializeObject<List<CompanyModel>>(jsonResponse);

                return new ResultViewModel<List<CompanyModel>>() { result = companyData };
            }
            else
            {
                return new ResultViewModel<List<CompanyModel>>("API 請求失敗") { };
            }
        }
        catch (Exception ex)
        {

            return new ResultViewModel<List<CompanyModel>>(ex.Message) { };

        }
    }
    #endregion
    #region 分公司資料每日廢止異動查詢
    public async Task<ResultViewModel<List<CompanyModel>>> GetBranchCompanyAbolishData()
    {
        try
        {
            var today = DateTime.Now;
            var rocYear = today.Year - 1911;
            var formattedDate = $"{rocYear}{today:MMdd}";

            var url = $"https://data.gcis.nat.gov.tw/od/data/api/50F22B9D-5A21-4E0B-835B-721459C1182A?$format=json&$filter=Chg_App_Date eq {formattedDate}&$skip=0&$top=500";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                var companyData = JsonConvert.DeserializeObject<List<CompanyModel>>(jsonResponse);

                return new ResultViewModel<List<CompanyModel>>() { result = companyData };
            }
            else
            {
                return new ResultViewModel<List<CompanyModel>>("API 請求失敗") { };
            }
        }
        catch (Exception ex)
        {

            return new ResultViewModel<List<CompanyModel>>(ex.Message) { };

        }
    }
    #endregion
    #region 商業資料每日歇業異動查詢
    public async Task<ResultViewModel<List<CompanyModel>>> GetStopBusinessData()
    {
        try
        {
            var today = DateTime.Now;
            var rocYear = today.Year - 1911;
            var formattedDate = $"{rocYear}{today:MMdd}";

            var url = $"https://data.gcis.nat.gov.tw/od/data/api/632775D2-2FAA-4ECA-81DA-89C74349AB8B?$format=json&$filter=Business_Last_Change_Date eq {formattedDate}&$skip=0&$top=500";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                var companyData = JsonConvert.DeserializeObject<List<CompanyModel>>(jsonResponse);

                return new ResultViewModel<List<CompanyModel>>() { result = companyData };
            }
            else
            {
                return new ResultViewModel<List<CompanyModel>>("API 請求失敗") { };
            }
        }
        catch (Exception ex)
        {

            return new ResultViewModel<List<CompanyModel>>(ex.Message) { };

        }
    }
    #endregion
    #region 有限合夥資料每日解散異動查詢
    public async Task<ResultViewModel<List<CompanyModel>>> GetLtdDisbandData()
    {
        try
        {
            var today = DateTime.Now;
            var rocYear = today.Year - 1911;
            var formattedDate = $"{rocYear}{today:MMdd}";

            var url = $"https://data.gcis.nat.gov.tw/od/data/api/33BCDDF6-59D6-4EB8-9E96-1F6CC452F8BE?$format=json&$filter=Last_Chg_Date eq {formattedDate}&$skip=0&$top=500";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                var companyData = JsonConvert.DeserializeObject<List<CompanyModel>>(jsonResponse);

                return new ResultViewModel<List<CompanyModel>>() { result = companyData };
            }
            else
            {
                return new ResultViewModel<List<CompanyModel>>("API 請求失敗") { };
            }
        }
        catch (Exception ex)
        {

            return new ResultViewModel<List<CompanyModel>>(ex.Message) { };

        }
    }
    #endregion
    #region 台灣就業通網站職缺清單
    public async Task<ResultViewModel<List<CompanyModel>>> GetVacanciesData()
    {
        try
        {
            var url = $"https://free.taiwanjobs.gov.tw/webservice_taipei/Webservice.ashx?jobno={050313}&zipno={104}&count=500&T=CSV";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                var companyData = JsonConvert.DeserializeObject<List<CompanyModel>>(jsonResponse);

                return new ResultViewModel<List<CompanyModel>>() { result = companyData };
            }
            else
            {
                return new ResultViewModel<List<CompanyModel>>("API 請求失敗") { };
            }
        }
        catch (Exception ex)
        {

            return new ResultViewModel<List<CompanyModel>>(ex.Message) { };

        }
    }
    #endregion
}
