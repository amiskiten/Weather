namespace Weather.Data;

public record Forecast(
    [property: JsonPropertyName("forecastday")] List<OneDayData> Days)
{
    public virtual bool Equals(Forecast? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Days.SequenceEqual(other.Days);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Days);
    }
}
