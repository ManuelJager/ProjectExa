namespace Exa.Grids.Blocks
{
    /// <summary>
    /// Supports converting a template component to the runtime data container
    /// </summary>
    /// <typeparam name="TOut"></typeparam>
    public interface ITemplateComponent<out TOut>
    {
        TOut Convert();
    }
}