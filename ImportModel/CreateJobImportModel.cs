using Microsoft.AspNetCore.Http;

namespace Job.ImportModel;

public class CreateJobImportModel
{
    public IFormFile file { get; set; }
}