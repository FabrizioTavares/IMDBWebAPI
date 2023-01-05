namespace Service;

public static class Settings
{
    public static string ImdbApiSecret => Environment.GetEnvironmentVariable("IMDB_API_Secret") ?? string.Empty;
}