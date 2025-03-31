using System.Text.Json.Serialization;

namespace OgcApi.Net.Resources;

public class QueryableProperty
{
    /// <summary>
    /// Possible values: integer, number, string, boolean, array, object
    /// Required except for geometry type property. In that case <see cref="Type"/> is ommited and <see cref="Reference"/> is used.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Type { get; set; }

    /// <summary>
    /// Name of the queryable property
    /// </summary>
    public string Title { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Description { get; set; }

    /// <summary>
    /// Possible values: date-time, date, time, duration
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Format { get; set; }

    // string properties
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? MinLength { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? MaxLength { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Pattern { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[] Enum { get; set; }

    // numeric properties
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? MultipleOf { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Minimum { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? ExclusiveMinimum { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Maximum { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? ExclusiveMaximum { get; set; }

    /// <summary>
    /// Only valid when this is a geometry property. The JSON schema of the GeoJSON geometry object
    /// </summary>
    /// <example>https://geojson.org/schema/Point.json</example>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("$ref")]
    public string Reference { get; set; }
}
