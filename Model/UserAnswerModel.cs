namespace Job.Model;

public class UserAnswerModel
{
    public int ua_id { get; set; }
    public string ua_goodList1 { get; set; }
    public string ua_goodList2 { get; set; }
    public string ua_goodList3 { get; set; }
    public string ua_badList1 { get; set; }
    public string ua_badList2 { get; set; }
    public string ua_badList3 { get; set; }
    public DateTime testTime { get; set; }
    public string? MBTI_Result { get; set; }
    public string? HOL_Result { get; set; }
    public string? test_Result { get; set; }
    public string account { get; set; }
}