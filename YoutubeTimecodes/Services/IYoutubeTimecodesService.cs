namespace YoutubeTimecodes.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IYoutubeTimecodesService
    {
        Task<List<string>> GetYoutubeTimecodesAsync(string youtubeVideoUrl);
    }
}