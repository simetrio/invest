namespace Invest.TaxCalculator.BusinessLogic
{
    public static class Container
    {
        public static void ConfigureContainer(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<WeatherForecastService>();
        }
    }
}