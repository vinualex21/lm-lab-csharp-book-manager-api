using System.Text.Json.Serialization;

namespace BookManagerApi.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Genre
    {
        Thriller,
        Romance,
        Fantasy,
        Fiction,
        Horror,
        Education,
    }
}

