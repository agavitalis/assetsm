using System.Collections.ObjectModel;

namespace AssetsM.Models
{
    public class Asset
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Asset> Assets { get; set;}
    }
}
