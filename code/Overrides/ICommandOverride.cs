namespace CommandEntities;

/// <summary>
/// Defines an override to the behavior of an existing console command.
/// </summary>
public interface ICommandOverride
{
    /// <summary>
    /// The name of the console command to be overridden.
    /// </summary>
    string CommandName { get; }
    /// <summary>
    /// Flags determining whether the override applies to server, client, or both.
    /// </summary>
    CommandOverrideType CommandOverrideType { get; }
    /// <summary>
    /// Executes custom behavior for the specified command. Called by
    /// <c>BaseCommandRunner.RunCommand</c>.
    /// </summary>
    /// <param name="command">
    /// Information such as command name, args, and target clients for the command being run.
    /// </param>
    void RunCommand(CommandData command);
}
