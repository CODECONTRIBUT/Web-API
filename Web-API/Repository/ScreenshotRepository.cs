using Microsoft.EntityFrameworkCore;
using Web_API.Interfaces;
using Web_API.Models;

namespace Web_API.Repository
{
    public class ScreenshotRepository : IScreenshotRepository
    {
        private readonly storeContext _dbContext;
        public ScreenshotRepository(storeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Screenshot?> DeleteScreenshotAsync(int screenshotId)
        {
            var exsitingScreenshot = _dbContext.Screenshots.FirstOrDefault(s => s.Id == screenshotId);
            if (exsitingScreenshot == null)
                return null;

            _dbContext.Remove(exsitingScreenshot);
            await _dbContext.SaveChangesAsync();
            return exsitingScreenshot;
        }

        public async Task<Screenshot?> GetScreenshotByIdAsync(int id)
        {
            var screenshotItem = await _dbContext.Screenshots.FirstOrDefaultAsync(s => s.Id == id);
            if (screenshotItem == null)
                return null;

            return screenshotItem;
        }

        public async Task<Screenshot> CreateScreenshotAsync(int productId, Screenshot screenshot)
        {
            var existingProduct = await _dbContext.Products.FirstOrDefaultAsync(m => m.Id == productId);
            if (existingProduct == null)
                return null;

            screenshot.ProductId = productId;
            await _dbContext.AddAsync(screenshot);
            await _dbContext.SaveChangesAsync();
            return screenshot;
        }
    }
}
