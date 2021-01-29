using System.Collections.Generic;
using UnityEngine;

namespace Exa.Drawing
{
    public class SuperSamplingArgs<TSample>
    {
        public delegate TSample Sampler(Vector2 point, Vector2 localPoint);
        public delegate TSample SamplesAverageFactory(IEnumerable<TSample> values);
        public delegate void Applier(Vector2Int pixel, TSample averagedValue);

        public int sampleSize;
        public Sampler sampler;
        public SamplesAverageFactory samplesAverageFactory;
        public Applier applier;

        public SuperSamplingArgs<TSample> Override(SuperSamplingArgs<TSample> original) {
            if (sampleSize != 0) {
                original.sampleSize = sampleSize;
            }

            if (sampler != null) {
                original.sampler = sampler;
            }

            if (samplesAverageFactory != null) {
                original.samplesAverageFactory = samplesAverageFactory;
            }

            if (applier != null) {
                original.applier = applier;
            }

            return original;
        }
    }
}