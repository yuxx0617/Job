using Job.Model;

namespace Job.ViewModel;

public class UserViewModel
{
    public string account { get; set; }
    public string name { get; set; }
    public string password { get; set; }
    public string phone { get; set; }
    public string address { get; set; }
    public int edu { get; set; }
    public int sex { get; set; }
    public DateTime birth { get; set; }
    public DateTime loginDay { get; set; }
}