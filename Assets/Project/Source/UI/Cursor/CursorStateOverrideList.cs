using System;
using Exa.Types.Generics;
using UnityEngine.Events;

namespace Exa.UI
{
    public class CursorStateOverrideList : OverrideList<CursorState>
    {
        public UnityEvent<bool> ContainsItemChange { get; } = new UnityEvent<bool>();

        public CursorStateOverrideList(CursorState defaultValue, Action<CursorState> onValueChange)
            : base(defaultValue, onValueChange) { }

        public override void Add(ValueOverride<CursorState> valueOverride) {
            if (overrides.Count == 0)
                ContainsItemChange?.Invoke(true);

            base.Add(valueOverride);
        }

        public override void Remove(ValueOverride<CursorState> valueOverride) {
            if (overrides.Contains(valueOverride))
                base.Remove(valueOverride);

            if (overrides.Count == 0)
                ContainsItemChange?.Invoke(false);
        }
    }
}