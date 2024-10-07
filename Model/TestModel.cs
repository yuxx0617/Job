using System.ComponentModel.DataAnnotations;

namespace Job.Model;

public class TestModel
{
    [Key]
    public int t_id { get; set; }
    public string quetion { get; set; }
    public string? bgImg { get; set; }
    public int bgColor { get; set; }
    public string animateImg { get; set; }
    public string? ts_idList { get; set; }
}