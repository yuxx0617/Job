namespace Job.ViewModel;

public class CompanyViewModel
{
    public string? Business_Accounting_NO { get; set; }
    public string? Company_Name { get; set; }
    public string? Company_Status { get; set; }
    public string? Company_Status_Desc { get; set; }
    public string? Company_Setup_Date { get; set; }
    public List<BusinessTypeViewModel>? Cmp_Business { get; set; }

}
