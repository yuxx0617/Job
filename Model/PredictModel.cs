using System.ComponentModel.DataAnnotations;

namespace Job.Model;

public class PredictModel
{
    [Key]
    public int p_id { get; set; }
    public string type { get; set; }
    public int c_amount { get; set; } = 0;
    public int v_amount { get; set; } = 0;
    public string date { get; set; }
}