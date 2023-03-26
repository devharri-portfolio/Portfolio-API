namespace PortfolioAPI.Services.GoogleCaptcha;

public interface IGoogleCaptchaService
{
    Task<bool> VerifyToken(string token);
}