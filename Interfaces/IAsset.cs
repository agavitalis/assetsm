using AssetsM.Models;

namespace AssetsM.Interfaces
{
    public interface IAsset
    {
        Task<Asset> GetAsset(Guid assetId);
        Task<IEnumerable<Asset>> GetAssets();
    }
}
