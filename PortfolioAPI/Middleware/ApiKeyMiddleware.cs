namespace PortfolioAPI.Middleware;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _config;
    const string APIKEY = "x-api-key";

    public ApiKeyMiddleware(RequestDelegate next, IConfiguration config)
	{
        _next = next;
        _config = config;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string? apiKey = context.Request.Headers[APIKEY];

        if(!ApiKeyValidation(apiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized client");
            return;
        }

        await _next(context);
    }

    private bool ApiKeyValidation(string? apikey)
    {
        if(string.IsNullOrWhiteSpace(apikey))
        {
            return false;
        }

        string actualApiKey = _config.GetValue<string>("ApiKey")!;

        return apikey == actualApiKey;
    }
}