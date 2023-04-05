using Sandbox;
using System.Collections.Generic;
using System.Linq;

namespace CommandEntities;

/// <summary>
/// Defines mappings between console commands and custom implementations of those commands.
/// These will be referred to during <c>BaseCommandRunner.RunCommand</c>, which offers
/// a way to provide sensible alternatives to commands that would cause problems when run
/// as-is by <c>ConsoleSystem</c>.
/// </summary>
public static class OverrideManager
{

    /// <summary>
    /// Defines which server commands are overridden, and what method shall be invoked
    /// in their place.
    /// </summary>
    public static Dictionary<string, ICommandOverride> ServerCommands { get; set; } = new();

    /// <summary>
    /// Defines which client commands are overridden, and what method shall be invoked
    /// in their place.
    /// </summary>
    public static Dictionary<string, ICommandOverride> ClientCommands { get; set; } = new();

    /// <summary>
    /// Finds and instantiates all implementations of <c>ICommandOverride</c> via 
    /// <c>TypeLibrary</c>. Adds these command overrides to <c>ServerCommands</c> 
    /// or <c>ClientCommands</c> depending on <c>CommandOverrideType</c>.
    /// </summary>
    [Event.Entity.PostSpawn]
    public static void Init()
    {
        var overrides = TypeLibrary.GetTypes<ICommandOverride>()
            .Where(t => !t.IsInterface);
        Log.Trace($"CommandEntities - Loaded {overrides.Count()} command override(s).");

        foreach (var type in overrides)
        {
            var instance = type.Create<ICommandOverride>();
            if (instance.CommandOverrideType.HasFlag(CommandOverrideType.Server))
            {
                AddCommand(instance, ServerCommands);
                Log.Trace($"CommandEntities - Added server command {instance.CommandName}");
            }
            if (instance.CommandOverrideType.HasFlag(CommandOverrideType.Client))
            {
                AddCommand(instance, ClientCommands);
                Log.Trace($"CommandEntities - Added client command {instance.CommandName}");
            }
        }

        Log.Trace($"CommandEntities - Loaded {ServerCommands.Count} server command overrides and {ClientCommands.Count} client command overrides.");
    }

    private static void AddCommand(ICommandOverride command, Dictionary<string, ICommandOverride> dict)
    {
        if (dict.ContainsKey(command.CommandName))
        {
            Log.Error($"Duplicate command override for \"{command.CommandName}\"");
        }
        dict[command.CommandName] = command;
    }
}
