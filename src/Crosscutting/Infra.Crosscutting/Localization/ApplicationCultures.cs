using System.Globalization;

namespace Goal.Infra.Crosscutting.Localization;

public static class ApplicationCultures
{
    public static CultureInfo Portugues { get => field ??= new CultureInfo("pt-BR"); } = null!;
    public static CultureInfo English { get => field ??= new CultureInfo("en-US"); } = null!;
}
