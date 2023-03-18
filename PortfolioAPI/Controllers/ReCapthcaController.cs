using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PortfolioAPI.Services;

namespace PortfolioAPI.Controllers;

[EnableCors("MyPolicy")]
[Route("api/[controller]")]
[ApiController]
public class PortfolioController : ControllerBase
{
    private readonly GoogleCaptchaService _captchaService;

    public PortfolioController(GoogleCaptchaService captchaService)
    {
        _captchaService = captchaService;
    }

    // GET: api/recaptcha
    [HttpGet]
    public async Task<bool> Get([FromQuery] string token)
    {
        //Verify response token from Google
        var captchaResult = await _captchaService.VerifyToken(token);

        if (captchaResult == false)
        {
            return false;
        }

        return true;
    }
}