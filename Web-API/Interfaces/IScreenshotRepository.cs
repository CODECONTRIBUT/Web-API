using Web_API.Dtos.Product;
using Web_API.Models;

namespace Web_API.Interfaces
{
    public interface IScreenshotRepository
    {
        Task<Screenshot> CreateScreenshotAsync(int productId, Screenshot screenshot);

        Task<Screenshot?> DeleteScreenshotAsync(int screenshotId);

        Task<Screenshot?> GetScreenshotByIdAsync(int id);
    }
}
