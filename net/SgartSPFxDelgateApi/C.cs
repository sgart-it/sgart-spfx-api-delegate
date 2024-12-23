using System.Globalization;

namespace SgartSPFxDelgateApi;

public static class C
{
    public const string VERSION = "1.2024-12-21";
    public const string VERSION_BUILD = "20241223"; //YYYYMMDD
    public const string API_NAME = "SgartSPFxDelgateApi";

    public const string LOG_START = "START";
    public const string LOG_STOP = "STOP";
    public const string LOG_REQUEST_BEGIN = "REQUEST_BEGIN";
    public const string LOG_REQUEST_END = "REQUEST_END";
    public const string LOG_BEGIN = "BEGIN";
    public const string LOG_END = "END";
    public const string LOG_ERROR = "ERROR";

    public static readonly CultureInfo CI_IT = new CultureInfo(1040);
}
