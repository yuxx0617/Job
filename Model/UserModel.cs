using System.ComponentModel.DataAnnotations;

namespace Job.Model;

public class UserModel
{
    [Key]
    public string account { get; set; }
    public string name { get; set; }
    public string password { get; set; }
    public string phone { get; set; }
    public string address { get; set; }
    public int edu { get; set; } = 0;
    public int sex { get; set; } = 0;
    public DateTime birth { get; set; }
    public string? emailValid { get; set; }
    public bool role { get; set; }
    public string salt { get; set; }
    public DateTime loginDay { get; set; }
}