using System.ComponentModel.DataAnnotations;

namespace Job.ViewModel;

public class JobViewModel
{
    [Key]
    public int j_id { get; set; }
    public string name { get; set; }
    public string? MBTI { get; set; }
    public string? HOL { get; set; }
}