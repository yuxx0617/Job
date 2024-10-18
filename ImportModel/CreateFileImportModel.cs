using Microsoft.AspNetCore.Http;

namespace Job.ImportModel;

public class CreateFileImportModel
{
    public IFormFile file { get; set; }
}