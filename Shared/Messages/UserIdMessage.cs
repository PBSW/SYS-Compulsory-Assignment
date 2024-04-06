using Shared.Messages;

namespace Shared.User.Dto;

public class UserIdMessage : IInfrastructureMessage
{
    public UserIdMessage(Dictionary<string, string> headers)
    {
        Headers = headers;
    }

    public int UserId { get; set; }
    public Dictionary<string, string> Headers { get; set; }
}