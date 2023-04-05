using Sandbox;

namespace CommandEntities;

/// <summary>
/// Works around errors received when running the "map" console command from code.
/// </summary>
public class MapCommandOverride : ICommandOverride
{
    public string CommandName => "map";

    public CommandOverrideType CommandOverrideType => CommandOverrideType.Server;

    /// <summary>
    /// Loads the map specified by the first argument of <paramref name="command"/>.
    /// </summary>
    /// <param name="command">
    /// The command whose first argument shall be interpreted as a map name to load.
    /// </param>
    public void RunCommand(CommandData command)
    {
        if (command.Args.Length < 1)
        {
            Log.Error($"Unable to load map: no map name was provided!");
            return;
        }
        Game.ChangeLevel(command.Args[0]);
    }
}
