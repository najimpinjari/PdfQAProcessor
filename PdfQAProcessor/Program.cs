using PdfQAProcessor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add essential services to the container
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Register PdfService and HuggingFaceService (example)
builder.Services.AddSingleton<PdfService>();
builder.Services.AddSingleton<HuggingFaceService>(new HuggingFaceService("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"));

var app = builder.Build();

// Configure Swagger and endpoints
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
