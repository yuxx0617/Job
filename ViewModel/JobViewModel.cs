using System.ComponentModel.DataAnnotations;

namespace Job.ViewModel;

public class JobViewModel
{
    public int j_id { get; set; }
    public string name { get; set; }
    public string? oneDown { get; set; }
    public string? oneTothree { get; set; }
    public string? threeTofive { get; set; }
    public string? fiveToten { get; set; }
    public string? tenTofifteen { get; set; }
    public string? fifteenUp { get; set; }
    public string? skill { get; set; }
    public string? certificate { get; set; }
    public string? tool { get; set; }
    public string? contentImg { get; set; }
    public string? experienceImg { get; set; }
}