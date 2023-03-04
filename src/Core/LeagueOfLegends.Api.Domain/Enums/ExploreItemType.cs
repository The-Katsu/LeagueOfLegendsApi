using System.ComponentModel;

namespace LeagueOfLegends.Api.Domain.Enums;

public enum ExploreItemType
{
    [Description("featured-video")]
    FeaturedVideo,
    [Description("story-preview")]
    StoryPreview,
    [Description("link-out")]
    LinkOut
}