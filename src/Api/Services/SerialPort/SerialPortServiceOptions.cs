using System.ComponentModel.DataAnnotations;
using System.IO.Ports;
using System.Text.Json.Serialization;

namespace PicoStation.Api.Services;

public class SerialPortServiceOptions
{
    [Required, RegularExpression("COM\\d+")]
    public string PortName { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int BaudRate { get; set; } = 9600;

    [Range(typeof(TimeSpan), "00:00:00", "01:00:00")]
    public TimeSpan ReadTimeout { get; set; } = TimeSpan.Zero;

    [Range(typeof(TimeSpan), "00:00:00", "01:00:00")]
    public TimeSpan WriteTimeout { get; set; } = TimeSpan.Zero;

    public bool DtrEnable { get; set; } = false;

    public bool RtsEnable { get; set; } = false;

    public StopBits StopBits { get; set; } = StopBits.One;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public SerialPortEncoding Encoding { get; set; } = SerialPortEncoding.ASCII;

    public string NewLine { get; set; } = "\n";

    public bool IgnoreFirstLineInResponse { get; set; } = false;
}
