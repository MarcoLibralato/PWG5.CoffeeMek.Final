namespace PWG5.CoffeeMek.Simulator.Models;

using System.Text.Json.Serialization;

public class ProductionMessage
{
    [JsonPropertyName("location")]
    public LocationEnum Location { get; set; }
    [JsonPropertyName("machineStatus")]
    public string MachineStatus { get; set; }
    [JsonPropertyName("timestampLocal")]
    public DateTimeOffset TimestampLocal { get; set; }
    [JsonPropertyName("timestampUtc")]
    public DateTimeOffset TimestampUtc { get; set; }
    [JsonPropertyName("isMachineBlocked")]
    public bool IsMachineBlocked { get; set; }
    [JsonPropertyName("blockDescription")]
    public string BlockDescription { get; set; } = default!;
    [JsonPropertyName("lastMaintenance")]
    public DateTimeOffset LastMaintenance { get; set; }
    [JsonPropertyName("payload")]
    public object Payload { get; set; }

}
