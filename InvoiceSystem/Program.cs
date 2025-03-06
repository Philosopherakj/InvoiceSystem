var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddSingleton<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddSingleton<IInvoiceService, InvoiceService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
