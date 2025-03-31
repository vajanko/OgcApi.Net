using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OgcApi.Net.Resources;

public class Queryables
{
    [JsonPropertyName("$id")]
    public string Id { get; set; }

    [JsonPropertyName("$schema")]
    public string Schema { get; set; }

    public string Type { get; set; }

    public string Title { get; set; }

    public Dictionary<string, QueryableProperty> Properties { get; set; }
}
