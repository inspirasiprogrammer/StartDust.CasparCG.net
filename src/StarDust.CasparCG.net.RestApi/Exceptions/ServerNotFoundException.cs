namespace StarDust.CasparCG.net.RestApi.Exceptions;

public class ServerNotFoundException : InvalidOperationException
{
    public Guid ServerId { get; }
    public ServerNotFoundException(Guid serverId) : base($"The server with id: {serverId} no more exists.")
    {
        ServerId = serverId;
    }
}