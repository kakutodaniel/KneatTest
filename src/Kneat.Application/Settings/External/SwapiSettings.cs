namespace Kneat.Application.Settings.External
{
    public class SwapiSettings: BaseSettings
    {
        public static string Section => "SwapiSettings";

        public string BaseUrl { get; set; }

        public string StarShipsResource { get; set; }

        public string ContentType { get; set; }
       

    }
}
