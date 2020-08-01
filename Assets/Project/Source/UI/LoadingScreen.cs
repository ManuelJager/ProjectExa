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
                var euler = new Vector3(0, 0, time * 360f % 360f);
                imageTransform.rotation = Quaternion.Euler(euler);
                time += Time.deltaTime;
            }
        }

        public void ShowScreen()
        {
            time = 0f;
            loaded = false;
            gameObject.SetActive(true);
        }

        public void MarkLoaded()
        {
            loaded = true;
            gameObject.SetActive(false);
        }
    }
}