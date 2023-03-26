using PortfolioAPI.Models.Email;

namespace PortfolioAPI.Services.Email;

public interface IEmailService
{
    Task<bool> SendEmail(EmailDTO emailDto);
}