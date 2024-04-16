using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web_API.Dtos.Screenshot;
using Web_API.Interfaces;
using Web_API.Models;

namespace Web_API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ScreenshotsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ScreenshotsController> _logger;
        private readonly IScreenshotRepository _screenshotRepo;

        public ScreenshotsController(IMapper mapper, ILogger<ScreenshotsController> logger, IScreenshotRepository screenshotRepo)
        {
            _mapper = mapper;
            _logger = logger;
            _screenshotRepo = screenshotRepo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetScreenshot([FromRoute] int id)
        {
            try
            {
                var screenshot = await _screenshotRepo.GetScreenshotByIdAsync(id);
                return screenshot == null ? NotFound() : Ok(_mapper.Map<ScreenshotDto>(screenshot));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Screenshot Query failed");
                return BadRequest(ex.Message);
            }
        }


        //on existing Product webpage, upload or delete a screenshot would trigger these two endpoints below.
        //Otherwise, when create or delete a product, the logic of creation/deletion of screenshots is in ProductsController endpoints.
        [HttpPost("{productId}")]
        public async Task<IActionResult> CreateScreenshot([FromRoute] int productId, [FromBody] CreateScreenshotDto createdScreenshotDto)
        {
            try
            {
                var screenshot = _mapper.Map<Screenshot>(createdScreenshotDto);
                if (screenshot == null)
                    return NotFound();

                var screenshotItem = await _screenshotRepo.CreateScreenshotAsync(productId, screenshot);
                return screenshotItem == null ? BadRequest("Create screenshot error")
                                           : CreatedAtAction(nameof(GetScreenshot), new { id = screenshotItem.Id }, _mapper.Map<ScreenshotDto>(screenshotItem));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Screenshot creation failed");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScreenshot([FromRoute] int id)
        {
            try
            {
                var deletedScreenshot = await _screenshotRepo.DeleteScreenshotAsync(id);
                return deletedScreenshot == null ? BadRequest("Screenshot not exists or DB delete error") : NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Deletion of Screenshot failed");
                return BadRequest(ex.Message);
            }
        }
    }
}
