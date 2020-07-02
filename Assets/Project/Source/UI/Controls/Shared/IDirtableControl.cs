namespace Exa.UI.Components
{
    /// <summary>
    /// Supports a control that can be checked for a changed value
    /// </summary>
    public interface IDirtableControl
    {
        /// <summary>
        /// Checks if the value is up-to-date
        /// </summary>
        bool IsDirty { get; }

        /// <summary>
        /// Marks the current value as being up-to-date
        /// </summary>
        void MarkClean();
    }
}