using System.ComponentModel.DataAnnotations;

namespace Job.Model;

public class JobModel
{
    [Key]
    public int j_id { get; set; }
    public string name { get; set; }
    public string? MBTI { get; set; }
    public string? HOL { get; set; }
}