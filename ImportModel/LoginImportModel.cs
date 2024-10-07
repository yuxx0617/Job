namespace Job.ImportModel;

public class LoginImportModel
{
    public string account { get; set; }
    public string password { get; set; }
    public string? emailValid { get; set; }
    public bool role { get; set; }
}