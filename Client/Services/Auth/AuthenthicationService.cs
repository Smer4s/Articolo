﻿using Client.Configurations;
using Client.Models.Auth;
using Client.Services.Abstractions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Client.Extensions;

namespace Client.Services.Auth;

public class AuthenthicationService(IIdentityProviderHttpClient httpClient, IOptions<ApiOptions> options) : IAuthenticationService
{
    private readonly API _api = new API(options);
    public async Task Authenticate(AuthCredentials credentials)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, _api.TokenUrl);

        var values = new Dictionary<string, string>()
        {
            {"login", credentials.Login },
            {"password", credentials.Password },
        };

        request.Content = new StringContent(values.ToJson(), null, "application/json");

        var response = await httpClient.SendAsync(request, CancellationToken.None);
    }
}