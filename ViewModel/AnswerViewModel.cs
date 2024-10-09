using Job.Model;

namespace Job.ViewModel;

public class AnswerViewModel
{
    public int ua_id { get; set; }
    public string ua_goodList1 { get; set; }
    public string ua_goodList2 { get; set; }
    public string ua_goodList3 { get; set; }
    public string ua_badList1 { get; set; }
    public string ua_badList2 { get; set; }
    public string ua_badList3 { get; set; }
    public DateTime testTime { get; set; }
    public string MBTI_Result { get; set; }
    public string HOL_Result { get; set; }
    public string test_Result { get; set; }
    public string account { get; set; }
    public int count_MBTI_I { get; set; }
    public int count_MBTI_E { get; set; }
    public int count_MBTI_N { get; set; }
    public int count_MBTI_S { get; set; }
    public int count_MBTI_F { get; set; }
    public int count_MBTI_T { get; set; }
    public int count_MBTI_P { get; set; }
    public int count_MBTI_J { get; set; }
    public int count_HOL_I { get; set; }
    public int count_HOL_E { get; set; }
    public int count_HOL_S { get; set; }
    public int count_HOL_R { get; set; }
    public int count_HOL_C { get; set; }
    public int count_HOL_A { get; set; }
}