using System.ComponentModel.DataAnnotations;

namespace Job.Model;

public class TypeStatusModel
{
    [Key]
    public int c_id { get; set; }
    public string? businessNum { get; set; }
    public string? companyName { get; set; }
    public string? companyStatus { get; set; }
    public string? businessType { get; set; }
    public string? date { get; set; }
}
