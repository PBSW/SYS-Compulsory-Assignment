namespace Shared.Tweet.Dto;

/// <summary>
/// Dto for displaying a tweet
/// </summary>
public class TweetDTO
{
    public int Id { get; set; }
    public ulong AuthorId { get; set; }
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}