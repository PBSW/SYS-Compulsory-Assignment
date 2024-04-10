namespace Shared.Tweet.Dto;

/// <summary>
/// Dto for displaying a tweet
/// </summary>
public class TweetDTO
{
    /// <summary>
    /// The id of the tweet
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// The user id of the tweet creator
    /// </summary>
    public ulong AuthorId { get; set; }

    /// <summary>
    /// The content of the tweet 
    /// </summary>
    public string Content { get; set; } = null!;
    
    /// <summary>
    /// The UTC Datetime of when this tweet was first published 
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// A list of user ids of everyone who has liked this tweet
    /// </summary>
    public List<ulong> Likes { get; set; } = [];

    /// <summary>
    /// A list of tweets that are comments/responses to this one
    /// </summary>
    public List<TweetDTO> Replies { get; set; } = [];

    /// <summary>
    /// if this tweet is a comment or reply this will be the parent tweet
    /// </summary>
    public TweetDTO? ReplyTo { get; set; } = null;
}