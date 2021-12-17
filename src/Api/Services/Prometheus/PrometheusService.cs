using Microsoft.Extensions.Options;

namespace PicoStation.Api.Services;

public class PrometheusService
{
    private readonly ILogger<PrometheusService> _logger;
    private readonly PrometheusServiceOptions _options;
    private readonly SerialPortService _serialPortService;

    public PrometheusService(
        ILogger<PrometheusService> logger,
        IOptions<PrometheusServiceOptions> options,
        SerialPortService serialPortService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _serialPortService = serialPortService ?? throw new ArgumentNullException(nameof(serialPortService));
    }

    public string GetMetrics()
    {
        _logger.LogDebug("Reading Prometheus metrics from serial port.");
        return _serialPortService
            .SendCommand(_options.SerialPortCommand, _options.SerialPortEndOfResponse)
            .Replace("\r", string.Empty);
    }
}
