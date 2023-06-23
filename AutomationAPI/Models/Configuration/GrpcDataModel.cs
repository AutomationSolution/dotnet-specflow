namespace AutomationAPI.Models.Configuration;

public class GrpcDataModel
{
    public const string JsonSectionName = "gRPC";

    public Uri gRPCEndpoint { get; set; }
}
