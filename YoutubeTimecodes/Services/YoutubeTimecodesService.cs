namespace YoutubeTimecodes.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using YoutubeExplode;
    using YoutubeExplode.Videos.ClosedCaptions;

    public class YoutubeTimecodesService : IYoutubeTimecodesService
    {
        private readonly YoutubeClient _youtubeClient;
        private readonly ITextSummarizationService _textSummarizationService;

        public YoutubeTimecodesService(ITextSummarizationService textSummarizationService)
        {
            this._youtubeClient = new YoutubeClient();
            this._textSummarizationService = textSummarizationService ?? throw new ArgumentNullException(paramName: nameof(textSummarizationService));
        }

        public async Task<List<string>> GetYoutubeTimecodesAsync(string youtubeVideoUrl)
        {
            try
            {
                ClosedCaptionManifest closedCaptionManifest = await this._youtubeClient.Videos.ClosedCaptions.GetManifestAsync(videoId: youtubeVideoUrl);
                ClosedCaptionTrackInfo closedCaptionTrackInfo = closedCaptionManifest.GetByLanguage(language: "en");
                ClosedCaptionTrack closedCaptionTrack = await this._youtubeClient.Videos.ClosedCaptions.GetAsync(trackInfo: closedCaptionTrackInfo);
                string text = string.Join(". ", closedCaptionTrack.Captions);
                List<string> result = await this._textSummarizationService.GetSummariesAsync(text);

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(value: e.Message);
                throw;
            }
        }
    }
}