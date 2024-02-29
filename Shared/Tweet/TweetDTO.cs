namespace Shared;

public class TweetDTO
{
    public ulong Id { get; set; }
    public string? Content { get; set; }
    public string? Author { get; set; }
    public DateTime CreatedAt { get; set; }
    public IEnumerable<ulong> Likes { get; set; }
    public IEnumerable<TweetDTO> Replies { get; set; }
    public TweetDTO? ReplyTo { get; set; }
}