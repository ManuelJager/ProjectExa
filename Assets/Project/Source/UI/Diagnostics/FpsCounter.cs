using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Diagnostics
{
    public class FpsCounter : MonoBehaviour
    {
        [SerializeField] private Text _fpsText;

        public void Update()
        {
            _fpsText.text = $"{Mathf.RoundToInt(1f / Time.smoothDeltaTime)} FPS";
        }
    }
}