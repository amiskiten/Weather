namespace Weather.Data;
public record OneDayData(
    [property: JsonPropertyName("date")] DateTime Date,
    [property: JsonPropertyName("day")] DailyWeather Daily,
    [property: JsonPropertyName("astro")] AstroData AstroData,
    [property: JsonPropertyName("hour")] List<HourlyWeather> Hourly)
{
    public virtual bool Equals(OneDayData? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return 
            AstroData.DeepEquals(other.AstroData) &&
            Date.Equals(other.Date) &&
            Daily.DeepEquals(other.Daily) &&
            Hourly.SequenceEqual(other.Hourly);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Date,Daily, Hourly, AstroData);
    }
}