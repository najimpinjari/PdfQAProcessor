namespace PdfQAProcessor.Services
{
    public class ExtractedTextStore : IExtractedTextStore
    {
        private string _extractedText;

        public string GetExtractedText() => _extractedText;

        public void SetExtractedText(string extractedText)
        {
            _extractedText = extractedText;
        }
    }
}
