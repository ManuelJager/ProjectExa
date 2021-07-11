using System;
using Exa.Grids.Blocks.Components;

namespace Exa.Grids.Blocks {
    /// <summary>
    ///     Supports converting a template component to the runtime data container
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITemplatePartial<out T> : ITemplatePartial
        where T : struct, IBlockComponentValues {
        T ToBaseComponentValues();
    }

    public interface ITemplatePartial {
        Type GetDataType();
    }

    public static class ITemplatePartialExtensions {
        public static bool GetDataTypeIsOf<TType>(this ITemplatePartial partial)
            where TType : IBlockComponentValues {
            return partial.GetDataTypeIsOf(typeof(TType));
        }

        public static bool GetDataTypeIsOf(this ITemplatePartial partial, Type type) {
            return type.IsAssignableFrom(partial.GetDataType());
        }
    }
}