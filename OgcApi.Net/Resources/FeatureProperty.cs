using System.Text.Json.Serialization;

namespace OgcApi.Net.Resources;

public class FeatureProperty
{
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public bool Queryable { get; set; }

    public string Type { get; set; }

    public string Title { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Description { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Format { get; set; }

    // string properties
    public int? MinLength { get; set; }
    public int? MaxLength { get; set; }
    public string Pattern { get; set; }
    // enum like
    // "enum" : [ "AK121", "AL012", "AL030", "AL130", "BH075" ]
    // minLength, maxLength, enum and/or pattern


    // numeric properties
    public int? MultipleOf { get; set; }
    public int? Minimum { get; set; }
    public int? ExclusiveMinimum { get; set; }
    public int? Maximum { get; set; }
    public int? ExclusiveMaximum { get; set; }

    // temporal property - string property
    // date-time, date, time, or duration
    // "format" : "date-time"

    // geometry
    // "$ref" : "https://geojson.org/schema/Point.json"
}
