using Microsoft.AspNetCore.Mvc;
using PdfQAProcessor.Services;
using System.Threading.Tasks;

namespace PdfQAProcessor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly HuggingFaceService _huggingFaceService;
        private readonly IExtractedTextStore _extractedTextStore;

        // Constructor to inject HuggingFaceService and ExtractedTextStore
        public QuestionController(HuggingFaceService huggingFaceService, IExtractedTextStore extractedTextStore)
        {
            _huggingFaceService = huggingFaceService;
            _extractedTextStore = extractedTextStore;
        }

        // POST api/question/ask
        [HttpPost("ask")]
        public async Task<IActionResult> AskQuestion([FromBody] QuestionRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Question))
            {
                return BadRequest("Question cannot be empty.");
            }

            // Get the extracted text from IExtractedTextStore
            var context = _extractedTextStore.GetExtractedText();
            if (string.IsNullOrWhiteSpace(context))
            {
                return BadRequest("No PDF context found. Please upload a PDF first.");
            }

            try
            {
                // Get the answer from HuggingFaceService
                var answer = await _huggingFaceService.AskQuestionAsync(context, request.Question);
                return Ok(new { Answer = answer });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Return error status if something goes wrong
            }
        }
    }
    public class QuestionRequest
    {
        public string Question { get; set; } // The question asked by the user
    }
}
