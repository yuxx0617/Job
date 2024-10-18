using System.ComponentModel.DataAnnotations;

namespace Job.Model;

public class SubsidyModel
{
    [Key]
    public int s_id { get; set; }
    public string? name { get; set; }
    public int money { get; set; } = 0;
}