﻿using Client.Configurations;
using Client.Extensions;
using Client.Models;
using Client.Models.Auth;
using Client.Models.Publication;
using Client.Services.Abstractions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net;

namespace Client.Services;

public class UserService(IIdentityProviderHttpClient httpClient, IOptions<ApiOptions> options) : IUserService
{
	private readonly API _api = new API(options);
	public Task<IEnumerable<Publication>> GetFavorites()
	{
		throw new NotImplementedException();
	}

	public async Task<User> GetUser()
	{
		using var request = new HttpRequestMessage(HttpMethod.Get, _api.UserUrl);

		var response = await httpClient.SendAsync(request, CancellationToken.None);

		var userString = await response.Content.ReadAsStringAsync();

		return JsonConvert.DeserializeObject<User>(userString) ?? throw new JsonSerializationException();
	}

	public Task Logout() => Task.CompletedTask;

	public Task RegisterUser(AuthCredentials credentials)
	{
		throw new NotImplementedException();
	}

	public async Task UpdateUser(User user)
	{
		using var request = new HttpRequestMessage(HttpMethod.Put, _api.UserUrl);

		var values = new Dictionary<string, object>()
		{
			{ "userName", user.UserName },
			{ "login", user.Login},
			{ "birthDay", user.BirthDay },
			{ "email", user.Email },
			{ "description",user.Description },
			{ "gender",user.Gender},
		};

		request.Content = new StringContent(values.ToJson(), null, "application/json");

		await httpClient.SendAsync(request, CancellationToken.None);
	}
}
