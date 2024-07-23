using AssetsM.Data;
using AssetsM.Interfaces;
using AssetsM.Models;
using AssetsM.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AssetsM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetPicturesController : ControllerBase
    {
        private readonly AssetsMContext _context;
        private readonly IAsset _asset;
        private readonly IWebHostEnvironment _env;

        public AssetPicturesController(AssetsMContext context, IAsset asset, IWebHostEnvironment env)
        {
            _context = context;
            _asset = asset;
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> AddAssetPicturesBase64([FromForm] AssetPictureDto assetPictureDto)
        {
            string base64ImageString;
            if (assetPictureDto.UploadedImage != null && assetPictureDto.UploadedImage.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    assetPictureDto.UploadedImage.CopyTo(stream);
                    byte[] data = stream.ToArray();
                    base64ImageString = Convert.ToBase64String(data);
                }
            }
            return Ok(new
            {
                code = 200,
            });
        }

        [HttpPost(Name = "UploadImage")]
        public async Task<IActionResult> AddAssetPicturesUpload(AssetPictureDto assetPictureDto)
        {
            if (assetPictureDto.UploadedImage == null || assetPictureDto.UploadedImage.Length <= 0)
            {
                return BadRequest("No file uploaded.");
            }

           
            var uploadsFolderPath = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + assetPictureDto.UploadedImage.FileName;
            var filePath = Path.Combine(uploadsFolderPath, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await assetPictureDto.UploadedImage.CopyToAsync(fileStream);
            }


            return Ok(new
            {
                code = 200,
            });
        }



        public async Task<ActionResult<List<string>>> Get(string stem)
        {


        var uri = "https://raw.githubusercontent.com/qualified/challenge-data/master/words_alpha.txt";
        var results = new List<string>();
            using (HttpClient client = new HttpClient())
            {
                var str = await client.GetStringAsync(uri);
                var strList = str.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                if (string.IsNullOrEmpty(stem) || stem == "\\" || stem.Trim() == "")
                {
                    results = strList.ToList();
                }
                else
                {
                    results = strList.Where(x => x.StartsWith(stem)).ToList();
                }
            }

            if (results.Count == 0)
            {
                return NotFound();
            }

            var resultsObject = new StemServiceResult() { data = results };
            var resultsJson = JsonSerializer.Serialize<StemServiceResult>(resultsObject);
            return Ok(resultsJson);

        }
    }

    public class StemServiceResult
    {
        public List<string> data { get; set; }
    }
}

}
