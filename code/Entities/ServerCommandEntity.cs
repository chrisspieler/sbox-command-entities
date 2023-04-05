using Editor;
using Sandbox;

namespace CommandEntities;

/// <summary>
/// An entity that may be used to execute commands serverside.
/// </summary>
[Library("point_servercommand"), HammerEntity]
[Title("Server Command")]
internal class ServerCommandEntity : BaseCommandRunner
{
    /// <summary>
    /// Will run the specified <paramref name="commandString"/> on the server.
    /// </summary>
    /// <param name="commandString">The command to run.</param>
    [Input(Name = "Command")]
    public void RunCommand(string commandString)
    {
        Log.Trace($"({Name}) Executing server command: {commandString}");
        var command = CommandData.FromString(commandString);
        RunCommand(command);
    }
}
