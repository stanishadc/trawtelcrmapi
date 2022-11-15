using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using Amazon.S3;
using Microsoft.AspNetCore.HttpOverrides;
using Swashbuckle.AspNetCore.SwaggerUI;
using TrawtelAPI.CustomMiddleware;
using TrawtelCRMAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureRepositoryWrapper();
builder.Services.ConfigureMySqlContext(builder.Configuration);
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddAWSService<IAmazonDynamoDB>();
builder.Services.AddScoped<IDynamoDBContext, DynamoDBContext>();
builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("../swagger/v1/swagger.json", "Trawtel CRM API v1");
        c.DocExpansion(DocExpansion.None);
    });
}
else
{
    //app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("../swagger/v1/swagger.json", "Trawtel CRM API v1");
        c.DocExpansion(DocExpansion.None);
    });
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});
app.UseMiddleware<ApiKeyAuthMiddleware>();
app.UseCors("CorsPolicy");
app.UseAuthorization();
app.MapControllers();

app.Run();