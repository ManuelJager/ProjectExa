namespace Exa.UI.Controls
{
    /// <summary>
    /// Provides an interface for data objects that may be presented with an in-game tooltip
    /// </summary>
    public interface ITooltipPresenter
    {
        /// <summary>
        /// Get the collection of tooltip components
        /// </summary>
        /// <returns></returns>
        ITooltipComponent[] GetComponents();
    }
}