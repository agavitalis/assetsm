using AssetsM.Data;
using AssetsM.Interfaces;
using AssetsM.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;

namespace AssetsM.Repositories
{
    public class AssetRepo : IAsset
    {
        private readonly AssetsMContext _dbcontext;
        public AssetRepo(AssetsMContext dbcontext)
        {
           _dbcontext = dbcontext;
        }
        public async Task<Models.Asset> GetAsset(Guid assetId)
        {
            return await _dbcontext.Asset.SingleAsync(x=>x.Id == assetId);
        }

        public async Task<IEnumerable<Models.Asset>> GetAssets()
        {
            return await _dbcontext.Asset.ToListAsync();
        }

      
    }
}
