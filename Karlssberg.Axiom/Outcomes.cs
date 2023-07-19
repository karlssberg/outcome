namespace Karlssberg.Axiom;

public record Outcomes<TOutcome>
{
    public required TOutcome TrueOutcome { get; init; }
    public required TOutcome FalseOutcome { get; init; }

    internal TOutcome Resolve(bool value) => value ? TrueOutcome : FalseOutcome;
}

