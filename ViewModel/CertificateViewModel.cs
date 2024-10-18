using System.ComponentModel.DataAnnotations;

namespace Job.ViewModel;

public class CertificateViewModel
{
    public int c_id { get; set; }
    public string name { get; set; }
    public string? rank { get; set; }
    public string type { get; set; }
    public string testTIme { get; set; }
    public string applyTime { get; set; }
    public string address { get; set; }
    public string http { get; set; }
}