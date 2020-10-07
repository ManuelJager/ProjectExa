using UnityEngine;

namespace Exa.UI.Components
{
    public class CameraHelper : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        private void Awake()
        {
            _canvas.worldCamera = Camera.main;
        }
    }
}