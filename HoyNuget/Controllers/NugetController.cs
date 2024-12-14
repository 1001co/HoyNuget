using Azure.Core;
using Microsoft.AspNetCore.Mvc;

namespace HoyNuget.API.Controllers
{
    [Route("nuget")]
    public class NuGetController : Controller
    {
        private readonly string _packagePath = "Packages";

        [HttpGet("index.json")]
        public IActionResult GetServiceIndex()
        {
            return Ok(new
            {
                version = "3.0.0",
                resources = new[]
                {
                    new { id = "PackageBaseAddress", type = "PackageBaseAddress/3.0.0", uri = $"{Request.Scheme}://{Request.Host}/nuget" },
                    new { id = "PackagePublish", type = "PackagePublish/2.0.0", uri = $"{Request.Scheme}://{Request.Host}/nuget" }
                }
            });
        }

        [HttpPut("v3/package")]
        public async Task<IActionResult> UploadPackage()
        {
            if (!Request.HasFormContentType || !Request.Form.Files.Any())
                return BadRequest("No package provided.");

            var file = Request.Form.Files.First();
            var filePath = Path.Combine(_packagePath, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok();
        }

        [HttpGet("v3/package/{id}/{version}/{fileName}")]
        public IActionResult GetPackage(string id, string version, string fileName)
        {
            var filePath = Path.Combine(_packagePath, fileName);
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            return PhysicalFile(filePath, "application/octet-stream");
        }
    }
}
