namespace Exa.Grids.Blocks
{
    public interface ITemplateComponent<TOut>
    {
        TOut Convert();
    }
}