using System.Text;

namespace PicoStation.Api.Services;

public static class SerialPortEncodingExtensions
{
    public static Encoding GetEncoding(this SerialPortEncoding serialPortEncoding) =>
        serialPortEncoding switch
        {
            SerialPortEncoding.ASCII => Encoding.ASCII,
            SerialPortEncoding.UTF8 => Encoding.UTF8,
            SerialPortEncoding.UTF32 => Encoding.UTF32,
            SerialPortEncoding.Unicode => Encoding.Unicode,
            SerialPortEncoding.BigEndianUnicode => Encoding.BigEndianUnicode,
            _ => throw new ArgumentException("Invalid serial port encoding.", nameof(serialPortEncoding))
        };
}
