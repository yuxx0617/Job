namespace Job.ViewModel;

public class BusinessViewModel
{
    public string? President_No { get; set; }
    public string? Business_Name { get; set; }
    public string? Business_Current_Status { get; set; }
    public string? Business_Current_Status_Desc { get; set; }
    public string? Business_Setup_Approve_Date { get; set; }
    public string? Business_Organization_Type_Desc { get; set; }
    public string? Agency { get; set; }
    public string? Agency_Desc { get; set; }
    public string? Business_Address { get; set; }
    public List<BusinessTypeViewModel> Business_Item_Old { get; set; }

}
