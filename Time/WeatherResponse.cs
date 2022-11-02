namespace Time
{
    public class WeatherResponse
    {
        public TemperatureInfo Main { get; set; }
        public CountryInfo Sys { get; set; }
        public string Name { get; set; }
    }
}
