using System.ComponentModel.DataAnnotations;

namespace Job.Model;

public class ActRecordModel
{
    [Key]
    public int r_id { get; set; }
    public string account { get; set; }
    public string activity { get; set; }
    public string content { get; set; }
    public DateTime date { get; set; }
}