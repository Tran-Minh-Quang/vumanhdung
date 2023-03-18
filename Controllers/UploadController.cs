using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Team12EUP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UploadController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public class MediaUploadDTO
        {
            public List<IFormFile> file { get; set; }

        }
        [HttpPut("UploadImage")]
        public async Task<IActionResult> UploadMedia([FromForm] MediaUploadDTO dto)
        {
            try
            {
                List<string> pathList = new();

                foreach (var item in dto.file)
                {
                    if (dto.file != null && item.Length > 0 && (item.Length / 1048576) <= int.Parse(_configuration["ImageProfile:MaxSize"]))
                    {

                        var folderName = _configuration.GetValue<string>("Storage") + "Product";
                        var fileName = Path.GetFileName(item.FileName);
                        var folder = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                        var name = $"{fileName.Split(".")[0]}_{DateTime.Now.Ticks}_ORIGIN.png";
                        if (!Directory.Exists(folder))
                            Directory.CreateDirectory(folder);
                        var filePath = Path.Combine(folder, name);

                        using (var fileSteam = new FileStream(filePath, FileMode.Create))
                        {
                            await item.CopyToAsync(fileSteam);
                        }
                        var path = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{_configuration.GetValue<string>("Domain")}Product/{name}";
                        var namepath = path.Split('/').Last();
                        pathList.Add(path);
                    }
                }
                return Ok(pathList);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpPost("uploadVideo")]
        public async Task<IActionResult> UploadVideo(IFormFile videoFile)
        {
            try
            {
                if (videoFile == null || videoFile.Length == 0)
                {
                    return BadRequest("Video file is missing");
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(videoFile.FileName);
                var filePath = Path.Combine(_configuration.GetValue<string>("Video"), fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await videoFile.CopyToAsync(fileStream);
                }

                return Ok(_configuration.GetValue<string>("Video")+"\\"+ fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
    }
}
