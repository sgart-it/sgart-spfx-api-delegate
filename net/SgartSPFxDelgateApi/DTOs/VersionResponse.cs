namespace SgartSPFxDelgateApi.DTOs;

public class VersionResponse
{
    public string Version { get; set; } = C.VERSION + "." + C.VERSION_BUILD;
    public string Name { get; set; } = C.API_NAME;
}
