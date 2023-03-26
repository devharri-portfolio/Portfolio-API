using PortfolioAPI.Models.GoogleCaptcha;
using PortfolioAPI.Services.Email;
using PortfolioAPI.Services.GoogleCaptcha;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("GoogleCaptchaPolicy",
        policy =>
        {
            policy.WithOrigins("https://www.harrihonkanen.com")
                                .WithMethods("GET");
        });
    options.AddPolicy("EmailPolicy",
    policy =>
    {
        policy.WithOrigins("https://www.harrihonkanen.com")
                            .WithMethods("POST");
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IGoogleCaptchaService,GoogleCaptchaService>();
builder.Services.AddTransient<IEmailService,EmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();

