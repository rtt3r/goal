using System.Globalization;

namespace Goal.Infra.Crosscutting.Localization;

public static class ApplicationCultures
{
    private static CultureInfo portugues = null!;
    private static CultureInfo english = null!;

    public static CultureInfo Portugues => portugues ??= new CultureInfo("pt-BR");
    public static CultureInfo English => english ??= new CultureInfo("en-US");
}
