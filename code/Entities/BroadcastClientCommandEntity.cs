using Editor;
using Sandbox;

namespace CommandEntities;

/// <summary>
/// An entity that may be used run a command on every client.
/// </summary>
[Library("point_broadcastclientcommand"), HammerEntity]
[Title("Broadcast Client Command")]
public class BroadcastClientCommandEntity : BaseCommandRunner
{
    /// <summary>
    /// Will run the specified <paramref name="commandString"/> on every client.
    /// </summary>
    [Input(Name = "Command")]
    public void RunCommand(string commandString)
    {
        var commandData = CommandData.FromString(commandString).AsBroadcastCommand();
        RunCommand(commandData);
    }
}
