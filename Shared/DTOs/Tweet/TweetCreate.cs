
namespace Shared.Tweet.Dto;

/// <summary>
/// DTO for creating new tweets
/// </summary>
public class TweetCreate
{
    public string Content { get; set; } = null!;
    public int AuthorId { get; set; }
}