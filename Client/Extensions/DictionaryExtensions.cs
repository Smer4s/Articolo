using Newtonsoft.Json;

namespace Client.Extensions;

public static class DictionaryExtensions
{
    public static string ToJson(this IDictionary<string, object> dict) =>
        JsonConvert.SerializeObject(dict, Formatting.Indented);
    public static string ToJson(this IDictionary<string, string> dict) =>
        JsonConvert.SerializeObject(dict, Formatting.Indented);
}
