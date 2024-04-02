using Shared.Messages;

namespace Shared.User.Dto;

public class UserIdMessage : IInfrastructureMessage
{
    public int UserId { get; set; }
    public Dictionary<string, string> Headers { get; set; }
}