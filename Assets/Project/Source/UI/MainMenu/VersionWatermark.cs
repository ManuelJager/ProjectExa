using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI
{
    public class VersionWatermark : MonoBehaviour
    {
        [SerializeField] private Text _versionText;

        private void Awake()
        {
            _versionText.text = $"Build: {Application.version}";
        }
    }
}