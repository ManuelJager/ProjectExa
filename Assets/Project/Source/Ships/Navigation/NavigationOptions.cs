using UnityEngine;

namespace Exa.Ships
{
    public class NavigationOptions : MonoBehaviour
    {
        [Header("Options")]
        public bool continouslyApplySettings;

        [Header("PID-Quaternion-parameters")]
        public float qProportionalBase;
        public float qIntegral;
        public float qDerivitive;

        [Header("PD-Position-parameters")]
        public float pProportional;
        public float pDerivitive;
        public float maxVel;
    }
}