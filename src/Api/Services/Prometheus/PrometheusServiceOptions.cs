using System.ComponentModel.DataAnnotations;

namespace PicoStation.Api.Services;

public class PrometheusServiceOptions
{
    [Required]
    public string SerialPortCommand { get; set; } = string.Empty;

    [Required]
    public string SerialPortEndOfResponse { get; set; } = string.Empty;
}
