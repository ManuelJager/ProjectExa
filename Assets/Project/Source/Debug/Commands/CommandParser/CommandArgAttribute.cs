using System;

namespace Exa.Debugging.Commands.Parser
{
    /// <summary>
    /// Provides context for a property
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class CommandArgAttribute : Attribute
    {
        /// <summary>
        /// Argument tooltip
        /// </summary>
        internal string HelpText { get; }

        /// <summary>
        /// Order in the argument list
        /// </summary>
        internal int ArgumentOrder { get; }

        internal string[] aliases { get; }

        /// <summary>
        /// Flags if the argument key should be typed out in full, or if the value should be inferred from the position of itself in the argument list
        /// </summary>
        internal bool Positional => ArgumentOrder != -1;

        public CommandArgAttribute()
        {
            this.HelpText = "default help text";
            this.ArgumentOrder = -1;
            this.aliases = null;
        }

        public CommandArgAttribute(string HelpText)
        {
            this.HelpText = HelpText;
            this.ArgumentOrder = -1;
            this.aliases = null;
        }

        public CommandArgAttribute(int ArgumentOrder = -1, string HelpText = "default help text")
        {
            this.HelpText = HelpText;
            this.ArgumentOrder = ArgumentOrder;
            this.aliases = null;
        }

        public CommandArgAttribute(string[] aliases, string HelpText = "default help text")
        {
            this.HelpText = HelpText;
            this.ArgumentOrder = -1;
            this.aliases = aliases;
        }
    }
}