namespace wallet.infrastructure.Configurations;

public record Config
{
    public string Environment { get; init; }
    public string Type { get; init; }
}
