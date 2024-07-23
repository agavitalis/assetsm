using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace AssetsM.Models.Dtos
{
    public class AssetPictureDto
    {
        public Guid assetId { get; set; }

        [FromForm(Name = "UploadedImage")]
        public IFormFile UploadedImage { get; set; }

        [JsonIgnore]
        protected string? Image { get; set; }
    }
}
