using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI
{
    public class BuildMessageView : MonoBehaviour
    {
        [SerializeField] private Text _buildMessageText;
        [SerializeField] private BuildMessage _buildMessage;

        private void Awake()
        {
            _buildMessageText.text = _buildMessage.buildMessage;
        }
    }
}