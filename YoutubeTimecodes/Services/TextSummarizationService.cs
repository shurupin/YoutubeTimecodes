namespace YoutubeTimecodes.Services
{
    using Azure;
    using Azure.AI.TextAnalytics;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class TextSummarizationService : ITextSummarizationService
    {
        private readonly TextAnalyticsClient _textAnalyticsClient;

        public TextSummarizationService()
        {
            string azureKeyCredential = Environment.GetEnvironmentVariable("YoutubeTimecodes_AzureKeyCredential") ?? throw new ArgumentNullException(paramName: "YoutubeTimecodes_AzureKeyCredential");
            AzureKeyCredential credentials = new AzureKeyCredential(azureKeyCredential);
            string cognitiveServiceEndpoint = Environment.GetEnvironmentVariable("YoutubeTimecodes_CognitiveServiceEndpoint") ?? throw new ArgumentNullException(paramName: "YoutubeTimecodes_CognitiveServiceEndpoint");
            Uri endpoint = new Uri(cognitiveServiceEndpoint);
            this._textAnalyticsClient = new TextAnalyticsClient(endpoint, credentials);
        }

        public async Task<List<string>> GetSummariesAsync(string text)
        {
            try
            {
                text = @"The extractive summarization feature uses natural language processing techniques to locate key sentences in an unstructured text document. 
                These sentences collectively convey the main idea of the document. This feature is provided as an API for developers. 
                They can use it to build intelligent solutions based on the relevant information extracted to support various use cases. 
                In the public preview, extractive summarization supports several languages. It is based on pretrained multilingual transformer models, part of our quest for holistic representations. 
                It draws its strength from transfer learning across monolingual and harness the shared nature of languages to produce models of improved quality and efficiency.";

                List<string> result = new List<string>();


                // Prepare analyze operation input. You can add multiple documents to this list and perform the same
                // operation to all of them.
                var batchInput = new List<string>
            {
                text
            };

                TextAnalyticsActions actions = new TextAnalyticsActions()
                {
                    ExtractSummaryActions = new List<ExtractSummaryAction>() { new ExtractSummaryAction() }
                };

                // Start analysis process.
                AnalyzeActionsOperation operation = await this._textAnalyticsClient.StartAnalyzeActionsAsync(batchInput, actions);
                await operation.WaitForCompletionAsync();
                // View operation status.
                Console.WriteLine($"AnalyzeActions operation has completed");
                Console.WriteLine();

                Console.WriteLine($"Created On   : {operation.CreatedOn}");
                Console.WriteLine($"Expires On   : {operation.ExpiresOn}");
                Console.WriteLine($"Id           : {operation.Id}");
                Console.WriteLine($"Status       : {operation.Status}");

                Console.WriteLine();
                // View operation results.
                await foreach (AnalyzeActionsResult documentsInPage in operation.Value)
                {
                    IReadOnlyCollection<ExtractSummaryActionResult> summaryResults = documentsInPage.ExtractSummaryResults;

                    foreach (ExtractSummaryActionResult summaryActionResults in summaryResults)
                    {
                        if (summaryActionResults.HasError)
                        {
                            Console.WriteLine($"  Error!");
                            Console.WriteLine($"  Action error code: {summaryActionResults.Error.ErrorCode}.");
                            Console.WriteLine($"  Message: {summaryActionResults.Error.Message}");
                            continue;
                        }

                        foreach (ExtractSummaryResult documentResults in summaryActionResults.DocumentsResults)
                        {
                            if (documentResults.HasError)
                            {
                                Console.WriteLine($"  Error!");
                                Console.WriteLine($"  Document error code: {documentResults.Error.ErrorCode}.");
                                Console.WriteLine($"  Message: {documentResults.Error.Message}");
                                continue;
                            }

                            Console.WriteLine($"  Extracted the following {documentResults.Sentences.Count} sentence(s):");
                            Console.WriteLine();

                            foreach (SummarySentence sentence in documentResults.Sentences)
                            {
                                result.Add(sentence.Text);
                                Console.WriteLine($"  Sentence: {sentence.Text}");
                                Console.WriteLine();
                            }
                        }
                    }
                }


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