using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfQAProcessor.Services;
using System.IO;

namespace PdfQAProcessor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController_new : ControllerBase
    {
        private readonly PdfService _pdfService;
        private readonly IExtractedTextStore _extractedTextStore;

        // Constructor to inject PdfService and ExtractedTextStore service
        public PdfController_new(PdfService pdfService, IExtractedTextStore extractedTextStore)
        {
            _pdfService = pdfService;
            _extractedTextStore = extractedTextStore;
        }

        // POST api/pdf/upload
        [HttpPost("upload")]
        public IActionResult UploadPdf([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Please upload a valid PDF file.");
            }

            // Define the file path to save the uploaded PDF
            var filePath = Path.Combine("Uploads", file.FileName);
            Directory.CreateDirectory("Uploads"); // Ensure the directory exists

            // Save the file to the server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            // Extract text from the uploaded PDF
            var extractedText = _pdfService.ExtractTextFromPdf(filePath);

            // Store the extracted text in the IExtractedTextStore (singleton service)
            _extractedTextStore.SetExtractedText(extractedText);

            // Optionally delete the file after processing
            System.IO.File.Delete(filePath);

            return Ok(new { Message = "PDF uploaded and processed.", ExtractedText = extractedText });
        }
    }
}
