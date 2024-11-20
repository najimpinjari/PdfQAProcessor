namespace PdfQAProcessor.Services
{
    public interface IExtractedTextStore
    {
        string GetExtractedText();
        void SetExtractedText(string extractedText);
    }
}
