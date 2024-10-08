using Microsoft.AspNetCore.Http;

namespace Job.ImportModel;

public class EditTestImportModel
{
    public int t_id { get; set; }
    public string? question { get; set; }
    public IFormFile? bgImg { get; set; }
    public int? bgColor { get; set; }
    public IFormFile? animateImg { get; set; }
    public string? ts_idList { get; set; }
}