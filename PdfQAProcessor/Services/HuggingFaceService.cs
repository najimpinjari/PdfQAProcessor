using RestSharp;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace PdfQAProcessor.Services
{
    public class HuggingFaceService
    {
        private const string HuggingFaceApiUrl = "https://api-inference.huggingface.co/models/deepset/roberta-base-squad2";
        private readonly string _apiKey;

        public HuggingFaceService(string apiKey) => _apiKey = apiKey;

        //public HuggingFaceService(string apiKey)
        //{
        //    _apiKey = apiKey;
        //}

        public async Task<string> AskQuestionAsync(string context, string question)
        {
            var client = new RestClient(HuggingFaceApiUrl);
            // var request = new RestRequest(Method.Post);
            var request = new RestRequest("/your-endpoint", Method.Post);
            request.AddHeader("Authorization", $"Bearer {_apiKey}");
            request.AddHeader("Content-Type", "application/json");

            var requestBody = new
            {
                inputs = new
                {
                    question = question,
                    context = context
                }
            };

            request.AddJsonBody(requestBody);

            var response = await client.ExecuteAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception($"Error: {response.StatusCode} - {response.Content}");
            }

            dynamic result = JsonConvert.DeserializeObject(response.Content);
            return result?.answer ?? "No answer found.";
        }
    }
}
