
namespace Shared.Tweet.Dto;

/// <summary>
/// DTO for creating new tweets
/// </summary>
public class TweetCreate
{
    /// <summary>
    /// The text content of the tweet
    /// </summary>
    public string Content { get; set; } = null!;
}