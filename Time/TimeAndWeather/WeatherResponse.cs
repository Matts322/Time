namespace Time
{
    public class WeatherResponse
    {
        public WeatherResponse(TemperatureInfo main, CountryInfo sys, string name)
        {
            Main = main;
            Sys = sys;
            Name = name;
        }

        public TemperatureInfo Main { get; }
        public CountryInfo Sys { get; }
        public string Name { get; }
    }
}