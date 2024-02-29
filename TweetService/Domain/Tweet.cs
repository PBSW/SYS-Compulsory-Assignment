namespace TweetService.Domain;

public class Tweet
{
    public ulong Id { get; set; }
    public string? Content { get; set; }
    public string? Author { get; set; }
    public DateTime CreatedAt { get; set; }
    public IEnumerable<ulong> Likes { get; set; }
    public IEnumerable<Tweet> Replies { get; set; }
    public Tweet? ReplyTo { get; set; }
}