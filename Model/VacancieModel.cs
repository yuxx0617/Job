using System.ComponentModel.DataAnnotations;

namespace Job.Model;

public class VacancieModel
{
    [Key]
    public int v_id { get; set; }
    public string name { get; set; }
    public int type { get; set; }
    public int amount { get; set; }
    public string address { get; set; }
    public string companyName { get; set; }
    public string date { get; set; }
}