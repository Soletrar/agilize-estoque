
using Newtonsoft.Json;

namespace Agilize___Transferência.Models;

internal struct Config
{
    [JsonProperty("nome")]
    public string Name { get; set; }

    [JsonProperty("login")]
    public string Login { get; set; }

    [JsonProperty("senha")]
    public string Password { get; set; }

    [JsonProperty("produtos_link")]
    public string ProductsLink { get; set; }
}