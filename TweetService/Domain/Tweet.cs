namespace TweetService.Domain;

/// <summary>
/// The full data structure of a tweet
/// </summary>
public class Tweet
{
    /// <summary>
    /// The id of the tweet
    /// </summary>
    public ulong Id { get; set; }
    
    /// <summary>
    /// The text content of the tweet
    /// </summary>
    public string Content { get; set; } = null!;

    /// <summary>
    /// The id of the user who created this tweet
    /// </summary>
    public ulong AuthorId { get; set; }

    /// <summary>
    /// The timestamp of when this tweet was created
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// A list of user ids of everyone who has liked this tweet
    /// </summary>
    public List<ulong> Likes { get; set; } = [];

    /// <summary>
    /// A list of comments to this tweet
    /// </summary>
    public List<Tweet> Replies { get; set; } = [];
    
    /// <summary>
    /// The parent tweet if this is a comment
    /// </summary>
    public Tweet? ReplyTo { get; set; }
}