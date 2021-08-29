using System;
using System.Collections.Generic;
using Exa.Generics;
using Exa.Grids.Blocks;
using Exa.UI.Tooltips;

namespace Exa.Grids {
    public class GridTotals : ICloneable<GridTotals> {
        private readonly BlockContext context;
        private BlockMetadata metadata;
        private float unscaledTorque;
        private float unscaledPowerGeneration;
        private float hull;
        private float mass;

        public GridTotals(BlockContext context) {
            this.context = context;
        }

        public float Mass {
            get => mass;
            set => mass = value;
        }

        public float Hull {
            get => hull;
            set => hull = value;
        }

        public float UnscaledPowerGeneration {
            get => unscaledPowerGeneration;
            set => unscaledPowerGeneration = value;
        }

        public float UnscaledTorque {
            get => unscaledTorque;
            set {
                unscaledTorque = value;
                UnscaledTorqueChanged?.Invoke(value);
            }
        }

        public BlockMetadata Metadata {
            get => metadata;
            set => metadata = value;
        }

        public event Action<float> UnscaledTorqueChanged;

        public GridTotals Clone() {
            return new GridTotals(context) {
                mass = mass,
                hull = hull,
                unscaledPowerGeneration = unscaledPowerGeneration,
                unscaledTorque = unscaledTorque,
                metadata = metadata
            };
        }

        public BlockContext GetInjectedContext() {
            return context;
        }

        public void Reset() {
            Mass = 0f;
            Hull = 0f;
            UnscaledPowerGeneration = 0f;
            unscaledTorque = 0f;
            Metadata = new BlockMetadata();
        }

        public IEnumerable<ITooltipComponent> GetDebugTooltipComponents() {
            return new ITooltipComponent[] {
                new TooltipText($"Mass: {Mass}"),
                new TooltipText($"Hull: {Hull}")
            };
        }
    }
}