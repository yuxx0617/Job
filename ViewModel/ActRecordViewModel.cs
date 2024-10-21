using System.ComponentModel.DataAnnotations;

namespace Job.ViewModel;

public class ActRecordViewModel
{
    public int r_id { get; set; }
    public string account { get; set; }
    public string activity { get; set; }
    public string content { get; set; }
    public DateTime date { get; set; }
}