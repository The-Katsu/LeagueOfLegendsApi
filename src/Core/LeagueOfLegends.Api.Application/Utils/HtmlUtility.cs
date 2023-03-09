namespace LeagueOfLegends.Api.Application.Utils;

public static class HtmlUtility
{
    public static string GetPlaintText(string htmlString)
    {
        var separators = new[] {"&nbsp", "<p>", "</p>"};
        return separators
            .Aggregate(htmlString, (current, separator) => 
                current.Replace(separator, string.Empty));
    }
}