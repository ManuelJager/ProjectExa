using Exa.Grids.Blocks.Components;

namespace Exa.Grids.Blocks
{
    /// <summary>
    /// Supports converting a template component to the runtime data container
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITemplatePartial<out T>
        where T : IBlockComponentValues
    {
        T ToBaseComponentValues();
    }
}