using UnityEngine;

namespace Exa.UI
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private RectTransform imageTransform;
        private bool loaded;
        private float time;

        private void Update()
        {
            if (!loaded)
            {
                time += Time.deltaTime;
                imageTransform.rotation = Quaternion.Euler(0, 0, time * 360f);
            }
        }

        public void ShowScreen()
        {
            loaded = false;
            time = 0f;
        }

        public void MarkLoaded()
        {
            loaded = true;
        }
    }
}