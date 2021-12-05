namespace YoutubeTimecodes.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITextSummarizationService
    {
        Task<List<string>> GetSummariesAsync(string text);
    }
}