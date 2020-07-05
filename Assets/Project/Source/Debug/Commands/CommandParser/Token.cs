namespace Exa.Debugging.Commands.Parser
{
    /// <summary>
    /// Describes a part of the command
    /// </summary>
    public enum Token
    {
        /// <summary>
        /// Describes a string value without quotes
        /// EG: literal
        /// </summary>
        Literal,

        /// <summary>
        /// Describes a boolean value ( true ) for flag value
        /// EG: -flag
        /// </summary>
        Flag,

        /// <summary>
        /// Describes a key for a property. The key must be followed by a value of the correct type
        /// EG: --key
        /// </summary>
        Key,

        /// <summary>
        /// Describes a string value.
        /// EG: "string"
        /// </summary>
        String,

        /// <summary>
        /// Describes a number value.
        /// EG: 0.1
        /// </summary>
        Number,

        /// <summary>
        /// Represents the end of the command
        /// </summary>
        EOF
    }
}