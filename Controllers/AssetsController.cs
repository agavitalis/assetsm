using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AssetsM.Data;
using AssetsM.Models;
using AssetsM.Interfaces;
using AssetsM.Models.Dtos;

namespace AssetsM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly AssetsMContext _context;
        private readonly IAsset _asset;

        public AssetsController(AssetsMContext context, IAsset asset)
        {
            _context = context;
            _asset = asset;
        }

        [HttpGet]
        public async Task<IActionResult> GetAssets()
        {
            var assets = _asset.GetAssets();
            return Ok( new
            {
                code  = 200,
                data = assets
            });
        }

        [HttpGet("{assetId}")]
        public async Task<IActionResult> GetAsset(Guid assetId)
        {
           
            var asset = await _asset.GetAsset(assetId);
           
            if (asset == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                code = 200,
                data = asset
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(Asset asset)
        {
            
            asset.Id = Guid.NewGuid();
            _context.Add(asset);
            await _context.SaveChangesAsync();
         
            return Ok(new
            {
                code = 200,
                data = asset
            });
        }

  
        [HttpPut]
        public async Task<IActionResult> Edit(Guid id, [FromBody]AssetDto asset)
        {
              _context.Update(asset);
                    await _context.SaveChangesAsync();

            return Ok(new
            {
                code = 200,
                data = asset
            });
        }

      
        [HttpDelete("{assetId}"), ActionName("Delete")]
        public async Task<IActionResult> DeleteAsset(Guid assetId)
        {
            var asset = await _context.Asset.FindAsync(assetId);
            if (asset != null)
            {
                _context.Asset.Remove(asset);
            }

            await _context.SaveChangesAsync();
            return Ok(new
            {
                code = 200,
                data = asset
            });
        }

      
    }
}
