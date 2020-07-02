using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI
{
    public class BuildMessageView : MonoBehaviour
    {
        [SerializeField] private Text buildMessageText;
        [SerializeField] private BuildMessage buildMessage;

        private void Awake()
        {
            buildMessageText.text = buildMessage.buildMessage;
        }
    }
}