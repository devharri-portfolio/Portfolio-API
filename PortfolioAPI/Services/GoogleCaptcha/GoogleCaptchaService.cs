﻿using System.Net;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PortfolioAPI.Models.GoogleCaptcha;

namespace PortfolioAPI.Services;

public class GoogleCaptchaService
{
    private readonly IConfiguration _config;

    public GoogleCaptchaService(IConfiguration config)
    {
        _config = config;
    }

    public async Task<bool> VerifyToken(string token)
    {
        try
        {
            var url = $"https://www.google.com/recaptcha/api/siteverify?secret={_config.GetSection("GoogleCaptchaSecretKey")}&response={token}";

            using(var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return false;
                }

                var responseString = await response.Content.ReadAsStringAsync();
                var googleResult = JsonConvert.DeserializeObject<GoogleCaptchaResponse>(responseString);

                return googleResult.success && googleResult.score >= 0.5;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}