namespace Shared.Messages.AuthMessages;

public class ValidateTokenMessage
{
    public DateTime Expiration { get; set; }
    public string Token { get; set; }
}

public class ValidateTokenResponse
{
    public bool Valid { get; set; }
    public int? UserId { get; set; }
}

public class CreateTokenMessage
{
    public int UserId { get; set; }
    public DateTime Expiration { get; set; }
}

public class CreateTokenResponse
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}
