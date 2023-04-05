using Sandbox;

namespace CommandEntities;

/// <summary>
/// Represents an entity that may be used by mappers to run commands.
/// </summary>
[EditorModel("models/editor/axis_helper_thick")]
[Category("Gameplay"), Icon("terminal")]
public abstract partial class BaseCommandRunner : Entity
{
    /// <summary>
    /// Runs the specified <paramref name="command"/> as configured. Server
    /// may be overridden per the items in <c>CommandOverrides</c>.
    /// </summary>
    /// <param name="command">The command that shall be run.</param>
    public virtual void RunCommand(CommandData command)
    {
        if (command.IsServer)
        {
            RunServerCommand(command);
        }
        else
        {
            RunClientCommand(command);
        }
    }

    private void RunServerCommand(CommandData command)
    {
        if (OverrideManager.ServerCommands.ContainsKey(command.Name))
        {
            OverrideManager.ServerCommands[command.Name].RunCommand(command);
            return;
        }
        ConsoleSystem.Run(command.Name, command.Args);
    }

    private void RunClientCommand(CommandData command)
    {
        if (OverrideManager.ClientCommands.ContainsKey(command.Name))
        {
            OverrideManager.ClientCommands[command.Name].RunCommand(command);
            return;
        }
        foreach (var client in command.ClientList)
        {
            client.SendCommandToClient(command.ToString());
        }
    }
}
