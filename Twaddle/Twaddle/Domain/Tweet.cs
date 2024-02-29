
public class Tweet
{
    public ulong Id { get; set; }
    public string? Text { get; set; }
    public string? Author { get; set; }
    public IEnumerable<Tweet> Replies { get; set; }
    public int Likes { get; set; }
    public DateTime CreatedAt { get; set; }
}

