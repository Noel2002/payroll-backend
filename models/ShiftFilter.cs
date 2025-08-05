using System.Text.Json.Serialization;

namespace DotNetTest.Models;
public class ShiftFilter
{
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PaymentStatus? PaymentStatus { get; set; }
}