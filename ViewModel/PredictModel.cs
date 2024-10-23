using System.ComponentModel.DataAnnotations;

namespace Job.ViewModel;

public class PredictViewModel
{
    public int p_id { get; set; }
    public string type { get; set; }
    public int c_amount { get; set; }
    public int v_amount { get; set; }
    public string date { get; set; }
}