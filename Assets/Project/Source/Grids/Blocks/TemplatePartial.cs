using Exa.Grids.Blocks.Components;

namespace Exa.Grids.Blocks
{
    public abstract class TemplatePartial<T> : TemplatePartialBase, ITemplatePartial<T>
        where T : IBlockComponentData
    {
        public abstract T Convert();

        public override object DynamicConvert()
        {
            return Convert();
        }
    }

    public abstract class TemplatePartialBase
    {
        public abstract object DynamicConvert();
    }
}