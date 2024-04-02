namespace Shared.Messages;

public interface IInfrastructureMessage
{
    Dictionary<string, string> Headers { get; set; }
}