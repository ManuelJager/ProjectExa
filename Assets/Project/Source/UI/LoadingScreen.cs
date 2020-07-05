using UnityEngine;

namespace Exa.UI
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private RectTransform imageTransform;
        private bool loaded;
        private bool hideAfterLoad;
        private float time;

        private void Update()
        {
            if (!loaded)
            {
                time += Time.deltaTime;
                imageTransform.rotation = Quaternion.Euler(0, 0, time * 360f % 360f);
            }
        }

        public void ShowScreen(bool hideAfterLoad = true)
        {
            time = 0f;
            loaded = false;
            this.hideAfterLoad = hideAfterLoad;
            gameObject.SetActive(true);
        }

        public void MarkLoaded()
        {
            loaded = true;
            if (hideAfterLoad)
            {
                gameObject.SetActive(false);
            }
        }
    }
}