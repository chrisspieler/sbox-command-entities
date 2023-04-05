using Sandbox;
using System.Linq;
using System.Text.RegularExpressions;

namespace CommandEntities;

/// <summary>
/// Holds all data necessary to run a console command. Facilitiates the parsing 
/// and configuration of this data.
/// </summary>
public struct CommandData
{
    /// <summary>
    /// The name of the command that shall be run.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// All args of this command, in string format.
    /// </summary>
    public string[] Args { get; set; }
    /// <summary>
    /// The list of clients for whom this command shall be run. If null,
    /// this is a server command that shall not be run on clients.
    /// </summary>
    public To? ClientList { get; set; }

    /// <summary>
    /// If true, this command is configured to run on a client rather than a server.
    /// </summary>
    public bool IsClient => ClientList != null;

    /// <summary>
    /// If true, this command is configured to run on a server rather than a client.
    /// </summary>
    public bool IsServer => !IsClient;

    /// <summary>
    /// Creates a CommandData from <paramref name="commandString"/>, populating 
    /// the <c>Name</c> and <c>Args</c> properties. <c>ClientList</c> will remain
    /// null, implying that this is a server command. You should call <c>AsTargetedCommand</c>
    /// or <c>AsBroadcastCommand</c> if this command is meant to run on a client.
    /// </summary>
    /// <param name="commandString">
    /// The string that shall be parsed to populate <c>Name</c> and <c>Args</c>.
    /// </param>
    public static CommandData FromString(string commandString)
    {
        // Split the commandString in to smaller sections. These sections are delimited
        // by whitespace unless wrapped in double quotes.
        //
        // For example, the following two lines are both considered to be three sections:
        // chat_add ducc Hello
        // chat_add ducc "Hey, what's going on?"
        var splitString = Regex.Split(commandString, "(?<=^[^\"]*(?:\"[^\"]*\"[^\"]*)*) (?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
        string[] args = null;
        if (splitString.Length > 1)
        { 
            args = splitString.Skip(1).ToArray(); 
        }
        return new CommandData()
        {
            Name = splitString[0],
            Args = args
        };
    }

    /// <summary>
    /// Specify that this command would be run on the server only.
    /// </summary>
    public CommandData AsServerCommand()
    {
        var newCommand = this;
        newCommand.ClientList = null;
        return newCommand;
    }

    /// <summary>
    /// Specify that this command would be run on the specified clients only.
    /// </summary>
    /// <param name="clients">The clients to whom this command would be sent.</param>
    public CommandData AsTargetedCommand(To clients)
    {
        var newCommand = this;
        newCommand.ClientList = clients;
        return newCommand;
    }

    /// <summary>
    /// Specify that this command would be run on all clients only.
    /// </summary>
    public CommandData AsBroadcastCommand() => AsTargetedCommand(To.Everyone);

    /// <summary>
    /// Returns a string including the name and args of this command such that
    /// the string may be used as a console command.
    /// </summary>
    public override string ToString()
    {
        var argString = string.Join(' ', Args);
        return $"{Name} {argString}";
    }
}
