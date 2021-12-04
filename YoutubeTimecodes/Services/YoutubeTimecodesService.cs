namespace YoutubeTimecodes.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class YoutubeTimecodesService : IYoutubeTimecodesService
    {
        public YoutubeTimecodesService()
        {
        }

        public async Task<List<string>> GetYoutubeTimecodesAsync(string youtubeVideoUrl)
        {
            try
            {
                List<string> result = new List<string>() { "test1, test2" };
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