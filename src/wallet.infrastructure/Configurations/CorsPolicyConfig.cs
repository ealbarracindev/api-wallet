namespace wallet.infrastructure.Configurations;

public record CorsPolicyConfig()
{
    public string CorsPolicy { get; init; }
}
