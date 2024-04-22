using Web_API.Models;

namespace Web_API.Interfaces
{
    public interface IParentPlatformRepository
    {
        Task<List<Platform>> GetAllParentPlatforms();
    }
}
