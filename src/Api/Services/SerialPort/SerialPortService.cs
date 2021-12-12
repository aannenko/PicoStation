using System.IO.Ports;
using Microsoft.Extensions.Options;

namespace PicoStation.Api.Services;

public class SerialPortService
{
    private readonly ILogger<SerialPortService> _logger;
    private readonly SerialPortServiceOptions _options;
    private readonly SerialPort _port;

    public SerialPortService(
        ILogger<SerialPortService> logger,
        IOptions<SerialPortServiceOptions> options)
    {
        _logger = logger;
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _port = GetSerialPort(options.Value);
    }

    public string SendCommand(string command, string endOfResponse)
    {
        _logger.LogDebug($"Opening serial port '{_port.PortName}'.");
        _port.Open();

        try
        {
            _logger.LogDebug($"Sending command '{command}' to serial port '{_port.PortName}'.");
            _port.WriteLine(command);

            _logger.LogDebug($"Reading response from serial port '{_port.PortName}'.");
            if (_options.IgnoreFirstLineInResponse)
                _port.ReadLine();

            return _port.ReadTo(endOfResponse);
        }
        finally
        {
            _logger.LogDebug($"Closing serial port '{_port.PortName}'.");
            _port.Close();
        }
    }

    private static SerialPort GetSerialPort(SerialPortServiceOptions options) =>
        new(options.PortName, options.BaudRate)
        {
            ReadTimeout = options.ReadTimeout == TimeSpan.Zero
                ? SerialPort.InfiniteTimeout
                : (int)options.ReadTimeout.TotalMilliseconds,

            WriteTimeout = options.WriteTimeout == TimeSpan.Zero
                ? SerialPort.InfiniteTimeout
                : (int)options.WriteTimeout.TotalMilliseconds,

            DtrEnable = options.DtrEnable,
            RtsEnable = options.RtsEnable,
            StopBits = options.StopBits,
            Encoding = options.Encoding.GetEncoding(),
            NewLine = options.NewLine
        };

    public void Dispose() => _port?.Dispose();
}
