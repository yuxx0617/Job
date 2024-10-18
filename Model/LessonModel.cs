using System.ComponentModel.DataAnnotations;

namespace Job.Model;

public class LessonModel
{
    [Key]
    public int l_id { get; set; }
    public string name { get; set; }
    public string time { get; set; }
    public string? content { get; set; }
    public string? http { get; set; }
    public string? address { get; set; }
}