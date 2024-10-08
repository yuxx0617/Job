using Microsoft.AspNetCore.Http;

namespace Job.ImportModel;

public class CreateTestImportModel
{
    public string question { get; set; }
    public IFormFile? bgImg { get; set; }
    public int bgColor { get; set; }
    public IFormFile animateImg { get; set; }
}