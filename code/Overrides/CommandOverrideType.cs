using System;

namespace CommandEntities;

/// <summary>
/// Specifies whether a command override applies to the server, clients, or both.
/// </summary>
[Flags]
public enum CommandOverrideType
{
    /// <summary>
    /// Specifies that this command override applies to the server.
    /// </summary>
    Server = 1,
    /// <summary>
    /// Specifies that this command override applies to clients.
    /// </summary>
    Client = 2
}
