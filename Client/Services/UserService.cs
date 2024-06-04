using Client.Configurations;
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
	public async Task<IEnumerable<Publication>> GetFavorites()
	{
		using var request = new HttpRequestMessage(HttpMethod.Get, Path.Combine(_api.UserUrl,"favs"));

		var response = await httpClient.SendAsync(request, CancellationToken.None);

		var publications = await response.Content.ReadAsStringAsync();

		return JsonConvert.DeserializeObject<IEnumerable<Publication>>(publications) ?? throw new JsonSerializationException();

	}

	public async Task<User> GetUser()
	{
		using var request = new HttpRequestMessage(HttpMethod.Get, _api.UserUrl);

		var response = await httpClient.SendAsync(request, CancellationToken.None);

		var userString = await response.Content.ReadAsStringAsync();

		return JsonConvert.DeserializeObject<User>(userString) ?? throw new JsonSerializationException();
	}

	public Task Logout() => Task.CompletedTask;

	public async Task RegisterUser(AuthCredentials user)
	{
		using var request = new HttpRequestMessage(HttpMethod.Post, _api.UserUrl);

		var values = new Dictionary<string, object>()
		{
			{ "login", user.Login},
			{"password", user.Password }
		};

		request.Content = new StringContent(values.ToJson(), null, "application/json");

		await httpClient.SendAsync(request, CancellationToken.None);
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
