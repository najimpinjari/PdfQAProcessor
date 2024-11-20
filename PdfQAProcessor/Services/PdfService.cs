using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;

namespace PdfQAProcessor.Services
{
    public class PdfService
    {
        public string ExtractTextFromPdf(string filePath)
        {
            using var pdfReader = new PdfReader(filePath);
            using var pdfDocument = new PdfDocument(pdfReader);
            var text = new System.Text.StringBuilder();

            for (int i = 1; i <= pdfDocument.GetNumberOfPages(); i++)
            {
                var page = pdfDocument.GetPage(i);
                text.Append(PdfTextExtractor.GetTextFromPage(page));
            }

            return text.ToString();
        }
    }
}
