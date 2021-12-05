namespace YoutubeTimecodes.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class YoutubeTimecodesService : IYoutubeTimecodesService
    {
        private readonly ITextSummarizationService _textSummarizationService;

        public YoutubeTimecodesService(ITextSummarizationService textSummarizationService)
        {
            this._textSummarizationService = textSummarizationService ?? throw new ArgumentNullException(paramName: nameof(textSummarizationService));
        }

        public async Task<List<string>> GetYoutubeTimecodesAsync(string youtubeVideoUrl)
        {
            try
            {
                List<string> result = await this._textSummarizationService.GetSummariesAsync("");

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