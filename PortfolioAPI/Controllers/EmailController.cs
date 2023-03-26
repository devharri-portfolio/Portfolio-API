using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PortfolioAPI.Models.Email;
using PortfolioAPI.Services.Email;

namespace PortfolioAPI.Controllers;

[EnableCors("EmailPolicy")]
[Route("api/[controller]")]
[ApiController]
public class EmailController : ControllerBase
{
    private readonly IEmailService _emailService;

    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }
    // POST api/email
    [HttpPost]
    public async Task<bool> Post([FromBody]EmailDTO emailDTO)
    {
        var respose = await _emailService.SendEmail(emailDTO);

        return respose;
    }
}