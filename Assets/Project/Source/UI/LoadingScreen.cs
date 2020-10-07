using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private RectTransform _imageTransform;
        [SerializeField] private Text _loadingMessage;
        private bool _loaded;
        private float _time;

        private void Update()
        {
            if (!_loaded)
            {
                var euler = new Vector3(0, 0, _time * 360f % 360f);
                _imageTransform.rotation = Quaternion.Euler(euler);
                _time += Time.deltaTime;
            }
        }

        public void ShowScreen()
        {
            _time = 0f;
            _loaded = false;
            gameObject.SetActive(true);
        }

        public void ShowMessage(string message)
        {
            _loadingMessage.gameObject.SetActive(message != "");
            _loadingMessage.text = message;
        }

        public void HideScreen()
        {
            _loaded = true;
            gameObject.SetActive(false);
        }
    }
}