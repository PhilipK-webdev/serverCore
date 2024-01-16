using System.IO;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/api/v2/details", async context =>
{
    using (StreamReader reader = new StreamReader(context.Request.Body))
    {
        string requestBody = await reader.ReadToEndAsync();

        // Assuming your Node.js server endpoint is http://your-nodejs-server-endpoint
        string nodeJsServerUrl = "http://localhost:5000/api/v1/create";

        using (HttpClient client = new HttpClient())
        {
            StringContent content = new StringContent(JsonSerializer.Serialize(new { message = "success" }), System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(nodeJsServerUrl, content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(responseContent);
            }
            else
            {
                context.Response.StatusCode = (int)response.StatusCode;
            }
        }
    }
});

app.Run();