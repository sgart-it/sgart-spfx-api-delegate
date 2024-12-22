namespace SgartSPFxDelgateApi.DTOs;

public class DemoResponse
{
    public DateTime Now { get; set; }
    public int ReplyParam1 { get; set; }
    public string? Data { get; set; }
    public bool IsAuthenticated { get; set; }
    public string UserName { get; set; } = string.Empty;
    public IEnumerable<ClaimValue> Claims { get; set; } = [];
    public VersionResponse Version { get; set; } = new();
}
