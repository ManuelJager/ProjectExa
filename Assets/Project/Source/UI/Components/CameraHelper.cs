using UnityEngine;
#pragma warning disable CS0649

namespace Exa.UI.Components
{
    public class CameraHelper : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;

        private void Awake()
        {
            canvas.worldCamera = Camera.main;
        }
    }
}