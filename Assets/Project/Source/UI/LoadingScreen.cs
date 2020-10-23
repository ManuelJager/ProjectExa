using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0649

namespace Exa.UI
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private RectTransform imageTransform;
        [SerializeField] private Text loadingMessage;
        private bool loaded;
        private float time;

        private void Update() {
            if (!loaded) {
                var euler = new Vector3(0, 0, time * 360f % 360f);
                imageTransform.rotation = Quaternion.Euler(euler);
                time += Time.deltaTime;
            }
        }

        public void ShowScreen() {
            time = 0f;
            loaded = false;
            gameObject.SetActive(true);
        }

        public void ShowMessage(string message) {
            loadingMessage.gameObject.SetActive(message != "");
            loadingMessage.text = message;
        }

        public void HideScreen() {
            loaded = true;
            gameObject.SetActive(false);
        }
    }
}