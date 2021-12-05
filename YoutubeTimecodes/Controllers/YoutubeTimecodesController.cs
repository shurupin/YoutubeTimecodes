namespace YoutubeTimecodes.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using YoutubeTimecodes.Services;

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class YoutubeTimecodesController : ControllerBase
    {
        private readonly IYoutubeTimecodesService _youtubeTimecodesService;
        private readonly ILogger<YoutubeTimecodesController> _logger;

        public YoutubeTimecodesController(IYoutubeTimecodesService youtubeTimecodesService, ILogger<YoutubeTimecodesController> logger)
        {
            this._youtubeTimecodesService = youtubeTimecodesService ?? throw new ArgumentNullException(paramName: nameof(youtubeTimecodesService));
            this._logger = logger ?? throw new ArgumentNullException(paramName: nameof(logger));
        }

        [HttpGet]
        public async Task<List<string>> GetYoutubeTimecodesAsync(string youtubeVideoUrl)
        {
            var youtubeTimecodes = await this._youtubeTimecodesService.GetYoutubeTimecodesAsync(youtubeVideoUrl: youtubeVideoUrl);
            return youtubeTimecodes;
        }
    }
}