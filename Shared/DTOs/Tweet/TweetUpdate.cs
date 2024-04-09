namespace Shared.Tweet.Dto;

/// <summary>
/// DTO for updating an existing tweet
/// </summary>
public class TweetUpdate
{
    /// <summary>
    /// The new content of the tweet 
    /// </summary>
    public string Content { get; set; } = null!;
}