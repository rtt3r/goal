using System.Globalization;

namespace Goal.Infra.Crosscutting.Extensions;

public static class CultureInfoExtensions
{
    public static bool IsEqual(this CultureInfo culture, CultureInfo toCompare)
        => culture.CompareInfo.Name == toCompare.CompareInfo.Name;
}
