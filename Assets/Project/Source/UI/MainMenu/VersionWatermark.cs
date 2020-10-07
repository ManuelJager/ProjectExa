using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI
{
    public class VersionWatermark : MonoBehaviour
    {
        [SerializeField] private Text versionText;

        private void Awake()
        {
            versionText.text = $"Build: {Application.version}";
        }
    }
}