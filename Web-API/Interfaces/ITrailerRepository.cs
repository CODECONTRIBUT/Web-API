using Web_API.Models;

namespace Web_API.Interfaces
{
    public interface ITrailerRepository
    {
        Task<List<Trailer>> GetAllTrailers(int productId);
    }
}
