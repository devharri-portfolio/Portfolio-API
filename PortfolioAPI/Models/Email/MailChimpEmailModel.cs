namespace PortfolioAPI.Models.Email;

public class MailChimpEmailModel
{
    public string? key { get; set; }
    public MailChimpMessageModel? message { get; set; }
}