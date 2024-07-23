namespace AssetsM.Models
{
    public class AssetPicture
    {
        public Guid Id { get; set; }
        public string Image { get; set; }

        // Foreign key
        public int AssetId { get; set; }

        // Navigation property
        public Asset Asset { get; set; }
    }
}
