using System.Text.Json.Serialization;

public class UpdateShiftDto
{
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string? Comment { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PaymentStatus? PaymentStatus { get; set; }
};
