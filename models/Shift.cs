using System.Text.Json.Serialization;

namespace DotNetTest.Models
{
    public class Shift
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.PENDING;
        public Job Job { get; set; }
        public User User { get; set; }

    }
}

public enum PaymentStatus
{
    PENDING,
    COMPLETED,
}