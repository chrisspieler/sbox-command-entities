using Editor;
using Sandbox;

namespace CommandEntities;

/// <summary>
/// An entity that may be used to run commands on a specific client.
/// </summary>
[Library("point_clientcommand"), HammerEntity]
[Title("Client Command")]
public class ClientCommandEntity : BaseCommandRunner
{
    /// <summary>
    /// Under the context of the client associated with <paramref name="activator"/>, run 
    /// the specified <paramref name="commandString"/>.
    /// </summary>
    [Input( Name = "Command" )]
    public void RunCommand(Entity activator, string commandString)
    {
        if (activator.Client == null)
        {
            Log.Info($"{Name} - Cannot run command for clientless target: {activator}");
            return;
        }
        var toClient = To.Single(activator.Client);
        var commandData = CommandData.FromString(commandString)
            .AsTargetedCommand(toClient);
        RunCommand(commandData);
    }
}