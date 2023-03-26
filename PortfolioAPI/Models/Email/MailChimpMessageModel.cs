namespace PortfolioAPI.Models.Email;

public class MailChimpMessageModel
{
    public string? from_email { get; set; }
    public string? subject { get; set; }
    public string? text { get; set; }
    public List<To>? to { get; set; }
}

public class To
{
    public string? email { get; set; }
    public string? type { get; set; }
}