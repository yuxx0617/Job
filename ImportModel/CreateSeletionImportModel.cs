namespace Job.ImportModel;

public class CreateSeletionImportModel
{
    public string seletion { get; set; }
    public bool MBTI_I { get; set; } = false;
    public bool MBTI_E { get; set; } = false;
    public bool MBTI_N { get; set; } = false;
    public bool MBTI_S { get; set; } = false;
    public bool MBTI_F { get; set; } = false;
    public bool MBTI_T { get; set; } = false;
    public bool MBTI_P { get; set; } = false;
    public bool MBTI_J { get; set; } = false;
    public bool HOL_I { get; set; } = false;
    public bool HOL_E { get; set; } = false;
    public bool HOL_S { get; set; } = false;
    public bool HOL_R { get; set; } = false;
    public bool HOL_C { get; set; } = false;
    public bool HOL_A { get; set; } = false;
    public int t_id { get; set; }
}