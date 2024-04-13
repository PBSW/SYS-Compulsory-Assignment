namespace Shared.Domain;

/// <summary>
/// The full data structure of a tweet
/// </summary>
public class Tweet
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public int AuthorId { get; set; }
    public DateTime CreatedAt { get; set; }
}