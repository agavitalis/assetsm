using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AssetsM.Models;

namespace AssetsM.Data
{
    public class AssetsMContext : DbContext
    {
        public AssetsMContext (DbContextOptions<AssetsMContext> options)
            : base(options)
        {
        }

        public DbSet<Asset> Asset { get; set; } = default!;
    }
}
