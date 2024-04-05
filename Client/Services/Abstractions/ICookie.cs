namespace Client.Services.Abstractions
{
    public interface ICookie
    {
        public Task DeleteCookie(string key);
        public Task SetValue(string key, string value, int? days = null);
        public Task<string> GetValue(string key, string def = "");
    }
}
